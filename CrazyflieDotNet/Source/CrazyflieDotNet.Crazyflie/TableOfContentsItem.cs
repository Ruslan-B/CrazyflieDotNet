namespace CrazyflieDotNet.Crazyflie
{
    public class TableOfContentsItem
    {
        public byte Id { get; set; }
        public ItemValueType ValueType { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public string FullName => $"{Group}.{Name}";

        public override string ToString() => $"Id: {Id}, FullName: {FullName}, ValueType: {ValueType}";
    }
}