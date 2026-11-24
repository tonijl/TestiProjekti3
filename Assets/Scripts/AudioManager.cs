using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixerGroup sfxGroup;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(AudioClip clip, Vector3 position, float volume = 1f, float minPitch = 0.9f, float maxPitch = 1.1f)
    {
        if(clip == null)
        {
            return;
        }

        GameObject soundObject = new GameObject("TemporaryAudio");
        soundObject.transform.position = position;

        AudioSource source = soundObject.AddComponent<AudioSource>();

        source.clip = clip;
        source.outputAudioMixerGroup = sfxGroup;
        source.volume = volume;
        source.pitch = Random.Range(minPitch, maxPitch);
        source.spatialBlend = 0f;

        source.Play();

        Destroy(soundObject, clip.length / Mathf.Abs(source.pitch));

    }


}
