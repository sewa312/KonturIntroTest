using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeeSalary
{
    class EmployeeList
    {
        private List<Employee> list = new List<Employee>();

        public EmployeeList(string fileName)
        {
            ParseXml(fileName);
        }

        protected void ParseXml(string fileName)
        {
            XDocument xdoc = XDocument.Load(fileName);
            foreach (XElement employee in xdoc.Element("employees").Elements())
            {
                XElement firstName = employee.Element("firstName");
                XElement lastName = employee.Element("lastName");
                XAttribute idAttribute = employee.Attribute("id");
                
                if (firstName == null || lastName == null || idAttribute == null)
                {
                    throw new FormatException("employee should have name and id");
                }
                long id; 
                if (!long.TryParse(idAttribute.Value, out id))
                    throw new FormatException("invalid id");
               
                Employee parsedEmployee;
                decimal rate;
                if (employee.Name == EmployeeFullTime.TagName)
                {
                    XElement wageRateMounth = employee.Element("wageRateMounth");
                    if (!decimal.TryParse(wageRateMounth.Value, out rate))
                        throw new FormatException("invalid rate");

                    parsedEmployee = new EmployeeFullTime(id, lastName.Value, firstName.Value, rate);
                }
                else if (employee.Name == EmployeePartTime.TagName)
                {
                    XElement wageRateHour = employee.Element("wageRateHour");
                    if (!decimal.TryParse(wageRateHour.Value, out rate))
                        throw new FormatException("invalid rate");

                    parsedEmployee = new EmployeePartTime(id, lastName.Value, firstName.Value, rate);
                }
                else
                {
                    throw new FormatException("unknown employee type");
                }

                list.Add(parsedEmployee);
            }
        }

        public IEnumerable<Employee> GetSortedBase()
        {
            return from employee in list
                   orderby employee.Salary descending, employee.LastName, employee.FirstName
                   select employee;
        }

        public void ShowFirst5()
        {
            foreach (Employee employee in GetSortedBase().Take(5))
            {
                Console.WriteLine("ID {0} name {1} {2} salary = {3}", employee.ID, employee.FirstName, employee.LastName, employee.Salary);
            }
        }

        public void ShowLast3ID()
        {
            var sortedList = GetSortedBase().ToList();
            foreach (Employee employee in sortedList.Skip(sortedList.Count - 3))
            {
                Console.WriteLine("ID {0}", employee.ID);
            }
        }

        public void SaveTo(string fileName)
        {
            new XDocument(new XElement("employees", 
                from employee in list
                select employee.ExportToXml())).Save(fileName);
        }
    }
}
