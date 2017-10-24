using UnityEditor;
using UnityEngine;

#if !UNITY_4_3 && !UNITY_4_5
using UnityEngine.UI;
#endif

using System;
using System.Collections;
using System.Collections.Generic;

using Object = UnityEngine.Object;

namespace LocalizationEditor
{
    [CustomEditor(typeof(LELocalize))]
    public class LELocalizeEditor : Editor
    {
        LELocalizeState nextState;
        string newKey;

        public override void OnInspectorGUI()
        {
            LELocalize leLocalize = target as LELocalize;
            nextState = leLocalize.State;

            if (leLocalize.State.Equals(LELocalizeState.Initialized))
                DrawComponent(leLocalize);
            else if (leLocalize.State.Equals(LELocalizeState.AddingNewKey))
                DrawAddNewKey(leLocalize);
            else
                DrawOptions();

            leLocalize.State = nextState;
        }

        void DrawAddNewKey(LELocalize leLocalize)
        {
            newKey = EditorGUILayout.TextField(LEConstants.KeyLbl, newKey);
            if (!string.IsNullOrEmpty(newKey) && GUILayout.Button(LEConstants.AddStringBtn))
            {
                LEStringTableEditor.AddNewString(newKey, leLocalize.Text);
                LEStringTableEditor.Save();

                leLocalize.localized_string_key = newKey;
                nextState = LELocalizeState.Initialized;
            }
        }

        void DrawOptions()
        {
            // Only allow adding of keys if there is at least
            // one language present
            if (LEStringTableEditor.AllLangsVisual.Count > 0)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button(LEConstants.AddNewKeyLbl))
                {
                    newKey = string.Empty;
                    nextState = LELocalizeState.AddingNewKey;
                }

                if (GUILayout.Button(LEConstants.ExistingKeyLbl))
                    nextState = LELocalizeState.Initialized;
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                EditorGUILayout.HelpBox(LEConstants.AddLanguageWarning, MessageType.Warning);
                if (GUILayout.Button(LEConstants.LaunchLEBtn))
                    LEMenu.ShowLEManagerWindow();
            }
        }

        void DrawComponent(LELocalize leLocalize)
        { 
			#if !UNITY_4_3 && !UNITY_4_5
            if (leLocalize.TextComponent != null)
                EditorGUILayout.ObjectField(LEConstants.LocalizedText, (Object)leLocalize.TextComponent , typeof(LELocalize), true);
			#endif
            
            if (leLocalize.GUITextComponent != null)
                EditorGUILayout.ObjectField(LEConstants.LocalizedGUIText, (Object)leLocalize.GUITextComponent, typeof(LELocalize), true);
            
            if (leLocalize.TextMeshComponent != null)
                EditorGUILayout.ObjectField(LEConstants.LocalizedTextMesh, (Object)leLocalize.TextMeshComponent, typeof(LELocalize), true);
            
            EditorGUILayout.BeginHorizontal();
            leLocalize.localized_string_key = DrawKeys(leLocalize.useList, leLocalize.localized_string_key);
            leLocalize.useList = EditorGUILayout.Toggle(leLocalize.useList, GUILayout.Width(20f));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(LEConstants.ListenForEventsLbl);
            bool shouldListen = EditorGUILayout.Toggle(leLocalize.UseLEEditorListener);
            EditorGUILayout.EndHorizontal();
            if (shouldListen && !leLocalize.UseLEEditorListener)
            {
                leLocalize.RegisterForLEEvents();
                leLocalize.UseLEEditorListener = true;
            }
            else if (!shouldListen && leLocalize.UseLEEditorListener)
            {
                leLocalize.UnRegisterForLEEvents();
                leLocalize.UseLEEditorListener = false;
            }

            leLocalize.UpdateLocString();
        }

        string DrawKeys(bool useList, string currentKey)
        {
            string newKey = currentKey;

            if (useList)
            {
                string[] keyArray = LEStringTableEditor.GetLangKeys().ToArray();

                // Just make an array with the current value
                // LE Manager probably isn't loaded
                // That's ok
                if (keyArray.Length == 0)
                    keyArray = new string[]{currentKey};

                int currentIndex = Array.IndexOf(keyArray, currentKey);

                int newIndex = EditorGUILayout.Popup(LEConstants.KeyLbl, currentIndex, keyArray);
                if (currentIndex != newIndex && keyArray.IsValidIndex(newIndex))
                    newKey = keyArray[newIndex];
            }
            else
            {
                newKey = EditorGUILayout.TextField(LEConstants.KeyLbl, currentKey);
            }

            return newKey;
        }
    }
}