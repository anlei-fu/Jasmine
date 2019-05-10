using System.Collections.Generic;

namespace Jasmine.Interpreter.Tokenizers
{
    public  interface ISequenceReader<T>
    {
        T[] Sequence { get; }
        /// <summary>
        ///  content total length
        /// </summary>
        int Total { get; }
        /// <summary>
        /// current has read
        /// </summary>
        int Readed { get; }
        /// <summary>
        /// remain length to read
        /// </summary>
        int Remain { get; }
        /// <summary>
        /// current item
        /// </summary>
        /// <returns></returns>
        T Current();
        /// <summary>
        /// move to next n
        /// </summary>
        /// <param name="step"></param>
        void Next(int step = 1);
        /// <summary>
        /// back to previous n
        /// </summary>
        /// <param name="step"></param>
        void Back(int step = 1);
        /// <summary>
        /// has next n
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        bool HasNext(int step = 1);
        /// <summary>
        /// has previous n
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        bool HasPrevious(int step = 1);
        /// <summary>
        /// has forwards n,before call id,should do <see cref="HasNext(int)"/> check
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        T Forwards(int step=1);
        /// <summary>
        /// has last n , before call it ,should do <see cref="HasPrevious(int)"/> check
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        T Last(int step = 1);
        /// <summary>
        /// reset content and parameters
        /// </summary>
        /// <param name="input"></param>
        void Reset(IEnumerable<T> input);


    }
}
