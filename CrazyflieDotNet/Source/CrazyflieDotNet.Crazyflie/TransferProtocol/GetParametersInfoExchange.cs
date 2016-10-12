using System.Linq;

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public sealed class GetParametersInfoExchange : ICommandExchange<ParametersInfo>
    {
        private static readonly byte Header = CrtpHelper.CreateHeader(CommunicationPort.Parameters, CommunicationChannel.Channel0);
        private readonly byte[] _commandBytes;

        public GetParametersInfoExchange()
        {
            const byte getInfo = 0x01;
            _commandBytes = new[] {Header, getInfo};
        }

        public byte[] GetCommandBytes() => _commandBytes;

        public bool TryParse(byte[] data, out ParametersInfo result)
        {
            const int dataLength = 7;
            if (data.Length == dataLength && data.StartsWith(_commandBytes))
            {
                result = new ParametersInfo
                         {
                             Count = data[2],
                             Crc = CrtpHelper.GetCrc(data.Skip(3).Take(4).ToArray())
                         };
                return true;
            }
            result = default(ParametersInfo);
            return false;
        }
    }
}