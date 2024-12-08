using SbucheaTudorLab7.Models;

namespace SbucheaTudorLab7;

public partial class ListPage : ContentPage
{
    public ListPage()
    {
        InitializeComponent();
    }

    // Salvarea listei de cumparaturi
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }

    // Stergerea listei de cumparaturi
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }

    // Navigare catre ProductPage pentru selectarea produselor
    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext)
        {
            BindingContext = new Product()
        });
    }

    // Stergerea unui produs din lista
    async void OnDeleteItemClicked(object sender, EventArgs e)
    {
        if (listView.SelectedItem != null)
        {
            var selectedProduct = listView.SelectedItem as Product;
            if (selectedProduct != null)
            {
                var shopList = (ShopList)BindingContext;

                // Cream o instanta a produsului asociat si il stergem
                var listProduct = new ListProduct
                {
                    ShopListID = shopList.ID,
                    ProductID = selectedProduct.ID
                };

                await App.Database.DeleteListProductAsync(listProduct);

                // Reincarca produsele din listView
                listView.ItemsSource = await App.Database.GetListProductsAsync(shopList.ID);
            }
        }
    }

    // Actualizarea listei de produse la afisarea paginii
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (ShopList)BindingContext;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }
}
