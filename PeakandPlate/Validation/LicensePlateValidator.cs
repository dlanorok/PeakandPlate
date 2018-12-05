using System;
using System.Text.RegularExpressions;

namespace PeakandPlate.Validation
{
    public class LicensePlateValidator
    {
        /// <summary>
        /// Validates if a license plate was entered correctly.
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <returns></returns>
        public static bool IsValidLicensePlate(string licensePlate)
        {
            if (String.IsNullOrEmpty(licensePlate))
                return false;

            try
            {
                return Regex.IsMatch(licensePlate, "^[A-Z]{1,3}-[0-9]{1,4}$", RegexOptions.IgnoreCase);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
