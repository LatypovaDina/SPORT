using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
	/// Логика взаимодействия для Zakaz.xaml
	/// </summary>
	public partial class Zakaz : Window
	{
		public user24Entities ue = new user24Entities();
		public Zakaz()
		{
			InitializeComponent();
			vidacha.ItemsSource = ue.PickupPoint.ToList();

			
		}

		private void delete_Click(object sender, RoutedEventArgs e)
		{
			
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{

			using (user24Entities db = new user24Entities())
			{
				Random rnd = new Random();
				int i = rnd.Next(0, 3000);
				int pid = (from ut in db.PickupPoint where ut.Address == vidacha.Text select ut.PickupPointID).FirstOrDefault();
				int idd = (from dt in db.User where dt.UserSurname + dt.UserName + dt.UserPatronymic == FIO.Content.ToString() select dt.UserID).FirstOrDefault();
				Order order = new Order { OrderStatusID = 1, PickupPointID = pid, OrderCreateDate = DateTime.Now, OrderDeliveryDate = DateTime.UtcNow.AddDays(6), UserID = idd, OrderGetCode = i };
				db.Order.Add(order);
				db.SaveChanges();   // сохранение изменений
				
			}
				MessageBox.Show("Оформлено");
			
			
		}


		private void zakazi_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			int currentItem = zakazi.SelectedIndex;
			if (currentItem != -1)
			{
				cost.Text = (from ut in ue.Product where ut.ProductID.ToString() == id.Text select ut.ProductCost).FirstOrDefault().ToString();
				name.Text = (from ut in ue.Product where ut.ProductID.ToString() == id.Text select ut.ProductName).FirstOrDefault().ToString();
				kolVo.Text = (from ut in ue.Product where ut.ProductID.ToString() == id.Text select ut.ProductQuantityInStock).FirstOrDefault().ToString();
				art.Text = (from ut in ue.Product where ut.ProductID.ToString() == id.Text select ut.ProductArticleNumber).FirstOrDefault().ToString();
				skid.Text = (from ut in ue.Product where ut.ProductID.ToString() == id.Text select ut.ProductDiscountAmount).FirstOrDefault().ToString();
				maxSkid.Text = (from ut in ue.Product where ut.ProductID.ToString() == id.Text select ut.ProductMaxDiscountAmount).FirstOrDefault().ToString();
				proizvoditel.Text = (from ut in ue.Product from dt in ue.ProductManufacturer where ut.ProductManufacturerID == dt.ProductManufacturerID select dt.ProductManufacturerName).FirstOrDefault().ToString();
				opisaniye.Text = (from ut in ue.Product where ut.ProductID.ToString() == id.Text select ut.ProductDescription).FirstOrDefault().ToString();
			}

		}
	}
}
