namespace MauiApp1
{
    public partial class KrediHesaplama : ContentPage
    {
        public KrediHesaplama()
        {
            InitializeComponent(); 
            BindingContext = new KrediHesaplamaViewModel();
        }
    }
}
