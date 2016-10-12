namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public struct ParametersInfo
    {
        public byte Count { get; set; }

        public int Crc { get; set; }

        public override string ToString() => $"Count: {Count}, Crc: 0x{Crc:X}";
    }
}