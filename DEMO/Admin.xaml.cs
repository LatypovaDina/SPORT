
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DEMO
{
	/// <summary>
	/// Логика взаимодействия для Admin.xaml
	/// </summary>
	public partial class Admin : Window
	{
		public user24Entities ue = new user24Entities();
		
		public Admin()
		{
			InitializeComponent();
			tovarki.ItemsSource = ue.Product.ToList(); // устанавливаем привязку к кэшу
			
			all.Text = ue.Product.Count().ToString();
			kolVo.Text = ue.Product.Count().ToString();
		}

		private void exit_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mw = new MainWindow();
			mw.Show();
			this.Close();
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{

        }

		private void tovarki_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var ret = (Product)tovarki.SelectedItem;

			Redakt red = new Redakt(ret);
			red.Show();
			this.Close();
			using (user24Entities db = new user24Entities())
			{
				int idd = (from ut in db.Product where ut.ProductID.ToString() == id.Text select ut.ProductID).FirstOrDefault();
				int manID = (from ut in db.Product select ut.ProductManufacturerID).FirstOrDefault();

				int supID = (from ut in db.Product select ut.ProductSupplierID).FirstOrDefault();

				int catId = (from ut in db.Product select ut.ProductCategoryID).FirstOrDefault();
				int unId = (from ut in db.Product select ut.ProductCategoryID).FirstOrDefault();

				red.@new.Content = "Сохранить изменения";
				
				red.id.Text = idd.ToString();
				red.art.Text = (from ut in db.Product where ut.ProductID == idd select ut.ProductArticleNumber).FirstOrDefault();
				red.name.Text = (from ut in db.Product where ut.ProductID == idd select ut.ProductName).FirstOrDefault(); 
				red.mera.Text = (from ut in db.UnitType where ut.UnitTypeID == unId from dt in db.Product where dt.ProductID == idd select ut.UnitTypeName).FirstOrDefault();
				red.cost.Text = (from ut in db.Product where ut.ProductID == idd select ut.ProductCost).FirstOrDefault().ToString();
				red.maxSkid.Text = (from ut in db.Product where ut.ProductID == idd select ut.ProductMaxDiscountAmount).FirstOrDefault().ToString();
				red.proizvoditel.Text = (from ut in db.ProductManufacturer where ut.ProductManufacturerID == manID from dt in db.Product where dt.ProductID == idd select ut.ProductManufacturerName).FirstOrDefault();
				red.postavchik.Text = (from ut in db.ProductSupplier where ut.ProductSupplierID == supID from dt in db.Product where dt.ProductID == idd select ut.ProductSupplierName).FirstOrDefault();
				red.category.Text = (from ut in db.ProductCategory where ut.ProductCategoryID == catId from dt in db.Product where dt.ProductID == idd select ut.ProductCategoryName).FirstOrDefault();
				red.skid.Text = (from ut in db.Product where ut.ProductID == idd select ut.ProductDiscountAmount).FirstOrDefault().ToString();
				red.kolVo.Text = (from ut in db.Product where ut.ProductID == idd select ut.ProductQuantityInStock).FirstOrDefault().ToString();
				red.opisaniye.Text = (from ut in db.Product where ut.ProductID == idd select ut.ProductDescription).FirstOrDefault();

			}
		}

		private void newTovar_Click(object sender, RoutedEventArgs e)
		{
			Redakt red = new Redakt();
			red.Show();
			this.Close();
			
			red.@new.Content = "Добавить";
			
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{

		}
	}
}
