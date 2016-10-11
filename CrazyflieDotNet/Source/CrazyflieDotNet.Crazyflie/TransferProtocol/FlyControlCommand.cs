#region Imports

using System;
using System.Linq;

#endregion

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public sealed class FlyControlCommand : ICommand
    {
        private static readonly byte Header = CrtpHelper.CreateHeader(CommunicationPort.Commander, CommunicationChannel.Channel0);
        private readonly byte[] _commandBytes;

        public FlyControlCommand(float roll, float pitch, float yaw, ushort thrust)
        {
            _commandBytes = new[] {Header}
                .Concat(BitConverter.GetBytes(roll))
                .Concat(BitConverter.GetBytes(pitch))
                .Concat(BitConverter.GetBytes(yaw))
                .Concat(BitConverter.GetBytes(thrust))
                .ToArray();
        }

        public byte[] GetCommandBytes() => _commandBytes;
    }
}