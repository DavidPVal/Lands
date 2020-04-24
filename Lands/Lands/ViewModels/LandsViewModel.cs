namespace Lands.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class LandsViewModel : BaseViewModel
    {
        #region Servicios
        private ApiService apiService;
        #endregion

        #region Atributos
        private ObservableCollection<LandItemViewModel> lands;
        private bool isRefreshing;
        private string filter;
        private List<Lands> landList;
        #endregion

        #region Propiedades
        public string Filter
        {
            get { return filter; }
            set
            {
                SetValue(ref filter, value);
                Search();
            }
        }
        public ObservableCollection<LandItemViewModel> Lands
        {
            get { return lands; }
            set { SetValue(ref lands, value); }
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetValue(ref isRefreshing, value); }
        }        
        #endregion

        #region Costructores
        public LandsViewModel()
        {
            this.apiService = new ApiService();
            LoadLands();
        }
        #endregion

        #region Metodos
        private async void LoadLands()
        {
            this.IsRefreshing = true;
            var connection = await apiService.CheckConnection();

            if (!connection.IsSucces)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await this.apiService.GetList<Lands>(
                "https://restcountries.eu",
                "/rest",
                "/v2/all");

            if (!response.IsSucces)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }
            
            landList = (List<Lands>)response.Result;
            this.Lands = new ObservableCollection<LandItemViewModel>(this.ToLandItemViewModel());
            this.IsRefreshing = false;
        }
        #endregion

        #region Comandos
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadLands);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(Filter))
            {
                this.Lands = new ObservableCollection<LandItemViewModel>(
                    ToLandItemViewModel());
            }
            else
            {
                this.Lands = new ObservableCollection<LandItemViewModel>(
                    ToLandItemViewModel().Where(
                        l => l.Name.ToLower().Contains(Filter.ToLower()) || 
                             l.Capital.ToLower().Contains(Filter.ToLower())));
            }
        }
        #endregion

        #region Metodos
        private IEnumerable<LandItemViewModel> ToLandItemViewModel()
        {
            return this.landList.Select(l => new LandItemViewModel
            {
                Name = l.Name,
                TopLevelDomain = l.TopLevelDomain,
                Alpha2Code = l.Alpha2Code,
                Alpha3Code = l.Alpha3Code,
                CallingCodes = l.CallingCodes,
                Capital = l.Capital,
                AltSpellings = l.AltSpellings,
                Region = l.Region,
                Subregion = l.Subregion,
                Population = l.Population,
                Latlng = l.Latlng,
                Demonym = l.Demonym,
                Area = l.Area,
                Gini = l.Gini,
                Timezones = l.Timezones,
                Borders = l.Borders,
                NativeName = l.NativeName,
                NumericCode = l.NumericCode,
                Currencies = l.Currencies,
                Languages = l.Languages,
                Translations = l.Translations,
                Flag = l.Flag,
                RegionalBlocs = l.RegionalBlocs,
                Cioc = l.Cioc,
            });
        }
        #endregion 
    }
}
