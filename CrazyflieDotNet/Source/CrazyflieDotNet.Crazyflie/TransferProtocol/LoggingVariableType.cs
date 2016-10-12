namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public enum LoggingVariableType : byte
    {
        UInt8 = 0x01,
        UInt16 = 0x02,
        UInt32 = 0x03,
        Int8 = 0x04,
        Int16 = 0x05,
        Int32 = 0x06,
        Float32 = 0x07,
        Float16 = 0x08
    }
}