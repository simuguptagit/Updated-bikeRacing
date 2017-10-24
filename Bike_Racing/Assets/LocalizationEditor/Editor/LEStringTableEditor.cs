using UnityEngine;
using UnityEditor;
using System;
using System.Text;
using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LocalizationEditor
{
    using LELangDict = System.Collections.Generic.Dictionary<string, string>;
    using LELangDictCollection = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>;

    [InitializeOnLoad]
    public class LEStringTableEditor
    {

#if UNITY_EDITOR_OSX
        [DllImport("dl")]
        static extern IntPtr dlopen([MarshalAs(UnmanagedType.LPTStr)] string filename, int flags);

        [DllImport("dl")]
        static extern IntPtr dlsym(IntPtr handle, [MarshalAs(UnmanagedType.LPTStr)] string symbol);
        delegate void GetBidi(string in_logical_utf8, int logical_len, StringBuilder out_visual_utf8, int visual_len);

        static IntPtr lebidiLibHandle;
        static IntPtr getbidiHandle;
#endif

        static bool _needsSave = false;
        public static bool NeedsSave
        {
            get { return _needsSave; }
            set { _needsSave = value; }
        }

        static LEStringTableEditor()
        {
#if UNITY_EDITOR_OSX
            const int RTLD_NOW = 2;
            lebidiLibHandle = dlopen(Path.Combine(LESettings.FullRootDir, LEConstants.LEBidiOSXPath), RTLD_NOW);
            if (lebidiLibHandle == IntPtr.Zero)
                throw new Exception(LEConstants.BidiSupportDllError);

            getbidiHandle = dlsym(lebidiLibHandle, LEConstants.GetBidiHandle);
            if (getbidiHandle == IntPtr.Zero)
                throw new Exception(LEConstants.BidiSupportGetFunctionHandleError);
#endif
            // Load after scripts have been reloaded
            Load();

            if (!string.IsNullOrEmpty(LESettings.Instance.LastSelectedLocSet) &&
                AllLangsVisual.ContainsKey(LESettings.Instance.LastSelectedLocSet))
            {
                CurrentLanguage = LESettings.Instance.LastSelectedLocSet;
            }
            else if (AllLangsVisual.Count > 0)
            {
                CurrentLanguage = AllLangsVisual.Keys.ToArray()[0];
            }
        }

        #region Language Dictionary
        static string _currentLanguage = "en";
        public static string CurrentLanguage
        {
            get { return _currentLanguage; }
            set
            {
                _currentLanguage = value;

                if (!string.IsNullOrEmpty(_currentLanguage) && !LESettings.Instance.LastSelectedLocSet.Equals(_currentLanguage))
                {
                    LESettings.Instance.LastSelectedLocSet = _currentLanguage;
                    LESettings.Instance.Save();
                }
            }
        }

        static LELangDictCollection _allLangsVisual;
        public static LELangDictCollection AllLangsVisual
        {
            set
            {
                _allLangsVisual = value;
            }

            get
            {
                if (_allLangsVisual == null)
                    _allLangsVisual = new LELangDictCollection();

                return _allLangsVisual;
            }
        }

        static LELangDictCollection _allLangsLogical;
        public static LELangDictCollection AllLangsLogical
        {
            set
            {
                _allLangsLogical = value;
                RebuildMasterKeys();
                RebuildVisualTables();
            }

            get
            {
                if (_allLangsLogical == null)
                    _allLangsLogical = new LELangDictCollection();

                return _allLangsLogical;
            }
        }

        static List<string> _masterKeys;
        public static List<string> MasterKeys
        {
            set
            {
                _masterKeys = value;
            }

            get
            {
                if (_masterKeys == null)
                    _masterKeys = new List<string>();

                return _masterKeys;
            }
        }

        static Dictionary<string, string> _visStringCache;
        static Dictionary<string, string> VisualStringCache
        {
            get
            {
                if (_visStringCache == null)
                    _visStringCache = new Dictionary<string, string>();
                return _visStringCache;
            }
        }
        #endregion

        #region Save/Load Methods
        public static void Save()
        {
            SaveTables(LESettings.LogicalLocFilePath, LEConstants.LogicalFileExtension, AllLangsLogical);
            SaveTables(LESettings.VisualLocFilePath, LEConstants.VisualFileExtension, AllLangsVisual);

            NeedsSave = false;
        }

        static void SaveTables(string path, string ext, LELangDictCollection langCollection)
        {
            try
            {
                string loc_directory = path;
                if (!Directory.Exists(loc_directory))
                    Directory.CreateDirectory(loc_directory);

                // Write out one file per lang in JSON format
                foreach (var string_table in langCollection)
                {
                    string lang_data_file_path = Path.Combine(loc_directory, string_table.Key + ext);
                    string rawJson = Json.Serialize(string_table.Value);

                    File.WriteAllText(lang_data_file_path, rawJson);
                }

                AssetDatabase.Refresh();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }

        public static void Load()
        {
            LoadStringTables(LESettings.VisualLocFilePath, LEConstants.VisualFileExtension, ref _allLangsVisual);
            LoadStringTables(LESettings.LogicalLocFilePath, LEConstants.LogicalFileExtension, ref _allLangsLogical);

            RebuildMasterKeys();

            NeedsSave = false;
        }

        static void LoadStringTables(string path, string ext, ref LELangDictCollection langCollection)
        {
            if (langCollection == null)
                langCollection = new LELangDictCollection();
            else
                langCollection.Clear();

            // Check if the localization directory exists, if so then load
            // If not, do nothing
            if (Directory.Exists(path))
            {
                string[] stringTables = Directory.GetFiles(path, "*" + ext);
                foreach(string table in stringTables)
                {
                    string langCode = Path.GetFileName(table).Replace(ext, string.Empty);

                    Dictionary<string, object> tempDict = (Dictionary<string, object>)Json.Deserialize(File.ReadAllText(table));
                    LELangDict langDict = tempDict.ToDictionary(k => k.Key, v => v.Value.ToString());

                    langCollection.Add(langCode, langDict);
                }
            }
        }

        public static void ClearAll()
        {
            AllLangsLogical.Clear();
            AllLangsVisual.Clear();
            VisualStringCache.Clear();

            NeedsSave = true;
        }

        public static void DeleteLangsFromDisk()
        {
            bool shouldRefresh = false;

            // Delete visual files
            DirectoryInfo dirInfo = new DirectoryInfo(LESettings.VisualLocFilePath);
            if (dirInfo.Exists)
            {
                foreach(var fileInfo in dirInfo.GetFiles("*" + LEConstants.VisualFileExtension))
                    fileInfo.Delete();
                shouldRefresh = true;
            }

            // Delete logical files
            dirInfo = new DirectoryInfo(LESettings.LogicalLocFilePath);
            if (dirInfo.Exists)
            {
                foreach(var fileInfo in dirInfo.GetFiles("*" + LEConstants.LogicalFileExtension))
                    fileInfo.Delete();
                shouldRefresh = true;
            }

            NeedsSave = true;
            if (shouldRefresh)
                AssetDatabase.Refresh();
        }

        public static void AddNewLanguage(string name)
        {
            LELangDict newLang = new LELangDict();
            LELangDict newVisLang = new LELangDict();

            MasterKeys.ForEach(masterKey => {
                newLang.Add(masterKey, string.Empty);
                newVisLang.Add(Logical2Visual(masterKey), string.Empty);
            });

            AllLangsLogical.Add(name, newLang);
            AllLangsVisual.Add(name, newVisLang);

            NeedsSave = true;
        }

        public static void RemoveLanguage(string name)
        {
            string path = Path.Combine(LESettings.VisualLocFilePath, name + LEConstants.VisualFileExtension);
            string logicalPath = Path.Combine(LESettings.LogicalLocFilePath, name + LEConstants.LogicalFileExtension);

            // Delete the language file
            File.Delete(path);
            File.Delete(logicalPath);

            // Remove the language from the collection
            AllLangsVisual.Remove(name);
            AllLangsLogical.Remove(name);

            // If the deleted language is currently selected, unset it
            if (CurrentLanguage.Equals(name))
                CurrentLanguage = string.Empty;

            if (AllLangsLogical.Count == 0)
                MasterKeys.Clear();

            NeedsSave = true;
        }
        #endregion

        #region Access Methods
        public static void AddNewString(string key, string value)
        {
            // Add to logical tables
            foreach(var string_table in AllLangsLogical.Values)
                string_table.Add(key, value);

            // Add to the visual tables
            string visKey = Logical2Visual(key);
            string visValue = Logical2Visual(value);
            foreach(var string_table in AllLangsVisual.Values)
                string_table.Add(visKey, visValue);

            Dbg.Assert(!MasterKeys.Contains(key));
            MasterKeys.Add(key);

            NeedsSave = true;
        }

        public static void RemoveString(string key)
        {
            // Remove from visual table
            string visKey = Logical2Visual(key);
            foreach(var string_table in AllLangsVisual.Values)
                string_table.Remove(visKey);

            // Remove from logical table
            foreach(var string_table in AllLangsLogical.Values)
                string_table.Remove(key);

            MasterKeys.Remove(key);

            NeedsSave = true;
        }

        public static void UpdateString(string lang, string key, string value)
        {
            string visKey = GetPresentationFromCache(key);
            string visValue = Logical2Visual(value);

            Dbg.Assert(AllLangsVisual.ContainsKey(lang));
            Dbg.Assert(AllLangsVisual[lang].ContainsKey(visKey));

            Dbg.Assert(AllLangsLogical.ContainsKey(lang));
            Dbg.Assert(AllLangsLogical[lang].ContainsKey(key));

            AllLangsVisual[lang][visKey] = visValue;
            AllLangsLogical[lang][key] = value;

            NeedsSave = true;
        }

        public static void UpdateKey(string oldKey, string newKey)
        {
            // Update the logical tables
            string value;
            foreach(var string_table in AllLangsLogical.Values)
            {
                value = string.Empty;
                if (string_table.TryGetValue(oldKey, out value))
                {
                    string_table.Remove(oldKey);
                    string_table.Add(newKey, value);
                }
            }

            // Update the visual tables
            string visKey = Logical2Visual(oldKey);
            string newVisKey = Logical2Visual(newKey);
            foreach(var string_table in AllLangsVisual.Values)
            {
                value = string.Empty;
                if (string_table.TryGetValue(visKey, out value))
                {
                    string_table.Remove(visKey);
                    string_table.Add(newVisKey, value);
                }
            }

            int index = MasterKeys.IndexOf(oldKey);
            MasterKeys.Remove(oldKey);
            MasterKeys.Insert(index, newKey);

            NeedsSave = true;
        }

        public static List<string> GetLangOptions()
        {
            return AllLangsVisual.Keys.ToList();
        }

        public static List<string> GetLangKeys()
        {
            List<string> keys = new List<string>();

            if (!string.IsNullOrEmpty(CurrentLanguage) && AllLangsVisual.ContainsKey(CurrentLanguage))
                keys = AllLangsVisual[CurrentLanguage].Keys.ToList();

            return keys;
        }

        public static string GetLocString(string key, string defaultValue)
        {
            string result = defaultValue;

            if (!string.IsNullOrEmpty(CurrentLanguage) && AllLangsVisual.ContainsKey(CurrentLanguage))
                AllLangsVisual[CurrentLanguage].TryGetValue(key, out result);

            return result;
        }

        /// <summary>
        /// Converts the given logical string to its visual presentation and caches the result.
        /// </summary>
        /// <returns>The visual presentation of the string</returns>
        /// <param name="s">Logical string value</param>
        public static string GetPresentationFromCache(string s)
        {
            string visKey;
            if (!VisualStringCache.TryGetValue(s, out visKey))
            {
                visKey = Logical2Visual(s);
                VisualStringCache.Add(s, visKey);
            }
            return visKey;
        }

        /// <summary>
        /// Gets a localized string using a logical key
        /// </summary>
        /// <returns>The visual presentation of the localized string</returns>
        /// <param name="key">Logical Key.</param>
        public static string GetPresentationValue(string logicalKey)
        {
            string visKey = GetPresentationFromCache(logicalKey);
            return GetLocString(visKey, string.Empty);
        }
        #endregion

        #region Helper Methods
        static void RebuildMasterKeys()
        {
            MasterKeys.Clear();
            foreach(var string_table in AllLangsLogical.Values)
            {
                foreach(var key in string_table.Keys)
                {
                    if (!MasterKeys.Contains(key))
                        MasterKeys.Add(key);
                }
            }
        }

        static void RebuildVisualTables()
        {
            string visKey;
            string visValue;

            AllLangsVisual.Clear();
            foreach(var langKey in AllLangsLogical.Keys)
            {
                LELangDict visLang = new LELangDict();
                var string_table = AllLangsLogical[langKey];

                foreach(var kvp in string_table)
                {
                    visKey = Logical2Visual(kvp.Key);
                    visValue = Logical2Visual(kvp.Value);
                    visLang.Add(visKey, visValue);
                }
                AllLangsVisual.Add(langKey, visLang);
            }
        }

        const int MAX_BUFFER = 5000000;
        static StringBuilder buffer = new StringBuilder(MAX_BUFFER);

#if UNITY_EDITOR_WIN
  #if UNITY_4_6
        [DllImport ("lebidi-32-win")]
  #else
        [DllImport ("lebidi-64-win")]
  #endif
        static extern void GetBidi(string in_logical_utf8, int logical_len, StringBuilder out_visual_utf8, int visual_len);
#endif

        public static string Logical2Visual(string inLogical)
        {
            buffer.Length = 0;
#if UNITY_EDITOR_OSX
            GetBidi csharpfuncptr = (GetBidi)Marshal.GetDelegateForFunctionPointer(getbidiHandle, typeof(GetBidi));
            csharpfuncptr(inLogical, inLogical.Length, buffer, buffer.MaxCapacity);
#else
            GetBidi(inLogical, inLogical.Length, buffer, buffer.MaxCapacity);
#endif
            return buffer.ToString();
        }
        #endregion
    }

    public class Dbg
    {
        public static void Assert(bool condition, string msg = "")
        {
            #if DEBUG
            if (!condition) throw new Exception(msg);
            #endif
        }
    }
}
