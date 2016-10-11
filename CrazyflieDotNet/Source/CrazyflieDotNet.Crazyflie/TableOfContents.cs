namespace CrazyflieDotNet.Crazyflie
{
    public struct TableOfContents
    {
        /// <summary>
        ///     Number of log items contained in the log table of content.
        /// </summary>
        public byte Length { get; set; }

        /// <summary>
        ///     CRC values of the log TOC memory content. This is a fingerprint of the copter build that can be used to cache the
        ///     TOC.
        /// </summary>
        public int Crc { get; set; }

        /// <summary>
        ///     Maximum number of log packets that can be programmed in the copter.
        /// </summary>
        public byte MaximumNumberOfPackets { get; set; }

        /// <summary>
        ///     Maximum number of operation programmable in the copter. An operation is one log variable retrieval programming.
        /// </summary>
        public byte MaximumNumberOfOperation { get; set; }

        public override string ToString() => $"Table of contents information, Length: {Length}, Crc: 0x{Crc:X}, MaximumNumberOfOperation: {MaximumNumberOfOperation}, MaximumNumberOfPackets: {MaximumNumberOfPackets}";
    }
}