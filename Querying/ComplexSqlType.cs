namespace Querying
{
    class ComplexSqlType
    {
        public int Units { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{nameof(Units)}: {Units}, {nameof(Name)}: {Name}";
        }
    }
}