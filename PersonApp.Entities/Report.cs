using System;
using System.ComponentModel.DataAnnotations;

namespace PersonApp.Entities
{
    public class Report : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Kayıt Tarihi"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; }
        public ReportStatus ReportStatus { get; set; }
    }
}
