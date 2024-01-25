using System.Text;

namespace EZWiki.Helpers
{
    public class UrlHelpers
    {
        /// https://stackoverflow.com/questions/25259/how-does-stack-overflow-generate-its-seo-friendly-urls
        /// <summary>
        /// Produces optional, URL-friendly version of a title, "like-this-one". 
        /// hand-tuned for speed, reflects performance refactoring contributed
        /// by John Gietzen (user otac0n) 
        /// </summary>
        public static string URLFriendly(string title)
        {
            if (title == null) return "";

            const int maxlen = 80;
            int len = title.Length;
            bool prevdash = false;
            var sb = new StringBuilder(len);
            char c;

            for (int i = 0; i < len; i++)
            {
                c = title[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevdash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lowercase
                    sb.Append((char)(c | 32));
                    prevdash = false;
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' ||
                    c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!prevdash && sb.Length > 0)
                    {
                        sb.Append('-');
                        prevdash = true;
                    }
                }
                else if ((int)c >= 128)
                {
                    int prevlen = sb.Length;
                    sb.Append(RemapInternationalCharToAscii(c));
                    if (prevlen != sb.Length) prevdash = false;
                }
                if (i == maxlen) break;
            }

            if (prevdash)
                return sb.ToString().Substring(0, sb.Length - 1);
            else
                return sb.ToString();
        }


        public static string RemapInternationalCharToAscii(char c)
        {
            Dictionary<char, string> charMap = new Dictionary<char, string>
            {
                {'à', "a"}, {'å', "a"}, {'á', "a"}, {'â', "a"}, {'ä', "a"}, {'ã', "a"}, {'å', "a"}, {'ą', "a"},
                {'è', "e"}, {'é', "e"}, {'ê', "e"}, {'ë', "e"}, {'ę', "e"},
                {'ì', "i"}, {'í', "i"}, {'î', "i"}, {'ï', "i"}, {'ı', "i"},
                {'ò', "o"}, {'ó', "o"}, {'ô', "o"}, {'õ', "o"}, {'ö', "o"}, {'ø', "o"}, {'ő', "o"}, {'ð', "o"},
                {'ù', "u"}, {'ú', "u"}, {'û', "u"}, {'ü', "u"}, {'ŭ', "u"}, {'ů', "u"},
                {'ç', "c"}, {'ć', "c"}, {'č', "c"}, {'ĉ', "c"},
                {'ż', "z"}, {'ź', "z"}, {'ž', "z"},
                {'ś', "s"}, {'ş', "s"}, {'š', "s"}, {'ŝ', "s"},
                {'ñ', "n"}, {'ń', "n"},
                {'ý', "y"}, {'ÿ', "y"},
                {'ğ', "g"}, {'ĝ', "g"},
                {'ř', "r"},
                {'ł', "l"},
                {'đ', "d"},
                {'ß', "ss"},
                {'Þ', "th"},
                {'ĥ', "h"},
                {'ĵ', "j"}
            };

            if (charMap.ContainsKey(c))
            {
                return charMap[c];
            }
            else
            {
                return "";
            }
        }
    }
}
