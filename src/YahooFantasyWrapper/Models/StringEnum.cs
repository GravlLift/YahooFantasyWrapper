namespace YahooFantasyWrapper.Models
{
    public abstract class StringEnum
    {
        protected StringEnum(string value)
        {
            Value = value;
        }
        public string Value { get; }
        public override string ToString() => Value;
        public static implicit operator string(StringEnum e) { return e.ToString(); }
    }
}
