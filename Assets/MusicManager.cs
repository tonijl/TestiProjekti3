using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Music Settings")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioMixerGroup musicGroup;
    [SerializeField, Range(0f, 1f)] private float volume = 1f;

    private AudioSource musicSource;

    private void Awake()
    {

        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource = gameObject.AddComponent<AudioSource>();

        musicSource.clip = backgroundMusic;
        musicSource.outputAudioMixerGroup = musicGroup;
        musicSource.volume = volume;
        musicSource.loop = true;
        musicSource.playOnAwake = false;


    }


    void Start()
    {
       PlayMusic(); 
    }

    public void PlayMusic()
    {
        if(backgroundMusic == null)
        {
            return;
        }

        if(!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

        public void ChangeMusic(AudioClip newMusic)
        {

            if(newMusic == null)
            {
                return;
            }

            backgroundMusic = newMusic;
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }

    



    // Update is called once per frame
    void Update()
    {
        
    }
}
