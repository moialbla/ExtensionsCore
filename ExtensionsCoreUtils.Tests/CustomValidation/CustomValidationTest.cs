using ExtensionsCoreUtils.Tests.CustomValidation.Classes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit;

namespace ExtensionsCoreUtils.Tests.CustomValidation
{
    public class CustomValidationTest
    {


        [Fact]
        public void Test_Custom_Validation_Start()
        {
            IServiceCollection service = new ServiceCollection().ScanValidations("ExtensionsCoreUtils.Tests");
            Assert.Equal(2, service.ValidationList().Count);
        }


        [Fact]
        public void Test_Validation_Entity()
        {
            /// 1.- Create a student 
            StudentDto dto = new StudentDto() { StudentID = 1, DateOfBirth = new DateTime(), StudentName = "test" };
            /// 2.- Create a context of validation  
            ValidationContext valContext = new ValidationContext(dto, null, null);
            /// 3.- Create a container of results  
            var validationsResults = new List<ValidationResult>();
            /// 4.- Validate customer  
            bool correct = Validator.TryValidateObject(dto, valContext, validationsResults, true);

            Equals(validationsResults.Count == 0);
 
        }
    }
}
