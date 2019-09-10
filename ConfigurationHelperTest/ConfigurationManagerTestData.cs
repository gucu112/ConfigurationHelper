//-----------------------------------------------------------------------------------
// <copyright file="ConfigurationManagerTestData.cs" company="Gucu112">
//     Copyright (c) Gucu112 2017-2019. All rights reserved.
// </copyright>
// <author>Bartlomiej Roszczypala</author>
//-----------------------------------------------------------------------------------

namespace Gucu112.ConfigurationHelper.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Gets test data for <see cref="ConfigurationManagerTest" /> class.
    /// </summary>
    public static class ConfigurationManagerTestData
    {
        #region Public constructor

        /// <summary>
        /// Initializes static members of the <see cref="ConfigurationManagerTestData" /> class.
        /// </summary>
        static ConfigurationManagerTestData()
        {
            BuiltInTypes = GetBuiltInTypes();
        }

        #endregion

        #region Public fields

        /// <summary>
        /// Gets the built in types.
        /// </summary>
        /// <value>
        /// The built in types.
        /// </value>
        public static IEnumerable<object[]> BuiltInTypes { get; private set; }

        #endregion

        #region Private methods

        /// <summary>
        /// Gets the built in types.
        /// </summary>
        /// <returns>The built in types.</returns>
        private static IEnumerable<object[]> GetBuiltInTypes()
        {
            foreach (TypeCode typeCode in Enum.GetValues(typeof(TypeCode)))
            {
                Type type = Type.GetType($"System.{typeCode.ToString()}");
                object[] values = GetBuiltInTypeExampleValues(typeCode);
                foreach (object value in values)
                {
                    yield return new object[] { type, value };
                }
            }
        }

        /// <summary>
        /// Gets the built in type example values.
        /// </summary>
        /// <param name="typeCode">The type code.</param>
        /// <returns>
        /// The array of objects containing built in type example values.
        /// </returns>
        private static object[] GetBuiltInTypeExampleValues(TypeCode typeCode)
        {
            Random randomize = new Random();
            switch (typeCode)
            {
                case TypeCode.Empty:
                    return new object[] { null };
                case TypeCode.Object:
                    return new object[] { new object() };
                case TypeCode.DBNull:
                    return new object[] { DBNull.Value };
                case TypeCode.Boolean:
                    return new object[] { true, false };
                case TypeCode.Char:
                    List<char> charValues = Enumerable.Range(char.MinValue / 10, char.MaxValue / 10)
                        .Select(i => (char)(i * 10)).ToList();
                    charValues.AddRange(new List<char>() { char.MinValue, char.MaxValue });
                    return charValues.Select(c => (object)c).ToArray();
                case TypeCode.SByte:
                    List<sbyte> shortByteValues = Enumerable.Range(sbyte.MinValue / 10, Math.Abs(sbyte.MinValue / 10) + (sbyte.MaxValue / 10))
                        .Select(i => (sbyte)(i * 10)).ToList();
                    shortByteValues.AddRange(new List<sbyte>() { sbyte.MinValue, sbyte.MaxValue });
                    return shortByteValues.Select(b => (object)b).ToArray();
                case TypeCode.Byte:
                    List<byte> byteValues = Enumerable.Range(byte.MinValue / 10, byte.MaxValue / 10)
                        .Select(i => (byte)(i * 10)).ToList();
                    byteValues.AddRange(new List<byte>() { byte.MinValue, byte.MaxValue });
                    return byteValues.Select(b => (object)b).ToArray();
                case TypeCode.Int16:
                    List<short> shortValues = Enumerable.Range(short.MinValue / 1000, Math.Abs(short.MinValue / 1000) + (short.MaxValue / 1000))
                        .Select(i => i * 1000).Where(i => i < sbyte.MinValue || i > sbyte.MaxValue)
                        .Select(i => i + randomize.Next(0, 999)).Select(i => (short)i).ToList();
                    shortValues.AddRange(new List<short>() { short.MinValue, short.MaxValue });
                    return shortValues.Select(s => (object)s).ToArray();
                case TypeCode.UInt16:
                    List<ushort> unsignedShortValues = Enumerable.Range(ushort.MinValue / 1000, ushort.MaxValue / 1000)
                        .Select(i => i * 1000).Where(i => i > byte.MaxValue)
                        .Select(i => i + randomize.Next(0, 999)).Select(i => (ushort)i).ToList();
                    unsignedShortValues.AddRange(new List<ushort>() { ushort.MinValue, ushort.MaxValue });
                    return unsignedShortValues.Select(s => (object)s).ToArray();
                case TypeCode.Int32:
                    List<int> integerValues = Enumerable.Range(int.MinValue / 100000000, Math.Abs(int.MinValue / 100000000) + (int.MaxValue / 100000000))
                        .Select(i => i * 100000000).Where(i => i < short.MinValue || i > short.MaxValue)
                        .Select(i => i + randomize.Next(0, 99999999)).ToList();
                    integerValues.AddRange(new List<int>() { int.MinValue, int.MaxValue });
                    return integerValues.Select(i => (object)i).ToArray();
                case TypeCode.UInt32:
                    IEnumerable<uint> unsignedIntegerHalfValues = Enumerable.Range((int)uint.MinValue / 100000000, int.MaxValue / 100000000)
                        .Select(i => i * 100000000).Where(i => i < short.MinValue || i > short.MaxValue).Select(i => (uint)i);
                    List<uint> unsignedIntegerValues = unsignedIntegerHalfValues.Concat(unsignedIntegerHalfValues.Select(u => u + int.MaxValue))
                        .Select(i => (uint)(i + randomize.Next(0, 99999999))).ToList();
                    unsignedIntegerValues.AddRange(new List<uint>() { uint.MinValue, uint.MaxValue });
                    return unsignedIntegerValues.Select(i => (object)i).ToArray();
                case TypeCode.Int64:
                    return new object[] { long.MinValue, long.MaxValue };
                case TypeCode.UInt64:
                    return new object[] { ulong.MinValue, ulong.MaxValue };
                case TypeCode.Single:
                    List<float> floatValues = Enumerable.Range(int.MinValue / 100000000, Math.Abs(int.MinValue / 100000000) + (int.MaxValue / 100000000))
                        .Select(i => i * 100000000).Where(i => i < short.MinValue || i > short.MaxValue)
                        .Select(i => i + randomize.Next(0, 99999999)).Select(i => (float)i)
                        .Select(f => f + (randomize.Next(0, 9999999) / 10000000f)).ToList();
                    floatValues.AddRange(new List<float>() { float.MinValue, float.MaxValue });
                    return floatValues.Select(f => (object)f).ToArray();
                case TypeCode.Double:
                    List<double> doubleValues = Enumerable.Range(int.MinValue / 100000000, Math.Abs(int.MinValue / 100000000) + (int.MaxValue / 100000000))
                        .Select(i => i * 100000000).Where(i => i < short.MinValue || i > short.MaxValue)
                        .Select(i => i + randomize.Next(0, 99999999)).Select(i => (double)i)
                        .Select(d => d + (randomize.Next(0, 999999999) / 1000000000D)).ToList();
                    doubleValues.AddRange(new List<double>() { double.MinValue, double.MaxValue });
                    return doubleValues.Select(d => (object)d).ToArray();
                case TypeCode.Decimal:
                    return new object[] { decimal.MinValue, decimal.MaxValue };
                case TypeCode.DateTime:
                    return new object[] { DateTime.MinValue, DateTime.Now, DateTime.MaxValue };
                case TypeCode.String:
                    return new object[] { null, string.Empty, "TestString" };
                default:
                    return new object[] { null };
            }
        }

        #endregion
    }
}
