using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Listview
{
    public partial class MainPage : ContentPage
    {
        Button tele, euro;
        public MainPage()
        {
            tele = new Button
            {
                Text = "Telefonide listview",
                BackgroundColor = Color.LightGray,
                CornerRadius = 50,
                FontSize = 20,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            tele.Clicked += Tele_Clicked;
            euro = new Button
            {
                Text = "Euroopa listview",
                BackgroundColor = Color.LightBlue,
                CornerRadius = 50,
                FontSize = 20,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            euro.Clicked += Tele_Clicked;
            StackLayout stackLayout = new StackLayout
            {
                Children = { tele, euro }
            };
            Content = stackLayout;
        }

        private async void Tele_Clicked(object sender, EventArgs e)
        {
            if (sender == tele)
            {
                await Navigation.PushAsync(new List_Telefon());
            }
            else if (sender == euro)
            {
                await Navigation.PushAsync(new List_Europa());
            }
        }
    }
}
