#region Imports

using System.Collections.Generic;
using System.Linq;

#endregion

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public static class BlockControl
    {
        private const byte CreateBlock = 0x00;
        private const byte AppendBlock = 0x01;
        private const byte DeleteBlock = 0x02;
        private const byte StartBlock = 0x03;
        private const byte StopBlock = 0x04;
        private const byte ResetAllBlocks = 0x05;

        private static byte[] ToPayload(IEnumerable<LoggingVariable> items) => items.SelectMany(x => new[] {(byte) x.Type, x.Id}).ToArray();

        /// <summary>
        ///     Create a new log block.
        /// </summary>
        public sealed class CreateBlockExchange : BlockControlExchangeBase
        {
            public CreateBlockExchange(byte blockId, IEnumerable<LoggingVariable> variables)
                : base(CreateBlock, blockId, ToPayload(variables))
            {
            }
        }

        /// <summary>
        ///     Append itmes to an existing block.
        /// </summary>
        public sealed class AppendBlockExchange : BlockControlExchangeBase
        {
            public AppendBlockExchange(byte blockId, IEnumerable<LoggingVariable> variables)
                : base(AppendBlock, blockId, ToPayload(variables))
            {
            }
        }

        /// <summary>
        ///     Delete log block.
        /// </summary>
        public sealed class DeleteBlockExchange : BlockControlExchangeBase
        {
            public DeleteBlockExchange(byte blockId)
                : base(DeleteBlock, blockId)
            {
            }
        }

        /// <summary>
        ///     Enable log block transmission.
        /// </summary>
        public sealed class StartBlockExchange : BlockControlExchangeBase
        {
            /// <summary>
            ///     todo
            /// </summary>
            /// <param name="blockId">The block id.</param>
            /// <param name="interval">The interval in millisecond.</param>
            public StartBlockExchange(byte blockId, ushort interval)
                : base(StartBlock, blockId, new[] {(byte) (interval / 10)}) // send interval in 10s of ms.
            {
            }
        }

        /// <summary>
        ///     Disable log block transmission.
        /// </summary>
        public sealed class StopBlockExchange : BlockControlExchangeBase
        {
            public StopBlockExchange(byte blockId)
                : base(StopBlock, blockId)
            {
            }
        }

        /// <summary>
        ///     Delete all log blocks
        /// </summary>
        public sealed class ResetAllBlocksExchange : BlockControlExchangeBase
        {
            public ResetAllBlocksExchange()
                : base(ResetAllBlocks, 0x00)
            {
            }
        }

        public abstract class BlockControlExchangeBase : ICommandExchange<BlockControlResult>
        {
            private static readonly byte Header = CrtpHelper.CreateHeader(CommunicationPort.Logging, CommunicationChannel.Channel1);
            private readonly byte[] _commandBytes;

            protected BlockControlExchangeBase(byte control, byte blockId)
            {
                _commandBytes = new[] {Header, control, blockId};
            }

            protected BlockControlExchangeBase(byte control, byte blockId, byte[] payload)
            {
                _commandBytes = new[] {Header, control, blockId}.Concat(payload).ToArray();
            }

            public byte[] GetCommandBytes()
            {
                return _commandBytes;
            }

            public bool TryParse(byte[] data, out BlockControlResult result)
            {
                const int dataLength = 4;
                const int commandLength = 3;
                if (data.Length == dataLength && data.StartsWith(_commandBytes, commandLength))
                {
                    result = (BlockControlResult) data[3];
                    return true;
                }
                result = BlockControlResult.Error;
                return false;
            }
        }
    }
}