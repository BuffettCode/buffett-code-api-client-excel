using System.Linq;

namespace BuffettCodeIO.Property
{
    /// <summary>
    /// 項目の定義
    /// </summary>
    /// <remarks>
    /// APIレスポンスのcolumn_descriptionに対応します。
    /// </remarks>
    public class PropertyDescription
    {
        private static readonly string[] IGNORED_UNITS = { "なし", "単位無し" };

        /// <summary>
        /// 項目名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 日本語の名称
        /// </summary>
        public string JpName { get; set; }

        private string unit;

        /// <summary>
        /// 単位
        /// </summary>
        public string Unit
        {
            get { return IGNORED_UNITS.Contains(unit) ? "" : unit; }
            set { unit = value; }
        }

        public PropertyDescription(string name, string jpName, string unit)
        {
            Name = name;
            JpName = jpName;
            Unit = unit;
        }

        public static PropertyDescription Empty()
        {
            return new PropertyDescription("", "", "");
        }

        public override int GetHashCode() => (Name, JpName, Unit).GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            else if (this.GetType() != obj.GetType())
            {
                return false;
            }
            else if (this.GetHashCode() != obj.GetHashCode())
            {
                return false;
            }
            else
            {
                var pd = (PropertyDescription)obj;
                return this.Name.Equals(pd.Name)
                    && this.JpName.Equals(pd.JpName)
                    && this.Unit.Equals(pd.Unit);
            }
        }
    }
}