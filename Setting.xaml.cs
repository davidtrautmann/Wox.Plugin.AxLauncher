using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Wox.Plugin.AxLauncher
{
    /// <summary>
    ///  Interactive logic for Setting.xaml
    /// </summary>
    public partial class Setting : UserControl
    {
        public Setting()
        {
            InitializeComponent();

            Loaded += Setting_Loaded;
        }

        void Setting_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            tbAxcPath.Text = AxLauncher.settings.axcPath;
            tbAxcPath.TextChanged += (o, ex) =>
            {
                AxLauncher.settings.axcPath = tbAxcPath.Text;
                AxLauncher.saveSettings();
            };
        }
    }
}
