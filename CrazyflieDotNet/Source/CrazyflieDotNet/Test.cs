using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CrazyflieDotNet.Crazyflie.TransferProtocol;
using CrazyflieDotNet.Crazyradio.Driver;
using log4net;
using static CrazyflieDotNet.Crazyflie.TransferProtocol.BlockControl;

namespace CrazyflieDotNet
{
    public class Test
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
            client.Connect();
            BlockControlResult controlResult;
            controlResult = client.Send(new ResetAllBlocksExchange());
            Log.Info($"Reset all logging blocks, Result: {controlResult}");

            var @params = GetParameters(client);
            foreach (var param in @params)
            {

                var value = client.Send(new ReadParameterExchange(param));
                Log.Info($"Param: {param} Value: {value}");
            }
            var soundEffect = @params.FirstOrDefault(x => x.FullName == "sound.effect");

            client.Send(new WriteParameterExchange(soundEffect, 0));
            
            var items = GetLoggingVariables(client);

            var monitor = items.Where(x => x.Group == "stabilizer").ToList();

            byte blockId = 0x7f;
            controlResult = client.Send(new CreateBlockExchange(blockId, monitor));
            Log.Info($"Create logging block with id: {blockId}, Result: {controlResult}");

            controlResult = client.Send(new StartBlockExchange(blockId, interval: 20));
            Log.Info($"Start logging block with  id: {blockId}, Result: {controlResult}");

            client.Send(new FlyControlCommand(roll: 0, pitch: 0, yaw: 0, thrust: 5000));
            Log.Info($"Done in: {sw.ElapsedMilliseconds}");
            Log.Info("Execute test done! Press Escape!");


            client.Listen(new LoggingParser(blockId, monitor))
                .Subscribe(block =>
                           {
                               var pairs = string.Join("; ", block.Select(x => $"{x.Key.Name} = {x.Value}"));
                               Log.Info($"Values from block id: {block.Id}, {pairs}");
                           });

            while (true)
            {
                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    break;
                }
            }

            client.Send(new ResetAllBlocksExchange());
            client.Close();
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

        private static List<ParameterInfo> GetParameters(CrazyflieClient client)
        {
            var paramsInfo = client.Send(new GetParametersInfoExchange());
            Log.Info($"Parameters info: {paramsInfo}");

            var items = Enumerable.Range(0, paramsInfo.Count)
                .Select(id =>
                        {
                            var parameter = client.Send(new GetParameterExchange((byte) id));
                            Log.Info($"ParameterInfo: {parameter}");
                            return parameter;
                        })
                .ToList();
            return items;
        }
    }
}