using System;

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
    public sealed class LoggingVariable
    {
        public LoggingVariable(byte id, LoggingVariableType type, string group, string name)
        {
            Id = id;
            Type = type;
            Group = group;
            Name = name;
            Size = GetSize(type);
        }

        public byte Id { get; }
        public LoggingVariableType Type { get; }
        public string Group { get; }
        public string Name { get; }
        public string FullName => $"{Group}.{Name}";

        public int Size { get; }

        private static int GetSize(LoggingVariableType type)
        {
            switch (type)
            {
                case LoggingVariableType.UInt8:
                    return 1;
                case LoggingVariableType.UInt16:
                    return 2;
                case LoggingVariableType.UInt32:
                    return 4;
                case LoggingVariableType.Int8:
                    return 1;
                case LoggingVariableType.Int16:
                    return 2;
                case LoggingVariableType.Int32:
                    return 4;
                case LoggingVariableType.Float32:
                    return 4;
                case LoggingVariableType.Float16:
                    return 2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Type), type, null);
            }
        }

        private bool Equals(LoggingVariable other) => Id == other.Id;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((LoggingVariable) obj);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => $"Id: {Id}, FullName: {FullName}, Type: {Type}";
    }
}