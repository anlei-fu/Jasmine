namespace Jasmine.Orm.Model
{
    public  struct Expression
    {
        public Expression(object left,object right,string symbol)
        {
            Left = left;
            Right = right;
            Symbol = symbol;
        }
        public object Left { get; set; }
        public object Right { get; set; }
        public string Symbol { get; set; }

        public override string ToString()
        {
            return $"{Left} {Symbol} {Right}";
        }

    }
}
