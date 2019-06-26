using DomUcenikaSvilajnac.Common.Interfaces;
using DomUcenikaSvilajnac.Common.Models;
using DomUcenikaSvilajnac.DAL.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomUcenikaSvilajnac.DAL.RepoPattern
{
    public class MesecniPlanRadaRepository : Repository<MesecniPlanRada>, IMesecniPlanRadaRepository
    {
        public MesecniPlanRadaRepository(UcenikContext context) : base(context)
        {

        }
        public UcenikContext context
        {
            get { return context as UcenikContext; }
        }

    }
}
