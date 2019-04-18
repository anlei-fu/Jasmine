using System.Collections.Generic;
using System.Text;

namespace Jasmine.Common
{
    public class JasmineStringValueParser : IStringValueParser
    {
        private const char AT = '@';
        private const char LEFT_BRACE = '{';
        private const char RIGHT_BRACE = '}';

        public IList<StringValue> Parse(string str)
        {
            var ls = new List<StringValue>();
            StringValue temp = new StringValue(StringValueType.Nomal);
            bool leftFound = false;
            var builder = new StringBuilder();

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == AT)//@
                {
                    if (!leftFound)//is left pattern
                    {
                        if (i + 1 < str.Length)//over length
                        {
                            if (str[i + 1] == LEFT_BRACE)//asuure is left pattern @{
                            {
                                if (builder.Length != 0)
                                {
                                    temp.Value = builder.ToString();
                                    builder.Clear();
                                    ls.Add(temp);
                                }

                                temp = new StringValue(StringValueType.Property);
                                leftFound = true;

                                i++;
                            }
                            else
                            {
                                builder.Append(str[i]);
                            }
                        }
                        else
                        {
                            builder.Append(str[i]);
                        }
                    }
                    else
                    {
                        builder.Append(str[i]);
                    }
                }
                else if (str[i] == RIGHT_BRACE)
                {
                    if (leftFound)//pattern closed
                    {
                        temp.Value = builder.ToString();
                        ls.Add(temp);
                        builder.Clear();
                        temp = new StringValue(StringValueType.Nomal);
                        leftFound = false;
                    }
                    else
                    {
                        builder.Append(str[i]);
                    }
                }
                else
                {
                    builder.Append(str[i]);
                }

            }

            if (builder.Length != 0)
                ls.Add(new StringValue(StringValueType.Nomal) { Value = builder.ToString() });

            return ls;
        }

        
    }
}
