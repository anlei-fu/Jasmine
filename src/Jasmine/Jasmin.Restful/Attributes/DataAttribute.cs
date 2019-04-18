﻿using System;

namespace Jasmine.Restful.Attributes
{
    public class DataAttribute:Attribute
    {
        public DataAttribute()
        {

        }
        public DataAttribute(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
