using System;
using SQLite.Net;
using SQLite.Net.Attributes;

namespace HolaMoviles.Modelos
{
    [Table("Personas")]
    public class Persona
    {
        [Column(nameof(Nombre)), NotNull]
        public string Nombre { get; set; }

        [Column(nameof(PrimerApellido)), NotNull]
        public string PrimerApellido { get; set; }

        [Column(nameof(SegundoApellido)), NotNull]
        public string SegundoApellido { get; set; }

        [PrimaryKey, AutoIncrement, NotNull]
        public int Id { get; set; }
    }
}