#region Imports

using System.Linq;
using CrazyflieDotNet.Crazyradio.Driver;

#endregion

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public class CrazyflieClient
    {
        private readonly ICrazyradioDriver _driver;

        public CrazyflieClient(ICrazyradioDriver driver)
        {
            _driver = driver;
        }

        public Ack Send(ICommand command)
        {
            var requestData = command.GetCommandBytes();

            while (true)
            {
                var data = _driver.SendData(requestData);
                if (!Ack.IsRecieved(data))
                {
                    continue;
                }
                return Ack.Parse(data[0]);
            }
        }

        public TResult Send<TResult>(ICommandExchange<TResult> exchange)
        {
            var requestData = exchange.GetCommandBytes();

            while (true)
            {
                var data = _driver.SendData(requestData);
                if (!Ack.IsRecieved(data))
                {
                    continue;
                }

                // drop ack header
                var responceData = data.Skip(1).ToArray();
                TResult result;
                if (exchange.TryParse(responceData, out result))
                {
                    return result;
                }
            }
        }
    }
}