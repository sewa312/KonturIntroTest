using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalParse
{
    class Program
    {
        static void Main(string[] args)
        {
            ///GAMA_stop_word.scc429

            Signal signal = new Signal(File.Open(args[0], FileMode.Open));
            

            Console.WriteLine("| Время       | Адрес | Данные                     | Матрица | Контроль |");
            foreach (Block block in signal.Read())
            {
                string data = Convert.ToString(block.Data, 2).PadLeft(21, '0');
                var outputData = new StringBuilder()
                    .Append(data.Substring(0, 1));
                for (int i = 1; i < 21; i += 4)
                {
                    outputData.Append(' ').Append(data.Substring(i, 4));
                }

                Console.WriteLine("|{0,12:F2} |  {1} | {2} |       {3} |        {4} |", 
                    block.Time / 100.0,
                    Convert.ToString(block.Address, 8).PadLeft(4, '0'),
                    outputData,
                    block.Matrix,
                    block.Control);
                Console.Write("Press Q to exit or any key to continue... ");
                if (Console.ReadKey().KeyChar == 'q')
                    break;
                Console.CursorLeft = 0;
            }

            Console.ReadLine();
        }
    }
}
