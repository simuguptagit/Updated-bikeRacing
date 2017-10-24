using UnityEditor;
using UnityEngine;
using System.Collections;

namespace LocalizationEditor
{
    public class LEMenu : EditorWindow
    {
        const string menuItemLocation = "Window/" + LEConstants.MenuLbl;

        /// <summary>
        /// Displays the Localization Editor Window
        /// </summary>
        [MenuItem(menuItemLocation + "/" + LEConstants.LEEditorWindowLbl, false, 250)]
        public static void ShowLEManagerWindow()
        {
            var window = EditorWindow.GetWindow<LEStringTableEditorWindow> (false, LEConstants.LEEditorWindowLbl);
            window.LoadSettings();
            window.Show();
        }


        /** Divider **/


        [MenuItem(menuItemLocation + "/" + LEConstants.SceneCrawlerLbl, false, 324)]
        public static void DoLESceneCrawl()
        {
            LESceneCrawler.ProcessScene();
        }

        [MenuItem(menuItemLocation + "/" + LEConstants.GenKeysLbl, false, 325)]
        public static void DoGenStaticKeys()
        {
            LECodeGen.GenStaticKeysClass(LEStringTableEditor.MasterKeys);
        }

        /** Divider **/

        [MenuItem(menuItemLocation + "/" + LEConstants.ForumMenu, false, 338)]
        static void LEForumPost()
        {
            Application.OpenURL(LEConstants.ForumURL);
        }
        
        [MenuItem(menuItemLocation + "/" + LEConstants.DocsMenu, false, 339)]
        static void LEFreeDocs()
        {
            Application.OpenURL(LEConstants.DocURL);
        }


        /** Divider **/


        [MenuItem(menuItemLocation + "/" + LEConstants.BuyLinkText, false, 359)]
        static void LEBuy()
        {
            Application.OpenURL(LEConstants.BuyURL);
        }

        /*[MenuItem(menuItemLocation + "/" + LEConstants.RateLELbl, false, 340)]
        static void LERate()
        {
            Application.OpenURL(LEConstants.RateMeURL);
        }*/

        [MenuItem(menuItemLocation + "/" + LEConstants.ContactMenu + "/" + LEConstants.EmailMenu, false, 361)]
        static void LEEmail()
        {
            Application.OpenURL(LEConstants.Contact);
        }
        
        [MenuItem(menuItemLocation + "/" + LEConstants.ContactMenu + "/" + LEConstants.TwitterMenu, false, 362)]
        static void LETwitter()
        {
            Application.OpenURL(LEConstants.Twitter);
        }
    }
}
