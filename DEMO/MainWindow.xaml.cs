using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DEMO
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public bool sucess = true;
		public MainWindow()
		{
			InitializeComponent();
		}
		/// <summary>
		/// авторизация
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void vhod_Click(object sender, RoutedEventArgs e)
		{
			using (user24Entities ue = new user24Entities())
			{
				string idd = (from ut in ue.User where ut.UserLogin == login.Text && ut.UserPassword == password.Text select ut.UserSurname + ut.UserName + ut.UserPatronymic).FirstOrDefault();
				User user = ue.User.FirstOrDefault();
				if (ue.User.Any(i => i.UserLogin == login.Text))
				{
					if (ue.User.Any(i => i.UserPassword == password.Text))
					{
						if (ue.User.Any(i => i.UserLogin == login.Text && i.UserPassword == password.Text && i.RoleID == 1))
						{
							//Если роль - Клиент, то открываем страницу для клиента
							Client cli = new Client();
							cli.Show();
							cli.FIO.Content = idd;
							int iddi = (from ut in ue.User where ut.UserLogin == login.Text select ut.UserID).FirstOrDefault();
							int oid = Convert.ToInt32((from ut in ue.Order where ut.UserID == iddi select ut.UserID).FirstOrDefault());
							if (iddi == oid)
							{
								cli.zakaz.Visibility = Visibility.Visible;
							}
							else
							{
								cli.zakaz.Visibility = Visibility.Hidden;
							}
							this.Close();
							
						}
						//Если роль - Админ, то открываем страницу для админа
						if (ue.User.Any(i => i.UserLogin == login.Text && i.UserPassword == password.Text && i.RoleID == 2))
						{
							Admin adm = new Admin();
							adm.Show();
							
							this.Close();
						}
						//Если роль - Менеджер, то открываем страницу для менеджера
						if (ue.User.Any(i => i.UserLogin == login.Text && i.UserPassword == password.Text && i.RoleID == 3))
						{
							Manager man = new Manager();
							man.Show();
							man.FIO.Content = idd;
							this.Close();
						}
					}
					else
					{
						MessageBox.Show("Неверный пароль");
						sucess = false;
						CAPTCHA c = new CAPTCHA();
						c.Show();
					}
				}
				else
				{
					MessageBox.Show("Вас не существует");
					sucess = false;
					CAPTCHA c = new CAPTCHA();
					c.Show();
				}

				}
			}
		/// <summary>
		/// блокировка системы
		/// </summary>
		public void Block()
		{
			load.Content = "ПОДОЖДИТЕ! система заблокирована на 10 секунд";
			Task.WaitAll(new Task[] { Task.Delay(10000) });
		}
		/// <summary>
		/// вход в качестве гостя
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gost_Click(object sender, RoutedEventArgs e)
		{
			Gost go = new Gost();
			go.Show();
			this.Close();
		}

	}
}
