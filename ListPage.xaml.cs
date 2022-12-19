namespace Neagoe_Eliza_Lab7;
using Neagoe_Eliza_Lab7.Models;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    } 
    async void OnDeleteItemClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        Product p;
      
            p = listView.SelectedItem as Product;
            var lp = new ListProduct()
            {
                ShopListID = slist.ID,
                ProductID = p.ID
            };
            await App.Database.DeleteListProductAsync(lp);
            p.ListProducts = new List<ListProduct> { lp };
        }
    
   
    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)
       this.BindingContext)
        {
            BindingContext = new Product()
        });

    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (ShopList)BindingContext;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }
}