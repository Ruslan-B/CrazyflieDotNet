#region Imports



#endregion

using System.Linq;

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public sealed class GetLoggingInfoExchange : ICommandExchange<LoggingInfo>
    {
        private static readonly byte Header = CrtpHelper.CreateHeader(CommunicationPort.Logging, CommunicationChannel.Channel0);
        private readonly byte[] _commandBytes;

        public GetLoggingInfoExchange()
        {
            const byte getInfo = 0x01;
            _commandBytes = new[] {Header, getInfo};
        }

        public byte[] GetCommandBytes() => _commandBytes;

        public bool TryParse(byte[] data, out LoggingInfo result)
        {
            const int dataLength = 9;
            if (data.Length == dataLength && data.StartsWith(_commandBytes))
            {
                result = new LoggingInfo
                {
                    Count = data[2],
                    Crc = CrtpHelper.GetCrc(data.Skip(3).Take(4).ToArray()),
                    MaximumNumberOfPackets = data[7],
                    MaximumNumberOfOperation = data[8],
                };
                return true;
            }
            result = default(LoggingInfo);
            return false;
        }
    }
}