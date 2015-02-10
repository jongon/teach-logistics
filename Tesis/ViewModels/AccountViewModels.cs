using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Tesis.DAL;
using Tesis.Models;

namespace Tesis.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [EmailAddress(ErrorMessage = "El {0} es inválido")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "¿Recordar sesión?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string FirstName { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string LastName { get; set; }

        [Display(Name = "Cédula")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo números permitidos")]
        [UniqueUser(ErrorMessage = "Ya existe esta cédula registrada")]
        public string IdCard { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [EmailAddress(ErrorMessage = "{0} inválido")]
        [UniqueUser(ErrorMessage = "Ya existe este email registrado")]
        public string Email { get; set; }

        [DisplayName("Sección")]
        [Required(ErrorMessage = "Este Campo es requerido")]
        public Guid SectionId { get; set; }

        [DisplayName("Semestre")]
        [Required(ErrorMessage = "Este Campo es requerido")]
        public Guid SemesterId { get; set; }
    }

    public class ConfirmationViewModel
    {
        [Required]
        public string Id { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [EmailAddress(ErrorMessage = "{0} inválido")]
        public string Email { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public virtual string FirstName { get; set; }

        [DisplayName("Apellido")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public virtual string LastName { get; set; }

        [DisplayName("Cédula")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo números permitidos")]
        public virtual string IdCard { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

        [Display(Name = "Confirmar contraseña")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "La contraseña y la confirmación no coinciden.")]
        public virtual string ConfirmPassword { get; set; }
    }

    public class EditUserViewModel : ConfirmationViewModel
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Required(ErrorMessage = "Este Campo es requerido")]
        [DisplayName("Activación")]
        public virtual bool EmailConfirmed { get; set; }

        [Required(ErrorMessage = "Senestre Este Campo es requerido")]
        [DisplayName("Semestre")]
        public Guid? SemesterId { get; set; }

        [Required(ErrorMessage = "Sección Este Campo es requerido")]
        [DisplayName("Sección")]
        public Guid? SectionId { get; set; }
    }

    public class ApplicationUserViewModel {

        [DisplayName("Id")]
        public string Id { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Nombre")]
        public string FirstName { get; set; }
        [DisplayName("Apellido")]
        public string LastName { get; set; }
        [DisplayName("Cédula")]
        public string IdCard { get; set; }
        [DisplayName("Sección")]
        public Section Section { get; set; }
        [DisplayName("Roles")]
        public ICollection<IdentityUserRole> Roles { get; set; }

    }
}
