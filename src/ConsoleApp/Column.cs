namespace ConsoleApp
{
    public class Column
    {
        private readonly string name;
        private readonly string type;
        private readonly bool isNullable;
        private readonly short maxLength;

        public Column(string name, string type, bool isNullable, short maxLength)
        {
            this.name = name;
            this.type = type;
            this.isNullable = isNullable;
            this.maxLength = maxLength;
        }

        public override string ToString()
        {
            return $"Column {name} is {type}({maxLength}) and {isNullable}";
        }

        public string FluentMigratorCode()
        {
            return $"                  .WithColumn(\"{name}\"){TypeDescription()}{NullabilityDescription()}";
        }

        private string TypeDescription()
        {
            switch (type)
            {
                case "int":
                    return ".AsInt32()";
                case "nvarchar":
                    return $".AsString({StringLengthDescription()})";
                default:
                    return $".As{type}()";
            }
        }

        private string StringLengthDescription()
        {
            return maxLength == -1 ?
                "int.MaxValue"
              : $"{maxLength/2}";
        }

        private string NullabilityDescription()
        {
            return isNullable ? ".Nullable()" : ".NotNullable()";
        }
    }
}