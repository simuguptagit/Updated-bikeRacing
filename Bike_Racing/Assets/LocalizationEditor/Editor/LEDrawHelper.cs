using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace LocalizationEditor
{
    using Math = Mathf;
    public class LEDrawHelper
    {
        public float LineHeight = 20f;
        public float TopBuffer = 2f;
        public float LeftBuffer = 2f;
        public float BottomBuffer = 2f;
        public float RightBuffer = 2f;
        public const float ScrollBarWidth = 15f;

        public float CurrentLine = 0;
        public float CurrentLinePosition = 0;

        public Dictionary<string, Vector2> SizeCache;

        GUIStyle subHeaderStyle;
        GUIStyle mainHeaderStyle;

        Vector2 size;

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

        EditorWindow windowHandle;

        public LEDrawHelper(EditorWindow window, float topBuf=2f, float leftBuf=2f, float bottomBuf = 2f, float rightBuf=2f, float lineHeight=20f)
        {
            if (mainHeaderStyle == null || mainHeaderStyle.name.Equals(string.Empty))
            {
                mainHeaderStyle = new GUIStyle(GUI.skin.label);
                mainHeaderStyle.fontSize = 20;
                mainHeaderStyle.fontStyle = FontStyle.Bold;
            }

            if (subHeaderStyle == null || subHeaderStyle.name.Equals(string.Empty))
            {
                subHeaderStyle = new GUIStyle(GUI.skin.label);
                subHeaderStyle.fontSize = mainHeaderStyle.fontSize - 4;
                subHeaderStyle.fontStyle = FontStyle.Bold;
            }

            TopBuffer = topBuf;
            LeftBuffer = leftBuf;
            BottomBuffer = bottomBuf;
            RightBuffer = rightBuf;
            LineHeight = lineHeight;

            windowHandle = window;
            SizeCache = new Dictionary<string, Vector2>();

            ResetToTop();
        }

        public void ResetToTop()
        {
            CurrentLine = TopBuffer/LineHeight;
            CurrentLinePosition = LeftBuffer;
        }
        
        public void NewLine(float numNewLines = 1)
        {
            CurrentLine += numNewLines;
            CurrentLinePosition = LeftBuffer;
        }
        
        public float TopOfLine()
        {
            return LineHeight*CurrentLine;
        }
        
        public float VerticalMiddleOfLine()
        {
            return LineHeight*CurrentLine + LineHeight/2;
        }
        
        public float HorizontalMiddleOfLine()
        {
            return FullSeparatorWidth()/2f + LeftBuffer;
        }

        public float CenteredOnLine(float width)
        {
            return HorizontalMiddleOfLine()-width/2f;
        }
        
        public float PopupTop()
        {
            return TopOfLine()+1;
        }
        
        public float StandardHeight()
        {
            return LineHeight-2;
        }
        
        public float TextBoxHeight()
        {
            return LineHeight-4;
        }
        
        public float VectorFieldHeight()
        {
            return LineHeight*1.2f;
        }
        
        public float FullSeparatorWidth(bool scrollBarVisible = false)
        {
            float width = windowHandle.position.width-LeftBuffer-RightBuffer;
            if (scrollBarVisible)
                width -= ScrollBarWidth;
            return width;
        }
        
        public float WidthLeftOnCurrentLine()
        {
            return FullWindowWidth() - LeftBuffer - RightBuffer - CurrentLinePosition;
        }
        
        public float ScrollViewWidth()
        {
            return FullWindowWidth() - ScrollBarWidth;
        }
        
        public float FullWindowWidth()
        {
            return windowHandle.position.width;
        }
        
        public float HeightToBottomOfWindow()
        {
            return windowHandle.position.height - (CurrentLine*LineHeight);
        }
        
        public float CurrentHeight()
        {
            return CurrentLine*LineHeight;
        }

        public bool IsGroupVisible(float groupHeight, Vector2 verticalScrollbarPosition, float scrollViewHeight, float scrollViewY)
        {
            float topSkip = verticalScrollbarPosition.y;            
            float bottomThreshold = CurrentHeight() + groupHeight;            
            if (topSkip >= bottomThreshold) {                
                // the group is above our current window                
                return false;                
            }
            
            float bottomSkip = topSkip + scrollViewHeight + scrollViewY;            
            float topThreshold = CurrentHeight() - LineHeight;
            if (topThreshold >= bottomSkip) {                
                // the group is below our current window
                return false;
            }
            
            return true;
        }

        public bool IsVisible(Vector2 verticalScrollbarPosition, float scrollViewHeight, float scrollViewY)
        {
            float topSkip = verticalScrollbarPosition.y;            
            float bottomThreshold = CurrentHeight();            
            if (topSkip >= bottomThreshold) {                
                // the group is above our current window                
                return false;                
            }
            
            float bottomSkip = topSkip + scrollViewHeight + scrollViewY;            
            float topThreshold = CurrentHeight() - LineHeight;
            if (topThreshold >= bottomSkip) {                
                // the group is below our current window
                return false;
            }
            
            return true;
        }

        public void DrawMainHeaderLabel(string text, Color color, string cacheKey)
        {
            content.text = text;
            mainHeaderStyle.normal.textColor = color;

            if (!SizeCache.TryGetValue(cacheKey, out size))
            {
                size = mainHeaderStyle.CalcSize(content);
                SizeCache.Add(cacheKey, new Vector2(size.x, size.y));
            }
            CurrentLinePosition = Math.Max(HorizontalMiddleOfLine()-size.x/2f, 0);
            GUI.Label(new Rect(CurrentLinePosition, TopOfLine(), size.x, size.y), content, mainHeaderStyle);

            NewLine(size.y/LineHeight);
        }

        public Vector2 DrawSubHeader(string text, Color color, string cacheKey, bool addNewLine = true, bool floatRight = false)
        {
            content.text = text;
            subHeaderStyle.normal.textColor = color;

            if (!SizeCache.TryGetValue(cacheKey, out size))
            {
                size = subHeaderStyle.CalcSize(content);
                SizeCache.Add(cacheKey, new Vector2(size.x, size.y));
            }

            if (addNewLine)
                NewLine(0.75f);

            float xPos = CurrentLinePosition;
            if (floatRight)
                xPos = FullWindowWidth()-size.x-RightBuffer;

            GUI.Label(new Rect(xPos, TopOfLine(), size.x, size.y), content, subHeaderStyle);

            if (!floatRight)
                CurrentLinePosition += (size.x + 2);

            if (addNewLine)
                NewLine(size.y/LineHeight+0.5f);

            return size;
        }

        public void DrawSectionSeparator(bool isSeparatorVisible = false)
        {
            NewLine(0.25f);
            GUI.Box(new Rect(CurrentLinePosition, TopOfLine(), FullSeparatorWidth(isSeparatorVisible), 1), string.Empty);
        }

        public void TryGetCachedSize(string key, GUIContent content, GUIStyle style, out Vector2 size)
        {
            if (!SizeCache.TryGetValue(key, out size))
            {
                size = style.CalcSize(content);
                SizeCache.Add(key, new Vector2(size.x, size.y));
            }
        }
    }
}
