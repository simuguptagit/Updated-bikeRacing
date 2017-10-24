using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LocalizationEditor
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Merge the specified Dictionaries into source Dictionary.
        /// Values for existing keys will be left intact.
        /// </summary>
        /// <param name="variable">Variable.</param>
        /// <param name="others">Dictionaries to merge into source.</param>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        public static bool Merge<TKey, TValue>(this Dictionary<TKey, TValue> variable, params Dictionary<TKey, TValue>[] others)
        {
            bool result = true;
            try
            {
                foreach (var src in others) 
                {
                    foreach (KeyValuePair<TKey, TValue> pair in src)
                    {
                        if (!variable.ContainsKey(pair.Key))
                            variable.Add(pair.Key, pair.Value);
                    }
                }
            }
            catch

            {
                result = false;
            }
            
            return result;
        }
        
        /// <summary>
        /// Adds the value if the key does not exist, otherwise it updates the value for the given key
        /// </summary>
        /// <returns><c>true</c>, if add or update suceeded, <c>false</c> otherwise.</returns>
        /// <param name="key">Key of the value we are adding or updating.</param>
        /// <param name="value">Value to add or use to set as the current value for the Key.</param>
        public static bool TryAddOrUpdateValue<TKey, TValue>(this Dictionary<TKey, TValue> variable, TKey key, TValue value)
        {
            bool result;
            try
            {
                if (variable.ContainsKey(key))
                {
                    variable[key] = value;
                    result = true;
                }
                else
                    result = variable.TryAddValue(key, value);
            }
            catch
            {
                result = false;
            }
            
            return result;
        }
        
        public static bool TryAddValue<TKey, TValue>(this Dictionary<TKey, TValue> variable, TKey key, TValue value)
        {
            bool result;
            try
            {
                variable.Add(key, value);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}