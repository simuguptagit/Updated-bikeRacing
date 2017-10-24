using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LocalizationEditor
{
    public class LEManager
    {
        static Dictionary<string, string> locDict;

        static string _locSet = "";
        public static string CurrentLocSet
        {
            set 
            { 
                if (_locSet != value)
                {
                    _locSet = value;
                    LoadLocSet(_locSet);
                }
            }
            get
            {
                return _locSet;
            }
        }

        public static string Get(string key)
        {
            return GetLocString(key);
        }

        public static string GetLocString(string key)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(key))
                return result;

            if (locDict != null)
                locDict.TryGetValue(key, out result);

            return result;
        }

        static void LoadLocSet(string loc)
        {
            try
            {
                // Load current loc set
                TextAsset locset = Resources.Load<TextAsset>(loc);
                Dictionary<string, object> tempDict = (Dictionary<string, object>)Json.Deserialize(locset.text);
                locDict = tempDict.ToDictionary(k => k.Key, v => v.Value.ToString());
            }
            catch(Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        #if UNITY_EDITOR
        public delegate void LangChangeAction();
        public delegate void RenamedKeyAction(Dictionary<string, string> renamedKeys);
        public delegate void LocStringChangeAction(List<string> keys);

        public static event LangChangeAction OnLanguageChanged;
        public static event RenamedKeyAction OnRenamedKeys;
        public static event LocStringChangeAction OnLocStringChanged;

        public static void LanguageChanged()
        {
            if (OnLanguageChanged != null)
                OnLanguageChanged();
        }

        public static void KeysRenamed(Dictionary<string, string> renamedKeys)
        {
            if (OnRenamedKeys != null)
                OnRenamedKeys(renamedKeys);
        }

        public static void LocStringChanged(List<string> keys)
        {
            if (OnLocStringChanged != null)
                OnLocStringChanged(keys);
        }

        public static void SetCurrentLocSet(string locSetName, Dictionary<string, string> table)
        {
            _locSet = locSetName;
            locDict = table;
        }
        #endif

    }
}
