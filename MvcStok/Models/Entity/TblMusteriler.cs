namespace MvcStok.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class TblMusteriler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblMusteriler()
        {
            this.TblSatislar = new HashSet<TblSatislar>();
        }

        public int MUSTERIID { get; set; }
        [Required(ErrorMessage ="Bu alaný boþ býrakamazsýnýz...")]
        [StringLength(50,ErrorMessage ="En fazla 50 karakterlik Ýsim Girebilirisiniz !")]
        public string MUSTERIAD { get; set; }
        [Required(ErrorMessage = "Bu alaný boþ býrakamazsýnýz...")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakterlik Soyisim Girebilirisiniz !")]
        public string MUSTERISOYAD { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblSatislar> TblSatislar { get; set; }
    }
}
