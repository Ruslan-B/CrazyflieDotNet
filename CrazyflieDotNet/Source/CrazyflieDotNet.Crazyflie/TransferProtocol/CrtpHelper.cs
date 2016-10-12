#region Imports

using System;
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
            return BitConverter.ToInt32(data, 0);
        }
    }
}