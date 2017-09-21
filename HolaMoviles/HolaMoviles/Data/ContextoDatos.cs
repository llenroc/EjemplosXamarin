using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLite.Net;
using HolaMoviles.Modelos;

namespace HolaMoviles.Data
{
    public class ContextoDatos
    {
        public string RutaConexion { get; set; }

        public Func<SQLiteConnection> ObtenerConexion { get; set; }

        public IEnumerable<Persona> Obtener()
        {
            using (var conexion = ObtenerConexion())
            {
                var tabla = conexion.Table<Persona>();
                conexion.CreateTable<Persona>();

                return tabla.ToList();
            }
        }
    }
}
