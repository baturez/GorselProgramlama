using Microsoft.Maui.Controls;
using System;
using System.Windows.Input;

namespace MauiApp1
{
    public class RenkSeciciViewModel : BaseViewModel
    {
        private double _red;
        private double _green;
        private double _blue;
        private string _hexColor;
        private Color _colorPreview;

        public double Red
        {
            get => _red;
            set
            {
                if (SetProperty(ref _red, value))
                    UpdateColor();
            }
        }

        public double Green
        {
            get => _green;
            set
            {
                if (SetProperty(ref _green, value))
                    UpdateColor();
            }
        }

        public double Blue
        {
            get => _blue;
            set
            {
                if (SetProperty(ref _blue, value))
                    UpdateColor();
            }
        }

        public string HexColor
        {
            get => _hexColor;
            set => SetProperty(ref _hexColor, value);
        }

        public Color ColorPreview
        {
            get => _colorPreview;
            set => SetProperty(ref _colorPreview, value);
        }

        public ICommand GenerateRandomColorCommand { get; }
        public ICommand CopyColorCommand { get; }

        public RenkSeciciViewModel()
        {
            GenerateRandomColorCommand = new Command(GenerateRandomColor);
            CopyColorCommand = new Command(CopyColorToClipboard);
        }

        private void UpdateColor()
        {
            ColorPreview = Color.FromRgb((int)Red, (int)Green, (int)Blue);
            HexColor = $"#{((int)Red):X2}{((int)Green):X2}{((int)Blue):X2}";
        }

        private void GenerateRandomColor()
        {
            Random random = new Random();
            Red = random.Next(0, 256);
            Green = random.Next(0, 256);
            Blue = random.Next(0, 256);
        }

        private async void CopyColorToClipboard()
        {
            await Clipboard.SetTextAsync(HexColor);

            await Application.Current.MainPage.DisplayAlert("Bilgi", "Kopyalandı", "Tamam");
        }
    }
}
