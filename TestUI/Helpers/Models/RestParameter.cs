namespace TestUI.Rest.Models
{
    public class RestParameter
    {
        public RestParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}