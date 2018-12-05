using PeakandPlate;
using NUnit.Framework;
using PeakandPlate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakandPlate.Tests
{
    [TestFixture()]
    public class PredictorValidatorTests
    {
        PredictorValidator __validatorMock;
        PredictorValidator __validator;
        public PredictorValidatorTests()
        {
            __validator = new PredictorValidator();

            // Values for comparison
            IList<PeakRestriction> peakRestriction = new[]
            {
                new PeakRestriction() { Day = DayOfWeek.Monday, PlateNumber = 1 },
                new PeakRestriction() { Day = DayOfWeek.Monday, PlateNumber = 2 },
                new PeakRestriction() { Day = DayOfWeek.Tuesday, PlateNumber = 3 },
                new PeakRestriction() { Day = DayOfWeek.Tuesday, PlateNumber = 4 }
             };

            IList<RushHour> rushHours = new[]
            {
                new RushHour(){ TimeOfTheDay=PartsOfTheDay.Morning,
                    StartTime = TimeSpan.Parse("7:00"), EndTime=TimeSpan.Parse("9:30")},

                new RushHour(){ TimeOfTheDay=PartsOfTheDay.Afternoon,
                    StartTime = TimeSpan.Parse("16:00"), EndTime=TimeSpan.Parse("19:30")}
            };

            __validatorMock = new PredictorValidator(peakRestriction, rushHours);
        }

        [TestCase("AAA-8462", "1/2/2018", "6:00", true)]
        [TestCase("AAA-9603", "1/2/2018", "7:59", false)]
        [TestCase("AAA-4321", "1/1/2018", "16:00", false)]
        public void ValidatePeakandPlate_IsOk(string plateNumber, DateTime date, TimeSpan time, bool expectedResult)
        {
            var isOk = __validatorMock.ValidatePeakandPlate(new LicensePlate(plateNumber), date, time);

            Assert.AreEqual(isOk, expectedResult);

        }

        [TestCase("XYZ-7531", "1/1/2018", "4:00", true)]
        [TestCase("ERP-7542", "1/1/2018", "19:00", false)]
        [TestCase("ABC-9513", "1/2/2018", "9:31", true)]
        public void ValidatePeakandPlate_ValidateReal(string plateNumber, DateTime date, TimeSpan time, bool expectedResult)
        {
            var isOk = __validator.ValidatePeakandPlate(new LicensePlate(plateNumber), date, time);

            Assert.AreEqual(isOk, expectedResult);

        }
    }
}
