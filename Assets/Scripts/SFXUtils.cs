using UnityEngine;

public static class SFXUtils
{
    public enum Clips {
        Switch,
        Brickbreaking,
        Count
    };

    private static bool _initialized = false;
    private static AudioClip[] _clips;
    private static AudioSource _audioSrc;

    private static void Initialize()
    {
        if (_initialized) return;
        _initialized = true;
        _clips = new AudioClip[(int)Clips.Count];
		_clips[(int)Clips.Switch] = Resources.Load<AudioClip>("Audio/SFX/switch");
		_clips[(int)Clips.Brickbreaking] = Resources.Load<AudioClip>("Audio/SFX/brickbreaking");
        
        GameObject obj = GameObject.Find("Audio Manager");
        if (obj == null)
        {
            obj = new GameObject("Audio Manager");
            Object.DontDestroyOnLoad(obj);
        }
		_audioSrc = obj.AddComponent(typeof(AudioSource)) as AudioSource;
    }

    public static void PlayOnce(Clips clip, float volume)
    {
        Initialize();
        _audioSrc.PlayOneShot(_clips[(int)clip], volume);
    }
}
