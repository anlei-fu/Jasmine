using System;

namespace Jasmine.Serialization.Attributes
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
