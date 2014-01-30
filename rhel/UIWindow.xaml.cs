using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class uiWindow : Window {
        private MainWindow main;
        private string settingsPath;
        private List<int> charIDs;
        private bool saveaccounts = false;

        public uiWindow(MainWindow main) {
            InitializeComponent();
            this.main = main;
            this.settingsPath = String.Format("{0}\\{1}", main.localAppPath(), "settings");
            this.charIDs = this.getIDs();
            this.popListFromFile();
            this.popListFromSettings();
            this.saveaccounts = true;
        }
        private void popListFromSettings() {
            string dlstr = String.Format("https://api.eveonline.com/eve/CharacterName.xml.aspx?ids={0}", getCharIDs());
            System.Xml.XmlReader reader = System.Xml.XmlReader.Create(dlstr);
            List<Character> charlist = new List<Character>();
            while (reader.Read()) {
                if (reader.HasAttributes) {
                    if (reader.IsEmptyElement) {
                        Character cha = new Character(this);
                        cha.charName.Text = reader.GetAttribute(0);
                        cha.charID = Convert.ToInt32(reader.GetAttribute(1));
                        foreach (Account a in main.accountsPanel.Children) {
                            if (a.charIDs.Contains(cha.charID)) {
                                cha.accountID = Convert.ToInt32(a.accountID.Text);
                                break;
                            }
                            else {
                                cha.accountID = 0;
                            }
                        }

                        charlist.Add(cha);
                    }
                }

            }
            foreach (Character c in charlist) {
                List<int> idList = new List<int>();
                if (this.CharacterPanel.Children.Count == 0) {
                    foreach (Character chara in charlist) {
                        this.CharacterPanel.Children.Add(chara);
                    }
                }
                foreach( Character chara in this.CharacterPanel.Children ) {
                    idList.Add(chara.charID);
                }
                if (!idList.Contains(c.charID)) {
                    this.CharacterPanel.Children.Add(c);
                }
            }
        }
            

        private string getCharIDs() {
            string ids = "";
            foreach (int i in this.charIDs) {
                ids += String.Format("{0}{1}", i, ",");
            }
            ids = ids.Substring(0, ids.Length-1);
            return ids;
        }

        public void save_characters() {
            if (this.saveaccounts == false) {
                return;
            }
            StringCollection characters = new StringCollection();
            foreach (Character c in this.CharacterPanel.Children) {
                characters.Add(String.Format("{0},{1}", c.charID, c.charName.Text));
            }
            Properties.Settings.Default.Characters = characters;
            Properties.Settings.Default.Save();
        }
        private void ok_Click(object sender, RoutedEventArgs e) {
            Character mainchar = null;
            List<Character> copychars = new List<Character>();
            foreach (Character c in this.CharacterPanel.Children) {
                if (c.mainChar.IsChecked == true) {
                    mainchar = c;
                }
                if (c.copySettings.IsChecked == true && c.accountID != 0) {
                    copychars.Add(c);
                }
            }
            foreach (Character c in copychars) {
                System.IO.File.Delete(String.Format("{0}\\core_char_{1}.dat", this.settingsPath, c.charID));
                if ( mainchar.accountID != c.accountID ) {
                    System.IO.File.Delete(String.Format("{0}\\core_user_{1}.dat", this.settingsPath, c.accountID));
                    System.IO.File.Copy(String.Format("{0}\\core_user_{1}.dat", this.settingsPath, mainchar.accountID), String.Format("{0}\\core_user_{1}.dat", this.settingsPath, c.accountID));
                }
                System.IO.File.Copy(String.Format("{0}\\core_char_{1}.dat", this.settingsPath, mainchar.charID), String.Format("{0}\\core_char_{1}.dat", this.settingsPath, c.charID));     
            }
            this.Close();
        }

        private List<int> getIDs() {
            List<int> temp = new List<int>();

            foreach (string file in System.IO.Directory.EnumerateFiles(this.settingsPath)) {
                string[] split = file.Split(new string[] { "core_char_", ".dat" }, StringSplitOptions.None);
                if (split.Length == 3 && split[1].Length > 1) {
                    temp.Add(Convert.ToInt32(split[1]));
                }
            }
            return temp;
        }
        private void cancel_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void popListFromFile(){
            if (Properties.Settings.Default.Characters == null) {
                return;
            }
            foreach ( string chara in Properties.Settings.Default.Characters ) {
                string[] split = chara.Split(new char[] {','});
                Character m8 = new Character(this);
                m8.charID = Convert.ToInt32(split[0]);;
                m8.charName.Text = split[1];
                foreach (Account a in this.main.accountsPanel.Children) {
                    if (a.charIDs.Contains(m8.charID)) {
                        m8.accountID = Convert.ToInt32(a.accountID.Text);
                        break;
                    }
                    else {
                        m8.accountID = 0;
                    }
                }
                this.CharacterPanel.Children.Add(m8);
            }
        }

        private void selectAll_Click(object sender, RoutedEventArgs e) {
            foreach (Character c in this.CharacterPanel.Children) {
                if (c.mainChar.IsChecked == false) {
                    c.copySettings.IsChecked = true;
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e) {
            this.save_characters();
        }
    }
}
