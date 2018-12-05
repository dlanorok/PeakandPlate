using PeakandPlate.Model;
using PeakandPlate.Validation;
using System;

namespace PeakandPlate
{
    class Program
    {
        /*
         * Using object-oriented, tested code using the language 
         * that you feel most proficient in, please write a 
         * "pico y placa" predictor. 
         * 
         * The inputs should be: 
         * A license plate number (the full number, not the last digit), 
         * A date (as a String), and 
         * A time, 
         * 
         * and the program will return:
         * whether or not that car can be on the road.
         */
        static void Main(string[] args)
        {

            LicensePlate licensePlate;
            DateTime date;
            TimeSpan time;
            
            while (true)
            {
                Console.WriteLine("Write your license plate number: ");
                licensePlate = new LicensePlate(Console.ReadLine());

                if (licensePlate.IsValid())
                {
                    break;
                }
                else
                {
                    Console.WriteLine("This License Plate Number is invalid!");
                }
            }

            while (true)
            {

                Console.WriteLine("Date: ");
                var dateString = Console.ReadLine();

                // Validation for date string
                if (DateTime.TryParse(dateString, out date))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Date value is invalid!");
                }
            }

            while (true)
            {
                Console.WriteLine("Time: ");
                var timeString = Console.ReadLine();

                // Validation for time string
                if (TimeSpan.TryParse(timeString, out time))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Time value is invalid!");
                }
            }

            // Prediction Process:
            var predictor = new PredictorValidator();

            if (predictor.ValidatePeakandPlate(licensePlate, date, time))
            {
                Console.WriteLine("OK: Car can be on the road!");
            }
            else
            {
                Console.WriteLine("Error: Car can't be on the road!");
            }

            Console.ReadKey();

        }
    }
}
