using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomUcenikaSvilajnac.Common.Models
{
    [Table("Sekcija")]

    public class Sekcija
    {
        public int id { get; set; }
        public string naziv { get; set; }
        public string napomena { get; set; }
    }
}
