using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DEMO
{
	/// <summary>
	/// Логика взаимодействия для Redakt.xaml
	/// </summary>
	public partial class Redakt : Window
	{
		Product product1 = new Product();

		public user24Entities ue = new user24Entities();

		public Redakt(Product product = null)
		{
			InitializeComponent();

			product1 = product;

			InitComboBoxes();
			if (@new.Content.ToString() == "Добавить")
			{
				id.IsEnabled = false;
			}

		}
		private void InitComboBoxes()
		{
			category.ItemsSource = ue.ProductCategory.ToList();
			proizvoditel.ItemsSource = ue.ProductManufacturer.ToList();
			mera.ItemsSource = ue.UnitType.ToList();
			postavchik.ItemsSource = ue.ProductSupplier.ToList();
		}
			private void exit_Click(object sender, RoutedEventArgs e)
		{
			Admin adm = new Admin();
			adm.Show();
			this.Close();
        }

		private void redact_Click(object sender, RoutedEventArgs e)
		{

		}

		private void new_Click(object sender, RoutedEventArgs e)
		{
			int idd = (from ut in ue.UnitType where ut.UnitTypeName == mera.Text select ut.UnitTypeID).FirstOrDefault();
			int mid = (from ut in ue.ProductManufacturer where ut.ProductManufacturerName == proizvoditel.Text.ToString() select ut.ProductManufacturerID).FirstOrDefault();
			int sid = (from ut in ue.ProductSupplier where ut.ProductSupplierName == postavchik.Text.ToString() select ut.ProductSupplierID).FirstOrDefault();
			int cid = (from ut in ue.ProductCategory where ut.ProductCategoryName == category.Text.ToString() select ut.ProductCategoryID).FirstOrDefault();

			if (@new.Content.ToString() == "Сохранить изменения")
			{
				
				using (user24Entities db = new user24Entities())
				{
					if (int.Parse(skid.Text) < int.Parse(maxSkid.Text))
					{
						if (int.Parse(kolVo.Text) > 0)
						{
							Product product = product1;

							product.ProductArticleNumber = art.Text;
							product.ProductName = name.Text;
							product.UnitTypeID = idd;
							product.ProductCost = Convert.ToDecimal(cost.Text);
							product.ProductMaxDiscountAmount = byte.Parse(maxSkid.Text);
							product.ProductManufacturerID = mid;
							product.ProductSupplierID = sid;
							product.ProductCategoryID = cid;
							product.ProductDiscountAmount = byte.Parse(skid.Text);
							product.ProductQuantityInStock = Convert.ToInt32(kolVo.Text);
							product.ProductDescription = opisaniye.Text;
							product.ProductPhoto = null;

							db.Product.AddOrUpdate(product);
							db.SaveChanges();

							MessageBox.Show("Изменения сохранены");
						}
						else
						{
							MessageBox.Show("Количество не может быть отрицательным");
						}
					}
					else
					{
						MessageBox.Show("Скидка не должна превышать максимальную");
					}

				}
				
			}
			else if (@new.Content.ToString() == "Добавить")
			{
				if (art.Text != "" && name.Text != "" && mera.Text != "" && cost.Text != "" && maxSkid.Text != "" && proizvoditel.Text != "" && postavchik.Text != "" && category.Text != "" && skid.Text != "" && kolVo.Text != ""	 && opisaniye.Text != "")
				{

					if (int.Parse(skid.Text) < int.Parse(maxSkid.Text))
					{

						if (int.Parse(kolVo.Text) > 0)
						{
							using (user24Entities db = new user24Entities())
							{

								Product prod = new Product { ProductArticleNumber = art.Text, ProductName = name.Text, UnitTypeID = idd, ProductCost = int.Parse(cost.Text), ProductMaxDiscountAmount = byte.Parse(maxSkid.Text), ProductManufacturerID = mid, ProductSupplierID = sid, ProductCategoryID = cid, ProductDiscountAmount = byte.Parse(skid.Text), ProductQuantityInStock = Convert.ToInt32(kolVo.Text), ProductDescription = opisaniye.Text, ProductPhoto = null };
								db.Product.Add(prod);
								db.SaveChanges();   // сохранение изменений

							}
							MessageBox.Show("Добавлен");
						}
						else
						{ MessageBox.Show("Количество не может быть отрицательным"); }
					}
					else
					{
						MessageBox.Show("Скидка не должна превышать максимальную");
					}
				}
				else
				{
					MessageBox.Show("Вы забыли заполнить поля");
				}

			}	
		}

		private void proizvoditel_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void delete_Click(object sender, RoutedEventArgs e)
		{
			using (var db = new user24Entities())
			{
				var deleted = db.Product.ToList().Find(pr => pr.ProductID == product1.ProductID);

				db.Product.Remove(deleted);
				db.SaveChanges();

				MessageBox.Show("Товар удален");
			}
		}
	}
}
