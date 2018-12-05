using PeakandPlate.Validation;
using System;

namespace PeakandPlate.Model
{
    public class LicensePlate
    {
        /// <summary>
        /// Describes the license plate number
        /// </summary>
        public string PlateNumber { get; private set; }

        /// <summary>
        /// Returns the last digit for the plate number only when it is valid.
        /// </summary>
        public int? LastDigit
        {
            get
            {
                return IsValid() ? 
                    Convert.ToInt32(PlateNumber.Substring(PlateNumber.Length - 1, 1)) : 
                    default(int?);
            }
        }

        public LicensePlate(string plateNumber)
        {
            this.PlateNumber = plateNumber;
        }

        /// <summary>
        /// Returns true if the license plate number is valid.
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return LicensePlateValidator.IsValidLicensePlate(PlateNumber);
        }
    }
}
