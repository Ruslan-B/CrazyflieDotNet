using System.Linq;
using System.Text;

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public sealed class GetParameterExchange : ICommandExchange<Parameter>
    {
        private static readonly byte Header = CrtpHelper.CreateHeader(CommunicationPort.Parameters, CommunicationChannel.Channel0);
        private readonly byte[] _commandBytes;

        public GetParameterExchange(byte id)
        {
            const byte getItem = 0x00;
            _commandBytes = new[] {Header, getItem, id};
        }

        public byte[] GetCommandBytes() => _commandBytes;

        public bool TryParse(byte[] data, out Parameter result)
        {
            const int minDataLength = 5;
            if (data.Length > minDataLength && data.StartsWith(_commandBytes))
            {
                var group = data.Skip(4).TakeWhile(b => b != 0).ToArray();
                var name = data.Skip(group.Length + 5).TakeWhile(b => b != 0).ToArray();
                result = new Parameter
                         {
                             Id = data[2],
                             Type = (ParameterType) data[3],
                             Group = Encoding.ASCII.GetString(group),
                             Name = Encoding.ASCII.GetString(name)
                         };
                return true;
            }
            result = default(Parameter);
            return false;
        }
    }
}