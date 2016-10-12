using System;

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    [Flags]
    public enum ParameterType : byte
    {
        ReadOnly = 1 << 6,
        Int8 = 0x00,
        Int16 = 0x01,
        Int32 = 0x02,
        Int64 = 0x03,
        Float16 = 0x01 | 0x01 << 2,
        Float32 = 0x02 | 0x01 << 2,
        Float64 = 0x03 | 0x01 << 2,
        UInt8 = 0x00 | 0x01 << 3,
        UInt16 = 0x01 | 0x01 << 3,
        UInt32 = 0x02 | 0x01 << 3,
        UInt64 = 0x03 | 0x01 << 3
    }
}