using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.Scopes
{
    public  interface IVariableTable
    {
         IVariableTable Parnet { get; }
         void UnsetAll(string name);
         void Unset(string name);
         JObject GetVariable(string name);
         JObject Declare(string name);
         JObject Declare(string name, JObject obj);
        void Reset(string name, JObject obj);
    }
}
