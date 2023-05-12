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
		Order ord1 = new Order();
		int op1;
		public Order CurrentOrder { get; set; }
		public Zakaz(int op, Order ord)
		{
			InitializeComponent();
			op1 = op;
			CurrentOrder = ord;
			vidacha.ItemsSource = ue.PickupPoint.ToList();
		}
		/// <summary>
		/// обновление данных
		/// </summary>
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
		/// <summary>
		/// удаление 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
		/// <summary>
		/// оформление заказа
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			using (user24Entities db = new user24Entities())
			{
				int pid = (from ut in db.PickupPoint where ut.Address == vidacha.Text select ut.PickupPointID).FirstOrDefault();
				int ordd = (from ut in db.OrderProduct where ut.OrderID == op1 select ut.OrderID).FirstOrDefault();
				int idd = Convert.ToInt32((from ut in db.Order where ut.OrderID == op1 select ut.UserID).FirstOrDefault());
				Random rnd = new Random();
				int i = rnd.Next(0, 3000);
				if (ordd == op1)
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
		/// <summary>
		/// заполнение данными полей окна
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void zakazi_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			int currentItem = zakazi.SelectedIndex;
			if (currentItem != -1)
			{
				cost.Text = ue.Product.FirstOrDefault(ut => ut.ProductID.ToString() == id.Text).ProductCost.ToString();
				name.Text = ue.Product.FirstOrDefault(ut => ut.ProductID.ToString() == id.Text).ProductName.ToString();
				kolVo.Text = ue.Product.FirstOrDefault(ut => ut.ProductID.ToString() == id.Text).ProductQuantityInStock.ToString();
				art.Text = ue.Product.FirstOrDefault(ut => ut.ProductID.ToString() == id.Text).ProductArticleNumber.ToString();
				skid.Text = ue.Product.FirstOrDefault(ut => ut.ProductID.ToString() == id.Text).ProductDiscountAmount.ToString();
				maxSkid.Text = ue.Product.FirstOrDefault(ut => ut.ProductID.ToString() == id.Text).ProductMaxDiscountAmount.ToString();
				proizvoditel.Text = (from ut in ue.Product from dt in ue.ProductManufacturer where ut.ProductManufacturerID == dt.ProductManufacturerID select dt.ProductManufacturerName).FirstOrDefault().ToString();
				opisaniye.Text = ue.Product.FirstOrDefault(ut => ut.ProductID.ToString() == id.Text).ProductDescription.ToString();
			}
		}
		/// <summary>
		/// обновление окна
		/// </summary>
		private void RefreshWindow()
		{
			{
				this.CurrentOrder = new Order();
			}
		}
		/// <summary>
		/// оформление талона
		/// </summary>
		/// <returns></returns>
		private async Task PrintOrderCard()
		{
			var orders = CurrentOrder;
			var app = new Microsoft.Office.Interop.Excel.Application
			{
				SheetsInNewWorkbook = 1
			};


			RefreshWindow(); 
			var workbook = app.Workbooks.Add(Type.Missing);

			Microsoft.Office.Interop.Excel.Worksheet worksheet = app.Worksheets.Item[1];
			worksheet.Name = "Card";

			worksheet.Cells[1][1] = "Order number";
			worksheet.Cells[1][2] = "Product list";
			worksheet.Cells[1][3] = "Total cost";

			worksheet.Cells[2][1] = orders.OrderID;

			var fullProductList = string.Empty;
			fullProductList = orders.OrderProduct.Aggregate(fullProductList,
				(current, product) => current + $"{product.Product.ProductName}\n");
			worksheet.Cells[2][2] = fullProductList;
			worksheet.Cells[2][3] = orders.OrderProduct.Sum(p => p.Product.ProductCost);

			worksheet.Columns.AutoFit();

			app.Visible = true;

			app.Application.ActiveWorkbook.SaveAs(@"C:\Users\latdi\source\repos\DEMO\DEMO\test.xlsx");

			var excelDocument = app.Workbooks.Open(@"C:\Users\latdi\source\repos\DEMO\DEMO\test.xlsx");

			excelDocument.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF, @"C:\Users\latdi\source\repos\DEMO\DEMO\test.pdf");
			excelDocument.Close(false, "", false);
			app.Quit();
		}
		/// <summary>
		/// обработчик нажатия кнопки оформления талона
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void talon_Click(object sender, RoutedEventArgs e)
		{
			PrintOrderCard();
		}
	}
}
