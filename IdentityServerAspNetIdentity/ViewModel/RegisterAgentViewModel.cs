using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IdentityServerAspNetIdentity.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdentityServerAspNetIdentity.ViewModel
{
    public class RegisterAgentViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
         
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password",ErrorMessage ="Password and confirmation password not match.")]
        public string ConfirmPassword { get; set; }
        
        // public List<DepartmentDTO> DepartmentDto { get; set; }
        
        public Guid Department { get; set; }
        public SelectList Departments { get; set; }

        
   
    }
}