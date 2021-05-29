using System;
using System.Collections.Generic;
using Kalum2021.ModelView;

namespace Kalum2021.Models
{
    public class Horario
    {
        public string HorarioId{get;set;}
        public DateTime  HoarioInicio{get;set;}
        public DateTime  HoarioFinal{get;set;}

        public List<Clase> Clases {get;set;}
    }
}