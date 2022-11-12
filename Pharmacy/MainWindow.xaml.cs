using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
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

namespace Pharmacy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<User> usersList;
        List<Product> productList;
        public MainWindow()
        {
            InitializeComponent();
            usersList = new List<User>();
            usersList.Add(new User() {Id=1, Name = "John Doe", Age = 42, Email = "john@doe-family.com", Role = "Owner" });
            usersList.Add(new User() { Id = 2, Name = "Jane Doe", Age = 39, Email = "jane@doe-family.com", Role = "Employee" });
            usersList.Add(new User() { Id = 3, Name = "Sammy Doe", Age = 26, Email = "sammy.doe@gmail.com", Role="Employee"});
            usersDataList.ItemsSource = usersList;
            productList = new List<Product>();
            productList.Add(new Product() {Id=1,Name="P1",Price=2.50,Quantity=1 });
            productList.Add(new Product() {Id=2,Name="P2",Price=5,Quantity=2});
            productList.Add(new Product() {Id=3,Name="P3",Price=1,Quantity=4 });
            productList.Add(new Product() {Id=4,Name="P4",Price=2.50,Quantity=3 });
            
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Did you press me",
                Content = "this is where you put the message",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();

        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var content =await ContentNewMember.ShowAsync();
            switch (content)
            {
                case ContentDialogResult.Primary:
                    SaveToDb(contentName.Text,contentEmail.Text,contentRole.Text);
                break;
                
            }

        }
        private void SaveToDb(string name,string email,string role)
        {
            usersList.Add(new User() {Name=name,Age=25,Email=email,Role=role });
            usersDataList.ItemsSource = usersList;
            usersDataList.Items.Refresh();
        }

        private void deleteMember_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var res=button.DataContext as User;
            usersList.RemoveAll(x => x.Id == res.Id);
            usersDataList.ItemsSource = usersList;
            usersDataList.Items.Refresh();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (loginUsername.Text=="a" && loginPassword.Password.ToString() =="a")
            {
                tabGeneral.IsEnabled = true;
                tabMember.IsEnabled = true;
                tabMember.IsSelected = true;
                tabLogin.Header = "Logout";
                loginUsername.Clear();
                loginPassword.Clear();
                loginGroupBox.Visibility = Visibility.Hidden;
                logoutBtn.Visibility = Visibility.Visible;
            }
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            tabLogin.IsSelected = true;
            tabGeneral.IsEnabled = false;
            tabMember.IsEnabled = false;
            
            tabLogin.Header = "Login";
            loginGroupBox.Visibility = Visibility.Visible;
            logoutBtn.Visibility = Visibility.Hidden;
        }

    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        
        public string Role { get; set; }
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
