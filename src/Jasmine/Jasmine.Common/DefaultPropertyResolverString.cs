using System.Text;

namespace Jasmine.Common
{
    public class DefaultPropertyStringResolver : IPropertyStringResolver
    {
        private IStringValueParser _parser;
        public string Resolve(string source, IPropertyManager manager)
        {
            var segements = _parser.Parse(source);

            var sb = new StringBuilder();

            foreach (var item in segements)
            {
                switch (item.ValueType)
                {
                    case StringValueType.Globle:
                        sb.Append(item.Value);
                        break;
                    case StringValueType.Property:
                        sb.Append(manager.GetValue(item.Value));
                        break;
                    case StringValueType.Local:
                        sb.Append(item.Value);
                        break;
                    case StringValueType.Nomal:
                        sb.Append(item.Value);
                        break;
                }
            }

            return sb.ToString();

        }
    }
}
