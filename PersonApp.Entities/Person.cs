using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonApp.Entities
{
    public class Person : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Adınız"), Required, StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Soyadınız"), Required, StringLength(50)]
        public string Surname { get; set; }
        [Display(Name = "Firma"), StringLength(15)]
        public string Company { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; }
        public int ContactId { get; set; }
        public virtual List<Contact> Contacts { get; set; }
    }
}
