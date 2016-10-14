using System;

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public class ParameterConverter
    {
        public static object ConvertFrom(ParameterType type, byte[] data)
        {
            type &= ~ParameterType.ReadOnly;
            switch (type)
            {
                case ParameterType.Int8:
                    return (sbyte) data[0];
                case ParameterType.Int16:
                    return BitConverter.ToInt16(data, 0);
                case ParameterType.Int32:
                    return BitConverter.ToInt32(data, 0);
                case ParameterType.Int64:
                    return BitConverter.ToInt32(data, 0);
                case ParameterType.Float16:
                    throw new NotSupportedException();
                case ParameterType.Float32:
                    return BitConverter.ToSingle(data, 0);
                case ParameterType.Float64:
                    return BitConverter.ToDouble(data, 0);
                case ParameterType.UInt8:
                    return data[0];
                case ParameterType.UInt16:
                    return BitConverter.ToUInt16(data, 0);
                case ParameterType.UInt32:
                    return BitConverter.ToUInt32(data, 0);
                case ParameterType.UInt64:
                    return BitConverter.ToInt64(data, 0);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static byte[] GetBytes(ParameterType type, object value)
        {
            type &= ~ParameterType.ReadOnly;
            switch (type)
            {
                case ParameterType.Int8:
                    return BitConverter.GetBytes(Convert.ToSByte(value));
                case ParameterType.Int16:
                    return BitConverter.GetBytes(Convert.ToInt16(value));
                case ParameterType.Int32:
                    return BitConverter.GetBytes(Convert.ToInt32(value));
                case ParameterType.Int64:
                    return BitConverter.GetBytes(Convert.ToInt64(value));
                case ParameterType.Float16:
                    throw new NotSupportedException();
                case ParameterType.Float32:
                    return BitConverter.GetBytes(Convert.ToSingle(value));
                case ParameterType.Float64:
                    return BitConverter.GetBytes(Convert.ToDouble(value));
                case ParameterType.UInt8:
                    return new[] {Convert.ToByte(value)};
                case ParameterType.UInt16:
                    return BitConverter.GetBytes(Convert.ToUInt16(value));
                case ParameterType.UInt32:
                    return BitConverter.GetBytes(Convert.ToUInt32(value));
                case ParameterType.UInt64:
                    return BitConverter.GetBytes(Convert.ToUInt64(value));
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}