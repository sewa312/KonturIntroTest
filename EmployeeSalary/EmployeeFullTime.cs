using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeeSalary
{
    class EmployeeFullTime : Employee
    {
        public const string TagName = "fullTime";

        private decimal wageRateMounth;

        public EmployeeFullTime(long id, string lName, string fName, decimal rate) : base(id, lName, fName)
        {
            wageRateMounth = rate;
            //this.CalculateSalary();
        }

        public override XElement ExportToXml()
        {
            return new XElement(TagName,
                new XAttribute("id", ID),
                new XElement("firstName", FirstName),
                new XElement("lastName", LastName),
                new XElement("wageRateMounth", wageRateMounth));
        }

        protected override decimal CalculateSalary()
        {
            return wageRateMounth;
        }
    }
}
