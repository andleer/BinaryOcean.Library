using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace BinaryOcean.Library
{
    [DebuggerStepThrough]
    public static class StringExtensions
    {
        /// <summary>
        /// Returns the part of the target string that occurs before the first occurance of the matching string.
        /// </summary>
        /// <param name="s">The target string</param>
        /// <param name="value">The matching string</param>
        /// <returns>string</returns>
        public static string BeforeFirst(this string s, string value)
        {
            int index = s.IndexOf(value);
            if (index < 0) return string.Empty;

            return s.Substring(0, index);
        }

        /// <summary>
        /// Returns the part of the target string that occurs before the last occurance of the matching string.
        /// </summary>
        /// <param name="s">The target string</param>
        /// <param name="value">The matching string</param>
        /// <returns>string</returns>
        public static string BeforeLast(this string s, string value)
        {
            int index = s.LastIndexOf(value);
            if (index < 0) return string.Empty;

            return s.Substring(0, index);
        }

        /// <summary>
        /// Returns the part of the target string that occurs after the first occurance of the matching string.
        /// </summary>
        /// <param name="s">The target string</param>
        /// <param name="value">The matching string</param>
        /// <returns>string</returns>
        public static string AfterFirst(this string s, string value)
        {
            int index = s.IndexOf(value);
            if (index < 0) return string.Empty;

            return s.Substring(index + value.Length);
        }

        /// <summary>
        /// Returns the part of the target string that occurs after the last occurance of the matching string.
        /// </summary>
        /// <param name="s">The target string</param>
        /// <param name="value">The matching string</param>
        /// <returns>string</returns>
        public static string AfterLast(this string s, string value)
        {
            int index = s.LastIndexOf(value);
            if (index < 0) return string.Empty;

            return s.Substring(index + value.Length);
        }

        /// <summary>
        /// Replaces the keys with values from a Dictionary. Both the keys and values must be of type String.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static string Replace(this string s, Dictionary<string, string> dictionary)
        {
            StringBuilder sb = new StringBuilder(s);

            foreach (var pair in dictionary)
                sb.Replace(pair.Key, pair.Value);

            return sb.ToString();
        }

        /// <summary>
        /// Converts the specified string representation to a System.Boolean. Returns the value when successful. Returns null on failure.
        /// </summary>
        public static Boolean? AsBoolean(this string s)
        {
            Boolean value;
            if (Boolean.TryParse(s, out value))
                return value;

            return null;
        }

        /// <summary>
        /// Converts the specified string representation to a System.Boolean. Returns the success or failure of the conversion.
        /// </summary>
        public static bool IsBoolean(this string s)
        {
            return s.AsBoolean().HasValue;
        }

        /// <summary>
        /// Converts the specified string representation to a System.Boolean.  
        /// </summary>
        /// <exception cref="System.FormatException"></exception>
        public static Boolean ToBoolean(this string s)
        {
            return Boolean.Parse(s);
        }

        /// <summary>
        /// Converts the specified string representation to a System.Guid. Returns the value when successful. Returns null on failure.
        /// </summary>
        public static Guid? AsGuid(this string s)
        {
            Guid value;
            if (Guid.TryParse(s, out value))
                return value;

            return null;
        }

        /// <summary>
        /// Converts the specified string representation to a System.Guid. Returns the success or failure of the conversion.
        /// </summary>
        public static bool IsGuid(this string s)
        {
            return s.AsGuid().HasValue;
        }

        /// <summary>
        /// Converts the specified string representation to a System.Guid.  
        /// </summary>
        /// <exception cref="System.FormatException"></exception>
        public static Guid ToGuid(this string s)
        {
            return Guid.Parse(s);
        }

        /// <summary>
        /// Converts the specified string representation to a System.DateTime. Returns the value when successful. Returns null on failure.
        /// </summary>
        public static DateTime? AsDateTime(this string s)
        {
            DateTime value;
            if (DateTime.TryParse(s, out value))
                return value;

            return null;
        }

        /// <summary>
        /// Converts the specified string representation to a System.DateTime. Returns the success or failure of the conversion.
        /// </summary>
        public static bool IsDateTime(this string s)
        {
            return s.AsDateTime().HasValue;
        }

        /// <summary>
        /// Validates the string is a datetime within the valid SQL Server date range. Returns true or false.
        /// </summary>
        public static bool IsSqlDateTime(this string s)
        {
            var date = s.AsDateTime();

            return date.HasValue && date >= new DateTime(1753, 1, 1) && date < new DateTime(9999, 12, 31, 23, 59, 59);
        }

        /// <summary>
        /// Converts the specified string representation to a System.DateTime.  
        /// </summary>
        /// <exception cref="System.FormatException"></exception>
        public static DateTime ToDateTime(this string s)
        {
            return DateTime.Parse(s);
        }

        /// <summary>
        /// Converts the specified string representation to a System.TimeSpan. Returns the value when successful. Returns null on failure.
        /// </summary>
        public static TimeSpan? AsTimeSpan(this string s)
        {
            TimeSpan value;
            if (TimeSpan.TryParse(s, out value))
                return value;

            return null;
        }

        /// <summary>
        /// Converts the specified string representation to a System.TimeSpan. Returns the success or failure of the conversion.
        /// </summary>
        public static bool IsTimeSpan(this string s)
        {
            return s.AsTimeSpan().HasValue;
        }

        /// <summary>
        /// Converts the specified string representation to a System.TimeSpan.  
        /// </summary>
        /// <exception cref="System.FormatException"></exception>
        public static TimeSpan ToTimeSpan(this string s)
        {
            return TimeSpan.Parse(s);
        }

        /// <summary>
        /// Converts the specified string representation to a System.Decimal. Returns the value when successful. Returns null on failure.
        /// </summary>
        public static Decimal? AsDecimal(this string s)
        {
            Decimal value;
            if (Decimal.TryParse(s, out value))
                return value;

            return null;
        }

        /// <summary>
        /// Converts the specified string representation to a System.Decimal. Returns the success or failure of the conversion.
        /// </summary>
        public static bool IsDecimal(this string s)
        {
            return s.AsDecimal().HasValue;
        }

        /// <summary>
        /// Converts the specified string representation to a System.Decimal. 
        /// </summary>
        /// <exception cref="System.FormatException"></exception>
        public static Decimal ToDecimal(this string s)
        {
            return Decimal.Parse(s);
        }

        /// <summary>
        /// Converts the specified string representation to a System.Double. Returns the value when successful. Returns null on failure.
        /// </summary>
        public static Double? AsDouble(this string s)
        {
            Double value;
            if (Double.TryParse(s, out value))
                return value;

            return null;
        }

        /// <summary>
        /// Converts the specified string representation to a System.Double. Returns the success or failure of the conversion.
        /// </summary>
        public static bool IsDouble(this string s)
        {
            return s.AsDouble().HasValue;
        }

        /// <summary>
        /// Converts the specified string representation to a System.Double. Returns the success or failure of the conversion.
        /// </summary>
        public static bool IsNumeric(this string s)
        {
            return s.AsDouble().HasValue;
        }

        /// <summary>
        /// Converts the specified string representation to a System.Double. 
        /// </summary>
        /// <exception cref="System.FormatException"></exception>
        public static Double ToDouble(this string s)
        {
            return Double.Parse(s);
        }

        /// <summary>
        /// Converts the specified string representation to a System.Single. Returns the value when successful. Returns null on failure.
        /// </summary>
        public static Single? AsSingle(this string s)
        {
            Single value;
            if (Single.TryParse(s, out value))
                return value;

            return null;
        }

        /// <summary>
        /// Converts the specified string representation to a System.Single. Returns the success or failure of the conversion.
        /// </summary>
        public static bool IsSingle(this string s)
        {
            return s.AsSingle().HasValue;
        }

        /// <summary>
        /// Converts the specified string representation to a System.Single. 
        /// </summary>
        /// <exception cref="System.FormatException"></exception>
        public static Single ToSingle(this string s)
        {
            return Single.Parse(s);
        }

        /// <summary>
        /// Converts the specified string representation to a System.Int32. Returns the value when successful. Returns null on failure.
        /// </summary>
        public static Int32? AsInt32(this string s)
        {
            Int32 value;
            if (Int32.TryParse(s, out value))
                return value;

            return null;
        }

        /// <summary>
        /// Converts the specified string representation to a System.Int32. Returns the success or failure of the conversion.
        /// </summary>
        public static bool IsInt32(this string s)
        {
            return s.AsInt32().HasValue;
        }

        /// <summary>
        /// Converts the specified string representation to a System.Int32. 
        /// </summary>
        /// <exception cref="System.FormatException"></exception>
        public static Int32 ToInt32(this string s)
        {
            return Int32.Parse(s);
        }

        /// <summary>
        /// Converts the specified string representation to a System.Byte. Returns the value when successful. Returns null on failure.
        /// </summary>
        public static byte? AsByte(this string s)
        {
            byte value;
            if (byte.TryParse(s, out value))
                return value;

            return null;
        }

        /// <summary>
        /// Converts the specified string representation to a System.Byte. Returns the success or failure of the conversion.
        /// </summary>
        public static bool IsByte(this string s)
        {
            return s.AsByte().HasValue;
        }

        /// <summary>
        /// Converts the specified string representation to a System.Byte. 
        /// </summary>
        /// <exception cref="System.FormatException"></exception>
        public static byte ToByte(this string s)
        {
            return byte.Parse(s);
        }

        /// <summary>
        /// Converts the specified string representation to an Enum. Returns the value when successful. Returns null on failure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T? AsEnum<T>(this string s) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be of type Enum");

            // check to see that value exists in enum return null if not
            if (Enum.IsDefined(typeof(T), s))
                return s.ToEnum<T>();

            return null;
        }

        /// <summary>
        /// Converts the specified string representation to an Enum. Returns the success or failure of the conversion.
        /// </summary>
        public static bool IsEnum<T>(this string s) where T : struct
        {
            return s.AsEnum<T>().HasValue;
        }

        /// <summary>
        /// Converts the specified string representation to an Enum. 
        /// </summary>
        /// <exception cref="System.FormatException"></exception>
        public static T ToEnum<T>(this string s) where T : struct
        {
            return (T)Enum.Parse(typeof(T), s, true);
        }
    }
}