using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomUcenikaSvilajnac.Common.Models.ModelResources
{
    /// <summary>
    /// Sluzi za podakte koji ce se slati na front, u slucaju da neki podatak nije potrebno poslati na front, smanjuje se broj property-a u odnosu na klasu GodisnjiProgramRada.
    /// </summary>

    public class GodisnjiProgramRadaResource
    {
        public int Id { get; set; }
        public string ProgramskoPodrucje { get; set; }
        public string Tema { get; set; }
        public string Mesec { get; set; }
    }
}
