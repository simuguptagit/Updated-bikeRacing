using UnityEngine;
using System.Collections;

namespace LocalizationEditor
{
    public class LELanguageSelectSample : MonoBehaviour
    {
        Vector2 screenPos;
        int listEntry = 0;
        GUIContent[] languageDisplay;
        string[] locSetNames;
        float listWidth = 120;

		GUIContent _content;
		GUIContent content
		{
			get
			{
				if (_content == null)
					_content = new GUIContent();
				return _content;
			}
		}

		GUIStyle labelStyle;

        void LoadLocSetInfo()
        {
            if (locSetNames == null)
            {
                locSetNames = new string[]
                {
                    "en-US-SampleScene",
                    "es-PR-SampleScene",
                    "fr-FR-SampleScene",
                    "it-IT-SampleScene",
                    "de-DE-SampleScene",
                    "ja-JP-SampleScene",
                    "ko-KR-SampleScene",
                    "ru-RU-SampleScene",
                    "ar-SA-SampleScene",
                    "he-IL-SampleScene",
                    "ur-SampleScene"
                };
            }
            LEManager.CurrentLocSet = locSetNames[0];

            GUIStyle listStyle = GUI.skin.button;
            if (languageDisplay == null)
            {
                // Set the display name based on the CultureInfo
                languageDisplay = new GUIContent[locSetNames.Length];
                for(int i=0;  i<languageDisplay.Length;  i++)
                {
                    string culture = locSetNames[i].Replace("-SampleScene", string.Empty);
                    
                    try
                    {
                        if (culture.Equals("ar-SA"))
                            languageDisplay[i] = new GUIContent(LEManager.Get(LESampleKeys.ar));
                        else if (culture.Equals("he-IL"))
                            languageDisplay[i] = new GUIContent(LEManager.Get(LESampleKeys.he));
                        else if (culture.Equals("ur"))
                            languageDisplay[i] = new GUIContent(LEManager.Get(LESampleKeys.ur));
                        else
                        {
                            LECulture cultureInfo = LECultureFactory.Get(culture);
                            languageDisplay[i] = new GUIContent(cultureInfo.NativeName);
                        }
                    }
                    catch
                    {
                        languageDisplay[i] = new GUIContent(culture);
                    }

                    Vector2 size = listStyle.CalcSize(languageDisplay[i]);
                    listWidth = Mathf.Max(listWidth, size.x);
                }
            }

			labelStyle = new GUIStyle(GUI.skin.label);
			labelStyle.fontSize = 22;
			labelStyle.alignment = TextAnchor.MiddleCenter;
			labelStyle.normal.textColor = new Color(.196f, .196f, .196f);
        }

        void OnGUI()
        {
			// Populate the language options
            LoadLocSetInfo();

			// Calculate the position for the language selection
            screenPos.x = Screen.width - listWidth - 50;
            screenPos.y = 150;

			// Draw the language selection buttons
            Rect listRect = new Rect(screenPos.x, screenPos.y, listWidth, languageDisplay.Length*24);
            GUI.Box(listRect, string.Empty, "box");
            int newListEntry = GUI.SelectionGrid(listRect, listEntry, languageDisplay, 1);
            if (languageDisplay.IsValidIndex(newListEntry) && newListEntry != listEntry)
                listEntry = newListEntry;

			// Set the language
			// LEManager will only load a language if the requested language is not the one
			// that is currently loaded
            LEManager.CurrentLocSet = locSetNames[listEntry];

			// Here is an example of how to load a string in code
			content.text = LEManager.Get(LESampleKeys.FromCodeKey);
			GUI.Label(new Rect(120, 350, 500, 30), content, labelStyle);
        }
    }
}
