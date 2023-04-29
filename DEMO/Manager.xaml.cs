using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DEMO
{
	/// <summary>
	/// Логика взаимодействия для Manager.xaml
	/// </summary>
	public partial class Manager : Window
	{
		public Manager()
		{
			InitializeComponent();
		}

		private void exit_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mw = new MainWindow();
			mw.Show();
			this.Close();
		}

		private void tovarki_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{

        }

		private void tovarki_Selected(object sender, RoutedEventArgs e)
		{

        }

		private void tovarki_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{

        }

		private void tovarki_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

        }

		private void newOrder_Click(object sender, RoutedEventArgs e)
		{

        }
    }
}
