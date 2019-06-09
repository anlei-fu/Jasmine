﻿using Jasmine.Orm.Model;
using System;
using System.Collections.Generic;

namespace Jasmine.Orm.Interfaces
{
    public interface IQueryResultResolver
    {
        /// <summary>
        /// resolve one row to an object by given <see cref="QueryResultContext"/> and destination type
        /// </summary>
        /// <param name="context"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        IEnumerable<T> Resolve<T>(QueryResultContext context);
    }
}
