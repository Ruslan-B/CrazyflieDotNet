#region Imports

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CrazyflieDotNet.Crazyflie;
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
            
            var items = GetTableOfContentsItems(client);

            var monitor = items.Where(x => x.Group == "stabilizer").ToList();
            
            byte blockId = 0x7f;
            controlResult = client.Send(new CreateBlockExchange(blockId, monitor));
            Log.Info($"Create logging block with id: {blockId}, Result: {controlResult}");

            controlResult = client.Send(new StartBlockExchange(blockId, interval: 10));
            Log.Info($"Start logging block with  id: {blockId}, Result: {controlResult}");

            Log.Info($"Done in: {sw.ElapsedMilliseconds}");
            Log.Info("Execute test done! Press Escape!");

            //while (true)
            //{
            //    var data = driver.SendData(new byte[] { 0xff });
            //    if (data.Length > 4)
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

        private static List<TableOfContentsItem> GetTableOfContentsItems(CrazyflieClient client)
        {
            var tableOfContents = client.Send(new GetTableOfContentsExchange());
            Log.Info(tableOfContents);

            var items = new List<TableOfContentsItem>();

            foreach (byte id in Enumerable.Range(0, tableOfContents.Length))
            {
                var item = client.Send(new GetTableOfContentsItemExchange(id));
                items.Add(item);

                Log.Info($"TOC item: {item}");
            }
            return items;
        }
    }
}