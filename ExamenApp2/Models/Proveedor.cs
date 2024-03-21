using SQLite;

namespace ExamenApp2.Models
{
    public class Proveedor
    {
       [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Rtn { get; set; }
        public string Contacto { get; set; }
        public string TelefonoContacto { get; set; }
        public string Correo { get; set; }

    }
}
