﻿using DomUcenikaSvilajnac.Common.Interfaces;
using DomUcenikaSvilajnac.Common.Models;
using DomUcenikaSvilajnac.DAL.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomUcenikaSvilajnac.DAL.RepoPattern
{
    /// <summary>
    /// Nasledjuje genericku klasu Repository sa tipom Smer i ISmerRepository interfejs
    /// Videti Repository i Smer klasu i ISmerRepository interfejs radi dodatnog pojasnjena.
    /// </summary>
    class SmerRepository : Repository<Smer>, ISmerRepository
    {
        
        public SmerRepository(UcenikContext context) : base(context)
        {

        }

        /// <summary>
        /// Get the context.
        /// </summary>
        public UcenikContext context
        {
            get { return context as UcenikContext; }
        }
    }
}
