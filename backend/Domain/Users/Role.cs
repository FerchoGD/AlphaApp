using System.ComponentModel.DataAnnotations;

namespace Domain.Users
{
    public enum Role
    {
        [Display(Name = "Administrador")]
        Admin = 0,
        [Display(Name = "Gestor")]
        Manager = 1,
        [Display(Name = "Destinatario")]
        Destinater = 2
        
    }
}