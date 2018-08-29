using ExtensionsCoreUtils.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExtensionsCoreUtils.Tests.CustomValidation.Classes
{
    [DomainValidationName()]
    public class TeacherDto
    {
        public int TeacherID { get; set; }

        [Required]
        public string TeacherName { get; set; }

        [Required(ErrorMessage = "hola")]
        [Attributes.CustomValidation(ErrorMessage = "ERROR_RESOURCES_TEST", FunctionValidation = "function(value){ return true;}", FunctionName = "foo")]
        [Attributes.CustomValidation(ErrorMessage = "bar_message", FunctionValidation = "function(value){ return value === '';}")]
        public DateTime DateOfBirth { get; set; }
    }
}
