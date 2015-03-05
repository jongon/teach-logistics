namespace Tesis.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public partial class User : IdentityUser
    {
        [DisplayName("Usuario")]
        public override string UserName { get; set; }

        [DisplayName("Email")]
        public override string Email { get; set; }

        [DisplayName("Nombre")]
        public virtual string FirstName { get; set; }

        [DisplayName("Apellido")]
        public virtual string LastName { get; set; }

        [DisplayName("Cédula")]
        public virtual string IdCard { get; set; }

        [DisplayName("Activación")]
        public override bool EmailConfirmed { get; set; }

        [DisplayName("Sección")]
        public virtual Section Section { get; set; }

        public virtual Guid? SectionId { get; set; }

        [DisplayName("Grupo")]
        public virtual Group Group { get; set; }

        public virtual Guid? GroupId { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
