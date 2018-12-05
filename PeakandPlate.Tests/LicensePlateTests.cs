using NUnit.Framework;
using PeakandPlate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakandPlate.Tests
{
    [TestFixture]
    public class LicensePlateTests
    {

        [TestCase("AAA-1234", true)]
        [TestCase("QWE-123X", false)]
        [TestCase("POR-9875", true)]
        [TestCase("215-ASDF", false)]
        [TestCase("", false)]
        public void PlateNumber_IsValid(string plateNumber, bool expectedResult)
        {
            var licensePlate = new LicensePlate(plateNumber);

            Assert.IsNotNull(plateNumber);

            Assert.AreEqual(licensePlate.IsValid(), expectedResult);
        }

        [TestCase("APK-6542", true)]
        [TestCase("RTS-1234", true)]
        [TestCase("XYZ-", false)]
        public void LastDigit_IsNotNull(string plateNumber, bool expectedResult)
        {
            var licensePlate = new LicensePlate(plateNumber);

            Assert.AreEqual(licensePlate.LastDigit.HasValue, expectedResult);
            
        }
    }
}
