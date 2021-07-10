using BuffettCodeCommon.Config;
using System.Linq;

namespace BuffettCodeIO.Resolver
{
    /// <summary>
    /// ハードコードされた定数を用いるAPIリゾルバ
    /// </summary>
    public class V2ConstDataTypeResolver : IDataTypeResolver
    {
        /// <summary>
        /// インディケータの項目名
        /// </summary>
        /// <remarks>
        /// より具体的には /api/{version}/indicator が返却する項目名。
        /// APIの仕様が変更されたら反映する必要があります。
        /// </remarks>
        private static readonly string[] INDICATORS = {
            "stockprice",
            "trading_volume",
            "num_of_shares",
            "market_capital",
            "enterprise_value",
            "eps_forecast",
            "eps_actual",
            "per_forecast",
            "bps",
            "pbr",
            "per_pbr",
            "ebitda_forecast",
            "ebitda_actual",
            "ev_ebitda_forecast",
            "psr_forecast",
            "pcfr_forecast",
            "listed_years",
            "roe",
            "real_roe",
            "net_profit_margin",
            "total_asset_turnover",
            "financial_leverage",
            "roa",
            "roic",
            "ex_dividend",
            "dividend",
            "dividend_yield_forecast",
            "dividend_yield_actual",
            "dividend_payout_ratio",
            "doe",
            "gross_margin",
            "operating_margin",
            "net_sales_operating_cash_flow_ratio",
            "sga_ratio",
            "depreciation_gross_profit_ratio",
            "r_and_d_ratio",
            "interest_op_income_ratio",
            "interest_coverage_ratio",
            "real_corp_tax_rate",
            "net_sales_progress",
            "operating_income_progress",
            "net_income_progress",
            "net_sales_growth_rate_forecast",
            "operating_income_growth_rate_forecast",
            "net_income_growth_rate_forecast",
            "net_sales_per_employee",
            "operating_income_per_employee",
            "trade_receivables",
            "accounts_receivable_turnover",
            "inventories",
            "inventory_turnover",
            "trade_payables",
            "trade_payable_turnover",
            "working_capital",
            "ccc",
            "tangible_fixed_assets_turnover",
            "debt",
            "debt_assets_ratio",
            "debt_monthly_sales_ratio",
            "debt_market_capital_ratio",
            "operating_cash_flow_debt_ratio",
            "net_debt",
            "adjusted_debt_ratio",
            "de_ratio",
            "current_ratio",
            "net_debt_net_income_ratio",
            "equity",
            "equity_ratio",
            "free_cash_flow",
            "cash_market_capital_ratio",
            "cash_assets_ratio",
            "cash_monthly_sales_ratio",
            "accrual" };

        private static readonly V2ConstDataTypeResolver _instance = new V2ConstDataTypeResolver();

        public static V2ConstDataTypeResolver GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// 項目名が財務数値かどうかを判定します
        /// </summary>
        /// <param name="propertyName">項目名</param>
        /// <returns>財務数値の場合はtrue</returns>
        private bool IsIndicator(string propertyName)
        {
            return INDICATORS.Contains(propertyName);
        }

        /// <inheritdoc/>
        public DataTypeConfig Resolve(string propertyName)
        {
            return IsIndicator(propertyName) ? DataTypeConfig.Indicator : DataTypeConfig.Quarter;
        }
    }
}