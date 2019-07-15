using System.Text;

namespace Jasmine.Extensions
{
    public static class StringBuilderExtension
    {
        public static StringBuilder RemoveLastChar(this StringBuilder builder,char ch)
        {
            if (builder.Length!=0&&builder[builder.Length - 1] == ch)
                builder.Remove(builder.Length - 1, 1);

            return builder;
        }
    }
}
