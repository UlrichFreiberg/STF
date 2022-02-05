// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FormatFunction.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the MapValuesFunction type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.StringTransformationUtilities
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The Format function.
    /// </summary>
    public class FormatFunction
    {
        /// <summary>
        /// The reg exp.
        /// .
        /// NOT --> {FORMAT FLOAT 23.34 ##.## ###,##} --> 23,34
        /// {FORMAT FLOAT 23.34 en-US da-DK} --> 23,34
        /// {FORMAT DATE 2021-12-31 yyyy-MM-dd "dd/MM yyyy"} --> 21/12 2021
        /// </summary>
        public const string RegExp = @"^\s*(?<DataType>(""[^""]+"")|([^\s]+))\s+(?<ValueToFormat>(""[^""]+"")|([^\s]+))\s+(?<InputFormat>(""[^""]+"")|([^\s]+))\s+(?<OutputFormat>(""[^""]+"")|([^\s]+))\s*$";

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The stu map values.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [StringTransformationUtilFunction("FORMAT")]
        public string StuFormat(string arg)
        {
            string retVal = null;

            if (string.IsNullOrEmpty(arg))
            {
                ErrorMessage = $"arg [{arg}] is null or empty";
                return null;
            }

            var selectRegexp = new Regex(RegExp);
            var match = selectRegexp.Match(arg);

            if (!match.Success)
            {
                ErrorMessage = $"arg [{arg}] couldn't be parsed";
                return null;
            }

            var dataType = match.Groups["DataType"].Value.Trim().Trim('"');
            var valueToFormat = match.Groups["ValueToFormat"].Value.Trim().Trim('"');
            var inputFormat = match.Groups["InputFormat"].Value.Trim().Trim('"');
            var outputFormat = match.Groups["OutputFormat"].Value.Trim().Trim('"');

            switch (dataType.ToUpper())
            {
                case "INT":
                    retVal = HandleIntegerFormat(valueToFormat, inputFormat, outputFormat);
                    break;
                case "FLOAT":
                case "DECIMAL":
                    retVal = HandleDecimalFormat(valueToFormat, inputFormat, outputFormat);
                    break;
                case "DATE":
                case "DATETIME":
                    retVal = HandleDateTimeFormat(valueToFormat, inputFormat, outputFormat);
                    break;
                default:
                    ErrorMessage = $"Unsupported data type [{dataType}]";
                    break;
            }

            return retVal;
        }

        /// <summary>
        /// The handle integer format.
        /// </summary>
        /// <param name="valueToFormat">
        /// The value to format.
        /// </param>
        /// <param name="inputFormat">
        /// The input format.
        /// </param>
        /// <param name="outputFormat">
        /// The output format.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string HandleIntegerFormat(string valueToFormat, string inputFormat, string outputFormat)
        {
            int inputValue;

            ErrorMessage = $"Could not parse [{valueToFormat}] using [{inputFormat}]";

            try
            {
                if (!int.TryParse(valueToFormat, out inputValue))
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to parse int [{valueToFormat} - got exception [{ex}]";
                return null;
            }

            var retVal = inputValue.ToString(outputFormat);

            return retVal;
        }

        /// <summary>
        /// The handle date time format.
        /// </summary>
        /// <param name="valueToFormat">
        /// The value to format.
        /// </param>
        /// <param name="inputFormat">
        /// The input format.
        /// </param>
        /// <param name="outputFormat">
        /// The output format.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string HandleDateTimeFormat(string valueToFormat, string inputFormat, string outputFormat)
        {
            DateTime inputValue;

            ErrorMessage = $"Failed to parse [{valueToFormat}] using [{inputFormat}]";

            try
            {
                if (!DateTime.TryParseExact(
                        valueToFormat,
                        inputFormat,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AllowWhiteSpaces,
                        out inputValue))
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage += $" - got exception [{ex}]";
                return null;
            }

            ErrorMessage = null;

            // you have to state invariant culture - otherwise ToString interpret seperator using current culture
            var retVal = inputValue.ToString(outputFormat, CultureInfo.InvariantCulture);

            return retVal;
        }

        /// <summary>
        /// The handle decimal format.
        /// </summary>
        /// <param name="valueToFormat">
        /// The value to format.
        /// </param>
        /// <param name="inputFormat">
        /// The input format.
        /// </param>
        /// <param name="outputFormat">
        /// The output format.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string HandleDecimalFormat(string valueToFormat, string inputFormat, string outputFormat)
        {
            CultureInfo culture;

            bool CheckParsedValue(decimal parsedValue)
            {
                culture = CultureInfo.CreateSpecificCulture(inputFormat);

                var checkValue = parsedValue.ToString(culture);
                var ok = valueToFormat.Equals(checkValue);

                return ok;
            }

            culture = CultureInfo.CreateSpecificCulture(inputFormat);

            if (!decimal.TryParse(valueToFormat, NumberStyles.Any, culture, out var inputValue))
            {
                ErrorMessage = $"Could not parse [{valueToFormat}] using [{inputFormat}]";
                return null;
            }

            if (!CheckParsedValue(inputValue))
            {
                ErrorMessage = $"Parsed value [{inputValue}] did not match the input given [{valueToFormat}] using [{inputFormat}]";
                return null;
            }

            culture = CultureInfo.CreateSpecificCulture(outputFormat);

            var retVal = inputValue.ToString(culture);
            return retVal;
        }
    }
}
