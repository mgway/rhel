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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class uiWindow : Window {
        private MainWindow main;
        private string settingsPath;
        private List<int> charIDs;
        private System.Net.WebClient wc = new System.Net.WebClient();
        public uiWindow(MainWindow main) {
            InitializeComponent();
            this.main = main;
            this.settingsPath = main.localAppPath() + "\\settings";
            this.charIDs = this.getIDs();
            this.getCharNames();
        }
        private void getCharNames() {
            string dlstr = String.Format("https://api.eveonline.com/eve/CharacterName.xml.aspx?ids={0}", getCharIDs());
            System.Xml.XmlReader reader = System.Xml.XmlReader.Create(dlstr);
            while (reader.Read()) {
                if (reader.HasAttributes) {
                    if (reader.IsEmptyElement) {
                        Character cha = new Character(this);
                        cha.charName.Text = reader.GetAttribute(0);
                        cha.charID = Convert.ToInt32(reader.GetAttribute(1));
                        this.CharacterPanel.Children.Add(cha);
                    }
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

        private void ok_Click(object sender, RoutedEventArgs e) {
            Character mainchar = null;
            List<Character> chars = new List<Character>();
            string mainfile = "";
            List<string> charfiles = new List<string>();

            foreach (Character c in this.CharacterPanel.Children) {
                if (Convert.ToBoolean(c.mainChar.IsChecked)) {
                    mainchar = c;
                    break;
                }
            }
            foreach (Character c in this.CharacterPanel.Children) {
                if (Convert.ToBoolean(c.copySettings.IsChecked)) {
                    chars.Add(c);
                }
            }

            foreach (string file in System.IO.Directory.EnumerateFiles(this.settingsPath)) {
                string[] split = file.Split(new string[] { "core_char_", ".dat" }, StringSplitOptions.None);
                if (split.Length == 3 && split[1].Length > 1) {
                    foreach( Character c in chars) {
                        if ( c.charID ==  Convert.ToInt32(split[1]) ) {
                            charfiles.Add(file);
                        }
                    }

                    if (Convert.ToInt32(split[1]) == mainchar.charID) {
                        mainfile = file;
                    }
                }


                foreach (string f in charfiles) {
                    string backup = f.Substring(0, f.Length - 3) + "bak";
                    if (!System.IO.File.Exists(backup)) {
                        System.IO.File.Copy(f, backup);
                    }
                    System.IO.File.Delete(file);
                    System.IO.File.Copy(mainfile, file);
                }
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
    }
}
