namespace GrammerTest.Grammer.Tokenizers
{
    public  interface ISequenceReader<T>
    {
        int Total { get; }
        int Readed { get; }
        int Remain { get; }
        T Current();
        T Next(int step = 1);
        T Previouce(int step = 1);
        bool HasNext(int step = 1);
        bool HasPreviouce(int step = 1);

        T Forward(int step=1);
        T Last(int step = 1);


    }
}
