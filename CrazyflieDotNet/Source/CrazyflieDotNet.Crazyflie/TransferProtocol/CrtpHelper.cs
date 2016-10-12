#region Imports

using System.Diagnostics;

#endregion

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    internal static class CrtpHelper
    {
        public static byte CreateHeader(CommunicationPort port, CommunicationChannel channel) => (byte) (((byte) port & 0x0f) << 4 | ((byte) channel & 0x03));

        public static int GetCrc(byte[] data)
        {
            Debug.Assert(data != null && data.Length == 4);
            return data[3] << 8 * 3 | data[2] << 8 * 2 | data[1] << 8 | data[0];
        }
    }
}