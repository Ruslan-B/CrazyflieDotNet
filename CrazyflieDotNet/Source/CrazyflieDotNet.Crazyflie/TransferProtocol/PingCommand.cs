namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public sealed class PingCommand : ICommand
    {
        private static readonly byte[] CommandBytes = {0xff};
        
        public byte[] GetCommandBytes() => CommandBytes;
    }
}