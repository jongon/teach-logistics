using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Tesis.DAL;
using Tesis.UtilityCode;

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
        private ApplicationDbContext _db;
        public RegisterViewModel()
        {
            this._db = new ApplicationDbContext();
        }

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

        [DisplayName("Rol")]
        [Required(ErrorMessage = "El Rol es requerido")]
        public string RoleName { get; set; }

        public SelectList Roles
        {
            get
            {
                return new SelectList(new Dictionary<string, string>() {
                    { "Estudiante", "Estudiante"},
                    { "Administrador", "Administrador"}
                }, "Key", "Value");
            }
            set { }
        }
    }

    public class ConfirmationViewModel
    {
        [Required(ErrorMessage = "Campo {0} es requerido")]
        public string Id { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [UniqueUser(ErrorMessage = "Ya existe este email registrado")]
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
        [UniqueUser(ErrorMessage = "Ya existe esta cédula registrada")]
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
        [Required(ErrorMessage = "Este Campo es requerido")]
        [DisplayName("Activado")]
        public virtual bool EmailConfirmed { get; set; }

        [Required(ErrorMessage = "Senestre Este Campo es requerido")]
        [DisplayName("Semestre")]
        public Guid? SemesterId { get; set; }

        [Required(ErrorMessage = "Sección Este Campo es requerido")]
        [DisplayName("Sección")]
        public Guid? SectionId { get; set; }
    }
}
