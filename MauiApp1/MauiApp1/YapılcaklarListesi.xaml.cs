namespace MauiApp1
{
    public partial class YapılcaklarListesi : ContentPage
    {
        public YapılcaklarListesi()
        {  
            InitializeComponent(); 
            BindingContext = new YapilacaklarListesiViewModel();

        }
    }
}
