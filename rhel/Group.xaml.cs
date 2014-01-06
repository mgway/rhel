using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace rhel {
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Group : UserControl {
        MainWindow main;
        List<Account> accounts;

        public Group(MainWindow main) {
            InitializeComponent();
            this.main = main;
            this.accounts = new List<Account>();
            
        }

        private void delete_Click(object sender, RoutedEventArgs e) {
            this.main.groupsPanel.Children.Remove(this);
            this.main.updateGroups();
        }

        private void groupChanged(object sender, RoutedEventArgs e) {
            this.main.updateGroups();
        }

        private void group_Loaded(object sender, RoutedEventArgs e) {
            return;
        }

        private void launch_Click(object sender, RoutedEventArgs e) {
            foreach (Account account in accounts) {
                account.launchAccount();
            }
        }

        private void configure_Click(object sender, RoutedEventArgs e) {
            GroupSelect select = new GroupSelect(this.main, this);
            select.Show();
        }

        public List<Account> getAccounts() {
            return this.accounts;
        }

        public void removeAccount(AccountSelect acct) {
            List<Account> list = new List<Account>();
            foreach (Account account in this.accounts) {
                if (acct.user == account.username.Text) {
                    list.Add(account);
                }
            }
            foreach (Account account in list) {
                this.accounts.Remove(account);
            }
            this.main.updateGroups();
        }
        public void addAccount(Account acct) {
            if (!this.accounts.Contains(acct)) {
                this.accounts.Add(acct);
            }
            this.main.updateGroups();
        }
    }
}
