using le_mur.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace le_mur.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoldersPage : ContentPage
    {
        public FoldersPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = new FoldersViewModel() { Navigation = this.Navigation };
        }
    }
}