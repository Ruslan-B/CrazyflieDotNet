using System.Linq;

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public static class ArrayOfByteExtension
    {
        public static bool StartsWith(this byte[] @this, byte[] value)
        {
            return StartsWith(@this, value, value.Length);
        }

        public static bool StartsWith(this byte[] @this, byte[] value, int length)
        {
            return @this.Length >= length && value.Length >= length && Enumerable.Range(0, length).All(i => @this[i] == value[i]);
        }
    }
}