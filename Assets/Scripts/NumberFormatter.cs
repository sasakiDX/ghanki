public static class NumberFormatter
{
    public static string Format(long value)
    {
        if (value < 1000)
        {
            return value.ToString();
        }
        else if (value < 10_000)
        {
            // 1ç ` 9.9ç
            return (value / 1000f).ToString("0.#") + "ç";
        }
        else if (value < 100_000_000)
        {
            // 1–œ ` 9999–œ
            return (value / 10_000f).ToString("0.#") + "–œ";
        }
        else
        {
            // 1‰­ˆÈã
            return (value / 100_000_000f).ToString("0.#") + "‰­";
        }
    }
}