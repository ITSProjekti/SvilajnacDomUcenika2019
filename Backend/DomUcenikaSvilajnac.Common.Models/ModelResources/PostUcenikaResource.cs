﻿using DomUcenikaSvilajnac.Common.Models;
using DomUcenikaSvilajnac.Common.Models.ModelResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomUcenikaSvilajnac.ModelResources
{
    /// <summary>
    /// Prilikom post-ovanja novog ucenika bice prosledjeni podaci koji su navedeni u klasi ispod.
    /// </summary>
    public class PostUcenikaResource
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        
        public string Prezime { get; set; }

        public string JMBG { get; set; }
        public string  Adresa { get; set; }
        public PostanskiBrojResource PostanskiBroj { get; set; }
        public PolResource Pol { get; set; }
        public Telefon Telefon { get; set; }
        
        public int Dan { get; set; }
        public int Mesec { get; set; }

        public int Godina { get; set; }

        public OpstinaUcenikaResource Opstina { get; set; }
        public OpstinaUcenikaResource OpstinaPrebivalista { get; set; }

        public MestoResource MestoRodjenja { get; set; }
        public MestoResource MestoPrebivalista { get; set; }

        public MestoResource MestoZavrseneSkole { get; set; }

        public DrzavaResource DrzavaRodjenja { get; set; }

        public PrethodnaSkolaResource PrethodnaSkola { get; set; }
        public UpisanaSkolaResource UpisanaSkola { get; set; }

        public SmerResource Smer { get; set; }

        public RazredResource Razred { get; set; }

        //public List<RoditeljiUcenikaResource> Roditelji { get; set; }

        public PostRoditeljaResource Roditelji { get; set; }
        public float PrethodniUspeh { get; set; }

        public TipPorodiceResource TipPorodice { get; set; }
        public StarateljResource Staratelji { get; set; }

        public string Slika { get; set; }
        public int MaterijalniPrihodi { get; set; }
        public VaspitnaGrupaResource VaspitnaGrupa { get; set; }
        public StatusPrijaveResource StatusPrijave { get; set; }
        public int BrojPutaUDomu { get; set; }

    }
}
