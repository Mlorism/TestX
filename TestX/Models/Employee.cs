using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestX.Models
{
	public class Employee
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }	
		public double Gross_Salary { get; set; }
		public double Net_Salary { get; set; }
		public DateTime Birth_Day { get; set; }
	}
}
