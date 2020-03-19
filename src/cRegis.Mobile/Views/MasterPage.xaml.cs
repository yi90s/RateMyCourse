using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cReg_Mobile.ViewModels;
using cReg_Mobile.Objects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;

namespace cReg_Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        public List<MasterPageContent> menuList { get; set; }


        public MasterPage()
        {
            InitializeComponent();
            menuList = new List<MasterPageContent>();
            // Adding menu items to menuList and you can define title ,page and icon
            menuList.Add(new MasterPageContent() { Title = "Profile", TargetType = typeof(ProfilePage) });
            menuList.Add(new MasterPageContent() { Title = "Course", TargetType = typeof(CoursePage) });
            menuList.Add(new MasterPageContent() { Title = "History", TargetType = typeof(HistoryPage) });
            menuList.Add(new MasterPageContent() { Title = "Wishlist", TargetType = typeof(WishlistPage) });
            menuList.Add(new MasterPageContent() { Title = "LogOut", TargetType = typeof(MainPage) });
            // Setting our list to be ItemSource for ListView in MainPage.xaml

            navigationDrawerList.ItemsSource = menuList;
            // Initial navigation, this can be used for our home page
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ProfilePage)));
        }


        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageContent)e.SelectedItem;
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