using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Linq;
using System.Text;
=======
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
>>>>>>> Добавьте файлы проекта.
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
<<<<<<< HEAD
=======
		public bool sucess = true;
>>>>>>> Добавьте файлы проекта.
		public MainWindow()
		{
			InitializeComponent();
		}

		private void vhod_Click(object sender, RoutedEventArgs e)
		{
			
			using (user24Entities ue = new user24Entities())
			{
<<<<<<< HEAD
=======
				User user = ue.User.FirstOrDefault();
>>>>>>> Добавьте файлы проекта.
				if (ue.User.Any(i => i.UserLogin == login.Text))
				{
					if (ue.User.Any(i => i.UserPassword == password.Text))
					{
						if (ue.User.Any(i => i.UserLogin == login.Text && i.UserPassword == password.Text && i.RoleID == 1))
						{
							//Если роль - Клиент, то открываем страницу для клиента
							Client cli = new Client();
							cli.Show();
<<<<<<< HEAD
=======
							//User u = ue.User.First(x => x.UserID == i.UserID);
							cli.FIO.Content = user.UserSurname + user.UserName + user.UserPatronymic;
>>>>>>> Добавьте файлы проекта.
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
<<<<<<< HEAD
=======
						sucess = false;
						CAPTCHA c = new CAPTCHA();
						c.Show();
						
>>>>>>> Добавьте файлы проекта.
					}
				}
				else
				{
					MessageBox.Show("Вас не существует");
<<<<<<< HEAD
=======
					sucess = false;
					CAPTCHA c = new CAPTCHA();
					c.Show();
>>>>>>> Добавьте файлы проекта.
				}
				
			}
		}
<<<<<<< HEAD

=======
		public void Block()
		{
			load.Content = "ПОДОЖДИТЕ! система заблокирована на 10 секунд";
			Task.WaitAll(new Task[] { Task.Delay(10000) });
		}
>>>>>>> Добавьте файлы проекта.
		private void gost_Click(object sender, RoutedEventArgs e)
		{
			Gost go = new Gost();
			go.Show();
			this.Close();
		}
	}
}
