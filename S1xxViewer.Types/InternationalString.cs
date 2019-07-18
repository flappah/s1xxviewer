namespace S1xxViewer.Types
{
    public class InternationalString
    {
        public string Language { get; set; }
        public string Value { get; set; }

        public InternationalString(string value)
        {
            Value = value;
        }

        public InternationalString(string value, string language)
        {
            Value = value;
            Language = language;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public static implicit operator InternationalString(string value)
        {
            return new InternationalString(value);
        }

        public static bool operator !=(InternationalString a, InternationalString b)
        {
            return a?.Value != b?.Value;
        }
        public static bool operator ==(InternationalString a, InternationalString b)
        {
            return a?.Value == b?.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value;
        }
    }
}
