using UnityEngine;
using System.Collections.Generic;
using System.Globalization;

namespace LocalizationEditor
{
    public abstract class LECulture
    {
        public abstract string Name { get; }
        public abstract string NativeName { get; }
        public abstract string DisplayName { get; }
        public abstract string TwoLetterISOLanguageName { get; }
        public abstract bool IsRightToLeft { get; }
    }

    class LEBuiltInCulture : LECulture
    {
        CultureInfo info;
        public LEBuiltInCulture(string code)
        {
            info = CultureInfo.GetCultureInfo(code);
        }

        public LEBuiltInCulture(CultureInfo cul)
        {
            info = cul;
        }

        public override string Name
        {
            get { return info.Name; }
        }
        
        public override string NativeName
        {
            get { return info.NativeName; }
        }

        public override string DisplayName
        {
            get { return info.DisplayName; }
        }
        
        public override string TwoLetterISOLanguageName
        {
            get { return info.TwoLetterISOLanguageName; }
        }
        
        public override bool IsRightToLeft
        {
            get { return info.TextInfo.IsRightToLeft; }
        }
    }

    class LECultureUrdu : LECulture
    {
        public override string Name
        {
             get { return TwoLetterISOLanguageName; }
        }

        public override string NativeName
        {
            get { return "اردو"; }
        }

        public override string DisplayName
        {
            get { return "Urdu"; }
        }

        public override string TwoLetterISOLanguageName
        {
            get { return "ur"; }
        }

        public override bool IsRightToLeft
        {
            get { return true; }
        }
    }

    public static class LECultureFactory
    {
        public static LECulture Get(string code)
        {
            LECulture cul = null;

            try
            {
                if (code.Equals("ur"))
                    cul = new LECultureUrdu();
                else
                    cul = new LEBuiltInCulture(code);
            }
            catch
            {
                cul = null;
            }

            return cul;
        }

        static List<LECulture> GetAll(CultureTypes type)
        {
            List<LECulture> cultures = new List<LECulture>();
            CultureInfo[] all = CultureInfo.GetCultures(type);
            foreach(CultureInfo cul in all)
                cultures.Add(new LEBuiltInCulture(cul));
            return cultures;
        }

        public static List<LECulture> GetAllSpecific()
        {
            return GetAll(CultureTypes.SpecificCultures);
        }

        public static List<LECulture> GetAllNeutral()
        {
            List<LECulture> cultures = GetAll(CultureTypes.NeutralCultures);
            cultures.Insert(0, new LECultureUrdu());
            return cultures;
        }
    }
}
