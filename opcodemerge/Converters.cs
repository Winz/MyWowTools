using System;
using System.Globalization;

namespace opcodemerge
{
    public static class Converters
    {
        /// <summary>
        /// Converts numeric's string representation to type. Returns 0 on failure.
        /// </summary>
        /// <typeparam name="T">The type which string representation is given.</typeparam>
        /// <param name="representation">The string representation of type.</param>
        /// <returns>The converted value.</returns>
        public static T ToNumeric<T>(this string representation)
            where T : IConvertible
        {
            return representation.ToNumeric<T>(false);
        }

        /// <summary>
        /// Converts numeric's string representation to type.
        /// </summary>
        /// <typeparam name="T">The type which string representation is given.</typeparam>
        /// <param name="representation">The string representation of type.</param>
        /// <returns>The converted value.</returns>
        /// <param name="throwException">Indicates whether an exception should be thrown if the conversion fails.</param>
        /// <exception cref="System.ArgumentException">String representation cannot be converted to numeric.</exception>
        public static T ToNumeric<T>(this string representation, bool throwException)
            where T : IConvertible
        {
            bool error;
            IConvertible value = 0.0f;
            CultureInfo culture = CultureInfo.InvariantCulture;

            if (string.IsNullOrEmpty(representation))
            {
                error = true;
                goto _end;
            }

            representation = representation.Trim();

            NumberStyles style = NumberStyles.None;
            if (representation.StartsWith("0x"))
            {
                style |= NumberStyles.AllowHexSpecifier;
                representation = representation.Substring(2);
            }

            switch (representation[representation.Length - 1])
            {
                case 'h':
                case 'H':
                    representation = representation.Substring(0, representation.Length - 1);
                    style |= NumberStyles.AllowHexSpecifier;
                    break;
                case 'u':
                case 'U':
                    representation = representation.Substring(0, representation.Length - 1);
                    break;
                default:
                    if (!style.HasFlag(NumberStyles.AllowHexSpecifier))
                        style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign;
                    break;
            }

            if ((style & NumberStyles.AllowDecimalPoint) != 0)
            {
                double double_value;
                error = !(
                    double.TryParse(representation, style, culture = CultureInfo.InvariantCulture, out double_value) ||
                    double.TryParse(representation, style, culture = CultureInfo.CurrentCulture, out double_value) ||
                    double.TryParse(representation, style, culture = CultureInfo.GetCultureInfo("en-US"), out double_value)
                    );
                value = double_value;
            }
            else
            {
                long long_value;
                error = !(
                    long.TryParse(representation, style, culture = CultureInfo.InvariantCulture, out long_value) ||
                    long.TryParse(representation, style, culture = CultureInfo.CurrentCulture, out long_value) ||
                    long.TryParse(representation, style, culture = CultureInfo.GetCultureInfo("en-US"), out long_value)
                    );
                value = long_value;
            }

        _end:
            if (error)
            {
                if (throwException)
                    throw new ArgumentException("Provided string cannot be converted to the specified type.", "representation");
                else
                    value = 0.0f;
            }

            T ret;
            try
            {
                ret = (T)value.ToType(typeof(T), culture);
            }
            catch
            {
                if (throwException)
                    throw;
                else
                    ret = default(T);
            }

            return ret;
        }
    }
}
