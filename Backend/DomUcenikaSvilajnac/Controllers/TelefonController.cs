﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DomUcenikaSvilajnac.Common.Interfaces;
using DomUcenikaSvilajnac.Common.Models;
using DomUcenikaSvilajnac.Common.Models.ModelResources;
using Microsoft.AspNetCore.Mvc;

namespace DomUcenikaSvilajnac.Controllers
{
    [Produces("application/json")]
    [Route("api/Telefoni")]
    public class TelefonController : Controller
    {
        public IMapper Mapper { get; }
        public IUnitOfWork UnitOfWork { get; }
        /// <summary>
        /// Inicijalizacija instance klase TelefonController i deklarisanje mappera i unitofwork-a.
        /// </summary>
        public TelefonController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Vraca listu svih telefona koji se trenutno nalaze u bazi.
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<TelefonResource>> GetTelefone()
        {
            var listaTelefona = await UnitOfWork.Telefoni.GetAllAsync();
            return Mapper.Map<List<Telefon>, List<TelefonResource>>(listaTelefona.ToList());
        }

        /// <summary>
        /// Vraca jedan red iz tabele, tj. jedan telefon na osnovu prosledjenog Id-a.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTelefonById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var telefon = await UnitOfWork.Telefoni.GetAsync(id);
            var noviTelefon = Mapper.Map<Telefon, TelefonResource>(telefon);
            if (telefon == null)
            {
                return NotFound();
            }

            return Ok(noviTelefon);
        }

        /// <summary>
        /// Metoda za update, menja podatke u nekom redu u tabeli, tj. o nekom telefonu na osnovu prosledjenog Id-a 
        /// i vraca podatke o telefonu koji su namenjeni za front.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTelefon([FromRoute] int id, [FromBody] TelefonResource telefon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stariTelefon = await UnitOfWork.Telefoni.GetAsync(id);
            if (id != stariTelefon.Id)
            {
                return BadRequest();
            }
            if (stariTelefon == null)
                return NotFound();


            telefon.Id = id;
            Mapper.Map<TelefonResource, Telefon>(telefon, stariTelefon);
            await UnitOfWork.SaveChangesAsync();

            var noviTelefon = await UnitOfWork.Telefoni.GetAsync(id);
            Mapper.Map<Telefon, TelefonResource>(noviTelefon);
            return Ok(telefon);
        }

        /// <summary>
        /// Dodavanje novog reda u tabeli, tj. novog telefona.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostTelefon([FromBody]  TelefonResource telefon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var noviTelefon = Mapper.Map<TelefonResource, Telefon>(telefon);

            UnitOfWork.Telefoni.Add(noviTelefon);
            await UnitOfWork.SaveChangesAsync();

            telefon = Mapper.Map<Telefon, TelefonResource>(noviTelefon);

            return Ok(telefon);
        }

        /// <summary>
        /// Brisanje jednog reda iz tabele na osnvou prosledjenog Id-a, tj. brisanje odredjenog telefona iz tabele.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTelefon([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var telefon = await UnitOfWork.Telefoni.GetAsync(id);
            if (telefon == null)
            {
                return NotFound();
            }

            var noviTelefon = Mapper.Map<Telefon, TelefonResource>(telefon);
            UnitOfWork.Telefoni.Remove(telefon);
            await UnitOfWork.SaveChangesAsync();

            return Ok(noviTelefon);
        }

    }
}