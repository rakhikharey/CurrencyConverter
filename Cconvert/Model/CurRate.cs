//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cconvert.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CurRate
    {
        public long CurId { get; set; }
        public string fromC { get; set; }
        public string toC { get; set; }
        public Nullable<double> rate { get; set; }
    }
}