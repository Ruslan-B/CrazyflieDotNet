#region Imports

using System.Linq;
using System.Text;

#endregion

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public sealed class GetLoggingVariableExchange : ICommandExchange<LoggingVariable>
    {
        private static readonly byte Header = CrtpHelper.CreateHeader(CommunicationPort.Logging, CommunicationChannel.Channel0);
        public static readonly byte GetItem = 0x00;
        private readonly byte[] _commandBytes;
        
        public GetLoggingVariableExchange(byte id)
        {
            _commandBytes = new[] {Header, GetItem, id};
        }

        public byte[] GetCommandBytes() => _commandBytes;

        public bool TryParse(byte[] data, out LoggingVariable result)
        {
            const int minDataLength = 6;
            if (data.Length > minDataLength && data.StartsWith(_commandBytes))
            {
                var group = data.Skip(4).TakeWhile(b => b != 0).ToArray();
                var name = data.Skip(group.Length + 5).TakeWhile(b => b != 0).ToArray();

                result = new LoggingVariable
                         {
                             Id = data[2],
                             Type = (LoggingVariableType) data[3],
                             Group = Encoding.ASCII.GetString(group),
                             Name = Encoding.ASCII.GetString(name)
                         };
                return true;
            }
            result = default(LoggingVariable);
            return false;
        }
    }
}