using System.Linq;

namespace RegisterMyFonts
{
    internal static class StringExtensions
    {
        public static bool EqualsIgnoreCase(this string a, string b)
        {
            return a.ToLower().Equals(b.ToLower());
        }

        public static bool EqualsIgnoreCase(this string a, params string[] b)
        {
            return b.Any(a.EqualsIgnoreCase);
        }
    }
}