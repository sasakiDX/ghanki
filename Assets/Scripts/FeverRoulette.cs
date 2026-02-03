using System.Collections;
using UnityEngine;
using TMPro;

public class FeverRoulette : MonoBehaviour
{
    [Header("Display")]
    public TMP_Text rouletteText;

    [Header("Timing")]
    public float intervalSeconds = 10f;
    public float firstDigitSpinDuration = 0.8f;
    public float lastDigitSpinDuration = 2.6f;
    public float fastTick = 0.05f;
    public float slowTick = 0.2f;

    [Header("Digits")]
    public int minDigit = 1;
    public int maxDigit = 9;

    [Header("Fever")]
    public float feverMultiplier = 2f;
    public float blinkInterval = 0.15f;
    [Range(0.01f, 1f)]
    public float feverChance = 0.02f; // 1/50
    public float feverCooldownSeconds = 300f; // 5 minutes

    private bool isSpinning;
    private bool isFever;
    private float nextFeverAllowedTime;

    void Start()
    {
        if (rouletteText != null)
        {
            rouletteText.text = "----";
        }
        StartCoroutine(RunLoop());
    }

    IEnumerator RunLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalSeconds);
            if (!isSpinning && !isFever)
            {
                yield return StartCoroutine(SpinOnce());
            }
        }
    }

    IEnumerator SpinOnce()
    {
        if (isFever || isSpinning) yield break;

        isSpinning = true;

        int baseDigit = Random.Range(minDigit, maxDigit + 1);
        int lastDigitFinal = DecideLastDigit(baseDigit);
        int[] digits = new int[4];

        // Roll first three digits one by one
        for (int i = 0; i < 3; i++)
        {
            float elapsed = 0f;
            while (elapsed < firstDigitSpinDuration)
            {
                digits[i] = Random.Range(minDigit, maxDigit + 1);
                UpdateText(digits);
                elapsed += fastTick;
                yield return new WaitForSeconds(fastTick);
            }
            digits[i] = baseDigit;
            UpdateText(digits);
        }

        // Roll last digit slowly and stop
        float slowElapsed = 0f;
        while (slowElapsed < lastDigitSpinDuration)
        {
            digits[3] = Random.Range(minDigit, maxDigit + 1);
            UpdateText(digits);

            float t = Mathf.Clamp01(slowElapsed / Mathf.Max(0.01f, lastDigitSpinDuration));
            float wait = Mathf.Lerp(fastTick, slowTick, t);
            slowElapsed += wait;
            yield return new WaitForSeconds(wait);
        }

        digits[3] = lastDigitFinal;
        UpdateText(digits);

        bool win = (lastDigitFinal == baseDigit);
        isSpinning = false;

        if (win && !isFever && Time.time >= nextFeverAllowedTime)
        {
            yield return StartCoroutine(StartFever());
        }
    }

    int DecideLastDigit(int baseDigit)
    {
        // Cooldown: force miss
        if (Time.time < nextFeverAllowedTime)
        {
            return PickDifferentDigit(baseDigit);
        }

        // 1/25 chance to match; otherwise pick a different digit
        if (Random.value < feverChance)
        {
            return baseDigit;
        }

        return PickDifferentDigit(baseDigit);
    }

    int PickDifferentDigit(int baseDigit)
    {
        int d;
        do
        {
            d = Random.Range(minDigit, maxDigit + 1);
        } while (d == baseDigit);
        return d;
    }

    IEnumerator StartFever()
    {
        if (isFever) yield break;
        isFever = true;
        nextFeverAllowedTime = Time.time + feverCooldownSeconds;

        if (MoneySystem.Instance != null)
        {
            MoneySystem.Instance.SetMultiplier(feverMultiplier);
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayWinSfx();
            AudioManager.Instance.PlayFeverBgm();
        }

        Coroutine blink = null;
        if (rouletteText != null)
        {
            blink = StartCoroutine(BlinkText());
        }

        if (AudioManager.Instance != null && AudioManager.Instance.IsFeverBgmPlaying())
        {
            while (AudioManager.Instance != null && AudioManager.Instance.IsFeverBgmPlaying())
            {
                yield return null;
            }
        }
        else
        {
            float duration = 0f;
            if (AudioManager.Instance != null)
            {
                duration = AudioManager.Instance.GetFeverBgmLength();
            }
            if (duration <= 0.1f)
            {
                duration = 10f;
            }
            yield return new WaitForSeconds(duration);
        }

        if (blink != null)
        {
            StopCoroutine(blink);
        }

        if (rouletteText != null)
        {
            rouletteText.enabled = true;
        }

        if (MoneySystem.Instance != null)
        {
            MoneySystem.Instance.SetMultiplier(1f);
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.RestoreBgm();
        }

        isFever = false;
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            rouletteText.enabled = !rouletteText.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    void UpdateText(int[] digits)
    {
        if (rouletteText == null) return;
        rouletteText.text = string.Format("{0}{1}{2}{3}", digits[0], digits[1], digits[2], digits[3]);
    }
}