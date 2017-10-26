using System;
using System.Collections.Generic;
using System.Linq;

namespace ConfigurationHelper.Test
{
    public static class ConfigTestData
    {
        #region Private fields

        /// <summary>
        /// The built in types
        /// </summary>
        private static IEnumerable<object[]> _builtInTypes;

        #endregion

        #region Public constructor

        /// <summary>
        /// Initializes the <see cref="ConfigTestData"/> class.
        /// </summary>
        static ConfigTestData()
        {
            _builtInTypes = GetBuiltInTypes();
        }

        #endregion

        #region Public fields

        /// <summary>
        /// Gets the built in types.
        /// </summary>
        /// <value>
        /// The built in types.
        /// </value>
        public static IEnumerable<object[]> BuiltInTypes => _builtInTypes;

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
                var type = Type.GetType($"System.{typeCode.ToString()}");
                var values = GetBuiltInTypeExampleValues(typeCode);
                foreach (var value in values)
                {
                    yield return new object[] { type, value };
                }
            }
        }

        /// <summary>
        /// Gets the built in type example values.
        /// </summary>
        /// <param name="typeCode">The type code.</param>
        /// <returns>The array of objects containing built in type example values.</returns>
        private static object[] GetBuiltInTypeExampleValues(TypeCode typeCode)
        {
            var randomize = new Random();
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
                    var charValues = Enumerable.Range(Char.MinValue / 10, Char.MaxValue / 10)
                        .Select(i => (char)(i * 10)).ToList();
                    charValues.AddRange(new List<char>() { Char.MinValue, Char.MaxValue });
                    return charValues.Select(c => (object)c).ToArray();
                case TypeCode.SByte:
                    var sByteValues = Enumerable.Range(SByte.MinValue / 10, Math.Abs(SByte.MinValue / 10) + SByte.MaxValue / 10)
                        .Select(i => (sbyte)(i * 10)).ToList();
                    sByteValues.AddRange(new List<sbyte>() { SByte.MinValue, SByte.MaxValue });
                    return sByteValues.Select(b => (object)b).ToArray();
                case TypeCode.Byte:
                    var byteValues = Enumerable.Range(Byte.MinValue / 10, Byte.MaxValue / 10)
                        .Select(i => (byte)(i * 10)).ToList();
                    byteValues.AddRange(new List<byte>() { Byte.MinValue, Byte.MaxValue });
                    return byteValues.Select(b => (object)b).ToArray();
                case TypeCode.Int16:
                    var int16Values = Enumerable.Range(Int16.MinValue / 1000, Math.Abs(Int16.MinValue / 1000) + Int16.MaxValue / 1000)
                        .Select(i => i * 1000).Where(i => i < SByte.MinValue || i > SByte.MaxValue)
                        .Select(i => i + randomize.Next(0, 999)).Select(i => (short)i).ToList();
                    int16Values.AddRange(new List<short>() { Int16.MinValue, Int16.MaxValue });
                        return int16Values.Select(s => (object)s).ToArray();
                case TypeCode.UInt16:
                    var uint16Values = Enumerable.Range(UInt16.MinValue / 1000, UInt16.MaxValue / 1000)
                        .Select(i => i * 1000).Where(i => i > Byte.MaxValue)
                        .Select(i => i + randomize.Next(0, 999)).Select(i => (ushort)i).ToList();
                    uint16Values.AddRange(new List<ushort>() { UInt16.MinValue, UInt16.MaxValue });
                    return uint16Values.Select(s => (object)s).ToArray();
                case TypeCode.Int32:
                    var int32Values = Enumerable.Range(Int32.MinValue / 100000000, Math.Abs(Int32.MinValue / 100000000) + Int32.MaxValue / 100000000)
                        .Select(i => i * 100000000).Where(i => i < Int16.MinValue || i > Int16.MaxValue)
                        .Select(i => i + randomize.Next(0, 99999999)).ToList();
                    int32Values.AddRange(new List<int>() { Int32.MinValue, Int32.MaxValue });
                    return int32Values.Select(i => (object)i).ToArray();
                case TypeCode.UInt32:
                    var uint32HalfValues = Enumerable.Range((int)UInt32.MinValue / 100000000, Int32.MaxValue / 100000000)
                        .Select(i => i * 100000000).Where(i => i < Int16.MinValue || i > Int16.MaxValue).Select(i => (uint)i);
                    var uint32Values = uint32HalfValues.Concat(uint32HalfValues.Select(u => u + Int32.MaxValue))
                        .Select(i => (uint)(i + randomize.Next(0, 99999999))).ToList();
                    uint32Values.AddRange(new List<uint>() { UInt32.MinValue, UInt32.MaxValue });
                    return uint32Values.Select(i => (object)i).ToArray();
                case TypeCode.Int64:
                    return new object[] { Int64.MinValue, Int64.MaxValue };
                case TypeCode.UInt64:
                    return new object[] { UInt64.MinValue, UInt64.MaxValue };
                case TypeCode.Single:
                    var floatValues = Enumerable.Range(Int32.MinValue / 100000000, Math.Abs(Int32.MinValue / 100000000) + Int32.MaxValue / 100000000)
                        .Select(i => i * 100000000).Where(i => i < Int16.MinValue || i > Int16.MaxValue)
                        .Select(i => i + randomize.Next(0, 99999999)).Select(i => (float)i)
                        .Select(f => f + randomize.Next(0, 9999999) / 10000000f).ToList();
                    floatValues.AddRange(new List<float>() { Single.MinValue, Single.MaxValue });
                    return floatValues.Select(f => (object)f).ToArray();
                case TypeCode.Double:
                    var doubleValues = Enumerable.Range(Int32.MinValue / 100000000, Math.Abs(Int32.MinValue / 100000000) + Int32.MaxValue / 100000000)
                        .Select(i => i * 100000000).Where(i => i < Int16.MinValue || i > Int16.MaxValue)
                        .Select(i => i + randomize.Next(0, 99999999)).Select(i => (double)i)
                        .Select(d => d + randomize.Next(0, 999999999) / 1000000000D).ToList();
                    doubleValues.AddRange(new List<double>() { Double.MinValue, Double.MaxValue });
                    return doubleValues.Select(d => (object)d).ToArray();
                case TypeCode.Decimal:
                    return new object[] { Decimal.MinValue, Decimal.MaxValue };
                case TypeCode.DateTime:
                    return new object[] { DateTime.MinValue, DateTime.Now, DateTime.MaxValue };
                case TypeCode.String:
                    return new object[] { null, String.Empty, "TestString" };
                default:
                    return new object[] { null };
            }
        }

        #endregion
    }
}