﻿using System;

namespace BuffettCodeAddin.Formatter
{
    class NumericFormatter : Formatter
    {
        private static NumericFormatter _instance = new NumericFormatter();

        private NumericFormatter()
        {
            // do nothing.
        }

        public static NumericFormatter GetInstance()
        {
            return _instance;
        }

        public string Format(string value, PropertyDescrption description)
        {
            if (long.TryParse(value, out long l))
            {
                return String.Format("{0:#,0}", l);
            }
            else if (double.TryParse(value, out double d))
            {
                return String.Format("{0:#,##0.#}", d);
            }
            else
            {
                return value;
            }
        }
    }
}
