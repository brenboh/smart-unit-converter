using System;
namespace SmartUnitConverter
{
    public static class DataStorage
    {
        /// <summary>
        /// Unit related to data storage: bytes, kilobytes, megabytes, gigabytes, terabytes
        /// </summary>
        public enum Unit
        {
            B = 1,
            KB = 2,
            MB = 3,
            GB = 4,
            TB = 5
        }

        /// <summary>
        /// Basic conversion from the supplied unit to the desired unit
        /// </summary>
        /// <param name="value">value to be converted</param>
        /// <param name="sourceUnit">unit of the value supplied</param>
        /// <param name="destinationUnit">unit to converted supplied value to</param>
        /// <returns>converted value</returns>
        public static double Convert(double value, Unit sourceUnit, Unit destinationUnit)
        {
            return value / Math.Pow(1024.0, destinationUnit - sourceUnit);
        }

        /// <summary>
        /// Does the basic conversion as well as converts to a unit that may make more sense based on value supplied
        /// </summary>
        /// <param name="value">value to be converted</param>
        /// <param name="sourceUnit">unit of the value supplied</param>
        /// <param name="destinationUnit">unit to converted supplied value to</param>
        /// <param name="smartValue">smart value based on value and supplied unit</param>
        /// <param name="smartUnit">unit the smart unit has been converted to</param>
        /// <returns>value converted to the desired value</returns>
        public static double SmartConvert(double value, Unit sourceUnit, Unit destinationUnit, out double smartValue, out Unit smartUnit)
        {
            double convertedValue = Convert(value, sourceUnit, destinationUnit);

            smartValue = value;
            smartUnit = sourceUnit;

            if (value != 0)
            {
                while ((Math.Abs(smartValue) < 1 && smartUnit != Unit.B) || (Math.Abs(smartValue) >= 1024 && smartUnit != Unit.TB))
                {
                    smartUnit = Math.Abs(smartValue) < 1 ? smartUnit - 1 : smartUnit + 1;
                    smartValue = Convert(value, sourceUnit, smartUnit);
                }
            }
            else
            {
                smartUnit = destinationUnit;
            }

            return convertedValue;
        }
    }
}
