﻿using System;
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
    /// Interaction logic for Character.xaml
    /// </summary>
    public partial class Character : UserControl {
        public int charID;
        public int accountID;
        private uiWindow uiwin;
        public Character(uiWindow win) {
            InitializeComponent();
            this.uiwin = win;
        }

        private void main_Click(object sender, RoutedEventArgs e) {
            this.copySettings.IsChecked = false;
            this.copySettings.IsEnabled = false;
            foreach (Character c in this.uiwin.CharacterPanel.Children) {
                if (c != this) {
                    c.mainChar.IsChecked = false;
                    c.copySettings.IsEnabled = true;
                }
            }
        }
        private void account_Changed(object sender, RoutedEventArgs e) {
            this.uiwin.save_characters();
        }
    }
}
