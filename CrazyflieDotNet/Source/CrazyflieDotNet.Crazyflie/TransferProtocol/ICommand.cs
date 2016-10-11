namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public interface ICommand
    {
        byte[] GetCommandBytes();
    }
}