using Stir_Denisa_ProiectApp.Models;

async void OnChooseButtonClicked(object sender, EventArgs e)
{
    await Navigation.PushAsync(new ProductPage((Menu)
   this.BindingContext)
    {
        BindingContext = new Product()
    });

}

public partial class ListPage : ContentPage
{
    public ListPage()
    {
        InitializeComponent();
    }
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (Menu)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (Menu)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }
}
protected override async void OnAppearing()
{
    base.OnAppearing();
    var shopl = (Menu)BindingContext;

    listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
}