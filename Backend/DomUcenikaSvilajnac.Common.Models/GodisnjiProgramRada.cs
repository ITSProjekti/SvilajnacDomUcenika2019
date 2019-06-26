using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomUcenikaSvilajnac.Common.Models
{
    /// <summary>
    /// Klasa Soba, pravi tabelu "Soba" u bazi podataka sa poljima koja su navedena kao property u datoj klasi.
    /// </summary>
    /// 

    [Table("GodisnjiProgramRada")]
    public class GodisnjiProgramRada
    {
        public int Id { get; set; }

        public string ProgramskoPodrucje { get; set; }

        public string Tema { get; set; }

        public string Mesec { get; set; }
    }
}
