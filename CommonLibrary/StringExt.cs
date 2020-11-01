using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;


    namespace kashiash.utils
    {



        public static class StringExt
        {
            public static string Truncate(this string value, int maxLength)
            {
                if (string.IsNullOrEmpty(value)) return value;
                return value.Length <= maxLength ? value : value.Substring(0, maxLength);
            }


            public static string DigitsOnly(this string value)
            {
                if (string.IsNullOrEmpty(value)) return value;
                Regex regexObj = new Regex(@"[^\d]", RegexOptions.Compiled);
                return regexObj.Replace(value, string.Empty);

            }

            public static string CharsAndDigitsOnly(this string value)
            {
                if (string.IsNullOrEmpty(value)) return value;
                return Regex.Replace(value, "[^a-zA-Z0-9_.]+", string.Empty, RegexOptions.Compiled);
                //return regexObj.Replace(value, string.Empty);

            }

            public static decimal ToDecimal(this string text)
            {
                try
                {
                    return decimal.Parse(text, new CultureInfo("pl-PL"));
                }
                catch (Exception)
                {

                    return 0;
                }
            }

            public static decimal ToDecimal(this string text, CultureInfo culture)
            {
                try
                {
                    return decimal.Parse(text, culture);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            public static DateTime ToDate(this string text)
            {
                string datatext = text.Replace("\"", string.Empty);
                if (datatext != "NULL" && datatext != "")
                {
                    try
                    {
                        return DateTime.Parse(datatext);

                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("{0} is not in the correct format.", datatext);
                        return DateTime.MinValue;
                    }

                }
                return DateTime.MinValue;
            }

            public static Int64 ToInt64(this string text)
            {
                try
                {
                    return Int64.Parse(text);
                }
                catch
                {
                    return 0;
                }
            }


            public static int ToInt(this string text)
            {
                try
                {
                    return int.Parse(text);
                }
                catch
                {
                    return 0;
                }
            }

        }
    }

