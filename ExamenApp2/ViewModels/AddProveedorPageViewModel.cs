using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExamenApp2.Models;
using ExamenApp2.Services;

namespace ExamenApp2.ViewModels
{
    public partial class AddProveedorPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string nombre;

        [ObservableProperty]
        private string rtn;

        [ObservableProperty]
        private string contacto;

        [ObservableProperty]
        private string telefonoContacto;

        [ObservableProperty]
        private string correo;

        private readonly ProveedorService _estudianteService;

        public AddProveedorPageViewModel()
        {
            _estudianteService = new ProveedorService();
        }

        public AddProveedorPageViewModel(Proveedor proveedor)
        {
            Nombre = proveedor.Nombre;
            Rtn = proveedor.Rtn;
            Contacto = proveedor.Contacto;
            TelefonoContacto = proveedor.TelefonoContacto;
            Correo = proveedor.Correo;
            Id = proveedor.Id;
            _estudianteService = new ProveedorService();
        }

        /// <summary>
        /// Agrega o actualiza un registro
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task AddUpdate()
        {
            try
            {
                Proveedor proveedor = new Proveedor
                {
                    Nombre = Nombre,
                    Rtn = Rtn,
                    Contacto = Contacto,
                    TelefonoContacto = TelefonoContacto,
                    Correo = Correo,
                    Id = Id
                };

                if (Validar(proveedor))
                {
                    if (Id == 0)
                    {
                        _estudianteService.Insert(proveedor);
                    }
                    else
                    {
                        _estudianteService.Update(proveedor);
                    }
                    await App.Current!.MainPage!.Navigation.PopAsync();
                }
            } catch (Exception ex)
            {
                Alerta("ERROR", ex.Message);
            }
        }

        /// <summary>
        /// Valida que los campos no esten vacíos
        /// </summary>
        /// <param name="Proveedor">Objeto a validar</param>
        /// <returns></returns>
        private bool Validar(Proveedor proveedor)
        {
            try
            {
                if (proveedor.Nombre == null || proveedor.Nombre == "")
                {
                    Alerta("ADVERTENCIA", "Nombre de proveedor incorrecto");
                    return false;
                }
                else if (proveedor.Rtn == null || proveedor.Rtn == "")
                {
                    Alerta("ADVERTENCIA", "Rtn ingresado no es valido");
                    return false;
                }
                else if (proveedor.Contacto == null || proveedor.Contacto == "")
                {
                    Alerta("ADVERTENCIA", "Nombre contacto no valido");
                    return false;
                }
                else if (proveedor.TelefonoContacto == null || proveedor.TelefonoContacto == "")
                {
                    Alerta("ADVERTENCIA", "Telefono de contacto no valido");
                    return false;
                }else if (proveedor.Correo == null || proveedor.Correo == "")
                {
                    Alerta("ADVERTENCIA", "Correo ingresado no valido");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Alerta("ERROR", ex.Message);
                return false;
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
