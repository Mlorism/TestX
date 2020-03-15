using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestX.Enumerators;
using TestX.Models;
using TestX.ViewModels;

namespace TestX.Resources
{
	public static class VirtualDataBase
	{
		#region Databases
		public static ObservableCollection<User> UsersDB = new ObservableCollection<User>()
		{
			new User(){Id = 0, Login="Tommy", Password="alamakota", Type = UserType.Admin},
			new User(){Id = 1, Login="Emma", Password="emmajestsuper", Type = UserType.Accountant},
			new User(){Id = 2, Login="Gregory", Password="lubieplacki", Type = UserType.Planer},
			new User(){Id = 3, Login="Eliza", Password="kopiacroze", Type = UserType.Planer},
			new User(){Id = 4, Login="Szymon", Password="papugitotezludzie", Type = UserType.Admin}
		};

		public static ObservableCollection<Employee> EmployeesDB = new ObservableCollection<Employee>()
		{
			new Employee(){ Id = 0, Name = "Krzysztof", Surname="Kowalski", Birth_Day = DateTime.Today.AddYears(58), Gross_Salary = 6500},
			new Employee(){ Id = 1, Name = "Anna", Surname="Adamska", Birth_Day = DateTime.Today.AddYears(22), Gross_Salary = 3000},
			new Employee(){ Id = 2, Name = "Jakub", Surname="Bielecki", Birth_Day = DateTime.Today.AddYears(33), Gross_Salary = 4600},
			new Employee(){ Id = 3, Name = "Jerzy", Surname="Wal", Birth_Day = DateTime.Today.AddYears(37), Gross_Salary = 3900},
			new Employee(){ Id = 4, Name = "Marvin", Surname="Nowak", Birth_Day = DateTime.Today.AddYears(26), Gross_Salary = 5500}
		};

		public static ObservableCollection<string> MesseagesDB = new ObservableCollection<string>()
		{
			"Hello World!",
			"I love cookies",
			"Im message box!",
			"Im console!",
			"Give me some love"
		};


		#endregion
	}
}
