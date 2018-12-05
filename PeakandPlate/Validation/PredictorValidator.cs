using System;
using System.Linq;
using System.Collections.Generic;
using PeakandPlate.Model;

namespace PeakandPlate
{
    /*Pico y placa
    * 
    * 
    * Quito, Ecuador
    * En los primeros días del mes de enero del 2010, 
    * se anunció la aplicación de esta medida en la 
    * ciudad de Quito en Ecuador debido a la congestión 
    * que se presenta en la ciudad.6​ Esta medida empezó 
    * a funcionar en marzo de 2010, donde por un día a 
    * la semana los autos no pueden circular según el 
    * último número de la placa, en dos turnos durante 
    * las horas pico entre las 7:00 y las 9:30 en la mañana 
    * y entre las 16:00 y las 19:30 en la tarde y noche.
    * El calendario de aplicación de la medida es el siguiente:
    * 
    * DÍA	        N° PLACA
    * LUNES	        1 y 2
    * MARTES	    3 y 4
    * MIERCOLES	    5 y 6
    * JUEVES	    7 Y 8
    * VIERNES	    9 y 0
    * 
    * Tomado de: https://es.wikipedia.org/wiki/Pico_y_placa
    */
    public class PredictorValidator
    {
        private readonly IList<PeakRestriction> __peakRestriction;
        private readonly IList<RushHour> __rushHours;

        public PredictorValidator(IList<PeakRestriction> peakRestriction, IList<RushHour> rushHours)
        {
            this.__peakRestriction = peakRestriction;
            this.__rushHours = rushHours;
        }

        public PredictorValidator()
        {
            // SetUp data for the prediction.
            // These are the peak restriction configuration for each day of the week.
            __peakRestriction = new[]
            {
                new PeakRestriction() { Day = DayOfWeek.Monday, PlateNumber = 1 },
                new PeakRestriction() { Day = DayOfWeek.Monday, PlateNumber = 2 },
                new PeakRestriction() { Day = DayOfWeek.Tuesday, PlateNumber = 3 },
                new PeakRestriction() { Day = DayOfWeek.Tuesday, PlateNumber = 4 },
                new PeakRestriction() { Day = DayOfWeek.Wednesday, PlateNumber = 5 },
                new PeakRestriction() { Day = DayOfWeek.Wednesday, PlateNumber = 6 },
                new PeakRestriction() { Day = DayOfWeek.Thursday, PlateNumber = 7 },
                new PeakRestriction() { Day = DayOfWeek.Thursday, PlateNumber = 8 },
                new PeakRestriction() { Day = DayOfWeek.Friday, PlateNumber = 9 },
                new PeakRestriction() { Day = DayOfWeek.Friday, PlateNumber = 0 }
            };

            // Here we can look the times of the day where is applicable the restriction.
            __rushHours = new[] 
            {
                new RushHour(){ TimeOfTheDay=PartsOfTheDay.Morning,
                    StartTime = TimeSpan.Parse("7:00"), EndTime=TimeSpan.Parse("9:30")},

                new RushHour(){ TimeOfTheDay=PartsOfTheDay.Afternoon,
                    StartTime = TimeSpan.Parse("16:00"), EndTime=TimeSpan.Parse("19:30")}
            };
        }

        public bool ValidatePeakandPlate(LicensePlate licensePlate, DateTime date, TimeSpan time)
        {
            return ValidatePeakandPlate(licensePlate, date + time);
        }

        public bool ValidatePeakandPlate(LicensePlate licensePlate, DateTime date)
        {
            var licensePlateNumber = licensePlate.LastDigit.Value;

            // Locate the restrictions for the current license Plate Number
            var peakRestrictions = from item in __peakRestriction
                         where (item.PlateNumber == licensePlateNumber)
                         && (item.Day==date.DayOfWeek)
                         select item;

            //If at least one restriction exists then verify rush hours
            if (peakRestrictions.Count() > 0)
            {
                var restriction = peakRestrictions.FirstOrDefault();
                
                // Now evaluate the time with the rush hours 
                foreach (var item in __rushHours)
                {
                    // If the restriction exists and the time is between Start Times and End Times
                    if (date.TimeOfDay >= item.StartTime && date.TimeOfDay <= item.EndTime)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
