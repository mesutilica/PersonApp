using System.ComponentModel.DataAnnotations;

namespace PersonApp.Entities
{
    public enum InfoType
    {
        [Display(Name = "Telefon")] PhoneNumer = 1, Email = 2, [Display(Name = "Konum")] Location = 3
    }
}
