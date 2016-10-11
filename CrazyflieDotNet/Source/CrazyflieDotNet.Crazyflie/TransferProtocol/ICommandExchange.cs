namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public interface ICommandExchange<TResult> : ICommand, IResponceParser<TResult>
    {
    }
}