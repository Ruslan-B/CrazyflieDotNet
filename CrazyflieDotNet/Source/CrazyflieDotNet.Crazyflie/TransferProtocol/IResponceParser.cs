namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public interface IResponceParser<TResult>
    {
        bool TryParse(byte[] data, out TResult result);
    }
}