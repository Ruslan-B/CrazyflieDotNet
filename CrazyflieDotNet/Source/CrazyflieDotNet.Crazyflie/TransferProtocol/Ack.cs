namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public struct Ack
    {
        public int RetryCount { get; set; }
        public bool PowerDetector { get; set; }
        public bool Recieved { get; set; }

        public static Ack Parse(byte headerByte)
        {
            return new Ack
            {
                RetryCount = headerByte >> 4,
                PowerDetector = (headerByte & 0x02) == 0x02,
                Recieved = (headerByte & 0x01) == 0x01
            };
        }

        public static bool IsRecieved(byte[] data) => data.Length > 0 && (data[0] & 0x01) == 0x01;

        public override string ToString() => $"Recieved: {Recieved}, PowerDetector: {PowerDetector}, RetryCount: {RetryCount}";
    }
}