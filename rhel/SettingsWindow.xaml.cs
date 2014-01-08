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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window {
        string windowed = "Fullscreen:Windowed:Fixed Window";
        string res = "1280×720:1280x800:1366×768:1440x900:1600×900:1680x1050:1920×1080:1920x1200:2048×1152:2560x1600";
        string scale = "90%:100%:110%:125%";
        string AA = "Disabled:Low:Medium:High";
        string PP = "Disabled:Low:High";
        string SQ = "Low:Medium:High";

        public SettingsWindow() {
            InitializeComponent();
            string[] split = windowed.Split(new string[] {":"}, StringSplitOptions.None);
            foreach (string s in split) {
                this.fullscreen.Items.Add(s);
            }
            split = res.Split(new string[] {":"}, StringSplitOptions.None);
            foreach (string s in split) {
                this.resolution.Items.Add(s);
            }
            split = scale.Split(new string[] {":"}, StringSplitOptions.None);
            foreach (string s in split) {
                this.uiScale.Items.Add(s);
            }
            this.vsync.Items.Add("On");
            this.vsync.Items.Add("Off");
            split = AA.Split(new string[] {":"}, StringSplitOptions.None);
            foreach (string s in split) {
                antiAlias.Items.Add(s);
            }
            split = SQ.Split(new string[] {":"}, StringSplitOptions.None);
            ComboBox[] boxes = new ComboBox[4];
            boxes[0]=shaderQuality;
            boxes[1]=textureQuality;
            boxes[2]=lodQuality;
            boxes[3]=interiorEffects;
            foreach (ComboBox box in boxes) {
                foreach (string s in split) {
                    box.Items.Add(s);
                }
            }
            split = PP.Split(new string[] {":"}, StringSplitOptions.None);
            boxes = new ComboBox[2];
            boxes[0] = postProcess;
            boxes[1] = shadowQuality;
            foreach (ComboBox box in boxes) {
                foreach (string s in split) {
                    box.Items.Add(s);
                }
            }
            string mon = "1:2:3:4:5:6";
            foreach (string s in mon.Split(new string[] {":"}, StringSplitOptions.None)) {
                this.monitor.Items.Add(s);
            }
            this.interiorShader.Items.Add("Low");
            this.interiorShader.Items.Add("High");
            this.monitor.SelectedIndex = Properties.Settings.Default.Display;
            this.vsync.SelectedIndex = Properties.Settings.Default.Vsync;
            this.uiScale.SelectedIndex = Properties.Settings.Default.UIScale;
            this.fullscreen.SelectedIndex = Properties.Settings.Default.Windowed;
            this.resolution.SelectedIndex = Properties.Settings.Default.WinSize;
            this.antiAlias.SelectedIndex = Properties.Settings.Default.AntiAlias;
            this.postProcess.SelectedIndex = Properties.Settings.Default.PostProcess;
            this.shaderQuality.SelectedIndex = Properties.Settings.Default.ShaderQuality;
            this.textureQuality.SelectedIndex = Properties.Settings.Default.TextureQuality;
            this.lodQuality.SelectedIndex = Properties.Settings.Default.LODQuality;
            this.shadowQuality.SelectedIndex = Properties.Settings.Default.ShadowQuality;
            this.interiorEffects.SelectedIndex = Properties.Settings.Default.InteriorEffects;
            this.interiorShader.SelectedIndex = Properties.Settings.Default.InteriorShader;
            this.turrets.IsChecked = Properties.Settings.Default.Turrets;
            this.effects.IsChecked = Properties.Settings.Default.Effects;
            this.missileEffects.IsChecked = Properties.Settings.Default.MissileEffects;
            this.cameraShake.IsChecked = Properties.Settings.Default.CameraShake;
            this.shipExplosions.IsChecked = Properties.Settings.Default.ShipExplo;
            this.droneModels.IsChecked = Properties.Settings.Default.DroneModels;
            this.trails.IsChecked = Properties.Settings.Default.Trails;
            this.gpuParticles.IsChecked = Properties.Settings.Default.GPUParticles;
            this.resourceCache.IsChecked = Properties.Settings.Default.ResourceCache;
            this.hdrEnabled.IsChecked = Properties.Settings.Default.HDR;
            this.loadStationEnv.IsChecked = Properties.Settings.Default.LoadStationEnv;
            this.DX9.IsChecked = Properties.Settings.Default.DX9;
        }

        private void okayClick(object sender, RoutedEventArgs e) {
            Properties.Settings.Default.Display = this.monitor.SelectedIndex;
            Properties.Settings.Default.Vsync = this.vsync.SelectedIndex;
            Properties.Settings.Default.UIScale = this.uiScale.SelectedIndex;
            Properties.Settings.Default.Windowed = this.fullscreen.SelectedIndex;
            Properties.Settings.Default.WinSize = this.resolution.SelectedIndex;
            Properties.Settings.Default.AntiAlias = this.antiAlias.SelectedIndex;
            Properties.Settings.Default.PostProcess = this.postProcess.SelectedIndex;
            Properties.Settings.Default.ShaderQuality = this.shaderQuality.SelectedIndex;
            Properties.Settings.Default.ShadowQuality = this.shadowQuality.SelectedIndex;
            Properties.Settings.Default.TextureQuality = this.textureQuality.SelectedIndex;
            Properties.Settings.Default.LODQuality = this.lodQuality.SelectedIndex;
            Properties.Settings.Default.InteriorEffects = this.interiorEffects.SelectedIndex;
            Properties.Settings.Default.InteriorShader = this.interiorShader.SelectedIndex;
            Properties.Settings.Default.Turrets = Convert.ToBoolean(this.turrets.IsChecked);
            Properties.Settings.Default.Effects =  Convert.ToBoolean(this.effects.IsChecked);
            Properties.Settings.Default.MissileEffects =  Convert.ToBoolean(this.missileEffects.IsChecked);
            Properties.Settings.Default.CameraShake =  Convert.ToBoolean(this.cameraShake.IsChecked);
            Properties.Settings.Default.ShipExplo =  Convert.ToBoolean(this.shipExplosions.IsChecked);
            Properties.Settings.Default.DroneModels =  Convert.ToBoolean(this.droneModels.IsChecked);
            Properties.Settings.Default.Trails =  Convert.ToBoolean(this.trails.IsChecked);
            Properties.Settings.Default.GPUParticles =  Convert.ToBoolean(this.gpuParticles.IsChecked);
            Properties.Settings.Default.ResourceCache = Convert.ToBoolean(this.resourceCache.IsChecked);
            Properties.Settings.Default.HDR =  Convert.ToBoolean(this.hdrEnabled.IsChecked);
            Properties.Settings.Default.LoadStationEnv =  Convert.ToBoolean(this.loadStationEnv.IsChecked);
            Properties.Settings.Default.DX9 = Convert.ToBoolean(this.DX9.IsChecked);
            Properties.Settings.Default.Save();
            this.Close();

        }
    }
}
