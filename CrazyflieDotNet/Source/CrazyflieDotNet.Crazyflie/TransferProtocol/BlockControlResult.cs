namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public enum BlockControlResult : byte
    {
        /// <summary>
        ///     No error.
        /// </summary>
        Ok = 0,

        /// <summary>
        ///     Block or variable not found.
        /// </summary>
        NotFound = 2,

        /// <summary>
        ///     Log block is too long.
        /// </summary>
        BlockIsTooLong = 7,

        /// <summary>
        ///     Unknown command received.
        /// </summary>
        UnknownCommand = 8,

        /// <summary>
        ///     Log block is exists.
        /// </summary>
        BlockExists = 17,

        /// <summary>
        ///     Generic error.
        /// </summary>
        Error = 0xff
    }
}