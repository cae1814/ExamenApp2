using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExamenApp2.Models;
using ExamenApp2.Services;
using ExamenApp2.Views;
using System.Collections.ObjectModel;

namespace ExamenApp2.ViewModels
{
    public partial class ProveedorMainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Proveedor> proveedorCollection = new ObservableCollection<Proveedor>();

        private readonly ProveedorService _proveedorService;

        public ProveedorMainPageViewModel()
        {
            _proveedorService = new ProveedorService();
        }

        /// <summary>
        /// Obtiene el listado de Proveedores
        /// </summary>
        public void GetAll()
        {
            var getAll = _proveedorService.GetAll();

            if (getAll?.Count > 0)
            {
                ProveedorCollection.Clear();
                foreach (var proveedor in getAll)
                {
                    ProveedorCollection.Add(proveedor);
                }
            }
        }

        /// <summary>
        /// Redirecciona al formulario de Proveedores
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task GoToAddProveedorPage()
        {
            await App.Current!.MainPage!.Navigation.PushAsync(new AddProveedorPage());
        }

        /// <summary>
        /// Selecciona el registro para editar o eliminar
        /// </summary>
        /// <param name="Proveedor">Objeto a editar o eliminar</param>
        /// <returns>Actualizar: Nos lleva al formulario de proveedores, Eliminar: Elimina el registro</returns>
        [RelayCommand]
        private async Task SelectProveedor(Proveedor proveedor)
        {
            try
            {
                string res = await App.Current!.MainPage!.DisplayActionSheet("Acción", "Salir", null, "Actualizar", "Eliminar");

                switch (res)
                {
                    case "Actualizar":
                        await App.Current.MainPage.Navigation.PushAsync(new AddProveedorPage(proveedor));
                        break;
                    case "Eliminar":
                        bool respuesta = await App.Current!.MainPage!.DisplayAlert("Eliminar Proveedor", "¿Esta seguro que desea eliminar el proveedor?", "Si", "No");

                        if (respuesta)
                        {
                            int del = _proveedorService.Delete(proveedor);
                            if (del > 0)
                            {
                                ProveedorCollection.Remove(proveedor);
                            }
                        }
                        break;
                }
            } catch (Exception ex)
            {
                Alerta("ERROR", ex.Message);
            }
        }

        /// <summary>
        /// Método personalizado para construir alertas
        /// </summary>
        /// <param name="Tipo">Tipo de Alerta</param>
        /// <param name="Mensaje">Mensaje de Alerta</param>
        private void Alerta(String Tipo, String Mensaje)
        {
            MainThread.BeginInvokeOnMainThread(async () => await App.Current!.MainPage!.DisplayAlert(Tipo, Mensaje, "Aceptar"));
        }
    }
}
