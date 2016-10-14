using System.Linq;

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public sealed class WriteParameterExchange : ICommandExchange<WriteParameterResult>
    {
        private static readonly byte Header = CrtpHelper.CreateHeader(CommunicationPort.Parameters, CommunicationChannel.Channel2);
        private readonly byte[] _commandBytes;

        public WriteParameterExchange(ParameterInfo parameter, object value)
        {
            var payload = ParameterConverter.GetBytes(parameter.Type, value);
            _commandBytes = new[] {Header, parameter.Id}.Concat(payload).ToArray();
        }

        public byte[] GetCommandBytes() => _commandBytes;

        public bool TryParse(byte[] data, out WriteParameterResult result)
        {
            const int dataLength = 3;
            if (data.Length == dataLength && data.StartsWith(_commandBytes, 2))
            {
                result = (WriteParameterResult) data[2];
                return true;
            }
            result = default(WriteParameterResult);
            return false;
        }
    }

    public enum WriteParameterResult : byte
    {
        Ok = 0x00,
    }
}