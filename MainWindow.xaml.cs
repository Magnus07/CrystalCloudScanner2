using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using xNet;
using System.Threading;

namespace CrystalCloudScanner2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        public MainWindow()
        {
            InitializeComponent();
            CheckUpdates();
        }
        private void CheckUpdates()
        {
            HttpRequest Req = new HttpRequest();
            string Response = System.Convert.ToString(Req.Get("http://hft.zzz.com.ua/CrystalCloudScanner2/ccs2.html"));
            string ActualVersion = Response.Substring("<p class=\"ActualVersion\">", "</p>");
            if (ActualVersion != Params.CurrentProgramVersion)
            {
                var Animation = new ThicknessAnimation();
                Animation.From = new Thickness(0, -49, 0, 0);
                Animation.To = new Thickness(0, 0, 0, 0);
                Animation.Duration = TimeSpan.FromSeconds(1);
                UpdateGrid.BeginAnimation(MarginProperty, Animation);
            }

        }
        private void DomainInfoImg_MouseEnter_1(object sender, MouseEventArgs e)
        {
            DomainInfoImg.Height = 80;
            DomainInfoImg.Width = 80;
        }

        private void DomainInfoImg_MouseLeave_1(object sender, MouseEventArgs e)
        {
            DomainInfoImg.Width = 64;
            DomainInfoImg.Height = 64;
        }

        private void ScanFileImg_MouseEnter(object sender, MouseEventArgs e)
        {
            ScanFileImg.Height = 80;
            ScanFileImg.Width = 80;
        }

        private void ScanFileImg_MouseLeave(object sender, MouseEventArgs e)
        {
            ScanFileImg.Height = 64;
            ScanFileImg.Width = 64;
        }

        private void ScanURLImg_MouseEnter_1(object sender, MouseEventArgs e)
        {
            ScanURLImg.Width = 80;
            ScanURLImg.Height = 80;
        }

        private void ScanURLImg_MouseLeave_1(object sender, MouseEventArgs e)
        {
            ScanURLImg.Height = 64;
            ScanURLImg.Width = 64;
        }

        private void IPInfoImg_MouseEnter_1(object sender, MouseEventArgs e)
        {
            IPInfoImg.Height = 80;
            IPInfoImg.Width = 80;
        }

        private void IPInfoImg_MouseLeave_1(object sender, MouseEventArgs e)
        {
            IPInfoImg.Height = 64;
            IPInfoImg.Width = 64;
        }

        private void SettingsImg_MouseEnter(object sender, MouseEventArgs e)
        {
            SettingsImg.Height = 80;
            SettingsImg.Width = 80;
        }

        private void SettingsImg_MouseLeave(object sender, MouseEventArgs e)
        {
            SettingsImg.Height = 64;
            SettingsImg.Width = 64;
        }

        private void ScanFileImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Params.CurrentPage != "")
            {
                switch (Params.CurrentPage)
                {
                    case "ScanFile":
                        break;
                    case "ScanURL":
                        var Animation1 = new DoubleAnimation();
                        Animation1.From = 1;
                        Animation1.To = 0.2;
                        Animation1.Duration = TimeSpan.FromSeconds(2.5);
                        Animation1.Completed += new EventHandler(FromURLtoFileCompleted);
                        ScanURLGrid.BeginAnimation(OpacityProperty, Animation1);                      
                        break;
                    case "IPInfo":
                        var Animation2 = new DoubleAnimation();
                        Animation2.From = 1;
                        Animation2.To = 0.2;
                        Animation2.Duration = TimeSpan.FromSeconds(2.5);
                        Animation2.Completed += new EventHandler(FromIPtoFileCompleted);
                        IPInfoGrid.BeginAnimation(OpacityProperty, Animation2);
                        break;
                    case "DomainInfo":
                        var Animation3 = new DoubleAnimation();
                        Animation3.From = 1;
                        Animation3.To = 0.2;
                        Animation3.Duration = TimeSpan.FromSeconds(2.5);
                        Animation3.Completed += new EventHandler(FromDomaintoFileCompleted);
                        DomainInfoGrid.BeginAnimation(OpacityProperty, Animation3);
                        break;
                    case "Settings":
                        var Animation4 = new DoubleAnimation();
                        Animation4.From = 1;
                        Animation4.To = 0.2;
                        Animation4.Duration = TimeSpan.FromSeconds(2.5);
                        Animation4.Completed += new EventHandler(FromSettingstoFileCompleted);
                        SettingsGrid.BeginAnimation(OpacityProperty, Animation4);
                        break;
                }
            }
            else
            {
                var Animation = new ThicknessAnimation();
                Animation.From = new Thickness(0,0,0,0);
                Animation.To = new Thickness(0,-400,0,31);
                Animation.Duration = TimeSpan.FromSeconds(0.7);
                MainImageGrid.BeginAnimation(MarginProperty, Animation);
                var Animation1 = new DoubleAnimation();
                Animation1.From = 1;
                Animation1.To = 0.1;
                Animation1.Duration = TimeSpan.FromSeconds(0.7);
                Animation1.Completed += new EventHandler(FromMainImageToScanFile);
                MainImageGrid.BeginAnimation(OpacityProperty, Animation1);
                var Animation4 = new DoubleAnimation();
                Animation4.From = 0.1;
                Animation4.To = 1;
                Animation4.Duration = TimeSpan.FromSeconds(2.5);
                ScanFileGrid.BeginAnimation(OpacityProperty, Animation4);
                var Animation3 = new ThicknessAnimation();
                Animation3.From = new Thickness(0, 264, 0, 0);
                Animation3.To = new Thickness(0, 303, 0, 0);
                Animation3.Duration = TimeSpan.FromSeconds(0.4);
                ControlPanelGrid.BeginAnimation(MarginProperty, Animation3);
                Params.CurrentPage = "ScanFile";
            }
        }
        private void FromURLtoFileCompleted(object sender, EventArgs e)
        {
            ScanURLGrid.Visibility = Visibility.Hidden;
            ScanFileGrid.Visibility = Visibility.Visible;
            var Animation = new DoubleAnimation();
            Animation.From = 0.2;
            Animation.To = 1;
            Animation.Duration = TimeSpan.FromSeconds(2.5);
            ScanFileGrid.BeginAnimation(OpacityProperty, Animation);
            Params.CurrentPage = "ScanFile";
        }
        private void FromIPtoFileCompleted(object sender, EventArgs e)
        {
            IPInfoGrid.Visibility = Visibility.Hidden;
            ScanFileGrid.Visibility = Visibility.Visible;
            var Animation = new DoubleAnimation();
            Animation.From = 0.2;
            Animation.To = 1;
            Animation.Duration = TimeSpan.FromSeconds(2.5);
            ScanFileGrid.BeginAnimation(OpacityProperty, Animation);
            Params.CurrentPage = "ScanFile";
        }
        private void FromDomaintoFileCompleted(object sender, EventArgs e)
        {
            DomainInfoGrid.Visibility = Visibility.Hidden;
            ScanFileGrid.Visibility = Visibility.Visible;
            var Animation = new DoubleAnimation();
            Animation.From = 0.2;
            Animation.To = 1;
            Animation.Duration = TimeSpan.FromSeconds(2.5);
            ScanFileGrid.BeginAnimation(OpacityProperty, Animation);
            Params.CurrentPage = "ScanFile";
        }
        private void FromSettingstoFileCompleted(object sender, EventArgs e)
        {
            SettingsGrid.Visibility = Visibility.Hidden;
            ScanFileGrid.Visibility = Visibility.Visible;
            var Animation = new DoubleAnimation();
            Animation.From = 0.2;
            Animation.To = 1;
            Animation.Duration = TimeSpan.FromSeconds(2.5);
            ScanFileGrid.BeginAnimation(OpacityProperty, Animation);
            Params.CurrentPage = "ScanFile";
        }
        private void FromMainImageToScanFile(object sender, EventArgs e)
        {
            MainImageGrid.Visibility = Visibility.Hidden;
            ScanFileGrid.Visibility = Visibility.Visible;
        }

        private void ScanURLImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Params.CurrentPage != "")
            {
                switch (Params.CurrentPage)
                {
                    case "ScanURL":
                        break;
                    case "ScanFile":
                        var Animation1 = new DoubleAnimation();
                        Animation1.From = 1;
                        Animation1.To = 0.2;
                        Animation1.Duration = TimeSpan.FromSeconds(2.5);
                        Animation1.Completed += new EventHandler(FromFiletoURLCompleted);
                        ScanFileGrid.BeginAnimation(OpacityProperty, Animation1);
                        break;
                    case "IPInfo":
                        var Animation2 = new DoubleAnimation();
                        Animation2.From = 1;
                        Animation2.To = 0.2;
                        Animation2.Duration = TimeSpan.FromSeconds(2.5);
                        Animation2.Completed += new EventHandler(FromIPtoURLCompleted);
                        IPInfoGrid.BeginAnimation(OpacityProperty, Animation2);
                        break;
                    case "DomainInfo":
                        var Animation3 = new DoubleAnimation();
                        Animation3.From = 1;
                        Animation3.To = 0.2;
                        Animation3.Duration = TimeSpan.FromSeconds(2.5);
                        Animation3.Completed += new EventHandler(FromDomaintoURLCompleted);
                        DomainInfoGrid.BeginAnimation(OpacityProperty, Animation3);
                        break;
                    case "Settings":
                        var Animation4 = new DoubleAnimation();
                        Animation4.From = 1;
                        Animation4.To = 0.2;
                        Animation4.Duration = TimeSpan.FromSeconds(2.5);
                        Animation4.Completed += new EventHandler(FromSettingstoURLCompleted);
                        SettingsGrid.BeginAnimation(OpacityProperty, Animation4);
                        break;
                }
            }
            else
            {
                var Animation = new ThicknessAnimation();
                Animation.From = new Thickness(0, 0, 0, 0);
                Animation.To = new Thickness(0, -400, 0, 31);
                Animation.Duration = TimeSpan.FromSeconds(0.7);
                MainImageGrid.BeginAnimation(MarginProperty, Animation);
                var Animation1 = new DoubleAnimation();
                Animation1.From = 1;
                Animation1.To = 0.1;
                Animation1.Duration = TimeSpan.FromSeconds(0.7);
                Animation1.Completed += new EventHandler(FromMainImageToScanURL);
                MainImageGrid.BeginAnimation(OpacityProperty, Animation1);
                var Animation4 = new DoubleAnimation();
                Animation4.From = 0.1;
                Animation4.To = 1;
                Animation4.Duration = TimeSpan.FromSeconds(2.5);
                ScanURLGrid.BeginAnimation(OpacityProperty, Animation4);
                var Animation3 = new ThicknessAnimation();
                Animation3.From = new Thickness(0, 264, 0, 0);
                Animation3.To = new Thickness(0, 303, 0, 0);
                Animation3.Duration = TimeSpan.FromSeconds(0.4);
                ControlPanelGrid.BeginAnimation(MarginProperty, Animation3);
                Params.CurrentPage = "ScanURL";
            }
        }
        private void FromFiletoURLCompleted(object sender, EventArgs e)
        {
            ScanFileGrid.Visibility = Visibility.Hidden;
            ScanURLGrid.Visibility = Visibility.Visible;
            var Animation = new DoubleAnimation();
            Animation.From = 0.2;
            Animation.To = 1;
            Animation.Duration = TimeSpan.FromSeconds(2.5);
            ScanURLGrid.BeginAnimation(OpacityProperty, Animation);
            Params.CurrentPage = "ScanURL";
        }
        private void FromIPtoURLCompleted(object sender, EventArgs e)
        {
            IPInfoGrid.Visibility = Visibility.Hidden;
            ScanURLGrid.Visibility = Visibility.Visible;
            var Animation = new DoubleAnimation();
            Animation.From = 0.2;
            Animation.To = 1;
            Animation.Duration = TimeSpan.FromSeconds(2.5);
            ScanURLGrid.BeginAnimation(OpacityProperty, Animation);
            Params.CurrentPage = "ScanURL";
        }
        private void FromDomaintoURLCompleted(object sender, EventArgs e)
        {
            DomainInfoGrid.Visibility = Visibility.Hidden;
            ScanURLGrid.Visibility = Visibility.Visible;
            var Animation = new DoubleAnimation();
            Animation.From = 0.2;
            Animation.To = 1;
            Animation.Duration = TimeSpan.FromSeconds(2.5);
            ScanURLGrid.BeginAnimation(OpacityProperty, Animation);
            Params.CurrentPage = "ScanURL";
        }
        private void FromSettingstoURLCompleted(object sender, EventArgs e)
        {
            SettingsGrid.Visibility = Visibility.Hidden;
            ScanURLGrid.Visibility = Visibility.Visible;
            var Animation = new DoubleAnimation();
            Animation.From = 0.2;
            Animation.To = 1;
            Animation.Duration = TimeSpan.FromSeconds(2.5);
            ScanURLGrid.BeginAnimation(OpacityProperty, Animation);
            Params.CurrentPage = "ScanURL";
        }
        private void FromMainImageToScanURL(object sender, EventArgs e)
        {
            MainImageGrid.Visibility = Visibility.Hidden;
            ScanURLGrid.Visibility = Visibility.Visible;
        }

        private void IPInfoImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Params.CurrentPage != "")
            {
                switch (Params.CurrentPage)
                {
                    case "IPInfo":
                        break;
                    case "ScanFile":
                        var Animation1 = new DoubleAnimation();
                        Animation1.From = 1;
                        Animation1.To = 0.2;
                        Animation1.Duration = TimeSpan.FromSeconds(2.5);
                        Animation1.Completed += new EventHandler(FromFiletoIPCompleted);
                        ScanFileGrid.BeginAnimation(OpacityProperty, Animation1);
                        break;
                    case "ScanURL":
                        var Animation2 = new DoubleAnimation();
                        Animation2.From = 1;
                        Animation2.To = 0.2;
                        Animation2.Duration = TimeSpan.FromSeconds(2.5);
                        Animation2.Completed += new EventHandler(FromURLtoIPCompleted);
                        ScanURLGrid.BeginAnimation(OpacityProperty, Animation2);
                        break;
                    case "DomainInfo":
                        var Animation3 = new DoubleAnimation();
                        Animation3.From = 1;
                        Animation3.To = 0.2;
                        Animation3.Duration = TimeSpan.FromSeconds(2.5);
                        Animation3.Completed += new EventHandler(FromDomaintoIPCompleted);
                        DomainInfoGrid.BeginAnimation(OpacityProperty, Animation3);
                        break;
                    case "Settings":
                        var Animation4 = new DoubleAnimation();
                        Animation4.From = 1;
                        Animation4.To = 0.2;
                        Animation4.Duration = TimeSpan.FromSeconds(2.5);
                        Animation4.Completed += new EventHandler(FromSettingstoIPCompleted);
                        SettingsGrid.BeginAnimation(OpacityProperty, Animation4);
                        break;
                }
            }
            else
            {
                var Animation = new ThicknessAnimation();
                Animation.From = new Thickness(0, 0, 0, 0);
                Animation.To = new Thickness(0, -400, 0, 31);
                Animation.Duration = TimeSpan.FromSeconds(0.7);
                MainImageGrid.BeginAnimation(MarginProperty, Animation);
                var Animation1 = new DoubleAnimation();
                Animation1.From = 1;
                Animation1.To = 0.1;
                Animation1.Duration = TimeSpan.FromSeconds(0.7);
                Animation1.Completed += new EventHandler(FromMainImageToIPInfo);
                MainImageGrid.BeginAnimation(OpacityProperty, Animation1);
                var Animation4 = new DoubleAnimation();
                Animation4.From = 0.1;
                Animation4.To = 1;
                Animation4.Duration = TimeSpan.FromSeconds(2.5);
                IPInfoGrid.BeginAnimation(OpacityProperty, Animation4);
                var Animation3 = new ThicknessAnimation();
                Animation3.From = new Thickness(0, 264, 0, 0);
                Animation3.To = new Thickness(0, 303, 0, 0);
                Animation3.Duration = TimeSpan.FromSeconds(0.4);
                ControlPanelGrid.BeginAnimation(MarginProperty, Animation3);
                Params.CurrentPage = "IPInfo";
            }
        }
        private void FromFiletoIPCompleted(object sender, EventArgs e)
        {
            ScanFileGrid.Visibility = Visibility.Hidden;
            IPInfoGrid.Visibility = Visibility.Visible;
            var Animation = new DoubleAnimation();
            Animation.From = 0.2;
            Animation.To = 1;
            Animation.Duration = TimeSpan.FromSeconds(2.5);
            IPInfoGrid.BeginAnimation(OpacityProperty, Animation);
            Params.CurrentPage = "IPInfo";
        }
        private void FromURLtoIPCompleted(object sender, EventArgs e)
        {
            ScanURLGrid.Visibility = Visibility.Hidden;
            IPInfoGrid.Visibility = Visibility.Visible;
            var Animation = new DoubleAnimation();
            Animation.From = 0.2;
            Animation.To = 1;
            Animation.Duration = TimeSpan.FromSeconds(2.5);
            IPInfoGrid.BeginAnimation(OpacityProperty, Animation);
            Params.CurrentPage = "IPInfo";
        }
        private void FromDomaintoIPCompleted(object sender, EventArgs e)
        {
            DomainInfoGrid.Visibility = Visibility.Hidden;
            IPInfoGrid.Visibility = Visibility.Visible;
            var Animation = new DoubleAnimation();
            Animation.From = 0.2;
            Animation.To = 1;
            Animation.Duration = TimeSpan.FromSeconds(2.5);
            IPInfoGrid.BeginAnimation(OpacityProperty, Animation);
            Params.CurrentPage = "IPInfo";
        }
        private void FromSettingstoIPCompleted(object sender, EventArgs e)
        {
            SettingsGrid.Visibility = Visibility.Hidden;
            IPInfoGrid.Visibility = Visibility.Visible;
            var Animation = new DoubleAnimation();
            Animation.From = 0.2;
            Animation.To = 1;
            Animation.Duration = TimeSpan.FromSeconds(2.5);
            IPInfoGrid.BeginAnimation(OpacityProperty, Animation);
            Params.CurrentPage = "IPInfo";
        }
        private void FromMainImageToIPInfo(object sender, EventArgs e)
        {
            MainImageGrid.Visibility = Visibility.Hidden;
            IPInfoGrid.Visibility = Visibility.Visible;
        }

        private void DomainInfoImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Params.CurrentPage != "")
            {
                switch (Params.CurrentPage)
                {
                    case "DomainInfo":
                        break;
                    case "ScanFile":
                        var Animation1 = new DoubleAnimation();
                        Animation1.From = 1;
                        Animation1.To = 0.2;
                        Animation1.Duration = TimeSpan.FromSeconds(2.5);
                        Animation1.Completed += new EventHandler(FromFiletoDomainCompleted);
                        ScanFileGrid.BeginAnimation(OpacityProperty, Animation1);
                        break;
                    case "ScanURL":
                        var Animation2 = new DoubleAnimation();
                        Animation2.From = 1;
                        Animation2.To = 0.2;
                        Animation2.Duration = TimeSpan.FromSeconds(2.5);
                        Animation2.Completed += new EventHandler(FromURLtoDomainCompleted);
                        ScanURLGrid.BeginAnimation(OpacityProperty, Animation2);
                        break;
                    case "IPInfo":
                        var Animation3 = new DoubleAnimation();
                        Animation3.From = 1;
                        Animation3.To = 0.2;
                        Animation3.Duration = TimeSpan.FromSeconds(2.5);
                        Animation3.Completed += new EventHandler(FromIPtoDomainCompleted);
                        IPInfoGrid.BeginAnimation(OpacityProperty, Animation3);
                        break;
                    case "Settings":
                        var Animation4 = new DoubleAnimation();
                        Animation4.From = 1;
                        Animation4.To = 0.2;
                        Animation4.Duration = TimeSpan.FromSeconds(2.5);
                        Animation4.Completed += new EventHandler(FromSettingstoDomainCompleted);
                        SettingsGrid.BeginAnimation(OpacityProperty, Animation4);
                        break;
                }
            }
            else
            {
                var Animation = new ThicknessAnimation();
                Animation.From = new Thickness(0, 0, 0, 0);
                Animation.To = new Thickness(0, -400, 0, 31);
                Animation.Duration = TimeSpan.FromSeconds(0.7);
                MainImageGrid.BeginAnimation(MarginProperty, Animation);
                var Animation1 = new DoubleAnimation();
                Animation1.From = 1;
                Animation1.To = 0.1;
                Animation1.Duration = TimeSpan.FromSeconds(0.7);
                Animation1.Completed += new EventHandler(FromMainImageToDomainInfo);
                MainImageGrid.BeginAnimation(OpacityProperty, Animation1);
                var Animation4 = new DoubleAnimation();
                Animation4.From = 0.1;
                Animation4.To = 1;
                Animation4.Duration = TimeSpan.FromSeconds(2.5);
                DomainInfoGrid.BeginAnimation(OpacityProperty, Animation4);
                var Animation3 = new ThicknessAnimation();
                Animation3.From = new Thickness(0, 264, 0, 0);
                Animation3.To = new Thickness(0, 303, 0, 0);
                Animation3.Duration = TimeSpan.FromSeconds(0.4);
                ControlPanelGrid.BeginAnimation(MarginProperty, Animation3);
                Params.CurrentPage = "DomainInfo";
            }
        }
        private void FromFiletoDomainCompleted(object sender, EventArgs e)
        {
            ScanFileGrid.Visibility = Visibility.Hidden;
            DomainInfoGrid.Visibility = Visibility.Visible;
            var Animation = new DoubleAnimation();
            Animation.From = 0.2;
            Animation.To = 1;
            Animation.Duration = TimeSpan.FromSeconds(2.5);
            DomainInfoGrid.BeginAnimation(OpacityProperty, Animation);
            Params.CurrentPage = "DomainInfo";
        }
        private void FromURLtoDomainCompleted(object sender, EventArgs e)
        {
            ScanURLGrid.Visibility = Visibility.Hidden;
            DomainInfoGrid.Visibility = Visibility.Visible;
            var Animation = new DoubleAnimation();
            Animation.From = 0.2;
            Animation.To = 1;
            Animation.Duration = TimeSpan.FromSeconds(2.5);
            DomainInfoGrid.BeginAnimation(OpacityProperty, Animation);
            Params.CurrentPage = "DomainInfo";
        }
        private void FromIPtoDomainCompleted(object sender, EventArgs e)
        {
            IPInfoGrid.Visibility = Visibility.Hidden;
            DomainInfoGrid.Visibility = Visibility.Visible;
            var Animation = new DoubleAnimation();
            Animation.From = 0.2;
            Animation.To = 1;
            Animation.Duration = TimeSpan.FromSeconds(2.5);
            DomainInfoGrid.BeginAnimation(OpacityProperty, Animation);
            Params.CurrentPage = "DomainInfo";
        }
        private void FromSettingstoDomainCompleted(object sender, EventArgs e)
        {
            SettingsGrid.Visibility = Visibility.Hidden;
            DomainInfoGrid.Visibility = Visibility.Visible;
            var Animation = new DoubleAnimation();
            Animation.From = 0.2;
            Animation.To = 1;
            Animation.Duration = TimeSpan.FromSeconds(2.5);
            DomainInfoGrid.BeginAnimation(OpacityProperty, Animation);
            Params.CurrentPage = "DomainInfo";
        }
        private void FromMainImageToDomainInfo(object sender, EventArgs e)
        {
            MainImageGrid.Visibility = Visibility.Hidden;
            DomainInfoGrid.Visibility = Visibility.Visible;
        }

        private void SettingsImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Params.CurrentPage != "")
            {
                switch (Params.CurrentPage)
                {
                    case "Settings":
                        break;
                    case "ScanFile":
                        var Animation1 = new DoubleAnimation();
                        Animation1.From = 1;
                        Animation1.To = 0.2;
                        Animation1.Duration = TimeSpan.FromSeconds(2.5);
                        Animation1.Completed += new EventHandler(FromFiletoSettingsCompleted);
                        ScanFileGrid.BeginAnimation(OpacityProperty, Animation1);
                        break;
                    case "ScanURL":
                        var Animation2 = new DoubleAnimation();
                        Animation2.From = 1;
                        Animation2.To = 0.2;
                        Animation2.Duration = TimeSpan.FromSeconds(2.5);
                        Animation2.Completed += new EventHandler(FromURLtoSettingsCompleted);
                        ScanURLGrid.BeginAnimation(OpacityProperty, Animation2);
                        break;
                    case "IPInfo":
                        var Animation3 = new DoubleAnimation();
                        Animation3.From = 1;
                        Animation3.To = 0.2;
                        Animation3.Duration = TimeSpan.FromSeconds(2.5);
                        Animation3.Completed += new EventHandler(FromIPtoSettingsCompleted);
                        IPInfoGrid.BeginAnimation(OpacityProperty, Animation3);
                        break;
                    case "DomainInfo":
                        var Animation4 = new DoubleAnimation();
                        Animation4.From = 1;
                        Animation4.To = 0.2;
                        Animation4.Duration = TimeSpan.FromSeconds(2.5);
                        Animation4.Completed += new EventHandler(FromDomaintoSettingsCompleted);
                        DomainInfoGrid.BeginAnimation(OpacityProperty, Animation4);
                        break;
                }
            }
            else
            {
                var Animation = new ThicknessAnimation();
                Animation.From = new Thickness(0, 0, 0, 0);
                Animation.To = new Thickness(0, -400, 0, 31);
                Animation.Duration = TimeSpan.FromSeconds(0.7);
                MainImageGrid.BeginAnimation(MarginProperty, Animation);
                var Animation1 = new DoubleAnimation();
                Animation1.From = 1;
                Animation1.To = 0.1;
                Animation1.Duration = TimeSpan.FromSeconds(0.7);
                Animation1.Completed += new EventHandler(FromMainImageToSettings);
                MainImageGrid.BeginAnimation(OpacityProperty, Animation1);
                var Animation4 = new DoubleAnimation();
                Animation4.From = 0.1;
                Animation4.To = 1;
                Animation4.Duration = TimeSpan.FromSeconds(2.5);
                SettingsGrid.BeginAnimation(OpacityProperty, Animation4);
                var Animation3 = new ThicknessAnimation();
                Animation3.From = new Thickness(0, 264, 0, 0);
                Animation3.To = new Thickness(0, 303, 0, 0);
                Animation3.Duration = TimeSpan.FromSeconds(0.4);
                ControlPanelGrid.BeginAnimation(MarginProperty, Animation3);
                Params.CurrentPage = "Settings";
            }
        }
        private void FromFiletoSettingsCompleted(object sender, EventArgs e)
        {
            ScanFileGrid.Visibility = Visibility.Hidden;
            SettingsGrid.Visibility = Visibility.Visible;
            var Animation = new DoubleAnimation();
            Animation.From = 0.2;
            Animation.To = 1;
            Animation.Duration = TimeSpan.FromSeconds(2.5);
            SettingsGrid.BeginAnimation(OpacityProperty, Animation);
            Params.CurrentPage = "Settings";
        }
        private void FromURLtoSettingsCompleted(object sender, EventArgs e)
        {
            ScanURLGrid.Visibility = Visibility.Hidden;
            SettingsGrid.Visibility = Visibility.Visible;
            var Animation = new DoubleAnimation();
            Animation.From = 0.2;
            Animation.To = 1;
            Animation.Duration = TimeSpan.FromSeconds(2.5);
            SettingsGrid.BeginAnimation(OpacityProperty, Animation);
            Params.CurrentPage = "Settings";
        }
        private void FromIPtoSettingsCompleted(object sender, EventArgs e)
        {
            IPInfoGrid.Visibility = Visibility.Hidden;
            SettingsGrid.Visibility = Visibility.Visible;
            var Animation = new DoubleAnimation();
            Animation.From = 0.2;
            Animation.To = 1;
            Animation.Duration = TimeSpan.FromSeconds(2.5);
            SettingsGrid.BeginAnimation(OpacityProperty, Animation);
            Params.CurrentPage = "Settings";
        }
        private void FromDomaintoSettingsCompleted(object sender, EventArgs e)
        {
            DomainInfoGrid.Visibility = Visibility.Hidden;
            SettingsGrid.Visibility = Visibility.Visible;
            var Animation = new DoubleAnimation();
            Animation.From = 0.2;
            Animation.To = 1;
            Animation.Duration = TimeSpan.FromSeconds(2.5);
            SettingsGrid.BeginAnimation(OpacityProperty, Animation);
            Params.CurrentPage = "Settings";
        }
        private void FromMainImageToSettings(object sender, EventArgs e)
        {
            MainImageGrid.Visibility = Visibility.Hidden;
            SettingsGrid.Visibility = Visibility.Visible;
        }
        private void OpenFile_Click()
        {
            dlg.ShowDialog();
            Params.path = dlg.FileName;
            Params.FileName = Convert.ToString(dlg.SafeFileName);
            textBox5.Text = Params.path;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFile_Click();
        }
        private string mdfivehash(string path)
        {
            try
            {
                using (FileStream fs = File.OpenRead(path))
                {
                    MD5 md5 = new MD5CryptoServiceProvider();
                    byte[] filebytes = new byte[fs.Length];
                    fs.Read(filebytes, 0, (int)fs.Length);
                    byte[] Sum = md5.ComputeHash(filebytes);
                    string result = BitConverter.ToString(Sum).Replace("-", String.Empty);
                    return result;
                }
            }
            catch
            {
                Params.Response = "UncorrectFilePath";
                return "";
            }
        }

        private void label6_Copy3_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void label6_Copy3_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void label6_Copy3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://hft.zzz.com.ua");
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Thread th = new Thread(ScanFileGettingResults);
                th.Start();
                th.Join();
                if (Params.Response == "[]")
                {
                    MessageBox.Show("Не вказаний шлях до файлу/Вказаний, але некоректно", "Помилка");
                }
                else if (Params.Response == "invalid token error")
                {

                }
                else if (Params.path == "UncorrectFilePath")
                {
                    MessageBox.Show("Неправильно вказаний шлях до файлу", "Помилка");
                }
                else if (Params.Response == "{\"response_code\": 0, \"resource\": \""+mdfivehash(Params.path)+"\", \"verbose_msg\": \"The requested resource is not among the finished, queued or pending scans\"}")
                {
                    MessageBox.Show("Файл відсутній в базі.Ви можете перевірити файл самостійно, на сайті VirusTotal.com", "Помилка");
                }
                else
                {
                    label2_Copy.Content = Params.total;
                    label2_Copy2.Content = Params.positives;
                    label2_Copy4.Content = Params.PastScanDate + " - дата останнього сканування";
                    var Animation1 = new DoubleAnimation();
                    Animation1.From = 0.2;
                    Animation1.To = 1;
                    Animation1.Duration = TimeSpan.FromSeconds(1.5);
                    ScanFileResultsGrid.BeginAnimation(OpacityProperty, Animation1);
                    ScanFileResultsGrid.Visibility = Visibility.Visible;
                }
            }
            catch
            {
                MessageBox.Show("На жаль, щось пішло не так(", "Непередбачувана помилка");
            }
        }
        private void ScanFileGettingResults()
        {
            var urlparams = new RequestParams();
            HttpRequest Request = new HttpRequest();
            urlparams["apikey"] = Params.token;
            urlparams["resource"] = mdfivehash(Params.path);
            try
            {
                Params.Response = System.Convert.ToString(Request.Post("https://www.virustotal.com/vtapi/v2/file/report", urlparams));
            }
            catch
            {
                MessageBox.Show("Некоректно вказаний ключ api, буде встановлено стандартне значення.", "Помилка");
                Params.token = Params.defaulttoken;
                Params.Response = "invalid token error";
            }
            try
            {
                Params.positives = System.Convert.ToInt32(Params.Response.Substring("\"positives\":", ","));
                Params.total = Convert.ToInt32(Params.Response.Substring("\"total\":", ","));
            }
            catch
            {

            }
            Params.PastScanDate = Params.Response.Substring("\"scan_date\": \"", "\",");
            Params.MoreInformationFile = Params.Response.Substring("\"permalink\": \"", "\"");
        }

        private void label2_Copy5_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void label2_Copy5_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void label2_Copy5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(Params.MoreInformationFile);
        }

        private void label2_Copy15_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void label2_Copy15_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void label2_Copy15_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(Params.MoreInformationURL);
        }

        private void ScanURLButton_Click(object sender, RoutedEventArgs e)
        {
            Params.scanurl = textBox1.Text;
            try
            {
                Thread th = new Thread(URLScanGettingResults);
                th.Start();
                th.Join();
                if (Params.Response == "[]")
                {
                    MessageBox.Show("Не вказана адреса сайту", "Помилка");
                }
                else if (Params.Response == "invalid token error")
                {

                }
                else if (Params.Response == "{\"response_code\": 0, \"resource\": \""+Params.scanurl+"\", \"verbose_msg\": \"Resource does not exist in the dataset\"}")
                {
                    MessageBox.Show("Адреса сайту вказана некоректно", "Помилка");
                }
                else
                {
                    label2_Copy10.Content = Params.total;
                    label2_Copy12.Content = Params.positives;
                    label2_Copy14.Content = Params.PastScanDate + " - дата останнього сканування";
                    var Animation1 = new DoubleAnimation();
                    Animation1.From = 0.2;
                    Animation1.To = 1;
                    Animation1.Duration = TimeSpan.FromSeconds(1.5);
                    ScanURLResultsGrid.BeginAnimation(OpacityProperty, Animation1);
                    ScanURLResultsGrid.Visibility = Visibility.Visible;
                }
            }
            catch
            {
                MessageBox.Show("На жаль, щось пішло не так(", "Непередбачувана помилка");
            }
        }
        private void URLScanGettingResults()
        {
            var urlparams = new RequestParams();
            HttpRequest Request = new HttpRequest();
            urlparams["apikey"] = Params.token;
            urlparams["resource"] = Params.scanurl;
            try
            {
                Params.Response = System.Convert.ToString(Request.Post("http://www.virustotal.com/vtapi/v2/url/report", urlparams));
            }
            catch
            {
                MessageBox.Show("Некоректно вказаний ключ api, буде встановлено стандартне значення.", "Помилка");
                Params.token = Params.defaulttoken;
                Params.Response = "invalid token error";
            }
            try
            {
                Params.positives = System.Convert.ToInt32(Params.Response.Substring("\"positives\":", ","));
                Params.total = Convert.ToInt32(Params.Response.Substring("\"total\":", ","));
            }
            catch
            {

            }
            Params.PastScanDate = Params.Response.Substring("\"scan_date\": \"", "\",");
            Params.MoreInformationURL = Params.Response.Substring("\"permalink\": \"", "\"");
        }

        private void DaleeIPButton_Click(object sender, RoutedEventArgs e)
        {
            Params.IPforIPINFO = textBox2.Text;
            try
            {
                Thread th = new Thread(IPGettingResults);
                th.Start();
                th.Join();
                if (Params.Response == "{\"response_code\": -1, \"verbose_msg\": \"Invalid IP address\"}" | Params.Response == "" | Params.Response == "{\"response_code\": 0, \"verbose_msg\": \"Missing IP address\"}" | Params.Response == "invalid token error")
                {
                    MessageBox.Show("Неправильно вказана IP адреса", "Помилка");
                }
                else
                {
                    label3_Copy11.Content = "Країна: " + Params.Country;
                    label3_Copy13.Content = "Власник: " + Params.Owner;
                    var Animation1 = new DoubleAnimation();
                    Animation1.From = 0.2;
                    Animation1.To = 1;
                    Animation1.Duration = TimeSpan.FromSeconds(1.5);
                    IPInfoResultsGrid.BeginAnimation(OpacityProperty, Animation1);
                    IPInfoResultsGrid.Visibility = Visibility.Visible;
                }
            }
            catch
            {
                MessageBox.Show("На жаль, щось пішло не так(", "Непередбачувана помилка");
            }
        }
        private void IPGettingResults()
        {
            var urlparams = new RequestParams();
            HttpRequest Request = new HttpRequest();
            urlparams["ip"] = Params.IPforIPINFO;
            urlparams["apikey"] = Params.token;
            try
            {
                Params.Response = Convert.ToString(Request.Get("https://www.virustotal.com/vtapi/v2/ip-address/report", urlparams));
            }
            catch
            {
                MessageBox.Show("Некоректно вказаний ключ api, буде встановлено стандартне значення.", "Помилка");
                Params.token = Params.defaulttoken;
                Params.Response = "invalid token error";
            }
            Params.Country = Params.Response.Substring("\"country\": \"", "\"");
            Params.Owner = Params.Response.Substring("\"as_owner\": \"", "\"");
        }
        private void DaleeDomainButton_Click(object sender, RoutedEventArgs e)
        {
            Params.DomainforDomainINFO = textBox3.Text;
            try
            {
                Thread th = new Thread(DomainGettingResults);
                th.Start();
                th.Join();
                if (Params.Response == "{\"response_code\": 0, \"verbose_msg\": \"Domain not found\"}" | Params.Response == "" | Params.Response == "invalid token error")
                {
                    MessageBox.Show("Домен не знайдено", "Помилка");
                }
                else
                {
                    if (Params.DomainCity == "")
                    {
                        label4_Copy2.Content = "Місто: " + "Не вказано.";
                    }
                    else if (Params.DomainCity != "")
                    {
                        label4_Copy2.Content = "Місто: " + Params.DomainCity;
                    }
                    if (Params.DomainOrganization == "")
                    {
                        label4_Copy13.Content = "Організація: " + "Не вказано.";
                    }
                    else if (Params.DomainOrganization != "")
                    {
                        label4_Copy13.Content = "Організація: " + Params.DomainOrganization;
                    }
                    if (Params.DomainCountry == "")
                    {
                        label4_Copy1.Content = "Країна: " + "Не вказано.";
                    }
                    else if (Params.DomainCountry != "")
                    {
                        label4_Copy1.Content = "Країна: " + Params.DomainCountry;
                    }
                    label4_Copy11.Content = "ip адреса: " + Params.DomainIPAdress;
                    label4_Copy3.Content = "Категорії: " + Params.DomainCategories;
                    var Animation1 = new DoubleAnimation();
                    Animation1.From = 0.2;
                    Animation1.To = 1;
                    Animation1.Duration = TimeSpan.FromSeconds(1.5);
                    DomainInfoResultsGrid.BeginAnimation(OpacityProperty, Animation1);
                    DomainInfoResultsGrid.Visibility = Visibility.Visible;
                    textBox3.Text = "";
                }
            }
            catch
            {
                MessageBox.Show("На жаль, щось пішло не так(", "Непередбачувана помилка");
            }
        }
        private void DomainGettingResults()
        {
            var urlparams = new RequestParams();
            HttpRequest Request = new HttpRequest();
            urlparams["domain"] = Params.DomainforDomainINFO;
            urlparams["apikey"] = Params.token;
            try
            {
                Params.Response = Convert.ToString(Request.Get("http://www.virustotal.com/vtapi/v2/domain/report", urlparams));
            }
            catch
            {
                MessageBox.Show("Некоректно вказаний ключ api, буде встановлено стандартне значення.", "Помилка");
                Params.token = Params.defaulttoken;
                Params.Response = "invalid token error";
            }
            Params.DomainCountry = Params.Response.Substring("country:", @"\");
            Params.DomainCity = Params.Response.Substring("city:", @"\");
            Params.DomainCategories = Params.Response.Substring("\"categories\": [\"", "\"");
            Params.DomainIPAdress = Params.Response.Substring("\"ip_address\": \"", "\"");
            Params.DomainOrganization = Params.Response.Substring("organization:", @"\");
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://hft.zzz.com.ua/Downloads/CrystalCloudScanner2UKR.zip");
        }

        private void ChangeAPIkeyButton_Click(object sender, RoutedEventArgs e)
        {
            Params.token = textBox4.Text;
        }
    }
    }
