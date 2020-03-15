using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestX.Interfaces;
using TestX.Models;
using TestX.Resources;

namespace TestX.ViewModels
{
	class EmployeeViewModel : BaseViewModel
	{
		#region Commands
		public RelayCommand NextUserCommand { get; private set; }
		public RelayCommand PreviousUserCommand { get; private set; }
		public RelayCommand SalaryCommand { get; private set; }
		public RelayCommand ClearDataCommand { get; set; }
		public RelayCommand CreateNewUserCommand { get; set; }
		#endregion

		#region Properties
		private ObservableCollection<Employee> employees;
		private Employee _currentEmployee
		{
			get; set;
		}
		public Employee CurrentEmployee
		{
			get
			{
				return _currentEmployee;
			}
			set
			{
				_currentEmployee = value;
				OnPropertyChange("Name");
				OnPropertyChange("Surname");
				OnPropertyChange("Full_Name");
				OnPropertyChange("Gross_Salary");
				OnPropertyChange("Net_Salary");
				OnPropertyChange("Age");
			}
		}
		public string Name
		{
			get { return CurrentEmployee.Name; }
			set
			{
				if (CurrentEmployee.Name != value)
				{
					CurrentEmployee.Name = value;
					OnPropertyChange("Name");
					OnPropertyChange("Full_Name");
				}
			}
		}
		public string Surname
		{
			get { return CurrentEmployee.Surname; }
			set
			{
				if (CurrentEmployee.Surname != value)
				{
					CurrentEmployee.Surname = value;
					OnPropertyChange("Surname");
					OnPropertyChange("Full_Name");
				}
			}
		}
		public string Full_Name
		{
			get { return $"{CurrentEmployee.Name} {CurrentEmployee.Surname}"; }
		}
		public double Gross_Salary
		{
			get { return CurrentEmployee.Gross_Salary; }
			set
			{
				if (CurrentEmployee.Gross_Salary != value)
				{
					CurrentEmployee.Gross_Salary = value;
					OnPropertyChange("Gross_Salary");
				}
			}
		}
		public double Net_Salary
		{
			get { return CurrentEmployee.Net_Salary; }
			set
			{
				CalculateSalary(CurrentEmployee.Gross_Salary);
			}
		}
		public int Age
		{
			get
			{
				DateTime today = DateTime.Today;
				int age = today.Year - CurrentEmployee.Birth_Day.Year;
				if (CurrentEmployee.Birth_Day > today.AddYears(-age)) age--;
				return -age;
			}
		}

		public ObservableCollection<Employee> VirutalDataBase { get; }
		#endregion

		#region Variables
		private bool SaveUserFlag = false;
		#endregion

		#region Constructor
		public EmployeeViewModel()
		{
			TabName = "Employees";

			employees = VirtualDataBase.EmployeesDB;

			CurrentEmployee = employees[0];

			NextUserCommand = new RelayCommand(NextUser, null);
			PreviousUserCommand = new RelayCommand(PreviousUser, null);
			SalaryCommand = new RelayCommand(CalculateSalary, null);
			ClearDataCommand = new RelayCommand(ClearData, null);
			CreateNewUserCommand = new RelayCommand(SaveNewUser, CanSaveUser);
		} // ProductViewModel()
		#endregion

		#region Button methods
		public void NextUser(object x)
		{
			if (CurrentEmployee.Id + 1 == employees.Count)
				CurrentEmployee = employees[0];
			else CurrentEmployee = employees[CurrentEmployee.Id + 1];
			OnPropertyChange("user");
		}
		public void PreviousUser(object x)
		{
			if (CurrentEmployee.Id == 0)
				CurrentEmployee = employees[employees.Count - 1];
			else CurrentEmployee = employees[CurrentEmployee.Id - 1];
			OnPropertyChange("user");
		}
		public void CalculateSalary(object Gross_Salary)
		{
			CurrentEmployee.Net_Salary = (double)Gross_Salary * 0.72;
			OnPropertyChange("Net_Salary");
		}
		public void ClearData(object x)
		{
			CurrentEmployee = new Employee();
			SaveUserFlag = true;
			OnPropertyChange("user");
		}
		public void SaveNewUser(object x)
		{
			Random rnd = new Random();
			CurrentEmployee.Id = employees.Count();
			CurrentEmployee.Name = Name;
			CurrentEmployee.Surname = Surname;
			CurrentEmployee.Gross_Salary = Gross_Salary;
			CurrentEmployee.Birth_Day = DateTime.Today.AddYears(rnd.Next(19, 65));
			employees.Add(CurrentEmployee);
			SaveUserFlag = false;
			OnPropertyChange("Age");
		}
		#endregion

		#region Validation methods		
		public bool CanSaveUser(object x)
		{
			if (SaveUserFlag == true)
			{
				if (CurrentEmployee.Name == null || CurrentEmployee.Surname == null || CurrentEmployee.Gross_Salary <= 0)
					return false;
				else return true;
			}

			else return false;
		}
		#endregion
	}
}
