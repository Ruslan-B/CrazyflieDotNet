using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public sealed class ConsoleParser : IResponceParser<string>
    {
        private static readonly byte Header = CrtpHelper.CreateHeader(CommunicationPort.Console, CommunicationChannel.Channel0);
        private readonly byte[] _header;

        public ConsoleParser()
        {
            _header = new[] { Header };
        }

        public bool TryParse(byte[] data, out string result)
        {
            const int minDataLength = 0;
            if (data.Length > minDataLength && data.StartsWith(_header))
            {
                var line = data.Skip(1).TakeWhile(b => b != 0).ToArray();
                result = Encoding.ASCII.GetString(line);
                return true;
            }
            result = default(string);
            return false;
        }
    }
}