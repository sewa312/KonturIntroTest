using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSalary
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeList list = null;
            try
            {
                list = new EmployeeList("base.xml");
            }
            catch (IOException ex)
            {
                Console.Error.WriteLine("file read error");
                Console.Error.WriteLine(ex);
                Environment.Exit(1);
            }
            catch (FormatException ex)
            {
                Console.Error.WriteLine("file parse error");
                Console.Error.WriteLine(ex);
                Environment.Exit(1);
            }

            Console.WriteLine("Show first 5 elements");
            list.ShowFirst5();
            Console.WriteLine("Show last 3 elements ID");
            list.ShowLast3ID();

            try
            {
                list.SaveTo("exported_base.xml");
            }
            catch (IOException ex)
            {
                Console.Error.WriteLine("file save error");
                Console.Error.WriteLine(ex);
                Environment.Exit(2);
            }


        }
    }
}
