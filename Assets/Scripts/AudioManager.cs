using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixerGroup sfxGroup;

    AudioSource globalAS;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        globalAS = GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip clip, GameObject objSource, float volume = 1f, float minPitch = 0.9f, float maxPitch = 1.1f)
    {
        if(clip == null)
        {
            return;
        }

        AudioSource source;

        // JOS Et laita objSourcea niin toistetaan annettu klippi AudioManagerin omasta audiosourcesta
        if (objSource == null)
        {
            source = globalAS;
        } else
        {
            source = objSource.GetOrAddComponent<AudioSource>();
        }

        source.outputAudioMixerGroup = sfxGroup;
        source.volume = volume;
        source.pitch = Random.Range(minPitch, maxPitch);
        source.spatialBlend = 0f;

        source.PlayOneShot(clip);
    }


}
