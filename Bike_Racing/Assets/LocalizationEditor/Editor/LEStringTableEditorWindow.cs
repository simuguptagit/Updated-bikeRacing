using UnityEngine;
using UnityEditor;
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace LocalizationEditor
{
    using LELangDict = System.Collections.Generic.Dictionary<string, string>;
    using LELangDictCollection = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>;

    [InitializeOnLoad]
    public class LEStringTableEditorWindow : EditorWindow
    {
        static LEStringTableEditorWindow windowHandle;

        Vector2 verticalScrollbarPosition;

        float scrollViewHeight = 0;
        float scrollViewY = 0;

        GUIStyle foldoutStyle = null;
        GUIStyle labelStyle = null;
        GUIStyle saveButtonStyle = null;
        GUIStyle loadButtonStyle = null;
        GUIStyle linkStyle = null;
        GUIStyle valueStyle = null;
        GUIStyle comboBoxStyle = null;
        GUIStyle keyStyle = null;

        string saveButtonText = LEConstants.SaveBtn;
        float buttonHeightMultiplier = 1.5f;

        Color headerColor
        {
            get {
                Color color;
                if (EditorGUIUtility.isProSkin)
                    color = LEConstants.MainHeaderColorProString.ToColor();
                else
                    color = LEConstants.MainHeaderColorString.ToColor();
                color.a = 1f;
                return color;
            }
        }

        Texture2D altBackground
        {
            get {
                Color col = headerColor;
                col.a = .2f;
                return MakeTex(2, 2, col);
            }
        }

        string highlightColor;

        HashSet<string> editingFields = new HashSet<string>();
        Dictionary<string, string> editFieldTextDict = new Dictionary<string, string>();

        LEDrawHelper _dh;
        LEDrawHelper drawHelper
        {
            get
            {
                if (_dh == null)
                    _dh = new LEDrawHelper(this);
                return _dh;
            }
        }

        static string[] _languageCodes;
        static string[] languageCodes
        {
            get
            {
                if (_languageCodes == null)
                    _languageCodes = new string[]{string.Empty};
                return _languageCodes;
            }

            set
            {
                _languageCodes = value;
            }
        }

        static string[] _languages;
        static string[] languages
        {
            get
            {
                if (_languages == null)
                    _languages = new string[]{string.Empty};
                return _languages;
            }

            set
            {
                _languages = value;
            }
        }

        static int langIndex = 0;
        static int newLangIndex = 0;

        float boxWidth = 300;
        float buttonWidth = 60;
        float largeButtonWidth = 110;
        static float languageWidth = 60;

        Vector2 size = Vector2.zero;
        GUIContent _content;
        GUIContent content
        {
            get {
                if (_content == null)
                    _content = new GUIContent();
                return _content;
            }
            set { _content = value; }
        }

        string newLocKey = string.Empty;
        string newLocValue = string.Empty;
        string newLangCode = string.Empty;

        Dictionary<string, string> renamedKeys = new Dictionary<string, string>();
        List<string> keysToRemove = new List<string>();
        List<string> languagesToRemove = new List<string>();
        Dictionary<string, string> updatedStringsForCurrent = new Dictionary<string, string>();

        static Dictionary<string, Dictionary<string, float>> groupHeightCollection = new Dictionary<string, Dictionary<string, float>>();
        Dictionary<string, float> groupHeights;

        static List<LECulture> newCultures = new List<LECulture>();

        static string[] _newCulturesDisplay;
        static string[] newCulturesDisplay
        {
            get
            {
                if (_newCulturesDisplay == null)
                    _newCulturesDisplay = new string[]{string.Empty};
                return _newCulturesDisplay;
            }

            set
            {
                _newCulturesDisplay = value;
            }
        }

        static LECulture currentCulture;
        bool isRTL = false;

#if UNITY_EDITOR_WIN
        static IntPtr lib;
        internal static class NativeMethods
        {
            [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
            internal static extern IntPtr LoadLibrary(string lpFileName);
        }

        public static void LoadNativeDll(string fileName)
        {
            if (lib != IntPtr.Zero)
                return;

            lib = NativeMethods.LoadLibrary(fileName);
            if (lib == IntPtr.Zero)
                throw new Win32Exception();
        }
#endif
        static LEStringTableEditorWindow()
        {
#if UNITY_EDITOR_WIN
            LoadNativeDll(Path.Combine(LESettings.FullRootDir, LEConstants.LEBidiWinPath));
#endif
            LoadLanguagesDisplay();
            LoadNewLanguagesDisplay();
            SelectLanguage(languageCodes.ToList().IndexOf(LESettings.Instance.LastSelectedLocSet));
        }

        public void LoadSettings()
        {
            minSize = new Vector2(700, 515);

            if (LEStringTableEditor.AllLangsLogical.Count == 0)
                Load();
        }

        void SetStyles()
        {
            if (labelStyle == null || labelStyle.name.Equals(string.Empty))
            {
                labelStyle = new GUIStyle(GUI.skin.label);
                labelStyle.richText = true;

                labelStyle.fontSize = 14;
                labelStyle.padding = new RectOffset(0, 0, 3, 3);
            }

            if (saveButtonStyle == null || saveButtonStyle.name.Equals(string.Empty))
            {
                saveButtonStyle = new GUIStyle(GUI.skin.button);
                saveButtonStyle.fontSize = 14;
            }

            if (loadButtonStyle == null || loadButtonStyle.name.Equals(string.Empty))
            {
                loadButtonStyle = new GUIStyle(GUI.skin.button);
                loadButtonStyle.fontSize = 14;
            }

            if (foldoutStyle == null || foldoutStyle.name.Equals(string.Empty))
            {
                foldoutStyle = new GUIStyle(EditorStyles.foldout);
                foldoutStyle.richText = true;
            }

            if (linkStyle == null || linkStyle.name.Equals(string.Empty))
            {
                linkStyle = new GUIStyle(GUI.skin.label);
                linkStyle.fontSize = 16;
                linkStyle.alignment = TextAnchor.MiddleCenter;
                linkStyle.normal.textColor = Color.blue;
            }

            if (valueStyle == null || valueStyle.name.Equals(string.Empty))
            {
                valueStyle = new GUIStyle(GUI.skin.textArea);
                valueStyle.fixedWidth = 300;
                valueStyle.richText = true;
                valueStyle.padding = new RectOffset(5, 5, 5, 5);
            }

            if (comboBoxStyle == null || comboBoxStyle.name.Equals(string.Empty))
            {
                comboBoxStyle = new GUIStyle(EditorStyles.popup);
                comboBoxStyle.fontSize = 14;
                comboBoxStyle.fixedHeight = 24f;
            }

            if (keyStyle == null || keyStyle.name.Equals(string.Empty))
            {
                keyStyle = new GUIStyle(GUI.skin.textArea);
                keyStyle.fixedWidth = 300;
                keyStyle.richText = true;
                keyStyle.padding = new RectOffset(5, 5, 5, 5);
            }

            /*if (rateBoxStyle == null || rateBoxStyle.name.Equals(string.Empty))
            {
                string path = LESettings.RelativeRootDir + "/" + LEConstants.BorderTexturePath;
                rateBoxStyle = new GUIStyle(GUI.skin.box);
                rateBoxStyle.normal.background = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
                rateBoxStyle.border = new RectOffset(2, 2, 2, 2);
            }*/
        }

        Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for( int i = 0; i < pix.Length; ++i )
            {
                pix[ i ] = col;
            }
            Texture2D result = new Texture2D( width, height );
            result.SetPixels( pix );
            result.Apply();
            return result;
        }

        #region OnGUI
        void OnGUI ()
        {
            SetStyles();

            size = Vector2.zero;

            drawHelper.ResetToTop();

            Color currentColor = headerColor;
            Texture2D currentAltBackground = altBackground;
            Texture2D normalBackground = GUI.skin.textArea.normal.background;

            drawHelper.DrawMainHeaderLabel(LEConstants.MainHeaderLbl, currentColor, LEConstants.SizeMainHeaderKey);
            DrawHeader();

            drawHelper.DrawSubHeader(LEConstants.AddNewHeaderLbl, currentColor, LEConstants.SizeAddNewHeaderKey);
            DrawAddNewSection();

            drawHelper.DrawSectionSeparator();

            drawHelper.DrawSubHeader(LEConstants.LocStringSectionHeader, currentColor, LEConstants.SizeLocStringHeaderKey);

            languageWidth = CalcMinWithForArray(languages, comboBoxStyle);

            if (LEStringTableEditor.AllLangsLogical.Count == 0)
            {
                content.text = LEConstants.AddNewLanguageMsg;
                drawHelper.TryGetCachedSize(LEConstants.SizeAddNewLanguageMsgKey, content, labelStyle, out size);
                EditorGUI.LabelField(new Rect(drawHelper.HorizontalMiddleOfLine()-size.x/2f, drawHelper.TopOfLine(), size.x, size.y), content, labelStyle);

                return;
            }

            // Check if the new languages and code have been loaded
            // If not, load them now (fix for Windows)
            if (languages == null || languageCodes == null)
            {
                 LoadLanguagesDisplay();
                 LoadNewLanguagesDisplay();
            }


            // Select the next available language if the current one is invalid
            if (!languageCodes.Contains(LEStringTableEditor.CurrentLanguage))
            {
                if (languageCodes.IsValidIndex(langIndex-1))
                    SelectLanguage(langIndex-1);
                else
                    SelectLanguage(0);
            }

            // Draw language drop down
            content.text = LEConstants.LanguageLbl;
            size = labelStyle.CalcSize(content);
            EditorGUI.LabelField(new Rect(drawHelper.CurrentLinePosition, drawHelper.TopOfLine(), size.x, size.y), content, labelStyle);
            drawHelper.CurrentLinePosition += size.x + 5;

            int newIndex = EditorGUI.Popup(new Rect(drawHelper.CurrentLinePosition, drawHelper.PopupTop(), languageWidth, comboBoxStyle.fixedHeight), langIndex, languages, comboBoxStyle);
            if (languageCodes.IsValidIndex(newIndex) && newIndex != langIndex)
                SelectLanguage(newIndex);
            drawHelper.CurrentLinePosition += languageWidth + 2f;

            // Draw Delete Language Button
            content.text = LEConstants.DeleteBtn;
            drawHelper.TryGetCachedSize(LEConstants.SizeDeleteBtnKey, content, GUI.skin.button, out size);
            if (GUI.Button(new Rect(drawHelper.CurrentLinePosition, drawHelper.PopupTop(), size.x, size.y), content))
            {
                languagesToRemove.Add(languageCodes[langIndex]);

                if (languageCodes.IsValidIndex(langIndex-1))
                    SelectLanguage(langIndex-1);
                else
                    SelectLanguage(0);
            }

            // Set the alignment for the current culture selected
            if (languageCodes.IsValidIndex(langIndex) && currentCulture != null && currentCulture.IsRightToLeft)
            {
                keyStyle.alignment = TextAnchor.MiddleRight;
                valueStyle.alignment = TextAnchor.MiddleRight;
                isRTL = true;
            }
            else
            {
                keyStyle.alignment = TextAnchor.MiddleLeft;
                valueStyle.alignment = TextAnchor.MiddleLeft;
                isRTL = false;
            }

            drawHelper.NewLine(2);

            DrawNewLocStringField();
            drawHelper.NewLine(2);

            // Draw the string table labels
            content.text = LEConstants.KeysLbl;
            size = labelStyle.CalcSize(content);
            EditorGUI.LabelField(new Rect(drawHelper.CurrentLinePosition, drawHelper.TopOfLine(), size.x, size.y), content, labelStyle);
            drawHelper.CurrentLinePosition += boxWidth;

            content.text = LEConstants.ValuesLbl;
            size = labelStyle.CalcSize(content);
            EditorGUI.LabelField(new Rect(drawHelper.CurrentLinePosition, drawHelper.TopOfLine(), size.x, size.y), content, labelStyle);

            drawHelper.NewLine();

            // Draw each loc string, one on each line
            if (!languageCodes.IsValidIndex(langIndex) || !LEStringTableEditor.AllLangsVisual.ContainsKey(languageCodes[langIndex]))
                return;

            if (!groupHeightCollection.TryGetValue(LEStringTableEditor.CurrentLanguage, out groupHeights))
                groupHeights = new Dictionary<string, float>();

            float currentGroupHeightTotal = CalculateGroupHeightsTotal();
            scrollViewHeight = drawHelper.HeightToBottomOfWindow();
            scrollViewY = drawHelper.TopOfLine();
            verticalScrollbarPosition = GUI.BeginScrollView(new Rect(drawHelper.CurrentLinePosition, scrollViewY, drawHelper.FullWindowWidth(), scrollViewHeight),
                                                            verticalScrollbarPosition,
                                                            new Rect(drawHelper.CurrentLinePosition, scrollViewY, drawHelper.ScrollViewWidth(), currentGroupHeightTotal));
            int count = 0;
            float currentGroupHeight = 0;
            float beginningHeight = 0;
            var string_table = LEStringTableEditor.AllLangsLogical[languageCodes[langIndex]];
            foreach (var loc_string in string_table)
            {
                if (count%2 != 0)
                {
                    valueStyle.normal.background = currentAltBackground;
                    keyStyle.normal.background = currentAltBackground;
                }
                else
                {
                    valueStyle.normal.background = normalBackground;
                    keyStyle.normal.background = normalBackground;
                }

                groupHeights.TryGetValue(loc_string.Key, out currentGroupHeight);
                if (currentGroupHeight == 0f || currentGroupHeight.NearlyEqual(drawHelper.LineHeight))
                    currentGroupHeight = drawHelper.LineHeight;

                if (drawHelper.IsGroupVisible(currentGroupHeight, verticalScrollbarPosition, scrollViewHeight, scrollViewY) ||
                    (count == string_table.Count-1 && verticalScrollbarPosition.y.NearlyEqual(currentGroupHeightTotal - drawHelper.LineHeight)))
                {
                    beginningHeight = drawHelper.CurrentHeight();

                    DrawLocString(string_table, loc_string);
                    drawHelper.NewLine(1.1f);

                    currentGroupHeight = drawHelper.CurrentHeight() - beginningHeight;
                }
                else
                {
                    drawHelper.NewLine(currentGroupHeight/drawHelper.LineHeight);
                }

                groupHeights.TryAddOrUpdateValue(loc_string.Key, currentGroupHeight);
                groupHeightCollection.TryAddOrUpdateValue(LEStringTableEditor.CurrentLanguage, groupHeights);
                count++;
            }
            GUI.EndScrollView();

            // Remove keys from all langs
            keysToRemove.ForEach(key => {
                LEStringTableEditor.RemoveString(key);
            });
            keysToRemove.Clear();

            // Rename any keys in other languages
            foreach(var pair in renamedKeys)
                LEStringTableEditor.UpdateKey(pair.Key, pair.Value);

            // Send out the renamed key event
            if (renamedKeys.Count > 0)
                LEManager.KeysRenamed(renamedKeys);

            renamedKeys.Clear();

            // Update any values in current lang
            foreach(var pair in updatedStringsForCurrent)
                LEStringTableEditor.UpdateString(LEStringTableEditor.CurrentLanguage, pair.Key, pair.Value);

            if (updatedStringsForCurrent.Count > 0)
                LEManager.LocStringChanged(updatedStringsForCurrent.Keys.ToList());

            updatedStringsForCurrent.Clear();

            // Delete any languages that were deleted
            bool shouldRebuildLanguageLists = languagesToRemove.Count > 0;
            languagesToRemove.ForEach(lang => {
                LEStringTableEditor.RemoveLanguage(lang);
            });
            languagesToRemove.Clear();

            if (shouldRebuildLanguageLists)
            {
                LoadLanguagesDisplay();
                LoadNewLanguagesDisplay();
                AssetDatabase.Refresh();
            }
        }
        #endregion

        #region Draw Methods
        void DrawNewLocStringField()
        {
            if (isRTL)
                DrawVisualPresentation(newLocKey.Equals(LEConstants.NewKeyLbl)?string.Empty:newLocKey,
                                       newLocValue.Equals(LEConstants.NewValueLbl)?string.Empty:newLocValue);

            // Key label position
            newLocKey = string.IsNullOrEmpty(newLocKey) ? LEConstants.NewKeyLbl : newLocKey;
            Rect labelPosition = GetResizableTextBoxPosition(newLocKey, boxWidth, keyStyle);

            // Key box
            GUI.Box(labelPosition, string.Empty);

            // Key label
            newLocKey = DrawResizableTextBox(newLocKey, labelPosition, keyStyle);

            // Value label position
            newLocValue = string.IsNullOrEmpty(newLocValue)? LEConstants.NewValueLbl : newLocValue;
            Rect valuePosition = GetResizableTextBoxPosition(newLocValue, boxWidth, valueStyle);

            // Value box
            GUI.Box(valuePosition, string.Empty);

            // Value label
            newLocValue = DrawResizableTextBox(newLocValue, valuePosition, valueStyle);

            // Add String Button
            content.text = LEConstants.AddStringBtn;
            size = GUI.skin.button.CalcSize(content);
            if (GUI.Button(new Rect(drawHelper.CurrentLinePosition, drawHelper.TopOfLine(), size.x, size.y), content) && !string.IsNullOrEmpty(newLocKey) && !string.IsNullOrEmpty(newLocValue))
            {
                try
                {
                    LEStringTableEditor.AddNewString(newLocKey, newLocValue);

                    newLocKey = string.Empty;
                    newLocValue = string.Empty;

                    GUI.FocusControl(string.Empty);
                }
                catch(Exception ex)
                {
                    Debug.LogException(ex);
                }
            }

            float height = Math.Max(labelPosition.height, valuePosition.height);
            drawHelper.NewLine(height/drawHelper.LineHeight - 1);
        }

        void DrawLocString(LELangDict langDict, KeyValuePair<string, string> locStringEntry)
        {
            if (editingFields.Contains(locStringEntry.Key))
                DrawEditLocString(langDict, locStringEntry);
            else
                DrawReadOnlyLocString(langDict, locStringEntry);
        }

        void DrawEditLocString(LELangDict langDict, KeyValuePair<string, string> locStringEntry)
        {
            Rect labelPosition;
            Rect valuePosition;
            float height;

            if (isRTL)
            {
                drawHelper.NewLine();
                DrawVisualPresentation(editFieldTextDict[locStringEntry.Key+LEConstants.KeyMetaTag],
                                       editFieldTextDict[locStringEntry.Key+LEConstants.ValueMetaTag]);
            }

            // Key label position
            labelPosition = GetResizableTextBoxPosition(editFieldTextDict[locStringEntry.Key+LEConstants.KeyMetaTag], boxWidth, keyStyle);

            // Key box
            GUI.Box(labelPosition, string.Empty);

            // Key label
            editFieldTextDict[locStringEntry.Key+LEConstants.KeyMetaTag] =
                DrawResizableTextBox(editFieldTextDict[locStringEntry.Key+LEConstants.KeyMetaTag], labelPosition, keyStyle);

            // Value label position
            valuePosition = GetResizableTextBoxPosition(editFieldTextDict[locStringEntry.Key+LEConstants.ValueMetaTag], boxWidth, valueStyle);

            // Value box
            GUI.Box(valuePosition, string.Empty);

            // Value label
            editFieldTextDict[locStringEntry.Key+LEConstants.ValueMetaTag] =
                DrawResizableTextBox(editFieldTextDict[locStringEntry.Key+LEConstants.ValueMetaTag], valuePosition, valueStyle);

            height = Math.Max(labelPosition.height, valuePosition.height);
            drawHelper.NewLine(height/drawHelper.LineHeight);

            drawHelper.NewLine(.25f);

            // Apply Button
            content.text = LEConstants.ApplyBtn;
            size = GUI.skin.button.CalcSize(content);
            if (GUI.Button(new Rect(drawHelper.CurrentLinePosition, drawHelper.TopOfLine(), size.x, size.y), content))
            {
                string oldKey = editFieldTextDict[locStringEntry.Key+LEConstants.OrigKeyMetaTag];
                string newKey = editFieldTextDict[locStringEntry.Key+LEConstants.KeyMetaTag];
                string newValue = editFieldTextDict[locStringEntry.Key+LEConstants.ValueMetaTag];

                // Update the key
                if (!oldKey.Equals(newKey))
                    renamedKeys.Add(oldKey, newKey);

                // Update the value
                if (!newValue.Equals(locStringEntry.Value))
                    updatedStringsForCurrent.Add(newKey, newValue);

                SetDoneLocStringEditMode(locStringEntry);
            }
            drawHelper.CurrentLinePosition += size.x + 2f;

            // Cancel Button
            content.text = LEConstants.CancelBtn;
            size = GUI.skin.button.CalcSize(content);
            if (GUI.Button(new Rect(drawHelper.CurrentLinePosition, drawHelper.TopOfLine(), size.x, size.y), content))
                SetDoneLocStringEditMode(locStringEntry);
            drawHelper.CurrentLinePosition += size.x + 2f;

            // Delete button
            content.text = LEConstants.DeleteBtn;
            size = GUI.skin.button.CalcSize(content);
            if (GUI.Button(new Rect(drawHelper.CurrentLinePosition, drawHelper.TopOfLine(), size.x, size.y), content))
            {
                keysToRemove.Add(locStringEntry.Key);
                SetDoneLocStringEditMode(locStringEntry);
            }
            drawHelper.CurrentLinePosition += size.x + 2f;

            if (isRTL)
                drawHelper.NewLine();
        }

        void DrawVisualPresentation(string logicalKey, string logicalValue)
        {
            Rect labelPosition;
            Rect valuePosition;
            float height;

            // Draw the visual representation section
            content.text = LEConstants.VisualPresentation;
            drawHelper.TryGetCachedSize(LEConstants.SizeVisualPresentationKey, content, labelStyle, out size);
            EditorGUI.LabelField(new Rect(drawHelper.CurrentLinePosition, drawHelper.TopOfLine(), size.x, size.y), content, labelStyle);
            drawHelper.NewLine();

            // Key label position
            string visKey = LEStringTableEditor.GetPresentationFromCache(logicalKey);
            labelPosition = GetResizableTextBoxPosition(visKey, boxWidth, keyStyle);

            // Key box
            GUI.Box(labelPosition, string.Empty);

            // Key label
            EditorGUI.SelectableLabel(labelPosition, visKey, keyStyle);
            drawHelper.CurrentLinePosition += (labelPosition.width + 2);

            // Value label position
            string visValue = LEStringTableEditor.GetPresentationFromCache(logicalValue);
            valuePosition = GetResizableTextBoxPosition(visValue, boxWidth, valueStyle);

            // Value box
            GUI.Box(valuePosition, string.Empty);

            // Value label
            EditorGUI.SelectableLabel(valuePosition, visValue, valueStyle);

            height = Math.Max(labelPosition.height, valuePosition.height);
            drawHelper.NewLine(height/drawHelper.LineHeight + .2f);

            content.text = LEConstants.LogicalPresentation;
            drawHelper.TryGetCachedSize(LEConstants.SizeLogicalPresentationKey, content, labelStyle, out size);
            EditorGUI.LabelField(new Rect(drawHelper.CurrentLinePosition, drawHelper.TopOfLine(), size.x, size.y), content, labelStyle);
            drawHelper.NewLine();
        }

        void DrawReadOnlyLocString(LELangDict langDict, KeyValuePair<string, string> locStringEntry)
        {
            // Draw read only loc section
            string visKey = LEStringTableEditor.GetPresentationFromCache(locStringEntry.Key);
            string visValue = LEStringTableEditor.GetPresentationValue(locStringEntry.Key);

            // Key label position
            Rect labelPosition = GetResizableTextBoxPosition(locStringEntry.Key, boxWidth, keyStyle);

            // Key box
            GUI.Box(labelPosition, string.Empty);

            // Key label
            if (GUI.Button(labelPosition, visKey, keyStyle))
                SetLocStringToEditMode(locStringEntry);
            drawHelper.CurrentLinePosition += labelPosition.width + 2f;

            // Value label position
            Rect valuePosition = GetResizableTextBoxPosition(visValue, boxWidth, valueStyle);

            // Value box
            GUI.Box(valuePosition, string.Empty);

            // Value label
            if (GUI.Button(valuePosition, visValue, valueStyle))
                SetLocStringToEditMode(locStringEntry);
            drawHelper.CurrentLinePosition += valuePosition.width + 2f;

            float height = Math.Max(labelPosition.height, valuePosition.height);
            drawHelper.NewLine(height/drawHelper.LineHeight - 1);
        }

        void DrawHeader()
        {
            float width = buttonWidth;

            if (GUI.Button(new Rect(drawHelper.CurrentLinePosition, drawHelper.TopOfLine(), width, drawHelper.StandardHeight()*buttonHeightMultiplier), LEConstants.LoadBtn, loadButtonStyle))
                Load();

            drawHelper.CurrentLinePosition += (width + 2);

            GUIContent filePath = new GUIContent(LESettings.VisualLocFilePath);
            Vector2 size = labelStyle.CalcSize(filePath);
            EditorGUI.SelectableLabel(new Rect(drawHelper.CurrentLinePosition, drawHelper.TopOfLine(), drawHelper.WidthLeftOnCurrentLine(), size.y), filePath.text, labelStyle);
            drawHelper.CurrentLinePosition += (size.x + 2);

            drawHelper.NewLine(buttonHeightMultiplier+.1f);

            // Buy link
            content.text = LEConstants.BuyLinkText;
            drawHelper.TryGetCachedSize(LEConstants.SizeBuyLinkTextKey, content, linkStyle, out size);
            if (GUI.Button(new Rect(drawHelper.FullWindowWidth()-size.x-2f, drawHelper.TopOfLine()-drawHelper.LineHeight/2f, size.x, size.y), content, linkStyle))
            {
                Application.OpenURL(LEConstants.BuyURL);
            }

            if (NeedToSave())
            {
                width = largeButtonWidth;
                saveButtonStyle.normal.textColor = Color.red;
                saveButtonStyle.fontStyle = FontStyle.Bold;
                saveButtonText = LEConstants.SaveNeededBtn;
            }
            else
            {
                width = buttonWidth;
                saveButtonStyle.normal.textColor = GUI.skin.button.normal.textColor;
                saveButtonStyle.fontStyle = FontStyle.Normal;
                saveButtonText = LEConstants.SaveBtn;
            }

            if (GUI.Button(new Rect(drawHelper.CurrentLinePosition, drawHelper.TopOfLine(), width, drawHelper.StandardHeight()*buttonHeightMultiplier), saveButtonText, saveButtonStyle))
                Save();

            drawHelper.NewLine(buttonHeightMultiplier+.5f);

            drawHelper.DrawSectionSeparator();
        }

        /*void DrawRateBox(float left, float top, float width, float height)
        {
            GUI.Box(new Rect(left, top, 2f, height), string.Empty, rateBoxStyle);
            GUI.Box(new Rect(left, top, width, 2f), string.Empty, rateBoxStyle);
            GUI.Box(new Rect(left, top+height, width+2, 2f), string.Empty, rateBoxStyle);
            GUI.Box(new Rect(left+width, top, 2f, height), string.Empty, rateBoxStyle);
        }*/

        string DrawResizableTextBox(string text, Rect position, GUIStyle style)
        {
            string newValue = EditorGUI.TextArea(position, text, style);
            drawHelper.CurrentLinePosition += (position.width + 2);

            return newValue;
        }

        string DrawResizableTextBox(string text, float fixedWidth, out Rect position, GUIStyle style)
        {
            position = GetResizableTextBoxPosition(text, fixedWidth, style);
            return DrawResizableTextBox(text, position, style);
        }

        void DrawAddNewSection()
        {
            // Check the new cultures array
            if (newCulturesDisplay == null || newCulturesDisplay.Length == 0)
                LoadNewLanguagesDisplay();

            // New language popup
            newLangIndex = EditorGUI.Popup(new Rect(drawHelper.CurrentLinePosition, drawHelper.PopupTop(), LEConstants.NewLangPopupWidth, comboBoxStyle.fixedHeight),
                                           newLangIndex, newCulturesDisplay, comboBoxStyle);
            drawHelper.CurrentLinePosition += LEConstants.NewLangPopupWidth + 2f;

            // Custom name textbox (only shows up if Custom is selected)
            if (newCulturesDisplay.IsValidIndex(newLangIndex) && newCulturesDisplay[newLangIndex].Equals(LEConstants.CustomLbl))
            {
                content.text = LEConstants.NewCustomLangLbl;
                drawHelper.TryGetCachedSize(LEConstants.SizeNewCustomLangLblKey, content, valueStyle, out size);
                newLangCode = EditorGUI.TextField(new Rect(drawHelper.CurrentLinePosition, drawHelper.PopupTop(), size.x, size.y), newLangCode);
                drawHelper.CurrentLinePosition += size.x + 2f;

                // Add language button
                content.text = LEConstants.AddLanguageLbl;
                drawHelper.TryGetCachedSize(LEConstants.SizeNewLangBtnKey, content, GUI.skin.button, out size);
                if (GUI.Button(new Rect(drawHelper.CurrentLinePosition, drawHelper.TopOfLine(), size.x, size.y), content))
                {
                    LEStringTableEditor.AddNewLanguage(newLangCode);
                    LEStringTableEditor.CurrentLanguage = newLangCode;

                    SetCurrentLanguageInManager();
                    groupHeightCollection.Clear();

                    LoadLanguagesDisplay();

                    newLangCode = LEConstants.NewCustomLangLbl;

                    GUI.FocusControl(string.Empty);
                }
            }
            else
            {
                // Add language button
                content.text = LEConstants.AddLanguageLbl;
                drawHelper.TryGetCachedSize(LEConstants.SizeNewLangBtnKey, content, GUI.skin.button, out size);
                if (GUI.Button(new Rect(drawHelper.CurrentLinePosition, drawHelper.TopOfLine(), size.x, size.y), content))
                {
                    string newLangCode = newCultures[newLangIndex].Name;
                    LEStringTableEditor.AddNewLanguage(newLangCode);
                    LEStringTableEditor.CurrentLanguage = newLangCode;

                    SetCurrentLanguageInManager();
                    groupHeightCollection.Clear();

                    LoadLanguagesDisplay();
                    LoadNewLanguagesDisplay();

                    SelectLanguage(langIndex);
                }
            }

            drawHelper.NewLine(1.75f);
        }
        #endregion

        #region Helper Methods
        float CalculateGroupHeightsTotal()
        {
            if (LEStringTableEditor.AllLangsVisual.Count == 0 || !LEStringTableEditor.AllLangsVisual.ContainsKey(LEStringTableEditor.CurrentLanguage))
                return 0;

            float totalHeight = 0;

            if (groupHeights.Count == 0)
                totalHeight = LEStringTableEditor.AllLangsVisual[LEStringTableEditor.CurrentLanguage].Count * drawHelper.LineHeight;
            else
            {
                foreach(var kvp in groupHeights)
                    totalHeight += kvp.Value;
            }

            return totalHeight;
        }

        Rect GetResizableTextBoxPosition(string text, float fixedWidth, GUIStyle style)
        {
            GUIContent content = new GUIContent(text);
            Vector2 size = new Vector2(fixedWidth, style.CalcHeight(content, fixedWidth));

            return new Rect(drawHelper.CurrentLinePosition, drawHelper.TopOfLine(), size.x, size.y);
        }

        bool IsVisible(float groupHeight)
        {
            float topSkip = this.verticalScrollbarPosition.y;
            float bottomThreshold = drawHelper.CurrentHeight() + groupHeight;
            if (topSkip >= bottomThreshold) {
                // the group is above our current window
                return false;
            }

            float bottomSkip = topSkip + scrollViewHeight + scrollViewY;
            float topThreshold = drawHelper.CurrentHeight() - drawHelper.LineHeight;
            if (topThreshold >= bottomSkip) {
                // the group is below our current window
                return false;
            }

            return true;
        }

        static void SetCurrentLanguageInManager()
        {
            if (LEStringTableEditor.AllLangsVisual.ContainsKey(LEStringTableEditor.CurrentLanguage))
            {
                LEManager.SetCurrentLocSet(LEStringTableEditor.CurrentLanguage, LEStringTableEditor.AllLangsVisual[LEStringTableEditor.CurrentLanguage]);
                LEManager.LanguageChanged();
            }
        }

        void SetLocStringToEditMode(KeyValuePair<string, string> locStringEntry)
        {
            editFieldTextDict.Add(locStringEntry.Key+LEConstants.KeyMetaTag, locStringEntry.Key);
            editFieldTextDict.Add(locStringEntry.Key+LEConstants.OrigKeyMetaTag, locStringEntry.Key);
            editFieldTextDict.Add(locStringEntry.Key+LEConstants.ValueMetaTag, locStringEntry.Value);

            editingFields.Add(locStringEntry.Key);
        }

        void SetDoneLocStringEditMode(KeyValuePair<string, string> locStringEntry)
        {
            editFieldTextDict.Remove(locStringEntry.Key+LEConstants.KeyMetaTag);
            editFieldTextDict.Remove(locStringEntry.Key+LEConstants.OrigKeyMetaTag);
            editFieldTextDict.Remove(locStringEntry.Key+LEConstants.ValueMetaTag);

            editingFields.Remove(locStringEntry.Key);

            GUI.FocusControl(string.Empty);
        }

        static void LoadLanguagesDisplay()
        {
            languageCodes = LEStringTableEditor.AllLangsVisual.Keys.ToArray();
            langIndex = Array.IndexOf(languageCodes, LEStringTableEditor.CurrentLanguage);

            if (languageCodes.Length > 0)
                languages = new string[languageCodes.Length];
            else
            {
                languages = null;
                languageWidth = 20;
                return;
            }

            languageWidth = 0;
            for(int i=0;  i<languages.Length;  i++)
            {
                LECulture info = LECultureFactory.Get(languageCodes[i]);

                if (info != null)
                    languages[i] = FormatCultureDisplay(info);
                else
                    languages[i] = languageCodes[i];
            }
        }

        static void LoadNewLanguagesDisplay()
        {
            newCultures.Clear();

            var cultureDisplays = new List<string>();

            List<LECulture> specific = LECultureFactory.GetAllSpecific();
            foreach(LECulture cul in specific)
            {
                if (!LEStringTableEditor.AllLangsVisual.ContainsKey(cul.Name) &&
                    !string.IsNullOrEmpty(cul.TwoLetterISOLanguageName) &&
                    !cul.TwoLetterISOLanguageName.Equals(LEConstants.InvariantCultureCode))
                {
                    newCultures.Add(cul);
                    cultureDisplays.Add(LEConstants.SpecificCultureCategory + FormatCultureDisplay(cul));
                }
            }

            // Set the new language index to en-US if it's available
            newLangIndex = Math.Max(0, newCultures.FindIndex(x => x.Name.Equals("en-US")));

            List<LECulture> neutral = LECultureFactory.GetAllNeutral();
            foreach(LECulture cul in neutral)
            {
                if (!LEStringTableEditor.AllLangsVisual.ContainsKey(cul.Name) &&
                    !string.IsNullOrEmpty(cul.TwoLetterISOLanguageName) &&
                    !cul.TwoLetterISOLanguageName.Equals(LEConstants.InvariantCultureCode))
                {
                    newCultures.Add(cul);
                    cultureDisplays.Add(LEConstants.NeutralCultureCategory + FormatCultureDisplay(cul));
                }
            }
            cultureDisplays.Add(LEConstants.CustomLbl);

            newCulturesDisplay = cultureDisplays.ToArray();
        }

        static string FormatCultureDisplay(LECulture culture)
        {
            string nativeName = culture.NativeName;
            if (culture.IsRightToLeft)
                nativeName = LEStringTableEditor.Logical2Visual(nativeName);

            return culture.Name + " - " + nativeName +
                (!culture.NativeName.Equals(culture.DisplayName) ? " - " + culture.DisplayName : string.Empty);
        }

        static void SelectLanguage(int index)
        {
            if (languageCodes == null || !languageCodes.IsValidIndex(index))
                return;

            langIndex = index;
            LEStringTableEditor.CurrentLanguage = languageCodes[langIndex];
            SetCurrentLanguageInManager();
            groupHeightCollection.Clear();

            try
            {
                currentCulture = LECultureFactory.Get(languageCodes[langIndex]);
            }
            catch
            {
                currentCulture = null;
            }
        }

        float CalcMinWithForArray(string[] array, GUIStyle style)
        {
            float minWidth = 0;

            if (array == null || style == null)
                return minWidth;

            foreach(string s in array)
            {
                content.text = s;
                minWidth = Math.Max(style.CalcSize(content).x, minWidth);
            }

            return minWidth + 4f;
        }

        void Load()
        {
            LEStringTableEditor.Load();
            SetCurrentLanguageInManager();

            editingFields.Clear();
            editFieldTextDict.Clear();
            groupHeightCollection.Clear();

            LESettings.Instance.WarningCache.Clear();
            LESettings.Instance.Save();

            langIndex = 0;
            newLangIndex = 0;

            LoadNewLanguagesDisplay();
            LoadLanguagesDisplay();

            GUI.FocusControl(string.Empty);
        }

        void Save()
        {
            LEStringTableEditor.Save();
        }

        bool NeedToSave()
        {
            return LEStringTableEditor.NeedsSave;
        }

        public static void OnComplete()
        {
            LoadLanguagesDisplay();
            LoadNewLanguagesDisplay();
        }
        #endregion
    }
}
