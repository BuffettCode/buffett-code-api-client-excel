namespace BuffettCodeAddin.Formatter
{
    class InactionFormatter : Formatter
     {
        private static InactionFormatter _instance = new InactionFormatter();

        public static InactionFormatter GetInstance()
        {
            return _instance;
        }

        public string Format(string value, PropertyDescrption description)
        {
            return value;
        }

        private InactionFormatter()
        {
            // do nothing.
        }

    }
}
