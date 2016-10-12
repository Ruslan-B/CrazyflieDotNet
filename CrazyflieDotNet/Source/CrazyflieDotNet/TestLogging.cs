#region Imports

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CrazyflieDotNet.Crazyflie.TransferProtocol;
using CrazyflieDotNet.Crazyradio.Driver;
using log4net;
using static CrazyflieDotNet.Crazyflie.TransferProtocol.BlockControl;

#endregion

namespace CrazyflieDotNet
{
    public class TestLogging
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        public static void Execute(ICrazyradioDriver driver)
        {
            if (driver == null)
            {
                return;
            }

            var sw = Stopwatch.StartNew();

            var client = new CrazyflieClient(driver);
            BlockControlResult controlResult;
            controlResult = client.Send(new ResetAllBlocksExchange());
            Log.Info($"Reset all logging blocks, Result: {controlResult}");

            client.Send(new FlyControlCommand(roll: 0, pitch: 0, yaw: 0, thrust: 1000));

            var @params = GetParameters(client);

            //client.Send(new Parameters.GetParameterExchange());

            var items = GetLoggingVariables(client);

            var monitor = items.Where(x => x.Group == "stabilizer").ToList();

            //byte blockId = 0x7f;
            //controlResult = client.Send(new CreateBlockExchange(blockId, monitor));
            //Log.Info($"Create logging block with id: {blockId}, Result: {controlResult}");

            //controlResult = client.Send(new StartBlockExchange(blockId, interval: 100));
            //Log.Info($"Start logging block with  id: {blockId}, Result: {controlResult}");

            Log.Info($"Done in: {sw.ElapsedMilliseconds}");
            Log.Info("Execute test done! Press Escape!");

            //while (true)
            //{
            //    var data = driver.SendData(new byte[] { 0xff });
            //    if (data.Count > 4)
            //    {
            //        Console.WriteLine(BitConverter.ToString(data));
            //    }
            //    if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Escape)
            //    {
            //        break;
            //    }
            //}

            client.Send(new ResetAllBlocksExchange());
        }

        private static List<LoggingVariable> GetLoggingVariables(CrazyflieClient client)
        {
            var loggingInfo = client.Send(new GetLoggingInfoExchange());
            Log.Info($"Logging info: {loggingInfo}");

            var items = Enumerable.Range(0, loggingInfo.Count)
                .Select(id =>
                        {
                            var variable = client.Send(new GetLoggingVariableExchange((byte) id));
                            Log.Info($"Logging variable: {variable}");
                            return variable;
                        })
                .ToList();
            return items;
        }

        private static List<Parameter> GetParameters(CrazyflieClient client)
        {
            var paramsInfo = client.Send(new GetParametersInfoExchange());
            Log.Info($"Parameters info: {paramsInfo}");

            var items = Enumerable.Range(0, paramsInfo.Count)
                .Select(id =>
                        {
                            var parameter = client.Send(new GetParameterExchange((byte) id));
                            Log.Info($"Parameter: {parameter}");
                            return parameter;
                        })
                .ToList();
            return items;
        }
    }
}