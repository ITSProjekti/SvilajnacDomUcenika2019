using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomUcenikaSvilajnac.Common.Models
{
    /// <summary>
    /// Klasa Soba, pravi tabelu "Soba" u bazi podataka sa poljima koja su navedena kao property u datoj klasi.
    /// </summary>

    [Table("Sobe")]
    public class Soba
    {
        public int Id { get; set; }

        public int BrojSobe { get; set; }

        public string Sprat { get; set; }

        public string VrstaSobe { get; set; }

        public string TipSobe { get; set; }
    }
}
