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
        //public string[] phones { get; set; }
        //public List<Telefon> telefons { get; set; }
        //public ObservableCollection<Telefon> telefons { get; set; }
        public ObservableCollection<Ruhm<string, Telefon>> telefonideruhmades { get; set; }
        ListView list;
        Label lbl;
        Button lisa, kustuta;
        public MainPage()
        {
            //InitializeComponent();
            //phones = new string[] { "iPhone", "Samsung galaxy", "Huawei", "LG", "Xiaomi" };
            //telefons = new List<Telefon>
            //{
            //    new Telefon {Nimetus = "Samsung Galaxy S22 Ultra", Tootja = "Samsung", Hind = "1349", Pilt = "samsung.jpg"},
            //    new Telefon {Nimetus = "Xiaomi Mi 11 Lite 5G", Tootja = "Xiaomi", Hind = "399", Pilt = "xiaomi.jfif"},
            //    new Telefon {Nimetus = "iPhone 13", Tootja = "Apple", Hind = "1179", Pilt = "iphone.jfif"},
            //    new Telefon {Nimetus = "Xiaomi Note 10S pro", Tootja = "Xiaomi", Hind = "389", Pilt = "xiaomis.jfif"},
            //};

            //telefons = new ObservableCollection<Telefon>
            //{
            //    new Telefon {Nimetus = "Samsung Galaxy S22 Ultra", Tootja = "Samsung", Hind = "1349", Pilt = "samsung.jpg"},
            //    new Telefon {Nimetus = "Xiaomi Mi 11 Lite 5G", Tootja = "Xiaomi", Hind = "399", Pilt = "xiaomi.jfif"},
            //    new Telefon {Nimetus = "iPhone 13", Tootja = "Apple", Hind = "1179", Pilt = "iphone.jfif"},
            //    new Telefon {Nimetus = "Xiaomi Note 10S pro", Tootja = "Xiaomi", Hind = "389", Pilt = "xiaomis.jfif"},
            //};

            var telefonid = new List<Telefon>
            {
                new Telefon {Nimetus = "Samsung Galaxy S22 Ultra", Tootja = "Samsung", Hind = "1349", Pilt = "samsung.jpg"},
                new Telefon {Nimetus = "Xiaomi Mi 11 Lite 5G", Tootja = "Xiaomi", Hind = "399", Pilt = "xiaomi.jfif"},
                new Telefon {Nimetus = "iPhone 13", Tootja = "Apple", Hind = "1179", Pilt = "iphone.jfif"},
                new Telefon {Nimetus = "Xiaomi Note 10S pro", Tootja = "Xiaomi", Hind = "389", Pilt = "xiaomis.jfif"},
            };
            var ruhmad = telefonid.GroupBy(p => p.Tootja).Select(g => new Ruhm<string, Telefon>(g.Key, g));
            telefonideruhmades = new ObservableCollection<Ruhm<string, Telefon>>(ruhmad);

            //list = new ListView { ItemsSource = phones};
            //list.ItemSelected += List_ItemSelected;
            list = new ListView
            {
                SeparatorColor = Color.Orange,
                //Header = "Minu oma kollektsioon:",
                Header = "Telefonid ruhmades",
                //Footer = DateTime.Now.ToString("g"),
                Footer = DateTime.Now.ToString("T"), 
                HasUnevenRows = true,

                ItemsSource = telefonideruhmades,
                IsGroupingEnabled = true,
                GroupHeaderTemplate = new DataTemplate(() =>
                {
                    Label tootja = new Label();
                    tootja.SetBinding(Label.TextProperty, "Nimetus");
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Vertical,
                            Children = { tootja }
                        }
                    };
                }),
                ItemTemplate = new DataTemplate(() =>
                {
                    Label nimetus = new Label { FontSize = 20 };
                    nimetus.SetBinding(Label.TextProperty, "Nimetus");
                    Label hind = new Label();
                    hind.SetBinding(Label.TextProperty, "Hind");
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Vertical,
                            Children = { nimetus, hind }
                        }
                    };
                }),

                //ItemsSource = telefons,
                //ItemTemplate = new DataTemplate(()=>
                //{
                //    Label nimetus = new Label { FontSize = 20 };
                //    nimetus.SetBinding(Label.TextProperty, "Nimetus");
                //    Label tootja = new Label ();
                //    tootja.SetBinding(Label.TextProperty, "Tootja");
                //    Label hind = new Label ();
                //    hind.SetBinding(Label.TextProperty, "Hind");
                //    return new ViewCell
                //    {
                //        View = new StackLayout
                //        {
                //            Padding = new Thickness(0, 5),
                //            Orientation = StackOrientation.Vertical,
                //            Children = { nimetus, tootja, hind }
                //        }
                //    };
                //})

                //ItemTemplate = new DataTemplate(() =>
                //{
                //    ImageCell imgcell = new ImageCell { TextColor = Color.Red, DetailColor = Color.Green };
                //    imgcell.SetBinding(ImageCell.TextProperty, "Nimetus");
                //    Binding companyBinding = new Binding { Path = "Tootja", StringFormat = "Tore telefon firmalt {0}" };
                //    imgcell.SetBinding(ImageCell.DetailProperty, companyBinding);
                //    imgcell.SetBinding(ImageCell.ImageSourceProperty, "Pilt");
                //    return imgcell;
                //})
            };
            list.ItemTapped += List_ItemTapped;

            lbl = new Label
            {
                Text = "Telefonide loetelu",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 30,
                TextColor = Color.Black,
            };

            lisa = new Button { Text = "Lisa telefon" };
            kustuta = new Button { Text = "Kustuta telefon" };
            lisa.Clicked += Lisa_Clicked;
            kustuta.Clicked += Kustuta_Clicked;

            this.Content = new StackLayout { Children = { lbl, list, lisa, kustuta } };
        }

        private void Kustuta_Clicked(object sender, EventArgs e)
        {
            Telefon phone = list.SelectedItem as Telefon;
            if(phone != null)
            {
                //telefons.Remove(phone);
                list.SelectedItem = null;
            }
        }

        private void Lisa_Clicked(object sender, EventArgs e)
        {
            //telefons.Add(new Telefon { Nimetus = "telefon", Tootja = "Tootja", Hind = "1" });
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
