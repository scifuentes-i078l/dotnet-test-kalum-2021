using System;

namespace Kalum2021.Models
{
    public class Seminario
    {
        public string SeminarioId {get;set;}
        public string ModuloId {get;set;}
        public string NombreSeminario {get;set;}
        public DateTime  FechaInicio{get;set;}
        public DateTime  FechaFinal{get;set;}
        public virtual Modulo Modulo {get;set;}
    }
}