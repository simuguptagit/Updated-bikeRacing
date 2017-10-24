using UnityEditor;
using UnityEngine;
using System;
using System.Collections;

#if !UNITY_4_3 && !UNITY_4_5
using UnityEngine.UI;
#endif

namespace LocalizationEditor
{
    public class LESceneCrawler
    {
        public static void ProcessScene()
        {
            try
            {
                // Force autoindex to start at 1
                if (LESettings.Instance.AutoIndex < 1)
                    LESettings.Instance.AutoIndex = 1;

                int totalConfigured = 0;
                int totalComponents = 0;

    			#if !UNITY_4_3 && !UNITY_4_5
                // Text components
                Text[] textComponents = GameObject.FindObjectsOfType<Text>();
                totalComponents += textComponents.Length;
                for(int i=0;  i<textComponents.Length;  i++)
                {
                    if (ProcessGameObject(textComponents[i].gameObject, textComponents[i].text))
                        totalConfigured++;
                }
                #endif

                // GUIText components
                GUIText[] guiTextComponents = GameObject.FindObjectsOfType<GUIText>();
                totalComponents += guiTextComponents.Length;
                for(int i=0;  i<guiTextComponents.Length;  i++)
                {
                    if (ProcessGameObject(guiTextComponents[i].gameObject, guiTextComponents[i].text))
                        totalConfigured++;
                }

                // TextMesh components
                TextMesh[] textMeshComponents = GameObject.FindObjectsOfType<TextMesh>();
                totalComponents += textMeshComponents.Length;
                for(int i=0;  i<textMeshComponents.Length;  i++)
                {
                    if (ProcessGameObject(textMeshComponents[i].gameObject, textMeshComponents[i].text))
                        totalConfigured++;
                }

                // At the end of the process, save LE Editor
                LEStringTableEditor.Save();
                LESettings.Instance.Save();

                EditorUtility.DisplayDialog(LEConstants.ProcessSceneMsgTitle, 
                                            string.Format(LEConstants.ProcessSceneCompleteMsg, totalComponents, totalConfigured, totalComponents-totalConfigured), 
                                            LEConstants.OkLbl);
            }
            catch(Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        static bool ProcessGameObject(GameObject go, string value)
        {
            bool result = false;

            // Determine if this component already has a LELocalize attached
            if (go.GetComponent<LELocalize>() == null)
            {
                // Add LELocalize
                LELocalize leLocalize = go.AddComponent<LELocalize>();

                string key = LEConstants.AutoIndexPrefix + LESettings.Instance.AutoIndex++;
                LEStringTableEditor.AddNewString(key, value);

                leLocalize.State = LELocalizeState.Initialized;
                leLocalize.localized_string_key = key;

                result = true;
            }

            return result;
        }
    }
}
