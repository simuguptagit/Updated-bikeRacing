using UnityEngine;
using System;

namespace LocalizationEditor
{
    public class LEConstants
    {
        #region Meta Strings
        public const string OrigKeyMetaTag = "_origKey";
        public const string KeyMetaTag = "_key";
        public const string ValueMetaTag = "_value";
        public const string MetaDataFormat = "{0}_{1}";
        public const string AutoIndexPrefix = "ID";

        public const string SizeKeySuffix = "1";
        public const string SizeLeftValueSuffix = "2";
        public const string SizeRightValueSuffix = "3";
        public const string SizeClearChoicesKey = "4";
        public const string SizeAllLeftKey = "5";
        public const string SizeAllRightKey = "6";
        public const string SizeSaveAndUploadBtnKey = "7";
        public const string SizeMainHeaderKey = "8";
        public const string SizeSpreadsheetActionsKey = "9";
        public const string SizeLocStringHeaderKey = "10";
        public const string SizeAddNewHeaderKey = "11";
        public const string SizeSyncHeaderKey = "12";
        public const string SizeSyncDiffHeaerKey = "13";
        public const string SizeLeftSideHeaderKey = "14";
        public const string SizeRightSideHeaderKey = "15";
        public const string SizeLanguageLblKey = "16";
        public const string SizeNewLangBtnKey = "17";
        public const string SizeNewCustomLangLblKey = "18";
        public const string SizeAddNewLanguageMsgKey = "19";
        public const string SizeDeleteBtnKey = "20";
        public const string SizeNoSyncNeeded_1Key = "21";
        public const string SizeNoSyncNeeded_2Key = "22";
        public const string SizeSyncActionsLblKey = "23";
        public const string SizeCollapseLblKey = "24";
        public const string SizeExpandLblKey = "25";
        public const string SizeExportDlgTitleLblKey = "26";
        public const string SizeExportToLocalFileBtnKey = "27";
        public const string SizeExportToSheetsBtnKey = "28";
        public const string SizeExportToNewSheetBtnKey = "29";
        public const string SizeReauthenticateGoogleLblKey = "30";
        public const string SizeExportExcelWorkbookLblKey = "31";
        public const string SizeExcelFilePathLblKey = "32";
        public const string SizeBrowseLblKey = "33";
        public const string SizeBackBtnKey = "34";
        public const string SizeExportBtnKey = "35";
        public const string SizeAuthWithGoogleLblKey = "36";
        public const string SizeGoogleAuthInstruction1_1Key = "37";
        public const string SizeGoogleAuthInstruction1_2Key = "38";
        public const string SizeGoogleAuthInstruction2_1Key = "39";
        public const string SizeGoogleAuthInstruction2_2Key = "40";
        public const string SizeGoToAuthURLBtnKey = "41";
        public const string SizeEnterAccessCodeLblKey = "42";
        public const string SizeSetCodeBtnKey = "43";
        public const string SizeExportGoogleSheetLblKey = "44";
        public const string SizeSelectSpreadSheetLblKey = "45";
        public const string SizeUploadBtnKey = "46";
        public const string SizeExportNewGoogleSheetLblKey = "47";
        public const string SizeNewSheetFileNameLblKey = "48";
        public const string SizeExportCompleteLblKey = "49";
        public const string SizeExportMsg1Key = "50";
        public const string SizeExportMsg2Key = "51";
        public const string SizeForumLinkTextKey = "52";
        public const string SizeExportAgainBtnKey = "53";
        public const string SizeCloseBtnKey = "54";
        public const string SizeChooseImportExcelLblKey = "55";
        public const string SizeImportLocalLblKey = "56";
        public const string SizeImportGoogleSpreadsheetLblKey = "57";
        public const string SizeImportExcelWBLblKey = "58";
        public const string SizeImportBtnKey = "59";
        public const string SizeImportCompleteKey = "60";
        public const string SizeImportMsg1 = "61";
        public const string SizeImportMsg2 = "62";
        public const string SizeDownloadSheetLblKey = "63";
        public const string SizeDownloadBtnKey = "64";
        public const string SizeImportAgainBtnKey = "65";
        public const string SizeSyncDlgTitleLblKey = "66";
        public const string SizeSyncWithLocalFileBtnKey = "67";
        public const string SizeSyncWithSheetsBtnKey = "68";
        public const string SizeSyncWithNewSheetBtnKey = "69";
        public const string SizeSyncWithNewLocalFileBtnKey = "70";
        public const string SizeSyncExcelWorkbookLblKey = "71";
        public const string SizeSyncBtnKey = "72";
        public const string SizeSyncNewGoogleSheetLblKey = "73";
        public const string SizeSyncCompleteLblKey = "74";
        public const string SizeSyncCompleteMsg1Key = "75";
        public const string SizeSyncCompleteMsg2Key = "76";
        public const string SizeChooseGoogleSheetLblKey = "77";
        public const string SizeSelectSpreadSheetSyncLblKey = "78";
        public const string SizeSyncingWithGoogleSheetKey = "79";
        public const string SizeSyncingWithLocalFileKey = "80";
        public const string SizeVisualPresentationKey = "81";
        public const string SizeLogicalPresentationKey = "82";
        public const string SizeRateMeTextKey = "83";
        public const string SizeBuyLinkTextKey = "84";
        #endregion

        #region Button Strings
        public const string SaveBtn = "Save";
        public const string SaveAndUploadBtn = "Save and Upload";
        public const string SaveNeededBtn = "Save Needed";
        public const string LoadBtn = "Load";
        public const string ClearSearchBtn = "Clear Search";
        public const string ApplyBtn = "Apply";
        public const string DeleteBtn = "Delete";
        public const string CancelBtn = "Cancel";
        public const string AddStringBtn = "Add String";
        public const string SyncBtn = "Sync";
        public const string ExportToLocalFileBtn = "Export to Local File";
        public const string SyncWithLocalFileBtn = "Existing Local File";
        public const string SyncWithNewLocalFileBtn = "New Local File";
        public const string ExportToSheetsBtn = "Update existing Google SpreadSheet";
        public const string SyncWithSheetsBtn = "Existing Google Spreadsheet";
        public const string ExportToNewSheetBtn = "Upload new Google Spreadsheet";
        public const string SyncWithNewSheetBtn = "New Google Spreadsheet";
        public const string ExportBtn = "Export";
        public const string ImportBtn = "Import";
        public const string BackBtn = "Back";
        public const string DownloadBtn = "Download";
        public const string UploadBtn = "Upload";
        public const string ClearChoicesBtn = "Clear Selections";
        public const string AllLeftBtn = "All Left";
        public const string AllRightBtn = "All Right";
        public const string LeftSide = "Choose Left (Local)";
        public const string RightSide = "Choose Right ({0})";
        public const string RemoveOnlyLeft = "Remove Key (Does Not Exist on Right Side)";
        public const string RemoveOnlyRight = "Remove Key (Does Not Exist on Left Side)";
        public const string GoToAuthURLBtn = "Go to Google Authenticate URL";
        public const string SetCodeBtn = "Set Code";
        public const string ImportAgainBtn = "Import Again";
        public const string CloseBtn = "Close";
        public const string ExportAgainBtn = "Export Again";
        public const string LaunchLEBtn = "Launch Localization Editor Window";
        #endregion

        #region Label Strings
        public const string MenuLbl = "Localization Editor";
        public const string LEEditorWindowLbl = "String Table Editor";
        public const string MainHeaderLbl = "Localization Editor";
        public const string SyncHeaderLbl = "Localization Editor Sync Helper";
        public const string SyncActionsLbl = "Sync Actions";
        public const string SyncDifferencesLbl = "Sync Differences";
        public const string ExpandAllLbl = "Expand All";
        public const string CollapseAllLbl = "Collapse All";
        public const string NewKeyLbl = "< New Key >";
        public const string NewValueLbl = "< New Localized String >";
        public const string NewCustomLangLbl = "< New Language Code >";
        public const string NewLanguageLbl = "New Language";
        public const string AddLanguageLbl = "Add Language";
        public const string AddNewHeaderLbl = "Add New Language";
        public const string AddNewKeyLbl = "New Key";
        public const string ExistingKeyLbl = "Use Existing Key";
        public const string KeyLbl = "Key:";
        public const string KeysLbl = "Keys:";
        public const string ValueLbl = "Value:";
        public const string ValuesLbl = "Values:";
        public const string LanguageLbl = "Language:";
        public const string LanguagesLbl = "Languages:";
        public const string ExportDlgTitleLbl = "Choose Export Method";
        public const string ExportExcelWorkbookLbl= "Export Excel Workbook";
        public const string SyncExcelWorkbookLbl = "Sync with Excel Workbook";
        public const string SyncSectionHeader = "Spreadsheet Actions";
        public const string LocStringSectionHeader = "Localized Strings";
        public const string ExportGoogleSheetLbl = "Update Google Sheet";
        public const string ChooseGoogleSheetLbl = "Choose Google Sheet";
        public const string ExportNewGoogleSheetLbl = "Upload New Google Sheet";
        public const string SyncNewGoogleSheetLbl = "Sync with New Google Sheet";
        public const string SelectSpreadSheetLbl = "Select Spreadsheet to Import:";
        public const string SelectSpreadSheetSyncLbl = "Select Spreadsheet to Sync with:";
        public const string NewSheetFileNameLbl = "Enter filename for new sheet:";
        public const string SyncWindowTitleLbl = "Sync";
        public const string ChooseImportExcelLbl = "Choose Import Method";
        public const string ImportLocalLbl = "Import Local File";
        public const string ImportGoogleSpreadsheetLbl = "Import Google SpreadSheet";
        public const string ReauthenticateGoogleLbl = "Reauthenticate With Google";
        public const string ImportExcelWBLbl = "Import Excel Workbook";
        public const string ExcelFilePathLbl = "Excel File (.xlsx or .xls)";
        public const string BrowseLbl = "Browse ...";
        public const string OpenWBLbl = "Open Workbook";
        public const string SaveSyncFileLbl = "Save Excel File";
        public const string AuthWithGoogleLbl = "Authenticate With Google";
        public const string GoogleAuthInstruction1_1 = "1) Make sure you are logged in to the";
        public const string GoogleAuthInstruction1_2 = "correct google account in your browser.";
        public const string GoogleAuthInstruction2_1 = "2) Authorize access to your Google Sheet.";
        public const string GoogleAuthInstruction2_2 = "Enter the code specified after you accept.";
        public const string EnterAccessCodeLbl = "Enter Access Code from Google:";
        public const string DownloadSheetLbl = "Download Google Sheet";
        public const string ImportComplete = "Import Complete!";
        public const string ImportMsg1 = "Your import is complete. Close this window or";
        public const string ImportMsg2 = "select \""+ ImportAgainBtn + "\" to import a different spreadsheet.";
        public const string ImportSpreadsheetLbl = "Import Spreadsheet";
        public const string ExportSpreadsheetLbl = "Export Spreadsheet";
        public const string ClearSyncSettings = "Clear Sync Settings";
        public const string SceneCrawlerLbl = "Process Text in Scene";
        public const string GenKeysLbl = "Generate Static Keys Class";
        public const string ExportCompleteLbl = "Export Complete!";
        public const string ExportMsg1 = "Your export is complete. Close this window or";
        public const string ExportMsg2 =  "select \""+ ExportAgainBtn + "\" to export to a different spreadsheet.";
        public const string SkipUploadMsg = "Skipping upload, only right side changes chosen during sync.";
        public const string CustomLbl = "Custom";
        public const string AddNewLanguageMsg = "Add a new language or import a spreadsheet to begin localizing your strings.";
        public const string InvariantCultureCode = "iv";
        public const string KeepNewLangLbl = "Keep New Language";
        public const string RemoveLangLbl = "Remove Language";
        public const string NoSyncNeededLbl_1 = "No Differences Found!";
        public const string NoSyncNeededLbl_2 = "You can close this window. No action is needed.";
        public const string SyncCompleteLbl = "Sync Complete!";
        public const string ErrorDownloadingSheet = "Error downloading spreadsheet: ";
        public const string LocalizedText = "Localized Text";
        public const string LocalizedGUIText = "Localized GUIText";
        public const string LocalizedTextMesh = "Localized TextMesh";
        public const string AddLanguageWarning = "Before adding any strings, add a language using the Localization Editor Window!";
        public const string ProcessingComponentsFormat = "Processing all {0} components... ";
        public const string DoneProcessingFormat = "Done. Processed {0} {1} components.";
        public const string ListenForEventsLbl = "Register LE Editor Events";
        public const string ContactMenu = "Contact";
        public const string EmailMenu = "Email";
        public const string TwitterMenu = "Twitter";
        public const string ForumMenu = "LE Forum";
        public const string DocsMenu = "LE Documentation";
        public const string ProcessSceneCompleteMsg = "{0} Components Processed\n{1} Components Configured\n{2} Componenta Already Configured";
        public const string ProcessSceneMsgTitle = "Scene Processing Complete";
        public const string SyncDlgTitleLbl = "Choose Sync Method";
        public const string SyncCompleteMsg1 = "Your project is now configured to sync with";
        public const string SyncCompleteMsg2 = "the Excel file you chose:";
        public const string LocalFileLbl = "Local File";
        public const string GoogleSheetLbl = "Google Spreadsheet";
        public const string SyncingWithGoogleSheet = "Syncing With Google Sheet";
        public const string SyncingWithLocalFile = "Syncing With Excel File";
        public const string SyncingLbl = "Name: ";
        public const string ExcelFileLbl = "Excel File";
        public const string SyncSettingClearedTitle = "Sync Settings Cleared";
        public const string SyncSettingsMsg = "Sync settings have been cleared.";
        public const string VisualPresentation = "Visual Presentation";
        public const string LogicalPresentation = "Logical Presentation";
        public const string GeneratingLbl = "Generating:";
        public const string DoneGeneratingLbl = "Done Generating:";
        public const string RateLELbl = "Rate LE";
        public const string BuyLinkText = "Upgrade to Full Version";
        #endregion

        #region Error Strings
        public const string ErrorLbl = "Error!";
        public const string OkLbl = "Ok";
        public const string DirectoryNotFound = "Could not find part of the path: {0}";
        public const string LangAlreadyDefinedFormat = "Language \"{0}\" already defined!";
        public const string DuplicateKeyFormat = "Duplicate key found! (Key: {0})";
        public const string DiffChoiceNotFoundFormat = "No selection found for key: {0} in language: {1}";
        public const string DiffChoiceNotFoundForLang = "No selection for for language: {0}";
        public const string SyncCollectionNull = "The sync language collection is null! Nothing to sync with.";
        public const string SyncSettingsMissing = "Sync settings are missing. Couldn't complete sync.";
        public const string CultureNotFoundWarning = "Culture for \"{0}\" not found. If this is a custom language, ignore this warning.";
        public const string BidiSupportDllError = "Error loading lebidi-osx for RTL (right to left) language support.";
        public const string BidiSupportGetFunctionHandleError = "Error getting handle to GetBidi function.";
        #endregion

        #region Window Constants
        public const float MinLabelWidth = 200f;
        public const int Indent = 20;
        public const float VectorFieldBuffer = 0.75f;
        public const float MinTextAreaWidth = 100f;
        public const double DoubleClickTime = 0.5;
        public const double AutoSaveTime = 30;
        public const float PreferencesMinWidth = 640f;
        public const float PreferencesMinHeight = 280f;
        public const float RadioButtonWidth = 14f;
        public const string MainHeaderColorString = "#013859";
        public const string MainHeaderColorProString = "#36ccdb";
        public const float MaxSyncLogHeight = 150f;
        public const float ExcelWindowPadding = 20f;
        public const string DefaultCulture = "en";
        public const string SpecificCultureCategory = "Specific Cultures/";
        public const string NeutralCultureCategory = "Neutral Cultures/";
        public const float NewLangPopupWidth = 450f;
        public const float MaxSyncFileDisplayHeight = 55f;
                #endregion

        #region Default Preference Settings
        public const string DefaultLogicalFilePath = "Localized";
        public const string DefaultVisualFilePath = "Localized/Resources/";
        public const string VisualFileExtension = ".txt";
        public const string LogicalFileExtension = "-meta.txt";
        public const string SettingsPath = "Editor/le_editor_settings.bytes";
        public const string DefaultSheetName = "languages";
        public const string DefaultSyncName = "LE_String_Table";
        public const string DefaultSyncExt = "xlsx";
        public const string RootDir = "LocalizationEditor";
        #endregion

        #region Lebidi lib paths and settings
        public const string LEBidiOSXPath = "Editor/BidiSupport/lebidi-osx.dylib";
        public static string LEBidiWinPath
        {
            get
            {
#if UNITY_4_6
             return "Editor\\BidiSupport\\lebidi-32-win.dll";
#else
             return "Editor\\BidiSupport\\lebidi-64-win.dll";
#endif
            }
        }
        public const string GetBidiHandle = "GetBidi";
        #endregion

        #region Link Strings
        public const string RateMeText = "Click To Rate!";
        public const string ForumLinkText = "Suggest Features in the Forum";
        //public const string RateMeURL = "http://u3d.as/fNK";
        public const string BuyURL = "http://u3d.as/fNK";
        public const string ForumURL = "http://forum.unity3d.com/threads/localization-editor-15-languages-in-15-minutes.328019/";
        public const string DocURL = "http://localizationeditor.com/docs/le-quickstart.html";
        public const string Contact = "mailto:celeste%40stayathomedevs.com?subject=Question%20about%20LE&cc=steve%40stayathomedevs.com";
        public const string Twitter = "https://twitter.com/celestipoo";

        public const string BorderTexturePath = "Editor/Textures/boarder.png";
        public const string SyncFontPath = "Editor/Resources/UbuntuMono-R";
        #endregion

        #region Import Workbook Keys
        public const string WorkbookFileExportPathKey = "langs_export";
        #endregion

        #region SavedPreferences
        public const string ImportSpreadsheetName = "le_importspreadsheetname";
        #endregion
    }
}
