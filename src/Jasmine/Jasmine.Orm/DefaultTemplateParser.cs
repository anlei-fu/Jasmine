using Jasmine.Components;
using Jasmine.Orm.Model;
using System.Collections.Generic;

namespace Jasmine.Orm.Implements
{

    /// <summary>
    /// a simple place holder parser,
    /// between  '{' and '}' is the parameter,
    /// if '{' 's previouce char is '@' that means it  is a name not a  string-paramteter
    /// 
    /// for example:
    ///    
    ///  template:   select @{column} from @{table}  where name={name} 
    ///  suppose:  column="address"  table="info" name="jasmine"  
    ///  after replaced:  select  address from info where name='jasmine'
    /// </summary>
    public class DefaultTemplateParser : ISegmentParser
    {

        private DefaultTemplateParser()
        {

        }
        public static readonly ISegmentParser Instance=new DefaultTemplateParser();

        public IEnumerable<SqlTemplateSegment> Parse(string template)
        {
            template = template.Trim();

            var result = new List<SqlTemplateSegment>();

            var leftFound = false;
            var lastIndex = 0;


            for (int i = 0; i < template.Length; i++)
            {
                if(template[i]=='{'&&!leftFound)
                {
                    result.Add(new SqlTemplateSegment(template.Substring(lastIndex, i - lastIndex), false,true));
                    lastIndex = i;
                    leftFound = true;
                }
                else if(template[i]=='}'&&leftFound)
                {
                    if(lastIndex!=0&&template[lastIndex-1]=='@')
                       result.Add(new SqlTemplateSegment(template.Substring(lastIndex+1, i - lastIndex-1).Trim(), true,false));
                    else
                        result.Add(new SqlTemplateSegment(template.Substring(lastIndex + 1, i - lastIndex - 1).Trim(), true, true));

                    lastIndex = i+1;
                    leftFound = false;
                }
            }

            if (lastIndex != template.Length)
                result.Add(new SqlTemplateSegment(template.Substring(lastIndex, template.Length - lastIndex), false,true));

            return result;
        }
    }
}
