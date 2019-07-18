//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PersonalWebSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Uye
    {
        public int uyeID { get; set; }
        public Nullable<int> uyeDetayBilgiID { get; set; }
        public Nullable<int> rolID { get; set; }

        [Required(ErrorMessage = "Ad bo� ge�ilemez !")]
        [StringLength(50, ErrorMessage = "Ad en az 3 karakter i�ermelidir!", MinimumLength = 3)]
        public string adSoyad { get; set; }

        public virtual Rol Rol { get; set; }
        public virtual UyeDetay UyeDetay { get; set; }
    }
}
