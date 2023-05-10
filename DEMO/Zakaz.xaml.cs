using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
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
		int op1;
		public Zakaz(int op)
		{
			InitializeComponent();
			op1 = op;
			vidacha.ItemsSource = ue.PickupPoint.ToList();

			
		}
		public void Update()
		{
			int idd = (from dt in ue.User where dt.UserSurname + dt.UserName + dt.UserPatronymic == FIO.Content.ToString() select dt.UserID).FirstOrDefault();
			int oid = Convert.ToInt32((from dt in ue.Order where dt.UserID == idd select dt.OrderID).FirstOrDefault());
			zakazi.DataContext = ue.OrderProduct.Where(x => x.OrderID == oid).ToList();

			var p = (from ut in ue.OrderProduct from dt in ue.Order where ut.OrderID == dt.OrderID select ut.ProductID).ToList();
			foreach (int a in p)
			{
				summ.Text = (from ut in ue.Product where ut.ProductID == a select ut.ProductCost).ToList().Sum().ToString();
				skidka.Text = (from ut in ue.Product where ut.ProductID == a select ut.ProductDiscountAmount).ToList().Max().ToString();
			}
		}
		private void delete_Click(object sender, RoutedEventArgs e)
		{
			using (var db = new user24Entities())
			{
				var deleted = db.OrderProduct.ToList().Find(pr => pr.ProductID.ToString() == id.Text);
				db.OrderProduct.Remove(deleted);
				db.SaveChanges();

			}
			MessageBox.Show("Товар удален");
			Update();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{

			using (user24Entities db = new user24Entities())
			{
				int pid = (from ut in db.PickupPoint where ut.Address == vidacha.Text select ut.PickupPointID).FirstOrDefault();
				int ord = (from ut in db.OrderProduct where ut.OrderID == op1 select ut.OrderID).FirstOrDefault();
				int idd = Convert.ToInt32((from ut in db.Order where ut.OrderID == op1 select ut.UserID).FirstOrDefault());
				Random rnd = new Random();
				int i = rnd.Next(0, 3000);
				if (ord == op1)
				{
					Order order = new Order();
					order.OrderStatusID = 1;
					order.PickupPointID = pid;
					order.OrderCreateDate = DateTime.Now;
					order.OrderDeliveryDate = DateTime.UtcNow.AddDays(5);
					order.UserID = idd;
					order.OrderGetCode = i;

					db.Order.AddOrUpdate(order);
					db.SaveChanges();
				}
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
