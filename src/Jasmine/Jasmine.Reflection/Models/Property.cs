﻿using System.Reflection;

namespace Jasmine.Reflection.Models
{
    public class Property : Field
    {
        public new PropertyInfo RelatedInfo { get; set; }
        public bool HasGetter => Getter != null;
        public bool HasSetter => Setter != null;
    }
}
