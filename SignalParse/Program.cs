using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalParse
{
    class Program
    {
        static void Main(string[] args)
        {

            int i = -14;
            byte b = (byte)(i & 0x01);
            Console.WriteLine(b);
            Console.ReadLine();
        }
    }
}
