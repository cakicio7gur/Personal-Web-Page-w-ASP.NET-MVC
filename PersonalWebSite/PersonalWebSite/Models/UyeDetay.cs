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

    public partial class UyeDetay
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UyeDetay()
        {
            this.Uye = new HashSet<Uye>();
        }
    
        public int uyeDetayBilgiID { get; set; }

        [Required(ErrorMessage = "E-mail bo� ge�ilemez !")]
        [EmailAddress(ErrorMessage = "Ge�ersiz E-mail !")]
        public string eMail { get; set; }

        [Required(ErrorMessage = "Kullan�c� Ad� bo� ge�ilemez !")]
        [StringLength(16, ErrorMessage = "Kullan�c� Ad� 3-16 karakter aral���nda olmal�d�r !", MinimumLength = 3)]
        [RegularExpression("^[A-Za-z0-9UIOGCSuiogcs_ ]{0,50}$", ErrorMessage = "Kullan�c� Ad� �zel Karakter ��ermemelidir !")]
        public string kullaniciAdi { get; set; }

        [Required(ErrorMessage = "�ifre bo� ge�ilemez !")]
        [StringLength(100, ErrorMessage = "�ifre 6-16 karakter aral���nda olmal�d�r !", MinimumLength = 6)]
        public string sifre { get; set; }

        public string fotograf { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Uye> Uye { get; set; }
    }
}
