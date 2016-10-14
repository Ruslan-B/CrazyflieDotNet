using System.Linq;

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public sealed class ReadParameterExchange : ICommandExchange<object>
    {
        private static readonly byte Header = CrtpHelper.CreateHeader(CommunicationPort.Parameters, CommunicationChannel.Channel1);
        private readonly ParameterType _parameterType;
        private readonly byte[] _commandBytes;

        public ReadParameterExchange(ParameterInfo parameter)
        {
            _parameterType = parameter.Type;
            _commandBytes = new[] {Header, parameter.Id};
        }

        public byte[] GetCommandBytes() => _commandBytes;

        public bool TryParse(byte[] data, out object result)
        {
            const int minDataLength = 3;
            if (data.Length >= minDataLength && data.StartsWith(_commandBytes))
            {
                var paramData = data.Skip(2).ToArray();
                result = ParameterConverter.ConvertFrom(_parameterType, paramData);
                return true;
            }
            result = null;
            return false;
        }
    }
}