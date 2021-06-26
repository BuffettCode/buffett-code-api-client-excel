using BuffettCodeIO.Property;
namespace BuffettCodeIO.Formatter
{
    /// <summary>
    /// 値をそのまま返すだけのフォーマッタ
    /// </summary>
    public class InactionFormatter : IPropertyFormatter
    {
        private static readonly InactionFormatter _instance = new InactionFormatter();

        public static InactionFormatter GetInstance()
        {
            return _instance;
        }

        /// <inheritdoc/>
        public string Format(string value, PropertyDescription description)
        {
            return value;
        }

        private InactionFormatter()
        {
            // do nothing.
        }

    }
}