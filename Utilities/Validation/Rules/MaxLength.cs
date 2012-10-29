﻿/*
Copyright (c) 2012 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

#region Usings
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Utilities.DataTypes.ExtensionMethods;
using Utilities.DataTypes.Comparison;
using System.Linq;
#endregion

namespace Utilities.Validation.Rules
{
    /// <summary>
    /// Max length attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class MaxLengthAttribute : ValidationAttribute
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Value">Value to check</param>
        /// <param name="ErrorMessage">Error message</param>
        public MaxLengthAttribute(long Value, string ErrorMessage = "")
            : base(ErrorMessage.IsNullOrEmpty() ? "{0} is longer than {1}" : ErrorMessage)
        {
            this.CompareValue = Value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Value to compare to
        /// </summary>
        public long CompareValue { get; set; }

        #endregion

        #region Functions

        /// <summary>
        /// Formats the error message
        /// </summary>
        /// <param name="name">Property name</param>
        /// <returns>The formatted string</returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, CompareValue.ToString());
        }

        /// <summary>
        /// Determines if the property is valid
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="validationContext">Validation context</param>
        /// <returns>The validation result</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IEnumerable ValueList = value as IEnumerable;
            long Count = 0;
            foreach (object Item in ValueList)
            {
                ++Count;
                if (Count > CompareValue)
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }

        #endregion
    }
}