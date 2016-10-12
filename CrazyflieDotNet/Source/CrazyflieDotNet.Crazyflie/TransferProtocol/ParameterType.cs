using System;

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    [Flags]
    public enum ParameterType : byte
    {
        Int8 = 0x00,
        Int16 = 0x01,
        Int32 = 0x02,
        Int64 = 0x03,
        Float = 0x01 << 2,
        Float16 = 0x01 | Float,
        Float32 = 0x02 | Float,
        Float64 = 0x03 | Float,
        Unsigned = 0x01 << 3,
        UInt8 = 0x00 | Unsigned,
        UInt16 = 0x01 | Unsigned,
        UInt32 = 0x02 | Unsigned,
        UInt64 = 0x03 | Unsigned,
        ReadOnly = 1 << 6,
    }
}