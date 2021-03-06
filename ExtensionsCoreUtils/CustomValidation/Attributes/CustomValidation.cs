﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VroomJs;

namespace ExtensionsCoreUtils.Attributes
{
    /// <summary>
    /// A custom validation for create our own validations.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
    public sealed class CustomValidationAttribute : ValidationAttribute
    {

        static JsEngine js = new JsEngine(4, 32);
            
        public CustomValidationAttribute()
        {
        }

        /// <summary>
        /// The function name.
        /// </summary>
        public String FunctionName { get; set; }

        /// <summary>
        /// The custom validation
        /// </summary>
        public String FunctionValidation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            VroomJs.AssemblyLoader.EnsureLoaded();
            if (FunctionValidation == null)
            {
                throw new ArgumentNullException("The value is mut be not null");
            }
            bool valid = false;

                using (JsContext context = js.CreateContext())
                {
                String execute = String.Format("var fn = {{ exec: {0} }}", FunctionValidation);
                    // Create a global variable on the JS side.
                    context.Execute(execute);
                    // Get it and use "dynamic" to tell the compiler to use runtime binding.
                    dynamic x = context.GetVariable("fn");
                    // Call the method and print the result.
                    valid = (bool)x.exec(value);
                }

            return valid;
        }
    }
}
