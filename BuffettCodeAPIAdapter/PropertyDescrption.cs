using System.Linq;

namespace BuffettCodeAPIAdapter
{
    /// <summary>
    /// 項目の定義
    /// </summary>
    /// <remarks>
    /// APIレスポンスのcolumn_descriptionに対応します。
    /// </remarks>
    public class PropertyDescrption
    {
        private static readonly string[] INVALID_UNITS = { "なし", "単位無し" };

        /// <summary>
        /// 項目名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 日本語の名称
        /// </summary>
        public string Label { get; set; }

        private string unit;

        /// <summary>
        /// 単位
        /// </summary>
        public string Unit
        {
            get { return INVALID_UNITS.Contains(unit) ? "" : unit; }
            set { unit = value; }
        }

        public PropertyDescrption(string name, string label, string unit)
        {
            Name = name;
            Label = label;
            this.unit = unit;
        }
    }
}
