//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Asiento
    {
        public int Id_asiento { get; set; }
        public Nullable<int> Id_Avion { get; set; }
        public Nullable<int> Fila { get; set; }
        public Nullable<int> Columna { get; set; }
        public string Planta { get; set; }
    
        public virtual Avion Avion { get; set; }
    }
}