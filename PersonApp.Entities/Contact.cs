using System;
using System.ComponentModel.DataAnnotations;

namespace PersonApp.Entities
{
    public class Contact : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Bilgi"), Required, StringLength(50)]
        public string Info { get; set; }
        public InfoType InfoType { get; set; }
        [Display(Name = "Kayıt Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public int PersonId { get; set; }
        public virtual Person Person{ get; set; }
    }
}
