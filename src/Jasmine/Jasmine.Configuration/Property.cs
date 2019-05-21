using Jasmine.Common;
using Jasmine.Configuration.Exceptions;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Configuration
{
    public class Property:INameFearture
    {
        public string Group { get; set; }
        public string Name { get; set; }
        public Template[] Templates { get; set; }

        public string GetValue(JasmineConfigurationProvider provider,Dictionary<string,string> parameters)
        {
            var builder = new StringBuilder();

            foreach (var item in Templates)
            {
                if (item.IsPropertyNode)
                {
                    builder.Append(item.PropertyNode.GetValue(provider));
                }
                else if (item.IsVariable)
                {
                    if (!parameters.ContainsKey(item.Value))
                        throw new VariableNotFoundException(item.Value, $"{Group}.{Name}");

                    builder.Append(parameters[item.Value]);
                }
                else
                {
                    builder.Append(item.Value);
                }
            }

            return builder.ToString();
        }

    }
}
