using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("BGM")]
    public AudioSource bgmSource;
    public AudioClip bgmClip;
    public bool playOnStart = true;
    public float volume = 0.5f;

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioClip purchaseClip;
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

        if (bgmSource == null)
        {
            bgmSource = GetComponent<AudioSource>();
        }

        if (bgmSource == null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
        }

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
        if (purchaseClip == null || sfxSource == null) return;
        sfxSource.PlayOneShot(purchaseClip);
    }
}