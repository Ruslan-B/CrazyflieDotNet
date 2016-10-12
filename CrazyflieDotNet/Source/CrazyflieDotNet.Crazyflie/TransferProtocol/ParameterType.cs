using System;

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    [Flags]
    public enum ParameterType : byte
    {
        Int8 = 0x00, // int8_t '<b'
        Int16 = 0x01, // int16_t '<h'
        Int32 = 0x02, // int32_t '<i'
        Int63 = 0x03, // int64_t '<q'
        Float16 = 0x05, // FP16 ''
        Float32 = 0x06, // float '<f'
        Float63 = 0x07, // double '<d'
        UInt8 = 0x08, // uint8_t '<B'
        UInt16 = 0x09, // uint16_t '<H'
        UInt32 = 0x0A, // uint32_t '<L'
        UInt64 = 0x0B, //uint64_t '<Q'
    }
}