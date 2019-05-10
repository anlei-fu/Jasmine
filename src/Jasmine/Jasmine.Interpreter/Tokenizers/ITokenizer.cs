using System.Collections.Generic;

namespace Jasmine.Interpreter.Tokenizers
{
    public interface ITokenizer<TToken>
    {
        List<TToken> Tokenize(string input);
    }
}
