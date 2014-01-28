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
        Group parentGroup;
        MainWindow main;
        public GroupSelect(MainWindow main, Group group) {
            InitializeComponent();
            this.main = main;
            this.parentGroup = group;
            this.addAccounts();
        }

        private void okay_Click(object sender, RoutedEventArgs e) {
            foreach (AccountSelect acct in this.selectPanel.Children) {
                if (Convert.ToBoolean(acct.groupCheck.IsChecked)) {
                    foreach (Account account in this.main.accountsPanel.Children) {
                        if (account.username.Text == acct.user) {
                            this.parentGroup.addAccount(account);
                        }
                    }
                }
                else {
                    this.parentGroup.removeAccount(acct);
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
        public Group getParent() {
            return this.parentGroup;
        }
    }
}
