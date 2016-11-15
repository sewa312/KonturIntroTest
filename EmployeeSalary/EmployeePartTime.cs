using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSalary
{
    class EmployeePartTime : Employee
    {
        private decimal wageRateHour;

        public EmployeePartTime(long id, string lName, string fName, decimal rate) : base(id, lName, fName)
        {
            wageRateHour = rate;
            //this.CalculateSalary();
        }

        protected override decimal CalculateSalary()
        {
            return 20.8m * 8 * wageRateHour;
        }
    }
}
