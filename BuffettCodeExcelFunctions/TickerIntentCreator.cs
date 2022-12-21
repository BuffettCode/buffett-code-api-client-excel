using BuffettCodeAPIClient;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;

namespace BuffettCodeExcelFunctions
{
    public class TickerIntentCreator
    {
        public static ITickerIntentParameter Create(string ticker, string intent, FiscalQuarterPeriod latestFiscalQuarterPeriod)
        {
            if (intent.Equals("latest"))
            {
                return TickerDayParameter.Create(ticker, LatestDayPeriod.GetInstance());
            }
            else if (PeriodRegularExpressionConfig.BCodeUdfCompanyString == intent)
            {
                return TickerEmptyIntentParameter.Create(ticker, Snapshot.GetInstance());
            }
            else if (PeriodRegularExpressionConfig.DayRegex.IsMatch(intent))
            {
                return TickerDayParameter.Create(ticker, DayPeriod.Parse(intent));
            }
            else if (PeriodRegularExpressionConfig.FiscalQuarterRegex.IsMatch(intent))
            {
                var period = FiscalQuarterPeriod.Parse(intent);
                return TickerQuarterParameter.Create(ticker, period);
            }
            else if (PeriodRegularExpressionConfig.YearMonthRegex.IsMatch(intent))
            {
                var period = YearMonthPeriod.Parse(intent);
                return TickerYearMonthParameter.Create(ticker, period);
            }
            else if (PeriodRegularExpressionConfig.RelativeFiscalQuarterRegex.IsMatch(intent))
            {
                var period = RelativeFiscalQuarterPeriod.Parse(intent);
                return TickerQuarterParameter.Create(ticker, period);
            }
            else if (PeriodRegularExpressionConfig.BCodeUdfFiscalQuarterInputRegex.IsMatch(intent))
            {
                var match = PeriodRegularExpressionConfig.BCodeUdfFiscalQuarterInputRegex
                .Match(intent);
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
                    throw new ValidationError($"{intent} is not supported input format");
                }

            }
            else
            {
                throw new ValidationError($"{intent} is not supported input format");
            }
        }
    }
}