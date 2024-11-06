using System;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public class VucudKitleViewModel : BaseViewModel
    {
        private double _height; 
        private double _weight; 
        private double _bmi;    

        public double Height
        {
            get => _height;
            set => SetProperty(ref _height, Math.Round(value)); 
        }

        public double Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, Math.Round(value, 1)); 
        }

        public double BMI
        {
            get => _bmi;
            set => SetProperty(ref _bmi, value);
        }

        public ICommand CalculateBMICommand { get; }

        public VucudKitleViewModel()
        {
            CalculateBMICommand = new Command(OnCalculateBMI);
        }

        private async void OnCalculateBMI()
        {
            if (Weight > 0 && Height > 0)
            {
                double heightInMeters = Height / 100; 
                BMI = Weight / (heightInMeters * heightInMeters);

                
                string bmiCategory = GetBMICategory(BMI);

                await Application.Current.MainPage.DisplayAlert("Vücut Kitle İndeksi", bmiCategory, "Tamam");
            }
        }

        private string GetBMICategory(double bmi)
        {
            if (bmi < 16)
                return "İleri Düzeyde Zayıf";
            else if (bmi >= 16 && bmi <= 16.99)
                return "Orta Düzeyde Zayıf";
            else if (bmi >= 17 && bmi <= 18.49)
                return "Hafif Düzeyde Zayıf";
            else if (bmi >= 18.50 && bmi <= 24.9)
                return "Normal Kilolu";
            else if (bmi >= 25 && bmi <= 29.99)
                return "Hafif Şiman / Fazla Kilolu";
            else if (bmi >= 30 && bmi <= 34.99)
                return "1. Derecede Obez";
            else if (bmi >= 35 && bmi <= 39.99)
                return "2. Derecede Obez";
            else
                return "3. Derecede Obez / Morbid Obez";
        }
    }
}
