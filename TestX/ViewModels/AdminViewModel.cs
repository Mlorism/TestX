using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TestX.Enumerators;
using TestX.Interfaces;
using TestX.Models;
using TestX.Resources;

namespace TestX.ViewModels
{
	class AdminViewModel : BaseViewModel
	{
		#region Commands		
		public RelayCommand ChooseCurrentUserCommand {get; private set;}
		public RelayCommand ClearDataCommand { get; private set; }
		public RelayCommand SaveUserCommand { get; private set; }
		public RelayCommand DeleteUserCommand { get; private set; }
		public RelayCommand SignInUserCommand { get; private set; }
		#endregion

		#region Properties		
		public ObservableCollection<User> Users 
		{ 
			get { return VirtualDataBase.UsersDB; }
			set 
			{
				Users = value;				
			}
		} 
		private User _currentUser { get; set; }
		public User CurrentUser
		{
			get { return _currentUser; }
			set { 
				_currentUser = value;
				OnPropertyChange("CurrentUser");
				OnPropertyChange("Id");
				OnPropertyChange("Login");
				OnPropertyChange("Password");
				OnPropertyChange("Type");						
			}
		}
		public int Id 
		{ 
			get { return CurrentUser.Id; }			
		}
		public string Login { 
			get { return CurrentUser.Login; } 
			set 
			{ 
				if (CurrentUser.Login != value)
				{
					CurrentUser.Login = value;
					OnPropertyChange("Login");
				}
			} 
		}
		public string Password
		{
			get { return CurrentUser.Password; }
			set
			{
				if (CurrentUser.Password != value)
				{
					CurrentUser.Password = value;
					OnPropertyChange("Password");
				}
			}
		}
		public UserType Type 
		{ 
			get { return CurrentUser.Type; }
			set
			{
				if (CurrentUser.Type != value)
				{
					CurrentUser.Type = value;
					OnPropertyChange("Type");
				}
			}
		}			
		#endregion

		#region Constructor
		public AdminViewModel()
		{
			TabName = "Admin";			

			CurrentUser = Users[0];

			ChooseCurrentUserCommand = new RelayCommand(ChooseCurrentUser, null);
			ClearDataCommand = new RelayCommand(ClearData, null);
			SaveUserCommand = new RelayCommand(SaveUser, SaveUserValidation);
			DeleteUserCommand = new RelayCommand(DeleteUser, DeleteUserValidation);
			SignInUserCommand = new RelayCommand(SignInUser, SignInUserValidation);
		}
		#endregion

		#region Methods
		
		public void ChooseCurrentUser(Object user)
		{
			CurrentUser = new User();
			CurrentUser.Id = ((User)user).Id;
			CurrentUser.Login = ((User)user).Login;
			CurrentUser.Password = ((User)user).Password;
			CurrentUser.Type = ((User)user).Type;
			OnPropertyChange("Id");
			OnPropertyChange("Login");
			OnPropertyChange("Password");
			OnPropertyChange("Type");
		}

		public void ClearData(object x)
		{
			CurrentUser = new User
			{
				Id = (Users.Last().Id + 1)
			};
			OnPropertyChange("CurrentUser");		
		}

		public void SaveUser(object x)
		{
			if (CurrentUser.Id == (Users.Last().Id+1))
			{
				Users.Add(CurrentUser);
			}

			else
			{
				Users[CurrentUser.Id].Login = CurrentUser.Login;
				Users[CurrentUser.Id].Password = CurrentUser.Password;
				Users[CurrentUser.Id].Type = CurrentUser.Type;
				CollectionViewSource.GetDefaultView(Users).Refresh();

				//OnPropertyChange("Users"); if DataGrid ItemsSource IsAsync=true, list will also refresh (you must use OnPropertyChanged)
				//but will blink (whole), with CollectionViewSource above change look more natural as affects only changed data
			}
		}

		public void DeleteUser(object x)
		{
			if (Users.Count > 0)
			{
				User tempUser = Users.FirstOrDefault(u => u.Id == CurrentUser.Id);				
				Users.Remove(tempUser);				
			}			
		}

		public void SignInUser(object x)
		{
			User tempUser = Users.FirstOrDefault(u => u.Id == CurrentUser.Id);
			MainWindowViewModel.LoggedInUser = tempUser;
		}

		public bool SignInUserValidation(object x)
		{
			if (MainWindowViewModel.LoggedInUser.Id == CurrentUser.Id)
				return false;
			if (Users.FirstOrDefault(u => u.Id == CurrentUser.Id) == null)
				return false;

			else return true;
		}

		public bool SaveUserValidation(object x)
		{
			if (CurrentUser.Login != null && CurrentUser.Password != null && CurrentUser.Type != UserType.none)
			{
				if (CurrentUser.Login != "" && CurrentUser.Password !="")
					return true;
				else return false;
			}
				
			else return false;
		}

		public bool DeleteUserValidation (object x)
		{
			if (MainWindowViewModel.LoggedInUser.Id == CurrentUser.Id)
				return false;
			if (Users.FirstOrDefault(u => u.Id == CurrentUser.Id) == null)
				return false;

			else return true;
		}

		

		#endregion

	}
}
