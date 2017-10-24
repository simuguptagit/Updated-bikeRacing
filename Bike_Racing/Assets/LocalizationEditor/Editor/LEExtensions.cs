using UnityEngine;
using System;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace LocalizationEditor
{
    public static class GenericExtensions
    {
        public static bool IsCloneableType<T>(this T variable)
        {
            return typeof(ICloneable).IsAssignableFrom(variable.GetType());
        }
        
        public static bool IsGenericList<T>(this T variable)
        {
            foreach (Type @interface in variable.GetType().GetInterfaces()) {
                if (@interface.IsGenericType) {
                    if (@interface.GetGenericTypeDefinition() == typeof(IList<>)) {
                        return true;
                    }
                }
            }
            return false;
        }
        
        public static bool IsGenericDictionary<T>(this T variable)
        {
            foreach (Type @interface in variable.GetType().GetInterfaces()) {
                if (@interface.IsGenericType) {
                    if (@interface.GetGenericTypeDefinition() == typeof(IDictionary<,>)) {
                        return true;
                    }
                }
            }
            return false;
        }
    }
    
    public static class FlagExtensions
    {
        public static bool IsSet(this Enum variable, Enum flag)
        {
            ulong variableVal = Convert.ToUInt64(variable);
            ulong flagVal = Convert.ToUInt64(flag);
            return (variableVal & flagVal) == flagVal;
        }
    }
    
    public static class FloatExtensions
    {
        public const float TOLERANCE = 0.0001f;
        public static bool NearlyEqual(this float a, float b)
        {
            return Math.Abs(a - b) < TOLERANCE;
        }
    }
    
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a new string that hightlights the first instance of substring with html color tag
        /// Ex. "The sky is <color=blue>blue</color>!"
		/// Only supported in Unity 4.0+
        /// </summary>
        /// <returns>A new string formatted with the color tag around the first instance of substring.</returns>
        /// <param name="substring">Substring to highlight</param>
        /// <param name="color">Color to specify in the color tag</param>
        public static string HighlightSubstring(this string variable, string substring, string color)
        {
			if (!Application.unityVersion.StartsWith("3"))
			{
	            string highlightedString = "";
            
	            if (!string.IsNullOrEmpty(substring))
	            {
	                int index = variable.Replace("Schema:", "       ").IndexOf(substring, StringComparison.CurrentCultureIgnoreCase);
                
	                if (index != -1)
	                    highlightedString = string.Format("{0}<color={1}>{2}</color>{3}", 
	                                                      variable.Substring(0, index), color, variable.Substring(index, substring.Length), variable.Substring(index+substring.Length));
	                else
	                    highlightedString = variable.Clone() as string;
	            }
	            else
	                highlightedString = variable.Clone() as string;
            
	            return highlightedString;			
			}
			else
				return variable.Clone() as string;
        }

        /// <summary>
        /// Returns the Md5 Sum of a string.
        /// </summary>
        /// <returns>The Md5 sum.</returns>
        public static string Md5Sum(this string strToEncrypt)
        {
            UTF8Encoding ue = new UTF8Encoding();
            byte[] bytes = ue.GetBytes(strToEncrypt);
            
            // encrypt bytes
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(bytes);
            
            // Convert the encrypted bytes back to a string (base 16)
            string hashString = "";
            
            for (int i = 0; i < hashBytes.Length; i++)
            {
                hashString += Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
            }
            
            return hashString.PadLeft(32, '0');
        }

        /// <summary>
        /// Uppercases the first letter
        /// </summary>
        /// <returns>A copy of the string with the first letter uppercased.</returns>
        /// <param name="s">The string to uppercase.</param>
        public static string UppercaseFirst(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
    
    public static class ColorExtensions
    {
        public static string ToHexString(this Color32 color)
        {
            return string.Format("{0}{1}{2}", color.r.ToString("x2"), color.g.ToString("x2"), color.b.ToString("x2"));
        }
        
        public static Color ToColor(this string hex)
        {
            hex = hex.Replace("#", "");
            
            byte r = byte.Parse(hex.Substring(0,2), NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2,2), NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4,2), NumberStyles.HexNumber);
            
            return new Color32(r, g, b, 1);
        }

        public static bool NearlyEqual(this Color variable, Color other)
        {
            return  variable.r.NearlyEqual(other.r) &&
                    variable.g.NearlyEqual(other.g) &&
                    variable.b.NearlyEqual(other.b);
        }
    }

    public static class VectorExtensions
    {
        public static bool NearlyEqual(this Vector3 variable, Vector3 other)
        {
            return  variable.x.NearlyEqual(other.x) &&
                    variable.y.NearlyEqual(other.y) &&
                    variable.z.NearlyEqual(other.z);
        }
    }

    public static class EditorStringExtensions
    {
        static char[] dirSeparators = {'\\', '/'};
        public static string TrimLeadingDirChars(this string variable)
        {
            return variable.TrimStart(dirSeparators);
        }
    }

}

