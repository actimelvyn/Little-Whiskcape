using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Background Music")]
    public AudioSource backgroundMusicSource;
    public AudioClip backgroundMusic;

    [Header("Voice Lines")]
    public AudioClip openingSentence;
    public AudioClip lineBeforeDrinking;
    public AudioClip vaseLineBeforeFalling;
    public AudioClip sentenceAfterBreak;

    [Header("3D Sound Effects")]
    public AudioClip sfxCap;
    public AudioClip sfxDrinking;
    public AudioClip sfxAfterDrinking;
    public AudioClip boingFurniture;
    //public AudioClip cracklingFire;
    public AudioClip vaseBreak;

    private bool musicSpedUp = false; // To prevent setting the pitch repeatedly

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayOpeningSentence();
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null)
        {
            backgroundMusicSource.clip = backgroundMusic;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }

    public void Update()
    {
        // Check if there is less than 15 seconds left in the game
        Script_UI scriptUI = FindObjectOfType<Script_UI>();
        if (scriptUI != null && scriptUI.timeLeft <= 15f && !musicSpedUp)
        {
            SpeedUpMusic();
        }
    }

    private void SpeedUpMusic()
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.pitch = 1.3f; // Make the music 1.6 times faster
            musicSpedUp = true; // Prevent repeated calls
        }
    }

    public void PlayOpeningSentence()
    {
        if (openingSentence != null)
        {
            backgroundMusicSource.PlayOneShot(openingSentence);
        }
    }

    public void PlaySfxOnObject(GameObject obj, AudioClip clip)
    {
        if (obj == null || clip == null) return;

        AudioSource source = obj.GetComponent<AudioSource>();
        if (source == null)
        {
            source = obj.AddComponent<AudioSource>();
            source.spatialBlend = 1.0f; // Set to 3D
        }

        source.clip = clip;
        source.Play();
    }

    /*public void PlayCracklingFire(GameObject fireObject)
    {
        PlaySfxOnObject(fireObject, cracklingFire);
    }*/

    public void PlayVaseBreak(GameObject vaseObject)
    {
        PlaySfxOnObject(vaseObject, vaseBreak);
    }
}
