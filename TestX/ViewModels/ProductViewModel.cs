using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestX.Models;

namespace TestX.ViewModels
{
	class ProductViewModel : BaseViewModel
	{
		private Product product;

		#region Commands		
		public RelayCommand ReserveOneCommand { get; private set; }
		public RelayCommand ReserveTenCommand { get; private set; }
		public RelayCommand ReserveAllCommand { get; private set; }
		public RelayCommand FreeOneCommand { get; private set; }
		public RelayCommand FreeTenCommand { get; private set; }
		public RelayCommand FreeAllCommand { get; private set; }

		#endregion
		#region Properties
		public string Product_Name
		{
			get { return product.Product_Name; }
			set
			{
				if (product.Product_Name != value)
				{
					product.Product_Name = value;
					OnPropertyChange("Product_Name");
					OnPropertyChange("Full_Product_Name");
				}
			}
		}
		public string Code
		{
			get { return product.Code; }
			set
			{
				if (product.Code != value)
				{
					product.Code = value;
					OnPropertyChange("Code");
					OnPropertyChange("Full_Product_Name");
				}
			}
		}
		public string Full_Product_Name
		{
			get { return $"{product.Code} {product.Product_Name}"; }
		}
		public int Total_DC_Qty
		{
			get { return product.Total_DC_Qty; }
			set
			{
				if (product.Total_DC_Qty != value)
				{
					product.Total_DC_Qty = value;
					OnPropertyChange("Total_DC_Qty");
				}
			}
		}
		public int Reserved_Qty
		{
			get { return product.Reserved_Qty; }
			set
			{
				if (product.Reserved_Qty != value)
				{
					product.Reserved_Qty = value;
					OnPropertyChange("Reserved_Qty");
					OnPropertyChange("Free_Qty");
				}
			}
		}
		public int Free_Qty
		{
			get { return product.Total_DC_Qty - product.Reserved_Qty; }
			set
			{
				product.Free_Qty = product.Total_DC_Qty - product.Reserved_Qty;
				OnPropertyChange("Free_Qty");
			}
		}
		#endregion		
		public ProductViewModel()
		{
			TabName = "Products";
			product = new Product
			{
				Product_Name = "Kubek basic",
				Code = "266039",
				Total_DC_Qty = 500,
				Reserved_Qty = 123,
				Free_Qty = 500 - 123
			};
			
			ReserveOneCommand = new RelayCommand(ReserveOne, null);
			ReserveTenCommand = new RelayCommand(ReserveTen, null);
			ReserveAllCommand = new RelayCommand(ReserveAll, null);
			FreeOneCommand = new RelayCommand(FreeOne, null);
			FreeTenCommand = new RelayCommand(FreeTen, null);
			FreeAllCommand = new RelayCommand(FreeAll, null);
		} // ProductViewModel()
		#region Button methods
		public void ReserveAll(object x)
		{
			product.Reserved_Qty = product.Total_DC_Qty;
			product.Free_Qty = 0;
			OnPropertyChange("Reserved_Qty");
			OnPropertyChange("Free_Qty");
		}

		public void ReserveOne(object x)
		{
			if (product.Free_Qty > 0)
			{
				product.Reserved_Qty += 1;
				product.Free_Qty -= 1;
				OnPropertyChange("Reserved_Qty");
				OnPropertyChange("Free_Qty");
			}
		}
		public void ReserveTen(object x)
		{
			if (product.Free_Qty >= 10)
			{
				product.Reserved_Qty += 10;
				product.Free_Qty -= 10;
				OnPropertyChange("Reserved_Qty");
				OnPropertyChange("Free_Qty");
			}
		}
		public void FreeOne(object x)
		{
			if (product.Reserved_Qty > 0)
			{
				product.Reserved_Qty -= 1;
				product.Free_Qty += 1;
				OnPropertyChange("Reserved_Qty");
				OnPropertyChange("Free_Qty");
			}
		}
		public void FreeTen(object x)
		{
			if (product.Reserved_Qty >= 10)
			{
				product.Reserved_Qty -= 10;
				product.Free_Qty += 10;
				OnPropertyChange("Reserved_Qty");
				OnPropertyChange("Free_Qty");
			}
		}
		public void FreeAll(object x)
		{
			product.Reserved_Qty = 0;
			product.Free_Qty = product.Total_DC_Qty;
			OnPropertyChange("Reserved_Qty");
			OnPropertyChange("Free_Qty");
		}
		#endregion		
	}
}
