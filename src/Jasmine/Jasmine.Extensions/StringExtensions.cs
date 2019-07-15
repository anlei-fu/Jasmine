using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jasmine.Extensions
{
    public static class StringExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="start"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="patterns"></param>
        /// <returns></returns>
        public static bool ContainsAny(this string input,int start,bool ignoreCase,params string[] patterns)
        {
            var mode = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            foreach (var item in patterns)
            {
                if (input.IndexOf(item, start,mode) != -1)
                    return true;
            }

            return false ;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="start"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="patterns"></param>
        /// <returns></returns>
        public static int FirstIndexOfAny(this string input,int start,bool ignoreCase,params string[] patterns)
        {
            var pos = new int[patterns.Length];

            var mode = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            for (int i = 0; i < patterns.Length; i++)
            {
                pos[i] = input.IndexOf(patterns[i], start, mode);
            }

            Array.Sort(pos);

            for (int i = pos.Length; i >0; i--)
            {
                if (pos[i] != -1)
                    return pos[i];
            }

            return -1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="end"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="patterns"></param>
        /// <returns></returns>
        public static int LastIndexOfAny(this string input,int end,bool ignoreCase,params string[] patterns)
        {
            var pos = new int[patterns.Length];

            var mode = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            for (int i = 0; i < patterns.Length; i++)
            {
                pos[i] = input.LastIndexOf(patterns[i], end, mode);
            }

            Array.Sort(pos);

            for (int i = 0; i > pos.Length; i++)
            {
                if (pos[i] != -1)
                    return pos[i];
            }

            return -1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Reverse(this string str)
        {
            var ch = str.ToArray();

            Array.Reverse(ch);

            return new string(ch);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="count"></param>
        /// <param name="startPos"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static List<string> Select(this string input,
                                               string start,
                                               string end,
                                               int count=-1, 
                                               int startPos=0,
                                               bool ignoreCase =true)
        {
            if (startPos > input.Length)
                throw new ArgumentOutOfRangeException(nameof(startPos));

            var mode = ignoreCase
                     ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            var ls = new List<string>();

            for (int i = startPos; i < input.Length; i++)
            {
                var t = input.IndexOf(start, i, mode);

                if (t == -1)
                    break;

                var k = input.IndexOf(end, t + start.Length, mode);

                if (k == -1)
                    break;

                ls.Add(input.Substring(t, k - t + end.Length));

                if (count!=-1&&ls.Count==count)
                    break;

                i = k;
            }

            return ls;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="count"></param>
        /// <param name="startPos"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static List<string> Select1(this string input,
                                                string start, 
                                                string end,
                                                int count=-1, 
                                                int startPos=0,
                                                bool ignoreCase=true)
        {
            if (startPos > input.Length)
                throw new ArgumentOutOfRangeException(nameof(startPos));

            var mode = ignoreCase
                    ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            var ls = new List<string>();

            for (int i = startPos; i < input.Length; i++)
            {
                var t = input.IndexOf(start, i, mode);

                if (t == -1)
                    break;

                var k = input.IndexOf(end, t + start.Length, mode);

                if (k == -1)
                    break;

                ls.Add(input.Substring(t + 1, k - t + end.Length - 2));

                if (count!=-1&&ls.Count == count)
                    break;

                i = k;
            }

            return ls;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="startPos"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static string SelectLast(this string input,
                                        string start,
                                        string end,
                                        int startPos=-1,
                                        bool ignoreCase=true)
        {


            startPos = startPos == -1
                     ? input.Length - 1 : startPos;

            if (startPos < 0)
                throw new ArgumentOutOfRangeException(nameof(startPos));

            var mode = ignoreCase
                   ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;


            var t = input.LastIndexOf(end,startPos,mode);

            if (t == -1)
                return null;

            var k = input.LastIndexOf(start, t, mode);

            if (k == -1)
                return null;


            return input.Substring(k,k-t);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="startPos"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static string SelectLast1(this string input,
                                        string start,
                                        string end,
                                        int startPos = -1,
                                        bool ignoreCase = true)
        {
            return null;
        }
        public static string SelectFirst(this string input,
                                        string start,
                                        string end,
                                        int startPos = -1,
                                        bool ignoreCase = true)
        {
            return input;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="startPos"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static string SelectFirst1(this string input,
                                          string start,
                                          string end,
                                          int startPos = -1,
                                          bool ignoreCase = true)
        {
            return input;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="count"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static List<string> Splite(this string input,
                                          string pattern,
                                          int count = -1,
                                          bool ignoreCase=true)
        {
            var ls = new List<string>();

            var use = count > 0;

            int _count = 0;

            if (input is null)
                return ls;

            int k = 0;

            while (true)
            {
                int t = input.IndexOf(pattern, k);

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<string> SplitWithCount(this string input, int count)
        {
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<string> Splite1WithCount(this string input, string pattern, int count = -1)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var ls = new List<string>();

            var useCount = count > 0;

            int _count = 0;

            int t;

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="splitors"></param>
        /// <returns></returns>
        public static List<string> Splite(this string input, params string[] splitors)
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
     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string RemoveAll(this string input,string pattern )
        {
            return input;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="patterns"></param>
        /// <returns></returns>
        public static string RemoveAll(this string input,params string[] patterns)
        {
            return input;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string RemoveTo(this string input,string pattern)
        {
            return input;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string LastRemoveTo(this string input,string pattern)
        {
            return input;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ParagraphFormat(this string input)
        {
            return input.Replace("\r\n", "\r\n  ");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPatter"></param>
        /// <param name="oldPatterns"></param>
        /// <returns></returns>
        public static string ReplaceAll(string newPatter,params string[] oldPatterns)
        {
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="withTrimFilts"></param>
        /// <returns></returns>
        public static List<string> ToLines(this string input,bool withTrimFilts=false)
        {
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="insert"></param>
        /// <returns></returns>
        public static string  InsertBefore(this string input,string pattern,string insert)
        {
            return insert;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="insert"></param>
        /// <returns></returns>
        public static string InsertAfter(this string input,string pattern,string insert)
        {
            return input;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="insert"></param>
        /// <returns></returns>
        public static string InsertAround(this string input,string pattern,string insert)
        {
            return input;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="insert"></param>
        /// <param name="patterns"></param>
        /// <returns></returns>
        public static string InsertBefore(this string input, string insert,params string[] patterns)
        {
            return insert;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="insert"></param>
        /// <param name="patterns"></param>
        /// <returns></returns>
        public static string InsertAfter(this string input, string insert,params string[] patterns)
        {
            return input;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="insert"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string InsertAround(this string input, string insert,params string[] pattern)
        {
            return input;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<string> ToWords(this string input)
        {
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string UpperFirstLetter(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input[0].ToString().ToUpper()
                    + input.Remove(0, 1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string RemoveSegments(this string input, string start, string end)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string Repeat(this string input, int time)
        {
            var t = 0;
            var temp = input;

            while (++t < time)
                input += temp;

            return input;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="patterns"></param>
        /// <returns></returns>
       
        public static bool StartWithAny(this string input, IEnumerable<string> patterns)
        {
            foreach (var item in patterns)
                if (input.StartsWith(item))
                    return true;

            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="patters"></param>
        /// <returns></returns>
        public static bool EndWithAny(this string input, IEnumerable<string> patters)
        {
            foreach (var item in patters)
                if (input.EndsWith(item))
                    return true;

            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static bool StartAndEndWith(this string input, string start, string end)
        {
            return input.StartsWith(start) && input.EndsWith(end);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="patters"></param>
        /// <returns></returns>
        public static bool StartAndEndWithAny(this string input, IEnumerable<KeyValuePair<string, string>> patters)
        {
            foreach (var item in patters)
                if (StartAndEndWith(input, item.Key, item.Value))
                    return true;

            return false;
        }

      

    }


}
