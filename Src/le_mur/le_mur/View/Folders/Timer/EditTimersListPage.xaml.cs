using le_mur.ViewModel.Folders.Timer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace le_mur.View.Folders.Timer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTimersListPage : ContentPage
    {
        public EditTimersListPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = new EditTimersListViewModel() { Navigation = this.Navigation };
        }
    }
}