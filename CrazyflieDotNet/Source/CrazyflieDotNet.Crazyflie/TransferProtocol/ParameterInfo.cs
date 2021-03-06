﻿namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public struct ParameterInfo
    {
        public byte Id { get; set; }

        public ParameterType Type { get; set; }

        public string Group { get; set; }
        public string Name { get; set; }
        public string FullName => $"{Group}.{Name}";

        public override string ToString() => $"Id: {Id}, FullName: {FullName}, Type: {Type}";
    }
}