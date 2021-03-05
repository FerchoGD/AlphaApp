using System.ComponentModel.DataAnnotations;

namespace Domain.Communications
{
    public enum Type
    {
        [Display(Name = "Interno")]
        Internal = 0,
        [Display(Name = "Externo")]
        External = 1
    }
}