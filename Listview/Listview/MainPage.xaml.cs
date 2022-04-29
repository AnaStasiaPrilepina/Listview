using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Listview
{
    public partial class MainPage : ContentPage
    {
        //public string[] phones { get; set; }
        public List<Telefon> telefons { get; set; }
        ListView list;
        Label lbl;
        public MainPage()
        {
            //InitializeComponent();
            //phones = new string[] { "iPhone", "Samsung galaxy", "Huawei", "LG", "Xiaomi" };
            telefons = new List<Telefon>
            {
                new Telefon {Nimetus = "Samsung Galaxy S22 Ultra", Tootja = "Samsung", Hind = "1349"},
                new Telefon {Nimetus = "Xiaomi Mi 11 Lite 5G", Tootja = "Xiaomi", Hind = "399"},
                new Telefon {Nimetus = "iPhone 13", Tootja = "Apple", Hind = "1179"},
                new Telefon {Nimetus = "Xiaomi Note 10S pro", Tootja = "Xiaomi", Hind = "389"},
            };
            //list = new ListView { ItemsSource = phones};
            //list.ItemSelected += List_ItemSelected;
            list = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = telefons,
                ItemTemplate = new DataTemplate(()=>
                {
                    Label nimetus = new Label { FontSize = 20 };
                    nimetus.SetBinding(Label.TextProperty, "Nimetus");


                    Label tootja = new Label ();
                    tootja.SetBinding(Label.TextProperty, "Tootja");


                    Label hind = new Label ();
                    hind.SetBinding(Label.TextProperty, "Hind");

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Vertical,
                            Children = { nimetus, tootja, hind }
                        }
                    };
                })
            };
            list.ItemTapped += List_ItemTapped;

            lbl = new Label
            {
                Text = "Telefonide loetelu",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 30,
                TextColor = Color.Black,
            };
            this.Content = new StackLayout { Children = { lbl, list } };
        }

        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Telefon selectedPhone = e.Item as Telefon;
            if (selectedPhone != null)
            {
                await DisplayAlert("Выбранная модель", $"{selectedPhone.Tootja} - {selectedPhone.Nimetus}", "OK");
            }
        }

        private void List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                lbl.Text = e.SelectedItem.ToString();
            }
        }
    }
}
