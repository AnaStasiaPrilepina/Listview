using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Listview
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class List_Europa : ContentPage
    {
        public ObservableCollection<Europa> evropa { get; set; }
        Label lbl;
        ListView list;
        Button add, del;
        public List_Europa()
        {
            //InitializeComponent();
            evropa = new ObservableCollection<Europa>
            {
                new Europa {Country = "Estonia", City = "Tallinn", Flag = "euro_flag.png"},
                new Europa {Country = "Finland", City = "Helsinki", Flag = "euro_flag.png"},
                new Europa {Country = "Latvia", City = "Riga", Flag = "euro_flag.png"},
            };

            lbl = new Label
            {
                Text = "Europe",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 30,
                TextColor = Color.Black,
            };

            list = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = evropa,
                ItemTemplate = new DataTemplate(() =>
                {
                    ImageCell imgcell = new ImageCell { TextColor = Color.Black, DetailColor = Color.DarkBlue };
                    imgcell.SetBinding(ImageCell.TextProperty, "Country");
                    Binding companyBinding = new Binding { Path = "City", StringFormat = "Stolica {0}" };
                    imgcell.SetBinding(ImageCell.DetailProperty, companyBinding);
                    imgcell.SetBinding(ImageCell.ImageSourceProperty, "Flag");
                    return imgcell;
                }),
            };
            list.ItemTapped += List_ItemTapped;

            add = new Button { Text = "Add country" };
            del = new Button { Text = "Delete country" };
            add.Clicked += Add_Clicked;
            del.Clicked += Del_Clicked;

            this.Content = new StackLayout { Children = { lbl, list, add, del } };
        }

        private void Del_Clicked(object sender, EventArgs e)
        {
            Europa strana = list.SelectedItem as Europa;
            if (strana != null)
            {
                evropa.Remove(strana);
                list.SelectedItem = null;
            }
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            string one = await DisplayPromptAsync("Question 1", "Country name");
            string two = await DisplayPromptAsync("Question 2", "City name");

            if (evropa.Any(p => p.Country.Equals(one, StringComparison.InvariantCultureIgnoreCase) || p.Country.Equals(two, StringComparison.InvariantCultureIgnoreCase)))
            {
                await DisplayAlert("Attention", "Strana ili Gorod uze v spiske", "OK");
            }
            else
            {
                evropa.Add(new Europa { Country = one, City = two, Flag = "load.png" });

            }
        }

        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Europa selectedmaa = e.Item as Europa;
            if (selectedmaa != null)
            {
                await DisplayAlert("Strana v Evrope", "Dopolnitelnaja informacija", "OK");
            }
        }
    }
}