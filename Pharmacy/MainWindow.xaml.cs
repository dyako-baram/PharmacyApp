using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using Pharmacy.Model;

namespace Pharmacy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<User> usersList;
        List<Product> productList;
        List<Sell> sellList;
        Sell lastSoldItem =new Sell();
        CultureInfo culture = new CultureInfo("en-US");
        public MainWindow()
        {
            InitializeComponent();
            usersList = new List<User>();
            usersList.Add(new User() {Id=1, Name = "John Doe", Age = 42, Email = "john@doe-family.com", Role = "Owner" });
            usersList.Add(new User() { Id = 2, Name = "Jane Doe", Age = 39, Email = "jane@doe-family.com", Role = "Employee" });
            usersList.Add(new User() { Id = 3, Name = "Sammy Doe", Age = 26, Email = "sammy.doe@gmail.com", Role="Employee"});
            usersDataList.ItemsSource = usersList;
            productList = new List<Product>();
            productList.Add(new Product() { Id = 1, Name = "P1", Price = 2.50, Quantity = 1 });
            productList.Add(new Product() { Id = 2, Name = "P2", Price = 5, Quantity = 2 });
            productList.Add(new Product() { Id = 3, Name = "P3", Price = 1, Quantity = 4 });
            productList.Add(new Product() { Id = 4, Name = "P4", Price = 2.50, Quantity = 3 });
            sellList = new();
            sellList.Add(new Sell() { Name = "P1", Price = 2.50, Quantity = 1 });
            sellList.Add(new Sell() { Name = "P2", Price = 5, Quantity = 1 });
            sellList.Add(new Sell() { Name = "P3", Price = 3, Quantity = 1 });
            sellList.Add(new Sell() { Name = "P4", Price = 1.50, Quantity = 2 });
            sellList.Add(new Sell() { Name = "P5", Price = 0.50, Quantity = 3 });
            //sellList.Add(new Sell() { Name = "P6", Price = 0.50, Quantity = 3 });
            //sellList.Add(new Sell() { Name = "P7", Price = 0.50, Quantity = 3 });
            //sellList.Add(new Sell() { Name = "P8", Price = 0.50, Quantity = 3 });
            //sellList.Add(new Sell() { Name = "P9", Price = 0.50, Quantity = 3 });
            //sellList.Add(new Sell() { Name = "P10", Price = 0.50, Quantity = 3 });
            //sellList.Add(new Sell() { Name = "P11", Price = 0.50, Quantity = 3 });
            //sellList.Add(new Sell() { Name = "P12", Price = 0.50, Quantity = 3 });
            //sellList.Add(new Sell() { Name = "P13", Price = 0.50, Quantity = 3 });
            //sellList.Add(new Sell() { Name = "P14", Price = 0.50, Quantity = 3 });
            //sellList.Add(new Sell() { Name = "P15", Price = 0.50, Quantity = 3 });
            //sellList.Add(new Sell() { Name = "P16", Price = 0.50, Quantity = 3 });
            //sellList.Add(new Sell() { Name = "P17", Price = 0.50, Quantity = 3 });
            //sellList.Add(new Sell() { Name = "P18", Price = 0.50, Quantity = 3 });
            //sellList.Add(new Sell() { Name = "P19", Price = 0.50, Quantity = 3 });
            //sellList.Add(new Sell() { Name = "P20", Price = 0.50, Quantity = 3 });
            sellDataGrid.ItemsSource = sellList;
            var c= new CultureInfo("en-US");
            sellTotal.Text = $"Total: {sellList.Select(x => x.Total).Sum().ToString("C", culture)}";
            sellCount.Text = $"Amount of items: {sellList.Select(x => x.Quantity).Sum()}";


        }

        private async void AddnewMEmberBtn_Click(object sender, RoutedEventArgs e)
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



        private void increaseItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var res = button.DataContext as Sell;
            var index = sellList.FindIndex(x=>x.Name==res.Name);
            sellList[index].Quantity += 1;
            lastSoldItem = sellList[index];
            sellDataGrid.ItemsSource = sellList;
            sellDataGrid.Items.Refresh();
            sellTotal.Text = $"Total: {sellList.Select(x => x.Total).Sum().ToString("C", culture)}";
        }

        private void decreaseItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var res = button.DataContext as Sell;
            var index = sellList.FindIndex(x => x.Name == res.Name);
            sellList[index].Quantity -= 1;
            if (sellList[index].Quantity==0)
            {
                sellList.RemoveAll(x => x.Name == res.Name);
            }
            lastSoldItem = sellList[index];
            sellDataGrid.ItemsSource = sellList;
            sellDataGrid.Items.Refresh();
            sellTotal.Text = $"Total: {sellList.Select(x => x.Total).Sum().ToString("C", culture)}";
            sellCount.Text = $"Amount of items: {sellList.Select(x => x.Quantity).Sum()}";
        }

        private void deleteItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var res = button.DataContext as Sell;
            sellList.RemoveAll(x => x.Name == res.Name);
            sellDataGrid.ItemsSource = sellList;
            sellDataGrid.Items.Refresh();
            sellTotal.Text = $"Total: {sellList.Select(x => x.Total).Sum().ToString("C", culture)}";
            sellCount.Text = $"Amount of items: {sellList.Select(x => x.Quantity).Sum()}";
        }

        private void addItemToDatagrid_Click(object sender, RoutedEventArgs e)
        {
            
            var index = sellList.FindIndex(x => x.Name.ToLower() == barcodeText.Text.ToLower());
            sellList[index].Quantity += 1;
            lastSoldItem = sellList[index];
            sellDataGrid.ItemsSource = sellList;
            sellDataGrid.Items.Refresh();
            sellTotal.Text = $"Total: {sellList.Select(x => x.Total).Sum().ToString("C", culture)}";
            sellCount.Text = $"Amount of items: {sellList.Select(x=>x.Quantity).Sum()}";
        }
    }

    
    
}
