using UnityEditor;
using UnityEngine;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace LocalizationEditor
{
    public class LECodeGen
    {
        public static void GenStaticKeysClass(List<string> keys)
        {
            Debug.Log(LEConstants.GeneratingLbl + " " + LECodeGenConstants.StaticKeysFilePath);
            StringBuilder sb = new StringBuilder();
            
            sb.Append(LECodeGenConstants.AutoGenMsg);
            sb.Append("\n");
            sb.Append(LECodeGenConstants.StaticKeyClassHeader);
            
            foreach (var key in keys)
            {
                string visKey = LEStringTableEditor.Logical2Visual(key);

                sb.Append("\n");
                sb.Append("".PadLeft(LECodeGenConstants.IndentLevel2));
                sb.AppendFormat(LECodeGenConstants.StaticKeyFormat, visKey);
            }
            
            sb.Append("\n");
            sb.Append("}".PadLeft(LECodeGenConstants.IndentLevel1+1));
            sb.Append("\n");
            sb.Append("}");
            sb.Append("\n");

            File.WriteAllText(Path.Combine(LESettings.FullRootDir, LECodeGenConstants.StaticKeysFilePath), sb.ToString());
            Debug.Log(LEConstants.DoneGeneratingLbl + " " + LECodeGenConstants.StaticKeysFilePath);
            AssetDatabase.Refresh();
        }
    }
}
