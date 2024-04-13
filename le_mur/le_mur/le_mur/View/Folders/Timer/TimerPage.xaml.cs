using le_mur.ViewModel.Folders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using le_mur.ViewModel.Folders.Timer;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace le_mur.View.Folders.Timer
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TimerPage : ContentPage
	{
		public TimerPage()
		{
			InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = new TimerViewModel() { Navigation = this.Navigation };
        }

        public TimerPage(int id)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = new TimerViewModel(id) { Navigation = this.Navigation };
        }
    }
}