using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeeSalary
{
    abstract class Employee
    {
        public long ID { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public decimal Salary { get { return this.CalculateSalary(); } }

        public Employee(long id, string lName, string fName)
        {
            ID = id;
            FirstName = fName;
            LastName = lName;
        }

        protected abstract decimal CalculateSalary();

        public abstract XElement ExportToXml();
    }
}
