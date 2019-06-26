using System;
using System.Collections.Generic;
using System.Text;

namespace DomUcenikaSvilajnac.Common.Models.ModelResources
{
    public class MesecniPlanRadaResource
    {
        public int Id { get; set; }
        public string Mesec { get; set; }
        public string SadrzajProgramaVaspitnogRada { get; set; }
        public string MetodeIObliciVaspitnogRada { get; set; }
        public string Napomena { get; set; }
        public string Literatura { get; set; }
    }
}
