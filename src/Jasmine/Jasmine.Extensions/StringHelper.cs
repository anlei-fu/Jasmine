using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jasmine.Extensions
{
    public static class StringExtensions
    {

        public static int LastIndexOf(this string str, int start, HashSet<char> input)
        {
            if (start > str.Length || start < 0)
                throw new ArgumentOutOfRangeException();

            for (int i = start; i >= 0; i--)
            {
                if (input.Contains(str[i]))
                    return i;
            }
            return -1;
        }
        public static string Reverse(string str)
        {
            var ch = str.ToArray();

            Array.Reverse(ch);

            return new string(ch);
        }

        public static string RangeReverse(this string str, int start, int end)
        {
            char[] ch1 = null;

            try
            {
                ch1 = str.Substring(start, end - start).ToArray();
            }
            catch
            {
                throw new ArgumentOutOfRangeException();
            }

            Array.Reverse(ch1);

            var ch2 = str.ToArray();
            var t = 0;

            for (int i = start; i < end; i++, t++)
                ch2[i] = ch1[t];

            return new string(ch2);
        }



        public static List<string> SelectWithMatch(this string str, string start, string end)
        {
            var ls = new List<string>();

            int t = 0, k = 0;

            for (int i = 0; i < str.Length; i++)
            {
                t = str.IndexOf(start, i);

                if (t == -1)
                    break;

                k = str.IndexOf(end, t + start.Length);

                if (k == -1)
                    break;

                if (k < t)
                {
                    for (int j = k; j < str.Length; j++)
                    {
                        k = k + 1;
                        if (k > str.Length) break;
                        k = str.IndexOf(end, k);
                        if (k > t) break;
                        if (k == -1) return ls;
                    }
                }

                ls.Add(str.Substring(t, k - t + end.Length));

                i = k;
            }
            return ls;
        }
        public static string RangReplace(this string str, string oldpattern, string newpattern, int start, int end)
        {
            var temp = string.Empty;

            try
            {
                temp = str.Substring(start, end - start);
            }
            catch
            {
                return str;
            }

            str = str.Replace(temp, "*&ds2￥h5j%^w我kjd");

            temp = temp.Replace(oldpattern, newpattern);

            return str.Replace("*&ds2￥h5j%^w我kjd", temp);
        }

        public static List<string> Select(this string str, string start, string end)
        {
            var ls = new List<string>();

            int t = 0, k = 0;

            for (int i = 0; i < str.Length; i++)
            {
                t = str.IndexOf(start, i);

                if (t == -1)
                    break;

                k = str.IndexOf(end, t + start.Length);

                if (k == -1)
                    break;

                ls.Add(str.Substring(t, k - t + end.Length));
                i = k;
            }

            return ls;
        }
        public static List<string> Select1(this string str, string start, string end)
        {

            var ls = new List<string>();
            int t = 0, k = 0;

            for (int i = 0; i < str.Length; i++)
            {
                t = str.IndexOf(start, i);

                if (t == -1)
                    break;

                k = str.IndexOf(end, t + start.Length);

                if (k == -1)
                    break;

                ls.Add(str.Substring(t + 1, k - t + end.Length - 2));
                i = k;
            }
            return ls;
        }
        public static List<string> SelectByCount(this string str, string start, string end, int count = 1)
        {
            List<string> ls = new List<string>();
            if (str is null)
                return ls;

            int t = 0, k = 0;

            if (count < 0)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    t = str.IndexOf(start, i);

                    if (t == -1)
                        break;

                    k = str.IndexOf(end, t + start.Length);

                    if (k == -1)
                        break;

                    ls.Add(str.Substring(t, k - t + end.Length));
                    i = k;
                }
                return ls;
            }
            for (int i = 0; i < str.Length; i++)
            {
                t = str.IndexOf(start, i);

                if (t == -1)
                    break;

                k = str.IndexOf(end, t + start.Length);

                if (k == -1)
                    break;

                ls.Add(str.Substring(t, k - t + end.Length));

                if (ls.Count == count)
                    break;

                i = k;
            }
            return ls;
        }
        public static List<string> Splite(this string str, string pattern, int count = -1)
        {
            var ls = new List<string>();
            var use_count = true;

            if (count <= 0)
                use_count = false;

            int _count = 0;

            if (str is null)
                return ls;

            int t = 0;
            int k = 0;

            while (true)
            {
                t = str.IndexOf(pattern, k);

                if (t == -1)
                {
                    ls.Add(str.Substring(k, str.Length - k) + pattern);

                    break;
                }

                t += pattern.Length;
                ls.Add(str.Substring(k, t - k));
                k = t;

                if (k == str.Length)
                    return ls;

                if (!use_count)
                    continue;

                if (++_count == count)
                    return ls;
            }

            return ls;
        }
        public static List<string> Splite1(this string str, string pattern, int count = -1)
        {
            var ls = new List<string>();
            var use_count = true;

            if (count <= 0)
                use_count = false;

            int _count = 0;

            if (str is null)
                return ls;

            int t = 0;
            int k = 0;

            while (true)
            {
                t = str.IndexOf(pattern, k);

                if (t == -1)
                {
                    ls.Add(str.Substring(k, str.Length - k));

                    break;
                }

                ls.Add(str.Substring(k, t - k));
                k = t + 1;

                if (k == str.Length)
                    return ls;

                if (!use_count)
                    continue;

                if (++_count == count)
                    return ls;
            }
            return ls;
        }
        public static List<string> Splite(this string str, IEnumerable<string> splitors)
        {

            var t = 0;
            var k = 1000000;
            var ls = new List<string>();

            while (true)
            {
                foreach (var item in splitors)
                {
                    var b = str.IndexOf(item, t);
                    b = b == -1 ? 1000000 : b;

                    if (k >= b)
                        k = b;
                }

                if (k == 1000000)
                {
                    if (t != str.Length)
                        ls.Add(str.Substring(t, str.Length - t));

                    break;
                }

                ls.Add(str.Substring(t, k + 1 - t));

                t = k + 1;
                k = 1000000;
            }

            return ls;

        }
        public static string GetOnlyOneNewLineSpliter(this string str)
        {
            str = str.Replace("\r", "\n");
            int t = 0;

            while (t != -1)
            {
                str = str.Replace("\n\n", "\n");
                t = str.IndexOf("\n\n");
            }

            return str.Replace("\n", "\r\n");
        }
        public static string GetOnlyOneBlank(this string str)
        {
            str = str.Replace("\t", " ");

            var sb = new StringBuilder();

            var rm = false;

            foreach (var item in str)
            {
                var temp = item.ToString();

                if (temp == " ")
                {
                    if (rm)
                        continue;

                    rm = true;
                }
                else
                {
                    rm = false;
                }

                sb.Append(temp);
            }

            return sb.ToString();
        }
        public static string ParagraphFormat(this string str)
        {
            return str.Replace("\r\n", "\r\n  ");
        }
        public static string RemoveFormatterChars(this string str)
        {
            var set = new HashSet<char>() { '\n', '\t', '\r', ' ', };

            var sb = new StringBuilder();

            foreach (var item in str)
            {
                if (!set.Contains(item))
                    sb.Append(item);
            }

            return sb.ToString();
        }


        public static string UpperFirstLetter(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return str[0].ToString().ToUpper()
                    + str.Remove(0, 1);
        }
        public static string RemoveWithMatch(this string str, string start, string end)
        {
            int t = 0, k = 0;

            var sb = new StringBuilder();

            while (true)
            {
                t = str.IndexOf(start, k);
                if (t == -1)
                {
                    sb.Append(str.Substring(k, str.Length - k));

                    break;
                }

                sb.Append(str.Substring(k, t - k));

                k = str.IndexOf(end, t);

                if (k == -1)
                {
                    sb.Append(str.Substring(t, str.Length - t));

                    break;
                }

                k += end.Length;
            }
            return sb.ToString();
        }


        public static void RemoveWithMatch(this string str, string start, string end, int count = -1)
        {
            int t = 0, k = 0;
            var use_count = true;

            if (count <= 0)
                use_count = true;

            var _count = 0;

            while (true)
            {
                t = str.IndexOf(start, k);

                if (t == -1)
                    return;

                k = str.IndexOf(end, t + start.Length);

                if (k == -1)
                    return;

                str = str.Remove(t, k + end.Length - t);
                k = t;

                if (!use_count)
                    continue;

                if (++_count == count)
                    return;
            }
        }

        public static string Repeat(this string str, int time)
        {
            var t = 0;
            var temp = str;

            while (++t < time)
                str += temp;

            return str;
        }


        public static bool Contains(this string str, IEnumerable<string> patterns)
        {
            foreach (var item in patterns)
                if (!str.Contains(item))
                    return false;

            return true;
        }

        public static bool StartWith(this string str, IEnumerable<string> patterns)
        {
            foreach (var item in patterns)
                if (str.StartsWith(item))
                    return true;

            return false;
        }


        public static bool EndWith(this string str, IEnumerable<string> patters)
        {
            foreach (var item in patters)
                if (str.EndsWith(item))
                    return true;

            return false;
        }

        public static bool StartAndEndWith(this string str, string start, string end)
        {
            return str.StartsWith(start) && str.EndsWith(end);
        }

        public static bool StartAndEndWith(this string str, IEnumerable<KeyValuePair<string, string>> patters)
        {
            foreach (var item in patters)
                if (StartAndEndWith(str, item.Key, item.Value))
                    return true;

            return false;
        }

        public static bool ContainsOneOf(this string str, IEnumerable<string> patterns)
        {
            foreach (var item in patterns)
                if (str.Contains(item))
                    return true;

            return false;
        }

    }


}
