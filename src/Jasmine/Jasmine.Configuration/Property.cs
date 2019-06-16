using Jasmine.Common;
using Jasmine.Configuration.Exceptions;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Configuration
{
    /// <summary>
    /// ex
    /// 
    /// prefix: @{prefix}say
    /// </summary>
    public class Property:INameFearture
    {
        /// <summary>
        /// group belong to, contains some segements
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// property node
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// segments
        /// </summary>
        public PropertySegement[] Segments { get; set; }

        public string GetValue(JasmineConfigurationProvider provider,Dictionary<string,string> parameters)
        {
            var builder = new StringBuilder();

            foreach (var item in Segments)
            {
                if (item.IsTemplate)
                {
                    builder.Append(item.Template.GetValue(provider));
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
