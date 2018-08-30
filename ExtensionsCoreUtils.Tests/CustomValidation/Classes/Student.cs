using ExtensionsCoreUtils.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ExtensionsCoreUtils.Tests.CustomValidation.Classes
{
    [DomainValidationName("StudentTEST1")]
    public class StudentDto
    {
        public int StudentID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The name is mandatory")]
        public string StudentName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The age can´t no be a negative value")]
        public int Age { get; set; }

        [Required(ErrorMessage ="hola")]
        [Attributes.CustomValidation(ErrorMessage ="ERROR_RESOURCES_TEST", FunctionValidation = "function(value){ return true;}", FunctionName ="foo")]
        [Attributes.CustomValidation(ErrorMessage = "bar_message", FunctionValidation = "function(value){ return value === '';}")]
        public DateTime DateOfBirth { get; set; }

    }
}
