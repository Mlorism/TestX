using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestX.Models
{
	public class Product
	{		
		public string Product_Name { get; set; }
		public string Code { get; set; }
		public int Total_DC_Qty { get; set; }
		public int Reserved_Qty { get; set; }
		public int Free_Qty { get; set; }
	}
}
