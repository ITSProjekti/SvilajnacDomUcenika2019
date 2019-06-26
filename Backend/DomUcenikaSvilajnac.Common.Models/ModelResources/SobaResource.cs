using System;
using System.Collections.Generic;
using System.Text;

namespace DomUcenikaSvilajnac.Common.Models.ModelResources
{

    /// <summary>
    /// Sluzi za podakte koji ce se slati na front, u slucaju da neki podatak nije potrebno poslati na front, smanjuje se broj property-a u odnosu na klasu Sobe.
    /// </summary>
    
    public class SobaResource
    {
        public int Id { get; set; }

        public int BrojSobe { get; set; }

        public string Sprat { get; set; }

        public string VrstaSobe { get; set; }

        public string TipSobe { get; set; }
    }
}
