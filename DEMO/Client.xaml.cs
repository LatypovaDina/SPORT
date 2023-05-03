using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
<<<<<<< HEAD
=======
using System.Reflection.Emit;
>>>>>>> Добавьте файлы проекта.
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
	/// Логика взаимодействия для Client.xaml
	/// </summary>
	public partial class Client : Window
	{
		public user24Entities ue = new user24Entities();
		public Client()
		{
			InitializeComponent();
<<<<<<< HEAD
			ue = new user24Entities();
			ue.Product.Load(); // загружаем данные
			tovarki.DataContext = ue.Product.ToList(); // устанавливаем привязку к кэшу
			vidacha.ItemsSource = ue.PickupPoint.ToList();

			
		}

=======
			ue.Product.Load(); // загружаем данные
			tovarki.DataContext = ue.Product.ToList(); // устанавливаем привязку к кэшу
			vidacha.ItemsSource = Convert.ToString(ue.PickupPoint.ToList());
			kolVo.Text = Convert.ToString(tovarki.Items.Count);
			

		}
		private void viborka()
		{
			if (Convert.ToInt32(skid.Text) > 15)
			{
				name.Background = Brushes.DarkRed;
			}
		}
>>>>>>> Добавьте файлы проекта.
		private void exit_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mw = new MainWindow();
			mw.Show();
			this.Close();
		}

		private void tovarki_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void tovarki_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{

		}

		private void tovarki_Selected(object sender, RoutedEventArgs e)
		{

		}

		private void tovarki_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Ну типа че то оформили");
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			
		}

		private void show_Click(object sender, RoutedEventArgs e)
		{
			
		}

		private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
		{
			tovarki.DataContext = ue.Product.Where(x => x.ProductMaxDiscountAmount < 10).ToList();
<<<<<<< HEAD
=======
			kolVo.Text = tovarki.Items.Count.ToString();
>>>>>>> Добавьте файлы проекта.
		}

		private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
		{
			tovarki.DataContext = ue.Product.Where(x => x.ProductMaxDiscountAmount < 15 && x.ProductMaxDiscountAmount > 10).ToList();
<<<<<<< HEAD
=======
			kolVo.Text = tovarki.Items.Count.ToString();
>>>>>>> Добавьте файлы проекта.
		}

		private void ComboBoxItem_Selected_2(object sender, RoutedEventArgs e)
		{
			tovarki.DataContext = ue.Product.Where(x => x.ProductMaxDiscountAmount > 15).ToList();
<<<<<<< HEAD
=======
			kolVo.Text = tovarki.Items.Count.ToString();
		}

		private void ComboBoxItem_Selected_3(object sender, RoutedEventArgs e)
		{
			tovarki.DataContext = ue.Product.ToList();
			kolVo.Text = tovarki.Items.Count.ToString();
		}
		

		private void search_TextChanged(object sender, TextChangedEventArgs e)
		{
				tovarki.DataContext = ue.Product.Where(x => x.ProductName == search.Text).ToList();
			kolVo.Text = tovarki.Items.Count.ToString();
			
		}

		private void name_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (tovarki.SelectedItem == (ue.Product.Where(x => x.ProductMaxDiscountAmount > 15)))
			{
				name.Background = Brushes.GreenYellow;
			}
			else
			{ name.Background = Brushes.White; }
>>>>>>> Добавьте файлы проекта.
		}
	}
}
