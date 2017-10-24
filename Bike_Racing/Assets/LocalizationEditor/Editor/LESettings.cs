using UnityEngine;
using UnityEditor;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace LocalizationEditor
{
    [Serializable]
    public class LESettings
    {
        public enum SyncType
        {
            Google,
            Local,
            None
        }

        bool _prettyJson;
        public bool PrettyJson
        {
            get { return _prettyJson; }
            set
            {
                _prettyJson = value;
            }
        }
        
        static string _fullRootDir;
        public static string FullRootDir
        {
            get
            {
                if (!Directory.Exists(_fullRootDir))
                {
                    var results = AssetDatabase.FindAssets(LEConstants.RootDir);
                    if (results != null && results.Length > 0)
                    {
                        string assetPath = AssetDatabase.GUIDToAssetPath(results[0]);
                        string currentDir = Environment.CurrentDirectory;
                        
                        _fullRootDir = Path.Combine(currentDir, assetPath);
                    }
                    else
                        _fullRootDir = Path.Combine(Application.dataPath, LEConstants.RootDir);
                }
                
                return _fullRootDir;
            }
        }
        
        public static string RelativeRootDir
        {
            get
            {
                return FullRootDir.Replace(Environment.CurrentDirectory, string.Empty).TrimLeadingDirChars();
            }
        }

        public static string LogicalLocFilePath
        {
            get
            {
                return Path.Combine(FullRootDir, LEConstants.DefaultLogicalFilePath);
            }
        }

        public static string VisualLocFilePath
        {
            get
            {
                return Path.Combine(FullRootDir, LEConstants.DefaultVisualFilePath);
            }
        }

        static string settingsPath
        {
            get
            {
                return Path.Combine(FullRootDir, LEConstants.SettingsPath);
            }
        }

        static LESettings _instance;
        public static LESettings Instance
        {
            get
            {
                if (_instance == null)
                    Load();
                return _instance;
            }
        }

        LESettings() {
            ImportedLocalSpreadsheetName = string.Empty;
            ExportedLocalSpreadsheetName = string.Empty;

            ImportedGoogleSpreadsheetName = string.Empty;
            ExportedGoogleSpreadsheetPath = string.Empty;


            AccessTokenKey = string.Empty;
            AccessTokenTimeout = string.Empty;
            RefreshTokenKey = string.Empty;

            WarningCache = new Dictionary<string, bool>();

            LastSyncType = SyncType.None;
        }

        public string ImportedLocalSpreadsheetName;
        public string ExportedLocalSpreadsheetName;

        [OptionalField]
        public string SyncLocalSpreadsheetName;

        public string ImportedGoogleSpreadsheetName;
        public string ExportedGoogleSpreadsheetPath;

        [OptionalField]
        public string SyncGoogleSpreadsheetName;

        public string AccessTokenTimeout;
        public string AccessTokenKey;
        public string RefreshTokenKey;

        [OptionalField]
        public int AutoIndex = 1;

        [OptionalField]
        public string LastSelectedLocSet = "";

        [OptionalField]
        public Dictionary<string, bool> WarningCache;

        [OptionalField]
        public SyncType LastSyncType = SyncType.None;
        
        public void Save()
        {
            using (var stream = new MemoryStream())
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, this);
                
                File.WriteAllBytes(settingsPath, stream.ToArray());
            }
        }

        static void Load()
        {
            if (!Directory.Exists(Path.GetDirectoryName(settingsPath)))
                Directory.CreateDirectory(Path.GetDirectoryName(settingsPath));
            
            if (File.Exists(settingsPath))
            {
                byte[] bytes = File.ReadAllBytes(settingsPath);
                
                using (var stream = new MemoryStream(bytes))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    _instance = bin.Deserialize(stream) as LESettings;
                }
            }
            else
            {
                _instance = new LESettings();
            }

            if (_instance.WarningCache == null)
                _instance.WarningCache = new Dictionary<string, bool>();
        }
    }
}
