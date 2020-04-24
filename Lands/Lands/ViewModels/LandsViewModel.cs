namespace Lands.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class LandsViewModel : BaseViewModel
    {
        #region Servicios
        private ApiService apiService;
        #endregion
        #region Atributos
        private ObservableCollection<Lands> lands;
        #endregion

        #region Propiedades
        public ObservableCollection<Lands> Lands
        {
            get { return lands; }
            set { SetValue(ref lands, value); }
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
            var connection = await apiService.CheckConnection();

            if (!connection.IsSucces)
            {
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

            var lista = (List<Lands>)response.Result;
            this.Lands = new ObservableCollection<Lands>(lista);
        }
        #endregion
    }
}
