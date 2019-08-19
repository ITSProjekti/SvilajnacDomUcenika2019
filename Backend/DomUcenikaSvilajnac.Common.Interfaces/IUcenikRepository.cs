using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using DomUcenikaSvilajnac.Common.Models;
using System.Threading.Tasks;
using DomUcenikaSvilajnac.Common.Models.ModelResources;
using DomUcenikaSvilajnac.ModelResources;

namespace DomUcenikaSvilajnac.Common.Interfaces
{
    /// <summary>
    /// Interfejs za metode koje su samo za Ucenik klasu, tj ucenike.
    /// Pogledati IRepository i klasu Ucenik radi dodatnih pojasnjenja.
    /// </summary>
    public interface IUcenikRepository:IRepository<Ucenik>
    {
        Task<IEnumerable<UcenikResource>> podaciUcenika();
        Task<UcenikResource> podaciUcenikaById(int id);
        Task<IEnumerable<UcenikResource>> podaciUcenikaByStatusPrijave(int prijavaId);
        Task<IEnumerable<UcenikResource>> podaciUcenikaByVaspitnaGrupa(int vaspitnaGrupaId);
        Task<PostUcenikaResource> mapiranjeZaPostUcenika(PostUcenikaResource ucenik);
        Task<UcenikResource> mapiranjeZaDeleteUcenika(UcenikResource ucenik);
        Task<PutUcenikaResource> mapiranjeZaPutUcenika(int id);
        IEnumerable<UcenikResource> vratiPrimljeneUcenike(int brojMuskih, int brojZenskih, int bodovi);
        IEnumerable<UcenikResource> vratiPrimljene();
        float formulaZaRangiranje(int idUcenika);
        string htmlListaRangiranih(string muski, string zenski, string pismo,string razred, string naslov,string logo);
        IQueryable<Ucenik> vratiPoPolu(string pol, IQueryable<Ucenik> prethodni = null);
        IQueryable<Ucenik> vratiPoRazredu(string pol, IQueryable<Ucenik> prethodni = null);
        IQueryable<Ucenik> vratiPrimljeneUcenike();
        string vratiNaslove(string naslov);

    }
}
