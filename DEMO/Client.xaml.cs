using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
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
			tovarki.ItemsSource = ue.Product.ToList(); // устанавливаем привязку к кэшу
			vidacha.ItemsSource = ue.PickupPoint.ToList();
			all.Text = ue.Product.Count().ToString();
			kolVo.Text = ue.Product.Count().ToString();
		}
	
		private void viborka()
		{
			if (Convert.ToInt32(skid.Text) > 15)
			{
				name.Background = Brushes.DarkRed;
			}
		}

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
			kolVo.Text = ue.Product.Where(x => x.ProductMaxDiscountAmount < 10).Count().ToString();
		}

		private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
		{
			tovarki.DataContext = ue.Product.Where(x => x.ProductMaxDiscountAmount < 15 && x.ProductMaxDiscountAmount > 10).ToList();
			kolVo.Text = ue.Product.Where(x => x.ProductMaxDiscountAmount < 15 && x.ProductMaxDiscountAmount > 10).Count().ToString();
		}

		private void ComboBoxItem_Selected_2(object sender, RoutedEventArgs e)
		{
			tovarki.DataContext = ue.Product.Where(x => x.ProductMaxDiscountAmount > 15).ToList();
			kolVo.Text = ue.Product.Where(x => x.ProductMaxDiscountAmount > 15).Count().ToString();
		}

		private void ComboBoxItem_Selected_3(object sender, RoutedEventArgs e)
		{
			tovarki.DataContext = ue.Product.ToList();
			kolVo.Text = ue.Product.Count().ToString();
		}
		
		private void search_TextChanged(object sender, TextChangedEventArgs e)
		{
			tovarki.DataContext = ue.Product.Where(x => x.ProductName == search.Text).ToList();
			kolVo.Text = ue.Product.Where(x => x.ProductName == search.Text).Count().ToString();
		}

		private void name_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (Convert.ToInt16(skid.Text) > 15)
			{
				name.Background = Brushes.GreenYellow;
			}
			else
			{ name.Background = Brushes.White; }
		}

		private void MenuItem_Click(object sender, RoutedEventArgs e)
		{
			if (vidacha.SelectedItem != null)
			{
				using (user24Entities db = new user24Entities())
				{
					int idd = (from dt in db.User where dt.UserSurname + dt.UserName + dt.UserPatronymic == FIO.Content.ToString() select dt.UserID).FirstOrDefault();
					int oid = Convert.ToInt32((from dt in db.Order where dt.UserID == idd select dt.UserID).FirstOrDefault());
					Random rnd = new Random();
					int i = rnd.Next(0, 3000);
					int pid = (from ut in db.PickupPoint where ut.Address == vidacha.Text select ut.PickupPointID).FirstOrDefault();
					if (idd != oid)
					{
						Order order = new Order { OrderStatusID = 1, PickupPointID = pid, OrderCreateDate = DateTime.Now, OrderDeliveryDate = DateTime.UtcNow.AddDays(6), UserID = idd, OrderGetCode = i };
						db.Order.Add(order);
						int a = order.OrderID;
						db.SaveChanges();   // сохранение изменений
						zakaz.Visibility = Visibility.Visible;

						OrderProduct op = new OrderProduct { OrderID = a, ProductID = Convert.ToInt32(id.Text),  Count = 1 };
						db.OrderProduct.Add(op);
						db.SaveChanges();
					}
					else
					{
						int ord = (from dt in db.Order where dt.UserID == idd select dt.OrderID).FirstOrDefault();
						OrderProduct op = new OrderProduct { OrderID = ord, ProductID = Convert.ToInt32(id.Text), Count = 1 };
						db.OrderProduct.Add(op);
						db.SaveChanges();
					}


				}
				MessageBox.Show("В заказ добавлено");
			}
			else { MessageBox.Show("Вы забыли выбрать пункт выдачи"); }
			
		}
		private void MenuItem_ContextMenuOpening(object sender, ContextMenuEventArgs e)
		{
		}
		private void tovarki_MouseEnter(object sender, MouseEventArgs e)
		{
			
		}
		public class Real
		{
			public int Number { get; set; }
		}
		private void zakaz_Click(object sender, RoutedEventArgs e)
		{

			int idd = (from dt in ue.User where dt.UserSurname + dt.UserName + dt.UserPatronymic == FIO.Content.ToString() select dt.UserID).FirstOrDefault();
			int oid = Convert.ToInt32((from dt in ue.Order where dt.UserID == idd select dt.OrderID).FirstOrDefault());

			var ord = (from ut in ue.OrderProduct where ut.OrderID == oid select ut.OrderID).FirstOrDefault();
			var p = (from ut in ue.OrderProduct from dt in ue.Order where ut.OrderID == dt.OrderID select ut.ProductID).ToList();
			//var ret = (Order)tovarki.SelectedItem;

			Zakaz zak = new Zakaz(ord);
			zak.Show();
			zak.FIO.Content = FIO.Content;

			zak.zakazi.DataContext = ue.OrderProduct.Where(x => x.OrderID == oid).ToList();
			
			foreach (int a in p)
			{
				zak.summ.Text = (from ut in ue.Product where ut.ProductID == a select ut.ProductCost).ToList().Sum().ToString();
				zak.skidka.Text = (from ut in ue.Product where ut.ProductID == a select ut.ProductDiscountAmount).ToList().Max().ToString();
			}
			
		}
	}
}
