using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Tesis.DAL;
using Tesis.Models;
using Tesis.ViewModels;

namespace Tesis.UtilityCode
{
    public class UniqueUser : ValidationAttribute 
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public UniqueUser(): base("{0} se encuentra registrado en el sistema")
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string id = null;
            if (validationContext.ObjectInstance is EditUserViewModel)
            {
                EditUserViewModel userEdit = (EditUserViewModel)validationContext.ObjectInstance;
                id = userEdit.Id;
                
            }
            else if (validationContext.ObjectInstance is ConfirmationViewModel)
            {
                ConfirmationViewModel userEdit = (ConfirmationViewModel)validationContext.ObjectInstance;
                id = userEdit.Id;
            }

            User user;
            if (String.IsNullOrEmpty(id))
            {
                user = db.Users.Where(x => x.UserName.Equals((string)value) || x.IdCard.Equals((string)value)).FirstOrDefault();
            }
            else
            {
                user = db.Users.Where(x => (x.UserName.Equals((string)value) || x.IdCard.Equals((string)value)) && !x.Id.Equals(id)).FirstOrDefault();
            }

            if (user == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(errorMessage);
            }
        }
    }

    public class DateValidation : ValidationAttribute
    {
        public DateValidation()
            : base("{0} es incorrecta")
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationcontext)
        {
            DateTime date;

            if (DateTime.TryParseExact((string)value, "dd/MM/yyyy", null, DateTimeStyles.None, out date) == true)
                return ValidationResult.Success;
            else
            {
                var errorMessage = FormatErrorMessage(validationcontext.DisplayName);
                return new ValidationResult(errorMessage);
            }
        }
    }

    public class UniqueProductName : ValidationAttribute
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public UniqueProductName()
            : base("{0} de producto se encuentra registrado en el sistema")
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Product product;
            product = db.Products.Where(x => x.Name == value.ToString()).FirstOrDefault();
            if (product == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(errorMessage);
            }
        }
    }

    public class UniqueProductNumber : ValidationAttribute
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public UniqueProductNumber()
            : base("{0} de producto se encuentra registrado en el sistema")
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Product product;
            product = db.Products.Where(x => x.Number == (int)value).FirstOrDefault();
            if (product == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(errorMessage);
            }
        }
    }
}