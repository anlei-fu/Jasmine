using Jasmine.Spider.Grammer;

namespace GrammerTest
{

    public abstract class ActionFunctionAdapterBase
    {
        public ActionFunctionAdapterBase(JFunction function)
        {
            _function = function;
        }
        protected JFunction _function { get; }
    }
    public class ActionJFunctionAdapter:ActionFunctionAdapterBase
    {
        public ActionJFunctionAdapter(JFunction function) : base(function)
        {
        }

        public void Excute()
        {
            //_function.Excute(null, null);
        }
    }

    public class ActionFunctionAdpater<T> : ActionFunctionAdapterBase
    {
        public ActionFunctionAdpater(JFunction function) : base(function)
        {
        }

        public void Excute(T para)
        {

        }
    }
    public class ActionFunctionAdpater<T1,T2> : ActionFunctionAdapterBase
    {
        public ActionFunctionAdpater(JFunction function) : base(function)
        {
        }

        public void Excute(T1 para1,T2 para2)
        {

        }
    }

    public class ActionFunctionAdpater<T1, T2,T3> : ActionFunctionAdapterBase
    {
        public ActionFunctionAdpater(JFunction function) : base(function)
        {
        }

        public void Excute(T1 para1, T2 para2,T3 para3)
        {

        }
    }

    public class ActionFunctionAdpater<T1, T2, T3,T4> : ActionFunctionAdapterBase
    {
        public ActionFunctionAdpater(JFunction function) : base(function)
        {
        }

        public void Excute(T1 para1, T2 para2, T3 para3,T4 para4)
        {

        }
    }

    public class ActionFunctionAdpater<T1, T2, T3, T4,T5> : ActionFunctionAdapterBase
    {
        public ActionFunctionAdpater(JFunction function) : base(function)
        {
        }

        public void Excute(T1 para1, T2 para2, T3 para3, T4 para4,T5 para5)
        {

        }
    }


    public class JFunctionFunctionAdapter<T> : ActionFunctionAdapterBase
    {
        public JFunctionFunctionAdapter(JFunction function) : base(function)
        {
        }

        public T Excute()
        {
            return default(T);
        }

    }


    public class JFunctionFunctionAdapter<T,TReturn> : ActionFunctionAdapterBase
    {
        public JFunctionFunctionAdapter(JFunction function) : base(function)
        {
        }

        public TReturn Excute( T para)
        {
            return default(TReturn);
        }

    }


    public class JFunctionFunctionAdapter<T1,T2, TReturn> : ActionFunctionAdapterBase
    {
        public JFunctionFunctionAdapter(JFunction function) : base(function)
        {
        }

        public TReturn Excute(T1 para,T2 para2)
        {
            return default(TReturn);
        }

    }
    public class JFunctionFunctionAdapter<T1,T2,T3, TReturn> : ActionFunctionAdapterBase
    {
        public JFunctionFunctionAdapter(JFunction function) : base(function)
        {
        }

        public TReturn Excute(T1 para1,T2 para2,T3 para3)
        {
            return default(TReturn);
        }

    }
    public class JFunctionFunctionAdapter<T1, T2, T3,T4, TReturn> : ActionFunctionAdapterBase
    {
        public JFunctionFunctionAdapter(JFunction function) : base(function)
        {
        }

        public TReturn Excute(T1 para1, T2 para2, T3 para3,T4 para4)
        {
            return default(TReturn);
        }

    }
    public class JFunctionFunctionAdapter<T1, T2, T3, T4,T5, TReturn> : ActionFunctionAdapterBase
    {
        public JFunctionFunctionAdapter(JFunction function) : base(function)
        {
        }

        public TReturn Excute(T1 para1, T2 para2, T3 para3, T4 para4,T5 para5)
        {
            return default(TReturn);
        }

    }



}
