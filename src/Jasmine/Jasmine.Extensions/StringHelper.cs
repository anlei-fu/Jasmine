using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jasmine.Extensions
{
    public static class StringExtensions
    {

        public static int LastIndexOf(this string input, int start, HashSet<char> partterns)
        {
            if (start > input.Length || start < 0)
                throw new ArgumentOutOfRangeException();

            for (int i = start; i >= 0; i--)
            {
                if (input.Contains(input[i]))
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

        public static string RangeReverse(this string input, int start, int end)
        {
            char[] ch1 = null;

            try
            {
                ch1 = input.Substring(start, end - start).ToArray();
            }
            catch
            {
                throw new ArgumentOutOfRangeException();
            }

            Array.Reverse(ch1);

            var ch2 = input.ToArray();
            var t = 0;

            for (int i = start; i < end; i++, t++)
                ch2[i] = ch1[t];

            return new string(ch2);
        }



        public static List<string> SelectWithMatch(this string input, string start, string end)
        {
            var ls = new List<string>();

            int t = 0, k = 0;

            for (int i = 0; i < input.Length; i++)
            {
                t = input.IndexOf(start, i);

                if (t == -1)
                    break;

                k = input.IndexOf(end, t + start.Length);

                if (k == -1)
                    break;

                if (k < t)
                {
                    for (int j = k; j < input.Length; j++)
                    {
                        k = k + 1;

                        if (k > input.Length)
                            break;

                        k = input.IndexOf(end, k);

                        if (k > t)
                            break;

                        if (k == -1)
                            return ls;
                    }
                }

                ls.Add(input.Substring(t, k - t + end.Length));

                i = k;
            }
            return ls;
        }
        public static string RangReplace(this string input, string oldPattern, string newPattern, int start, int end)
        {
            var temp = string.Empty;

            try
            {
                temp = input.Substring(start, end - start);
            }
            catch
            {
                return input;
            }

            input = input.Replace(temp, "*&ds2￥h5j%^w我kjd");

            temp = temp.Replace(oldPattern, newPattern);

            return input.Replace("*&ds2￥h5j%^w我kjd", temp);
        }

        public static List<string> Select(this string input, string start, string end)
        {
            var ls = new List<string>();

            int t = 0, k = 0;

            for (int i = 0; i < input.Length; i++)
            {
                t = input.IndexOf(start, i);

                if (t == -1)
                    break;

                k = input.IndexOf(end, t + start.Length);

                if (k == -1)
                    break;

                ls.Add(input.Substring(t, k - t + end.Length));
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
        public static List<string> SelectWithCount(this string input, string start, string end, int count = 1)
        {
            var ls = new List<string>();

            if (input is null)
                return ls;

            int t = 0, k = 0;

            if (count < 0)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    t = input.IndexOf(start, i);

                    if (t == -1)
                        break;

                    k = input.IndexOf(end, t + start.Length);

                    if (k == -1)
                        break;

                    ls.Add(input.Substring(t, k - t + end.Length));
                    i = k;
                }
                return ls;
            }
            for (int i = 0; i < input.Length; i++)
            {
                t = input.IndexOf(start, i);

                if (t == -1)
                    break;

                k = input.IndexOf(end, t + start.Length);

                if (k == -1)
                    break;

                ls.Add(input.Substring(t, k - t + end.Length));

                if (ls.Count == count)
                    break;

                i = k;
            }
            return ls;
        }
        public static List<string> Splite(this string input, string pattern, int count = -1)
        {
            var ls = new List<string>();

            var use = count > 0;

            int _count = 0;

            if (input is null)
                return ls;

            int t = 0;
            int k = 0;

            while (true)
            {
                t = input.IndexOf(pattern, k);

                if (t == -1)
                {
                    ls.Add(input.Substring(k, input.Length - k) + pattern);

                    break;
                }

                t += pattern.Length;
                ls.Add(input.Substring(k, t - k));
                k = t;

                if (k == input.Length)
                    return ls;

                if (!use)
                    continue;

                if (++_count == count)
                    return ls;
            }

            return ls;
        }
        public static List<string> Splite1(this string input, string pattern, int count = -1)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var ls = new List<string>();

            var useCount = count>0;

            int _count = 0;

            int t ;

            int k = 0;

            while (true)
            {
                t = input.IndexOf(pattern, k);

                if (t == -1)
                {
                    ls.Add(input.Substring(k, input.Length - k));

                    break;
                }

                ls.Add(input.Substring(k, t - k));

                k = t + 1;

                if (k == input.Length)
                    return ls;

                if (!useCount)
                    continue;

                if (++_count == count)
                    return ls;
            }

            return ls;
        }
        public static List<string> Splite(this string input, IEnumerable<string> splitors)
        {

            var t = 0;
            var k = 1000000;

            var ls = new List<string>();

            while (true)
            {
                foreach (var item in splitors)
                {
                    var b = input.IndexOf(item, t);

                    b = b == -1 ? 1000000 : b;

                    if (k >= b)
                        k = b;
                }

                if (k == 1000000)
                {
                    if (t != input.Length)
                        ls.Add(input.Substring(t, input.Length - t));

                    break;
                }

                ls.Add(input.Substring(t, k + 1 - t));

                t = k + 1;

                k = 1000000;
            }

            return ls;

        }
        public static string GetOnlyOneNewLineSpliter(this string input)
        {
            input = input.Replace("\r", "\n");
            int t = 0;

            while (t != -1)
            {
                input = input.Replace("\n\n", "\n");
                t = input.IndexOf("\n\n");
            }

            return input.Replace("\n", "\r\n");
        }
        public static string GetOnlyOneBlank(this string input)
        {
            input = input.Replace("\t", " ");

            var sb = new StringBuilder();

            var rm = false;

            foreach (var item in input)
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
        public static string ParagraphFormat(this string input)
        {
            return input.Replace("\r\n", "\r\n  ");
        }
        public static string RemoveFormatterChars(this string input)
        {
            var set = new HashSet<char>() { '\n', '\t', '\r', ' ', };

            var sb = new StringBuilder();

            foreach (var item in input)
            {
                if (!set.Contains(item))
                    sb.Append(item);
            }

            return sb.ToString();
        }


        public static string UpperFirstLetter(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input[0].ToString().ToUpper()
                    + input.Remove(0, 1);
        }
        public static string RemoveWithMatch(this string input, string start, string end)
        {
            int t = 0, k = 0;

            var sb = new StringBuilder();

            while (true)
            {
                t = input.IndexOf(start, k);
                if (t == -1)
                {
                    sb.Append(input.Substring(k, input.Length - k));

                    break;
                }

                sb.Append(input.Substring(k, t - k));

                k = input.IndexOf(end, t);

                if (k == -1)
                {
                    sb.Append(input.Substring(t, input.Length - t));

                    break;
                }

                k += end.Length;
            }
            return sb.ToString();
        }


        public static void RemoveWithMatch(this string input, string start, string end, int count = -1)
        {
            int t = 0, k = 0;
            var use_count = true;

            if (count <= 0)
                use_count = true;

            var _count = 0;

            while (true)
            {
                t = input.IndexOf(start, k);

                if (t == -1)
                    return;

                k = input.IndexOf(end, t + start.Length);

                if (k == -1)
                    return;

                input = input.Remove(t, k + end.Length - t);
                k = t;

                if (!use_count)
                    continue;

                if (++_count == count)
                    return;
            }
        }

        public static string Repeat(this string input, int time)
        {
            var t = 0;
            var temp = input;

            while (++t < time)
                input += temp;

            return input;
        }


        public static bool Contains(this string input, IEnumerable<string> patterns)
        {
            foreach (var item in patterns)
                if (!input.Contains(item))
                    return false;

            return true;
        }

        public static bool StartWith(this string input, IEnumerable<string> patterns)
        {
            foreach (var item in patterns)
                if (input.StartsWith(item))
                    return true;

            return false;
        }


        public static bool EndWith(this string input, IEnumerable<string> patters)
        {
            foreach (var item in patters)
                if (input.EndsWith(item))
                    return true;

            return false;
        }

        public static bool StartAndEndWith(this string input, string start, string end)
        {
            return input.StartsWith(start) && input.EndsWith(end);
        }

        public static bool StartAndEndWith(this string input, IEnumerable<KeyValuePair<string, string>> patters)
        {
            foreach (var item in patters)
                if (StartAndEndWith(input, item.Key, item.Value))
                    return true;

            return false;
        }

        public static bool ContainsOneOf(this string input, IEnumerable<string> patterns)
        {
            foreach (var item in patterns)
                if (input.Contains(item))
                    return true;

            return false;
        }

    }


}
