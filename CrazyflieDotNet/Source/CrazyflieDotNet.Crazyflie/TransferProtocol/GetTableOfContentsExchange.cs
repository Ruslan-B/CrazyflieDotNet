#region Imports



#endregion

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public sealed class GetTableOfContentsExchange : ICommandExchange<TableOfContents>
    {
        private static readonly byte Header = CrtpHelper.CreateHeader(CommunicationPort.Logging, CommunicationChannel.Channel0);
        private readonly byte[] _commandBytes;

        public GetTableOfContentsExchange()
        {
            const byte getInfo = 0x01;
            _commandBytes = new[] {Header, getInfo};
        }

        public byte[] GetCommandBytes() => _commandBytes;

        public bool TryParse(byte[] data, out TableOfContents result)
        {
            const int dataLength = 9;
            if (data.Length == dataLength && data.StartsWith(_commandBytes))
            {
                result = new TableOfContents
                {
                    Length = data[2],
                    Crc = data[6] << 8 * 3 | data[5] << 8 * 2 | data[4] << 8 | data[3],
                    MaximumNumberOfPackets = data[7],
                    MaximumNumberOfOperation = data[8],
                };
                return true;
            }
            result = default(TableOfContents);
            return false;
        }
    }
}