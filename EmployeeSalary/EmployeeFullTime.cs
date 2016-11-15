using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSalary
{
    class EmployeeFullTime : Employee
    {
        private decimal wageRateMounth;

        public EmployeeFullTime(long id, string lName, string fName, decimal rate) : base(id, lName, fName)
        {
            wageRateMounth = rate;
            //this.CalculateSalary();
        }

        protected override decimal CalculateSalary()
        {
            return wageRateMounth;
        }
    }
}
