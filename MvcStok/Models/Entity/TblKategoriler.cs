namespace MvcStok.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class TblKategoriler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblKategoriler()
        {
            this.TblUrunler = new HashSet<TblUrunler>();
        }

        public short KATEGORIID { get; set; }

        [Required(ErrorMessage = "Kategori Adýný Boþ Býrakamazsýnýz...")]//Validation
        public string KATEGORIAD { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblUrunler> TblUrunler { get; set; }
    }
}
