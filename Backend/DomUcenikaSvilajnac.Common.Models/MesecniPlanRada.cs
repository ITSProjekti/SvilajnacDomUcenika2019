using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomUcenikaSvilajnac.Common.Models
{
    [Table("MesecniPlanRada")]
    public class MesecniPlanRada
    {
        [Key]
        public int Id { get; set; }
        public string Mesec { get; set; }
        public string SadrzajProgramaVaspitnogRada { get; set; }
        public string MetodeIObliciVaspitnogRada { get; set; }
        public string Napomena { get; set; }
        public string Literatura { get; set; }
    }
}
