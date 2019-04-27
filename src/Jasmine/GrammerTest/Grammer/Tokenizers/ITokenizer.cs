using System.Collections.Generic;

namespace GrammerTest.Grammer.Tokenizers
{
    public interface ITokenizer<TToken>
    {
        List<TToken> Tokenize();
    }
}
