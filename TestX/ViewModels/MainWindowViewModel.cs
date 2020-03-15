using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TestX.Enumerators;
using TestX.Interfaces;
using TestX.Models;
using TestX.Resources;
using TestX.Views;

namespace TestX.ViewModels
{
	class MainWindowViewModel : BaseViewModel
	{		
		private static User loggedInUser = VirtualDataBase.UsersDB[4];		
		public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;

		static ObservableCollection<TabsPermissions> UserTabPermissions;
		public static ICollection<ITab> Tabs { get; set; }
		//Tabs contains ViewModels to be loaded according to user's permissions		

		public RelayCommand SignOutCommand { get; private set; }
		public static User LoggedInUser
		{
			get { return loggedInUser; }
			set 
			{ 
				loggedInUser = value;
				SignInNewUser();
				RaiseStaticPropertyChanged("LoggedInUserId");
			}
		}
		public static int LoggedInUserId 
		{
			get {return loggedInUser.Id ; }			
		}

		private static readonly Dictionary<TabsPermissions, object> TabsContainter = new Dictionary<TabsPermissions, object>
		{
			{TabsPermissions.Admin, new AdminViewModel() },
			{TabsPermissions.Employee, new EmployeeViewModel() },
			{TabsPermissions.Messages, new MessageViewModel() },
			{TabsPermissions.Product, new ProductViewModel() },
			{TabsPermissions.Options, new OptionsViewModel() }
		}; // Dictionary contains dependence between TabPermissions and ViewModels
		public MainWindowViewModel()	
		{
			SignInNewUser();
			SignOutCommand = new RelayCommand(SignOut, null);
		}		

		#region Methods
		public static ObservableCollection<TabsPermissions> CalculatePermissions(UserType type)
		{
			ObservableCollection<TabsPermissions> permissions = new ObservableCollection<TabsPermissions>();

			if (type == UserType.Admin)
			{
				permissions.Add(TabsPermissions.Admin);
			}

			if (type == UserType.Accountant || type == UserType.Admin)
			{
				permissions.Add(TabsPermissions.Employee);
			}

			if (type == UserType.Planer || type == UserType.Admin)
			{
				permissions.Add(TabsPermissions.Product);
			}

			permissions.Add(TabsPermissions.Messages);

			permissions.Add(TabsPermissions.Options);

			return permissions;
		} 
		// CalculatePermissions() assigns permissions depending on the user type					
		
		public static void RaiseStaticPropertyChanged (string PropertyName)
		{
			StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(PropertyName));
		}

		public static void SignInNewUser()
		{
			if (UserTabPermissions != null)
			UserTabPermissions.Clear();

			UserTabPermissions = CalculatePermissions(loggedInUser.Type);
			Tabs = new ObservableCollection<ITab>();

			foreach (var x in UserTabPermissions)
			{
				Tabs.Add((ITab)TabsContainter[x]);
			}

			RaiseStaticPropertyChanged("Tabs");
		}

		public void SignOut(object x)
		{
			LoggedInUser = VirtualDataBase.UsersDB.First();			
			RaiseStaticPropertyChanged("LoggedInUser");
		}
		#endregion
	}
}

