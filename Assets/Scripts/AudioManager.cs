using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("BGM")]
    public AudioSource bgmSource;
    public AudioClip bgmClip;
    public bool playOnStart = true;
    public float volume = 0.5f;

    void Awake()
    {
        if (bgmSource == null)
        {
            bgmSource = GetComponent<AudioSource>();
        }

        if (bgmSource == null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
        }

        bgmSource.playOnAwake = false;
        bgmSource.loop = true;
        bgmSource.volume = Mathf.Clamp01(volume);

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
}