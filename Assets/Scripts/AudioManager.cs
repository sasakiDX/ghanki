using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("BGM")]
    public AudioSource bgmSource;
    public AudioClip bgmClip;
    public AudioClip feverBgmClip;
    public bool playOnStart = true;
    public float volume = 0.5f;

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioClip purchaseClip;
    public AudioClip clickClip;
    public AudioClip winClip;
    public float sfxVolume = 0.7f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //BGM用
        if (bgmSource == null)
        {
            bgmSource = GetComponent<AudioSource>();
        }
        if (bgmSource == null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
        }

        //効果音用
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }

        bgmSource.playOnAwake = false;
        bgmSource.loop = true;
        bgmSource.volume = Mathf.Clamp01(volume);

        sfxSource.playOnAwake = false;
        sfxSource.loop = false;
        sfxSource.volume = Mathf.Clamp01(sfxVolume);

        if (bgmClip != null)
        {
            bgmSource.clip = bgmClip;
        }
    }

    void Start()
    {
        if (playOnStart && bgmSource != null && bgmSource.clip != null)
        {
            bgmSource.Play();
        }
    }

    public void PlayPurchaseSfx()
    {
        PlaySfx(purchaseClip);
    }

    public void PlayClickSfx()
    {
        PlaySfx(clickClip);
    }

    public void PlayWinSfx()
    {
        PlaySfx(winClip);
    }

    public float GetFeverBgmLength()
    {
        return feverBgmClip != null ? feverBgmClip.length : 0f;
    }

    public bool IsFeverBgmPlaying()
    {
        return bgmSource != null && bgmSource.isPlaying && bgmSource.clip == feverBgmClip;
    }

    //BGM切替
    public void PlayFeverBgm()
    {
        if (bgmSource == null || feverBgmClip == null) return;
        bgmSource.Stop();
        bgmSource.clip = feverBgmClip;
        bgmSource.loop = false;
        bgmSource.Play();
    }

    //BGM戻す
    public void RestoreBgm()
    {
        if (bgmSource == null || bgmClip == null) return;
        bgmSource.Stop();
        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    void PlaySfx(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;
        sfxSource.PlayOneShot(clip);
    }
}