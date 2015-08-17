namespace Chucksoft.Entities.Database
{
    public class DatabaseColumn
    {
        public string Name { get; set; }
        public string ColumnType { get; set;}
        public string SqlColumnTypeAndSize { get; set; }
        public string SqlColumnType { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsNullable { get; set; }
        public bool IsForeignKey { get; set; }
        public string SqlClientDbType { get; set; }
        public int Size { get; set; }
    }
}