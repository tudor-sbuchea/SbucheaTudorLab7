using System;
using SbucheaTudorLab7.Data;
using System.IO;


namespace SbucheaTudorLab7
{
    public partial class App : Application
    {
        static ShopListDatabase database;
        public static ShopListDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new
                   ShopListDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.
                   LocalApplicationData), "ShoppingList.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
