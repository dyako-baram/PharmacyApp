using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy
{
    public partial class MainWindow : Window
    {
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (loginUsername.Text == "a" && loginPassword.Password.ToString() == "a")
            {
                tabGeneral.IsEnabled = true;
                tabMember.IsEnabled = true;
                tabGeneral.IsSelected = true;
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
}
