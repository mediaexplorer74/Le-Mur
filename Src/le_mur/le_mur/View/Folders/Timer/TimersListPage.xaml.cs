using le_mur.ViewModel.Folders.Timer;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace le_mur.View.Folders.Timer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimersListPage : ContentPage
    {
        public TimersListPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = new TimersListViewModel() { Navigation = this.Navigation };
        }
    }
}