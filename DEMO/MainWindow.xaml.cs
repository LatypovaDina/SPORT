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

		private void vhod_Click(object sender, RoutedEventArgs e)
		{

			using (user24Entities ue = new user24Entities())
			{
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
							//User u = ue.User.First(x => x.UserID == i.UserID);
							cli.FIO.Content = user.UserSurname + user.UserName + user.UserPatronymic;
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
		
		public void Block()
		{
			load.Content = "ПОДОЖДИТЕ! система заблокирована на 10 секунд";
			Task.WaitAll(new Task[] { Task.Delay(10000) });
		}
		private void gost_Click(object sender, RoutedEventArgs e)
		{
			Gost go = new Gost();
			go.Show();
			this.Close();
		}

	}
}
