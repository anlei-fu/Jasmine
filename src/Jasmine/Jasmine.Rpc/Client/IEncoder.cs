using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Rpc.Client
{
  public  interface IEncoder
    {
        byte[] Encode(byte[] source);
    }
}
