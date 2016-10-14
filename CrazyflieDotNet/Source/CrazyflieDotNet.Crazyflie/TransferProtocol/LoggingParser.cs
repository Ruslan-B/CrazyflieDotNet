using System.Collections.Generic;
using System.Linq;

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public sealed class LoggingParser : IResponceParser<LoggingBlock>
    {
        private static readonly byte Header = CrtpHelper.CreateHeader(CommunicationPort.Logging, CommunicationChannel.Channel2);
        private readonly byte[] _headerAndBlockId;
        private readonly LoggingVariable[] _variables;

        public LoggingParser(byte blockId, IEnumerable<LoggingVariable> variables)
        {
            _variables = variables.ToArray();
            _headerAndBlockId = new[] {Header, blockId};
        }

        public bool TryParse(byte[] data, out LoggingBlock result)
        {
            const int minDataLength = 0;
            if (data.Length > minDataLength && data.StartsWith(_headerAndBlockId))
            {
                result = new LoggingBlock {Timestamp = data[4] << 16 | data[3] << 8 | data[2]};
                var skip = 5;
                foreach (var variable in _variables)
                {
                    var size = variable.Size;
                    var value = LoggingVariableConverter.ConvertFrom(variable.Type, data.Skip(skip).Take(size).ToArray());
                    result.Add(variable, value);
                    skip += size;
                }
                return true;
            }
            result = default(LoggingBlock);
            return false;
        }
    }

    public class LoggingBlock : Dictionary<LoggingVariable, object>
    {
        public byte Id { get; set; }
        public int Timestamp { get; set; }
    }
}