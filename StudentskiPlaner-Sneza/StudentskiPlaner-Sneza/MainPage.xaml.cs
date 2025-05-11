using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudentskiPlanerSneza 
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnToggleThemeClicked(object sender, EventArgs e)
        {
            App.IsDarkTheme = !App.IsDarkTheme;
            App.ApplyTheme(App.IsDarkTheme);
        }
    }
}
