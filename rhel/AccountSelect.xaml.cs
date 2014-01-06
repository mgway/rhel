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
    /// Interaction logic for AccountSelect.xaml
    /// </summary>
    public partial class AccountSelect : UserControl {
        public string user;
        GroupSelect gs;

        public AccountSelect(string user, GroupSelect gs) {
            InitializeComponent();
            this.user = user;
            this.usern.Text = this.user;
            this.gs = gs;
            this.primary.IsChecked = false;

            foreach (Account acct in gs.parent.getAccounts()) {
                if (this.user == acct.username.Text) {
                    this.groupCheck.IsChecked = true;
                }
            }
        }

        private void primaryAcctChecked(object sender, RoutedEventArgs e) {
            foreach (AccountSelect accts in gs.selectPanel.Children) {
                if (accts != this) {
                    accts.uncheckPrimary();
                }
            }
        }

        private void uncheckPrimary() {
            this.primary.IsChecked = false;
        }
    }
}
