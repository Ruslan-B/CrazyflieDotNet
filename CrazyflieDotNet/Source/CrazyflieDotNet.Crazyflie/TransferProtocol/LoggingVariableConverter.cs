using System;

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public class LoggingVariableConverter
    {
        public static object ConvertFrom(LoggingVariableType variableType, byte[] data)
        {
            switch (variableType)
            {
                case LoggingVariableType.UInt8:
                    return data[0];
                case LoggingVariableType.UInt16:
                    return BitConverter.ToUInt16(data, 0);
                case LoggingVariableType.UInt32:
                    return BitConverter.ToUInt32(data, 0);
                case LoggingVariableType.Int8:
                    return (sbyte)data[0];
                case LoggingVariableType.Int16:
                    return BitConverter.ToInt16(data, 0);
                case LoggingVariableType.Int32:
                    return BitConverter.ToInt32(data, 0);
                case LoggingVariableType.Float32:
                    return BitConverter.ToSingle(data, 0);
                case LoggingVariableType.Float16:
                    throw new NotSupportedException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(variableType), variableType, null);
            }
        }
    }
}