using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestX.Resources;

namespace TestX.ViewModels
{
	public class MessageViewModel : BaseViewModel
	{ 
		public ObservableCollection<string> MyMessages { get; private set; }

		public RelayCommand MessageBoxCommand { get; set; }
		public RelayCommand ConsoleCommand { get; set; }

		
		public MessageViewModel()
		{
			TabName = "Messages";

			MyMessages = VirtualDataBase.MesseagesDB;

			MessageBoxCommand = new RelayCommand(DisplayInMessageBox, MessageBoxCanUse);
			ConsoleCommand = new RelayCommand(DisplayInMessageLog, MessageLogCanUse);
		} // MesseageViewModel()

		public void DisplayInMessageBox(object message)
		{
			MessageBox.Show($"MessageBox: {message.ToString()}");
		}

		public void DisplayInMessageLog(object message)
		{
			MessageBox.Show($"Message Log: {message.ToString()}");
		}

		public bool MessageBoxCanUse(object message)
		{
			if (message.ToString().Contains("console"))
			//if ((string)message)
			{
				return false;
			}
			else return true;
		}

		public bool MessageLogCanUse(object message)
		{
			if (message.ToString().Contains("box"))
			{
				return false;
			}
			else return true;
		}

	}
}

