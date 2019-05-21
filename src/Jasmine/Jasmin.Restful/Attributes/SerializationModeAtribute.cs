using Jasmine.Serialization;
using System;

namespace Jasmine.Restful.Attributes
{
    public  class SerializationModeAtribute:Attribute
    {
        public SerializationModeAtribute(SerializeMode serializeMode)
        {
            SerializeMode = serializeMode;
        }
        public SerializeMode SerializeMode { get; }
    }
}
