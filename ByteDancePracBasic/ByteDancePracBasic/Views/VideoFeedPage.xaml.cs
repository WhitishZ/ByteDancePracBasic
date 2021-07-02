using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ByteDancePracBasic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoFeedPage : ContentPage
    {
        public VideoFeedPage()
        {
            InitializeComponent();
            FeedListView.ItemTapped += (object sender, ItemTappedEventArgs e) => {
                // Disable selection highlight.
                if (sender is ListView lv) lv.SelectedItem = null;
                // Do something with the selection.
            };
        }
    }
}