using System;
using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public class AbstractStat<TItem> : IStat<TItem>
         where TItem : IStatItem
    {
        private int _slowest;
        private int _fatest;

        private DateTime _lastCaculate;


        public int Avarage => throw new NotImplementedException();

        public int Total => throw new NotImplementedException();

        public int Success => throw new NotImplementedException();

        public int Failed => throw new NotImplementedException();

        public int Fatest => throw new NotImplementedException();

        public int Slowest => throw new NotImplementedException();

        public float FaileRate => throw new NotImplementedException();

        public float SuccesRate => throw new NotImplementedException();

        public void Add(TItem item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
