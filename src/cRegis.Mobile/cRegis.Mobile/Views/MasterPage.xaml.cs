using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cRegis.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;

namespace cRegis.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        public List<MasterPageModel> menuList { get; set; }


        public MasterPage()
        {
            InitializeComponent();
            menuList = new List<MasterPageModel>();
            // Adding menu items to menuList and you can define title ,page and icon
            menuList.Add(new MasterPageModel() { Title = "Profile", TargetType = typeof(ProfilePage) });
            menuList.Add(new MasterPageModel() { Title = "Course", TargetType = typeof(CoursePage) });
            menuList.Add(new MasterPageModel() { Title = "History", TargetType = typeof(HistoryPage) });
            menuList.Add(new MasterPageModel() { Title = "Wishlist", TargetType = typeof(WishlistPage) });
            menuList.Add(new MasterPageModel() { Title = "LogOut", TargetType = typeof(MainPage) });
            // Setting our list to be ItemSource for ListView in MainPage.xaml

            navigationDrawerList.ItemsSource = menuList;
            // Initial navigation, this can be used for our home page
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ProfilePage)));
        }


        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageModel)e.SelectedItem;
            Type page = item.TargetType;
            if (item.Title != "LogOut")
                Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            else
            {
                DisplayAlert("Log Out", "Log Out Success", "Okay");
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
            IsPresented = false;
        }
    }
}   