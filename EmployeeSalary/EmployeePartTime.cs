using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeeSalary
{
    class EmployeePartTime : Employee
    {
        public const string TagName = "partTime";

        private decimal wageRateHour;

        public EmployeePartTime(long id, string lName, string fName, decimal rate) : base(id, lName, fName)
        {
            wageRateHour = rate;
            //this.CalculateSalary();
        }

        public override XElement ExportToXml()
        {
            return new XElement(TagName, 
                new XAttribute("id", ID),
                new XElement("firstName", FirstName),
                new XElement("lastName", LastName),
                new XElement("wageRateHour", wageRateHour));
        }

        protected override decimal CalculateSalary()
        {
            return 20.8m * 8 * wageRateHour;
        }
    }
}
