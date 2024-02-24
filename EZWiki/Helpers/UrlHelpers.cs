using System.Text;
using System.Text.RegularExpressions;

namespace EZWiki.Helpers
{
    public class UrlHelpers
    {
        public static string URLFriendly(string title)
        {
            string slug = title.ToLowerInvariant();
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
            slug = Regex.Replace(slug, @"\s+", " ").Trim();
            slug = slug.Replace(" ", "-");

            slug = Regex.Replace(slug, @"-+", "-");
            slug = Transliterate(slug);
            return slug;
        }

        public static string Transliterate(string s)
        {
            string[,] charMap =
            {
                {"à", "a"}, {"å", "a"}, {"á", "a"}, {"â", "a"}, {"ä", "a"}, {"ã", "a"}, {"å", "a"}, {"ą", "a"},
                {"è", "e"}, {"é", "e"}, {"ê", "e"}, {"ë", "e"}, {"ę", "e"},
                {"ì", "i"}, {"í", "i"}, {"î", "i"}, {"ï", "i"}, {"ı", "i"},
                {"ò", "o"}, {"ó", "o"}, {"ô", "o"}, {"õ", "o"}, {"ö", "o"}, {"ø", "o"}, {"ő", "o"}, {"ð", "o"},
                {"ù", "u"}, {"ú", "u"}, {"û", "u"}, {"ü", "u"}, {"ŭ", "u"}, {"ů", "u"},
                {"ç", "c"}, {"ć", "c"}, {"č", "c"}, {"ĉ", "c"},
                {"ż", "z"}, {"ź", "z"}, {"ž", "z"},
                {"ś", "s"}, {"ş", "s"}, {"š", "s"}, {"ŝ", "s"},
                {"ñ", "n"}, {"ń", "n"},
                {"ý", "y"}, {"ÿ", "y"},
                {"ğ", "g"}, {"ĝ", "g"},
                {"ř", "r"},
                {"ł", "l"},
                {"đ", "d"},
                {"ß", "ss"},
                {"Þ", "th"},
                {"ĥ", "h"},
                {"ĵ", "j"}
            };

            StringBuilder sb = new StringBuilder(s);

            // Apply transliteration mappings
            for (int i = 0; i < sb.Length; i++)
            {
                for (int j = 0; j < charMap.GetLength(0); j++)
                {
                    if (sb.ToString().Contains(charMap[j, 0]))
                    {
                        sb.Replace(charMap[j, 0], charMap[j, 1]);
                    }
                }
            }

            return sb.ToString();
        }
    }
}
