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
using System.Windows.Shapes;

namespace rhel {
    /// <summary>
    /// Interaction logic for GroupSelect.xaml
    /// </summary>
    public partial class GroupSelect : Window {
        public Group parent;
        MainWindow main;
        public GroupSelect(MainWindow main, Group group) {
            InitializeComponent();
            this.main = main;
            this.parent = group;
            this.addAccounts();
            this.Title = group.groupname.ToString().Substring(33);
        }

        private void okay_Click(object sender, RoutedEventArgs e) {
            foreach (AccountSelect acct in this.selectPanel.Children) {
                if (Convert.ToBoolean(acct.groupCheck.IsChecked)) {
                    foreach (Account account in this.main.accountsPanel.Children) {
                        if (account.username.Text == acct.user) {
                            this.parent.addAccount(account);
                        }
                    }
                }
                else {
                    this.parent.removeAccount(acct);
                }
            }
            this.Close();
        }
        private void addAccounts() {
            foreach (Account account in this.main.accountsPanel.Children) {
                AccountSelect acct = new AccountSelect(account.username.Text, this);
                this.selectPanel.Children.Add(acct);
            }
        }
    }
}
