using System;

public static class NumberFormatter
{
    private static readonly string[] Units = new string[]
    {
        "", "万", "億", "兆", "京", "垓", "秭", "穣", "溝", "澗", "正", "載", "極", "恒河沙", "阿僧祇", "那由他", "不可思議", "無量大数"
    };

    public static string FormatFull(long value)
    {
        if (value < 0)
        {
            return "-" + FormatFull(-value);
        }

        if (value < 10_000)
        {
            return value.ToString("N0") + "円";
        }

        decimal v = value;
        int unitIndex = 0;

        while (v >= 10_000m && unitIndex < Units.Length - 1)
        {
            v /= 10_000m;
            unitIndex++;
        }

        string fmt;
        if (v >= 100m)
        {
            fmt = "0";
        }
        else if (v >= 10m)
        {
            fmt = "0.#";
        }
        else
        {
            fmt = "0.##";
        }

        return v.ToString(fmt) + Units[unitIndex] + "円";
    }

    public static string FormatLimited(long value, int maxChars = 5)
    {
        string s = FormatFull(value);
        if (s.Length <= maxChars)
        {
            return s;
        }

        // Try a shorter format by trimming decimals
        string compact = FormatFullRounded(value, 0);
        if (compact.Length <= maxChars)
        {
            return compact;
        }

        // Fallback: remove currency suffix if still too long
        string noYen = compact.EndsWith("円") ? compact.Substring(0, compact.Length - 1) : compact;
        if (noYen.Length <= maxChars)
        {
            return noYen;
        }

        // Last resort: hard trim
        return noYen.Substring(0, Math.Min(maxChars, noYen.Length));
    }

    static string FormatFullRounded(long value, int decimals)
    {
        if (value < 0)
        {
            return "-" + FormatFullRounded(-value, decimals);
        }

        if (value < 10_000)
        {
            return value.ToString("N0") + "円";
        }

        decimal v = value;
        int unitIndex = 0;

        while (v >= 10_000m && unitIndex < Units.Length - 1)
        {
            v /= 10_000m;
            unitIndex++;
        }

        string fmt = decimals <= 0 ? "0" : "0." + new string('#', decimals);
        return v.ToString(fmt) + Units[unitIndex] + "円";
    }
}