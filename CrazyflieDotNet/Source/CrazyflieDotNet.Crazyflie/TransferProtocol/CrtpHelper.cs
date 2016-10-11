namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    internal static class CrtpHelper
    {
        public  static byte CreateHeader(CommunicationPort port, CommunicationChannel channel) => (byte)(((byte)port & 0x0f) << 4 | ((byte)channel & 0x03));
    }
}