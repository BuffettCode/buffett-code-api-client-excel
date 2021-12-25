using BuffettCodeAPIClient;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;

namespace BuffettCodeExcelFunctions
{
    public class TickerPeriodParameterCreator
    {
        public static ITickerPeriodParameter Create(string ticker, string periodParam, FiscalQuarterPeriod latestFiscalQuarterPeriod)
        {
            if (periodParam.Equals("latest"))
            {
                return TickerDayParameter.Create(ticker, LatestDayPeriod.GetInstance());
            }
            else if (PeriodRegularExpressionConfig.DayRegex.IsMatch(periodParam))
            {
                return TickerDayParameter.Create(ticker, DayPeriod.Parse(periodParam));
            }
            else if (PeriodRegularExpressionConfig.FiscalQuarterRegex.IsMatch(periodParam))
            {
                var period = FiscalQuarterPeriod.Parse(periodParam);
                return TickerQuarterParameter.Create(ticker, period);
            }
            else if (PeriodRegularExpressionConfig.RelativeFiscalQuarterRegex.IsMatch(periodParam))
            {
                var period = RelativeFiscalQuarterPeriod.Parse(periodParam);
                return TickerQuarterParameter.Create(ticker, period);
            }
            else if (PeriodRegularExpressionConfig.BCodeUdfFiscalQuarterInputRegex.IsMatch(periodParam))
            {
                var match = PeriodRegularExpressionConfig.BCodeUdfFiscalQuarterInputRegex
                .Match(periodParam);
                var fy = match.Groups["fiscalYear"].Value.Trim();
                var fq = match.Groups["fiscalQuarter"].Value.Trim();
                if (fy.Contains("LY"))
                {
                    var prevYears = RelativeFiscalQuarterPeriod.ParseRelativeValue("years", match);
                    if (prevYears > latestFiscalQuarterPeriod.Year)
                    {
                        throw new ValidationError($"{prevYears} is bigger than {latestFiscalQuarterPeriod.Year}");
                    }
                    var fiscalQuarter = fq.Replace("Q", "");
                    var period = FiscalQuarterPeriod.Create(latestFiscalQuarterPeriod.Year - prevYears, uint.Parse(fiscalQuarter));
                    return TickerQuarterParameter.Create(ticker, fy, fiscalQuarter, period);
                }
                else if (fq.Contains("LQ"))
                {
                    var prevQuarters = RelativeFiscalQuarterPeriod.ParseRelativeValue("quarters", match);
                    var period = FiscalQuarterPeriod.Create(uint.Parse(fy), latestFiscalQuarterPeriod.Quarter).Before(0, prevQuarters);
                    return TickerQuarterParameter.Create(ticker, fy, fq, period);
                }
                else
                {
                    throw new ValidationError($"{periodParam} is not supported input format");
                }

            }
            else
            {
                throw new ValidationError($"{periodParam} is not supported input format");
            }
        }
    }
}