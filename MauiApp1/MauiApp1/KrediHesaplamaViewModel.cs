using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null, Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
    }
    public class KrediHesaplamaViewModel : BaseViewModel
    {
        private string _selectedLoanType;
        private string _interestRate;
        private decimal _loanAmount;
        private int _loanTerm;
        private decimal _monthlyPayment;
        private decimal _totalPayment;
        private decimal _totalInterest;

        public ICommand SelectLoanTypeCommand { get; }
        public ICommand CalculateCommand { get; }

        public string SelectedLoanType
        {
            get => _selectedLoanType;
            set => SetProperty(ref _selectedLoanType, value);
        }

        public string InterestRate
        {
            get => _interestRate;
            set => SetProperty(ref _interestRate, value);
        }

        public decimal LoanAmount
        {
            get => _loanAmount;
            set => SetProperty(ref _loanAmount, value);
        }

        public int LoanTerm
        {
            get => _loanTerm;
            set => SetProperty(ref _loanTerm, value);
        }

        public decimal MonthlyPayment
        {
            get => _monthlyPayment;
            set => SetProperty(ref _monthlyPayment, value);
        }

        public decimal TotalPayment
        {
            get => _totalPayment;
            set => SetProperty(ref _totalPayment, value);
        }

        public decimal TotalInterest
        {
            get => _totalInterest;
            set => SetProperty(ref _totalInterest, value);
        }

        public KrediHesaplamaViewModel()
        {
            SelectLoanTypeCommand = new Command(OnSelectLoanType);
            CalculateCommand = new Command(OnCalculate);
        }

        private async void OnSelectLoanType()
        {
            string loanType = await Application.Current.MainPage.DisplayActionSheet("Kredi Türünü Seçin", "Cancel", null, "Ýhtiyaç Kredisi", "Konut Kredisi", "Taþýt Kredisi", "Ticari Kredisi");

            SelectedLoanType = loanType;

            switch (loanType)
            {
                case "Ýhtiyaç Kredisi":
                    InterestRate = "10"; 
                    break;
                case "Konut Kredisi":
                    InterestRate = "8";  
                    break;
                case "Taþýt Kredisi":
                    InterestRate = "5";  
                    break;
                case "Ticari Kredisi":
                    InterestRate = "5";  
                    break;
                default:
                    InterestRate = "0";
                    break;
            }
        }

        private void OnCalculate()
        {
            decimal oran = decimal.Parse(InterestRate); 
            decimal bsmv = GetBSMV(SelectedLoanType); 
            decimal kkdf = GetKKDF(SelectedLoanType);
            //vade = LoanTerm
            //tutar = LoanAmount
            //oran = InterestRate
            decimal brutFaiz = ((oran + (oran * bsmv) + (oran * kkdf)) / 100);
            MonthlyPayment = (((decimal)Math.Pow(1 + (double)brutFaiz, LoanTerm) * brutFaiz) / ((decimal)Math.Pow(1 + (double)brutFaiz, LoanTerm) - 1)) * LoanAmount;
           // MonthlyPayment = ((decimal)(Math.Pow(double)(1 + brutFaiz,(LoanTerm)*brutFaiz)/(Math.Pow(1+brutFaiz,LoanTerm) - 1))* LoanAmount;
            // MonthlyPayment = (LoanAmount * brutFaiz * (decimal)Math.Pow((double)(1 + brutFaiz), LoanTerm)) / ((decimal)Math.Pow((double)(1 + brutFaiz), LoanTerm) - 1);

            TotalPayment = MonthlyPayment * LoanTerm;

            TotalInterest = TotalPayment-LoanAmount;
        }

        private decimal GetBSMV(string loanType) 
        {
            switch (loanType)
            {
                case "Ýhtiyaç Kredisi": return 10m;
                case "Konut Kredisi": return 0m;
                case "Taþýt Kredisi": return 5m;
                case "Ticari Kredisi": return 5m;
                default: return 0m;
            }
        }

        private decimal GetKKDF(string loanType)
        {
            switch (loanType)
            {
                case "Ýhtiyaç Kredisi": return 15m;
                case "Konut Kredisi": return 0m;
                case "Taþýt Kredisi": return 15m;
                case "Ticari Kredisi": return 0m;
                default: return 0m;
            }
        }
    }

}
