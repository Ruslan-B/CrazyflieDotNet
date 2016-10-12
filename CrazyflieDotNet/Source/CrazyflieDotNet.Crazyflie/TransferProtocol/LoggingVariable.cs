namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public class LoggingVariable
    {
        public byte Id { get; set; }
        public LoggingVariableType Type { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public string FullName => $"{Group}.{Name}";

        public override string ToString() => $"Id: {Id}, FullName: {FullName}, Type: {Type}";
    }
}