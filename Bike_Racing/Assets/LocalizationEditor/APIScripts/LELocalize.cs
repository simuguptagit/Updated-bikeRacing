using UnityEngine;

#if !UNITY_4_3 && !UNITY_4_5
using UnityEngine.UI;
#endif

using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LocalizationEditor
{
    public enum LELocalizeState
    {
        Initialized,
        AddingNewKey,
        ShowOptions
    }

    [AddComponentMenu("LE Manager/LE Localize")]
    [ExecuteInEditMode]
    public class LELocalize : MonoBehaviour 
    {
        #if !UNITY_4_3 && !UNITY_4_5
        [SerializeField]
        Text localizedText;
        #endif

        [SerializeField]
        GUIText localizedGUIText;

        [SerializeField]
        TextMesh localizedTextMesh;

        public string localized_string_key;
        public bool useList = true;
        public bool UseLEEditorListener = true;

        public LELocalizeState State = LELocalizeState.ShowOptions;

        string lastLocUsed = "";

        #if !UNITY_4_3 && !UNITY_4_5
        public Text TextComponent
        {
            get
            {
                if (localizedText == null)
                    localizedText = GetComponent<Text>();
                return localizedText;
            }
        }
        #endif

        public GUIText GUITextComponent
        {
            get
            {
                if (localizedGUIText == null)
                    localizedGUIText = GetComponent<GUIText>();
                return localizedGUIText;
            }
        }

        public TextMesh TextMeshComponent
        {
            get
            {
                if (localizedTextMesh == null)
                    localizedTextMesh = GetComponent<TextMesh>();
                return localizedTextMesh;
            }
        }
        
        public string Text
        {
            get {
                if (GUITextComponent != null)
                    return GUITextComponent.text;
                
                else if (TextMeshComponent != null)
                    return TextMeshComponent.text;
                #if !UNITY_4_3 && !UNITY_4_5
                else if (TextComponent != null)
                    return TextComponent.text;
                #endif

                return string.Empty;
            }

            set
            {
                if (GUITextComponent != null)
                    GUITextComponent.text = value;
                else if (TextMeshComponent != null)
                    TextMeshComponent.text = value;
                #if !UNITY_4_3 && !UNITY_4_5
                else if (TextComponent != null)
                    TextComponent.text = value;
                #endif
            }
        }

        void OnGUI()
        {
            if (LEManager.CurrentLocSet.Equals(lastLocUsed) ||
                string.IsNullOrEmpty(localized_string_key))
                return;

            Text = LEManager.Get(localized_string_key);

            lastLocUsed = LEManager.CurrentLocSet;
        }

        #if UNITY_EDITOR
        void OnEnable()
        {
            if (UseLEEditorListener)
                RegisterForLEEvents();
        }
        
        void OnDisable()
        {
            if (UseLEEditorListener)
                UnRegisterForLEEvents();
        }

        public void RegisterForLEEvents()
        {
            LEManager.OnLanguageChanged += UpdateLocString;
            LEManager.OnRenamedKeys += OnRenamedKey;
            LEManager.OnLocStringChanged += OnLocStringChanged;
        }

        public void UnRegisterForLEEvents()
        {
            LEManager.OnLanguageChanged -= UpdateLocString;
            LEManager.OnRenamedKeys -= OnRenamedKey;
            LEManager.OnLocStringChanged -= OnLocStringChanged;
        }

        public void UpdateLocString()
        {
            // Do nothing if the component hasn't been set up yet
            if (!State.Equals(LELocalizeState.Initialized))
                return;

            Text = LEManager.GetLocString(localized_string_key);
        }

        public void OnRenamedKey(Dictionary<string, string> renamedKeys)
        {
            // Do nothing if the component hasn't been set up yet
            if (!State.Equals(LELocalizeState.Initialized))
                return;

            if (!renamedKeys.ContainsKey(localized_string_key))
                return;

            localized_string_key = renamedKeys[localized_string_key];

            EditorUtility.SetDirty(this);
        }

        public void OnLocStringChanged(List<string> keys)
        {
            // Do nothing if the component hasn't been set up yet
            if (!State.Equals(LELocalizeState.Initialized))
                return;

            if (keys.Contains(localized_string_key))
                UpdateLocString();
        }
        #endif
    }
}
