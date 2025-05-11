using System;
using StudentskiPlanerSneza;
using StudentskiPlanerSneza.Data;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.IO;






namespace StudentskiPlanerSneza
{
    public partial class App : Application
    {
        static Database database;
        public static string DbFileName = "tasks.db3";

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DbFileName);
                    database = new Database(dbPath);
                }
                return database;
            }
        }
        private static ResourceDictionary LightTheme = new ResourceDictionary
    {
        { "PageBackgroundColor", Color.White },
        { "CardBackgroundColor", Color.FromHex("#EEEEEE") },
        { "TitleTextColor", Color.FromHex("#C2185B") },
        { "BodyTextColor", Color.Black },
        { "AccentTextColor", Color.Red },
        { "PrimaryColor", Color.FromHex("#E91E63") },
        { "ButtonTextColor", Color.White },
        { "ToolbarBackgroundColor", Color.FromHex("#F8BBD0") }, 

        { "ToolbarItemColor", Color.FromHex("#000000") } 
};


        private static ResourceDictionary DarkTheme = new ResourceDictionary
    {
        { "PageBackgroundColor", Color.FromHex("#121212") },
        { "CardBackgroundColor", Color.FromHex("#1E1E1E") },
        { "TitleTextColor", Color.FromHex("#FF4081") },
        { "BodyTextColor", Color.White },
        { "AccentTextColor", Color.FromHex("#F06292") },
        { "PrimaryColor", Color.FromHex("#880E4F") },
        { "ButtonTextColor", Color.White },
        { "ToolbarBackgroundColor", Color.FromHex("#880E4F") },
        { "ToolbarItemColor", Color.FromHex("#FFFFFF") }
    };

        public static bool IsDarkTheme = false;



        public App()
        {




            InitializeComponent();
            ApplyTheme(IsDarkTheme);
           



            var baza = Database;

            Task.Run(async () =>
            {
                var postojeZadaci = await baza.GetItemsAsync();

                if (postojeZadaci.Count == 0)
                {
                    var zadaci = new List<Models.Zadaci>
            {
                new Models.Zadaci
                {
                    Naziv = "Učiti za ispit",
                    Opis = "Spremiti SQL upite",
                    Datum = DateTime.Now.AddDays(1),
                    Prioritet = Models.PrioritetZadatka.Visok,
                    Zavrsen = false,
                    PodsetnikUkljucen = false
                },
                new Models.Zadaci
                {
                    Naziv = "Napraviti prezentaciju",
                    Opis = "Završiti slajdove za seminar",
                    Datum = DateTime.Now.AddDays(2),
                    Prioritet = Models.PrioritetZadatka.Srednji,
                    Zavrsen = false,
                    PodsetnikUkljucen = true
                },
                new Models.Zadaci
                {
                    Naziv = "Vežbati kodiranje",
                    Opis = "Rad na zadacima iz OOP-a",
                    Datum = DateTime.Now.AddDays(3),
                    Prioritet = Models.PrioritetZadatka.Nizak,
                    Zavrsen = true,
                    PodsetnikUkljucen = false
                }
            };

                    foreach (var zad in zadaci)
                    {
                        await baza.SaveItemAsync(zad);
                    }
                }
            }).Wait();

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = (Color)Application.Current.Resources["ToolbarBackgroundColor"],
                BarTextColor = (Color)Application.Current.Resources["ToolbarItemColor"]
            };



        }
        public static void ApplyTheme(bool darkMode)
        {
            Application.Current.Resources.MergedDictionaries.Clear();

            if (darkMode)
                Application.Current.Resources.MergedDictionaries.Add(DarkTheme);
            else
                Application.Current.Resources.MergedDictionaries.Add(LightTheme);
        }








    }
}
