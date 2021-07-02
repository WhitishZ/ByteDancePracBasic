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
    public partial class ImageFeedPageWithAnimation : ContentPage
    {
        public ImageFeedPageWithAnimation()
        {
            InitializeComponent();

            FeedListView.ItemTapped += (object sender, ItemTappedEventArgs e) => {
                // Disable selection highlight.
                if (sender is ListView lv) lv.SelectedItem = null;
                // Do something with the selection.
            };
        }

        private async void OnLikeFeedClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Grid parentGrid = button.Parent as Grid;
            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);
            StackLayout likeFeedStackLayout = (StackLayout)parentGrid.Children.First(c => Grid.GetRow(c) == row && Grid.GetColumn(c) == col
                && c is StackLayout);
            Image likeFeedImage = (Image)likeFeedStackLayout.Children.First(c => c is Image);
            await likeFeedImage.ScaleTo(1.5, 125);
            await likeFeedImage.ScaleTo(1, 125);
        }
    }
}