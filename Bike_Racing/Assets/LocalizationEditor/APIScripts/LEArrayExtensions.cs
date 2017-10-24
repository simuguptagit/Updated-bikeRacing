using System;

namespace LocalizationEditor
{
    public static class ArrayExtensions
    {
        public static bool IsValidIndex(this Array variable, int index)
        {
            return index > -1 && variable != null && index < variable.Length;
        }
    }
}
