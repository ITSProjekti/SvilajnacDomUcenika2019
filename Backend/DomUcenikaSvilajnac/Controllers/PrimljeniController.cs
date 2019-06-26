using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DomUcenikaSvilajnac.Common.Models;
using DomUcenikaSvilajnac.DAL.Context;
using AutoMapper;
using DomUcenikaSvilajnac.ModelResources;
using DomUcenikaSvilajnac.Common.Interfaces;
using DomUcenikaSvilajnac.Common.Models.ModelResources;
using System.Collections.ObjectModel;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.IO;
using Syncfusion.Drawing;

namespace DomUcenikaSvilajnac.Controllers
{
    [Produces("application/json")]
    [Route("api/Primljeni")]
    public class PrimljeniController : Controller
    {
        public IMapper _mapper { get; }
        public IUnitOfWork UnitOfWork { get; }



        /// <summary>
        /// Inicijalizacija instance klase UcenikController i deklarisanje mappera i unitofwork-a.
        /// </summary>
        public PrimljeniController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            UnitOfWork = unitOfWork;
        }


        [HttpPost]
        public IActionResult GetUcenik([FromBody] PostPrimljeniResource podaci)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<UcenikResource> lista = UnitOfWork.Ucenici.vratiPrimljeneUcenike(podaci.brojMuskih, podaci.brojZenskih, podaci.bodovi);

            UnitOfWork.SaveChanges();
            return Ok(lista);
        }

        [HttpGet]
        public IActionResult  GetAction()
        {
            return Ok(UnitOfWork.Ucenici.vratiPrimljene() );
        }

      

        /*[HttpGet]
        public IActionResult GetUcenik(int brojMuskih, int brojZenskih, int bodovi)
        {
            brojMuskih = 3;
            brojZenskih = 1;
            bodovi = 26;
            List<Ucenik> lista = UnitOfWork.Ucenici.vratiPrimljeneUcenike(brojMuskih, brojZenskih, bodovi);
            UnitOfWork.SaveChanges();

            return Ok(lista);
        }*/
    }
}