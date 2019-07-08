using UnityEngine;

public static class SFXUtils
{
    public enum Clips {
        Switch,
        Brickbreaking,
        ElectricMotor,
        ElectricMotorOff,
        Control,
        Discontrol,
        Pulley,
        WoodenBox,
        Burnout,
        WalkHard1,
        WalkHard2,
        WalkHard3,
        WalkHard4,
        WalkHard5,
        WalkHard6,
        WalkWood1,
        WalkWood2,
        WalkWood3,
        WalkWood4,
        WalkWood5,
        WalkWood6,
        WalkMetal1,
        WalkMetal2,
        WalkMetal3,
        WalkMetal4,
        WalkMetal5,
        WalkMetal6,
        WalkPatrol1,
        WalkPatrol2,
        WalkPatrol3,
        WalkPatrol4,
        WalkPatrol5,
        WalkPatrol6,
        Jump,
        Fall,
        Count
    };

    private static bool _initialized = false;
    private static AudioClip[] _clips;
    private static AudioSource _audioSrc;

    private static System.Random random;
    private static int lastRand;

    private static void Initialize()
    {
        if (_initialized) return;
        _initialized = true;

        random = new System.Random();

        _clips = new AudioClip[(int)Clips.Count];
		_clips[(int)Clips.Switch] = Resources.Load<AudioClip>("Audio/SFX/switch");
		_clips[(int)Clips.Brickbreaking] = Resources.Load<AudioClip>("Audio/SFX/brickbreaking");
		_clips[(int)Clips.ElectricMotor] = Resources.Load<AudioClip>("Audio/SFX/electricmotor");
		_clips[(int)Clips.ElectricMotorOff] = Resources.Load<AudioClip>("Audio/SFX/electricmotoroff");
		_clips[(int)Clips.Control] = Resources.Load<AudioClip>("Audio/SFX/control");
		_clips[(int)Clips.Discontrol] = Resources.Load<AudioClip>("Audio/SFX/discontrol");
		_clips[(int)Clips.Pulley] = Resources.Load<AudioClip>("Audio/SFX/pulley");
		_clips[(int)Clips.WoodenBox] = Resources.Load<AudioClip>("Audio/SFX/woodenbox");
		_clips[(int)Clips.Burnout] = Resources.Load<AudioClip>("Audio/SFX/burnout");
		_clips[(int)Clips.WalkHard1] = Resources.Load<AudioClip>("Audio/SFX/walking on hard floor/walkhard1");
		_clips[(int)Clips.WalkHard2] = Resources.Load<AudioClip>("Audio/SFX/walking on hard floor/walkhard2");
		_clips[(int)Clips.WalkHard3] = Resources.Load<AudioClip>("Audio/SFX/walking on hard floor/walkhard3");
		_clips[(int)Clips.WalkHard4] = Resources.Load<AudioClip>("Audio/SFX/walking on hard floor/walkhard4");
		_clips[(int)Clips.WalkHard5] = Resources.Load<AudioClip>("Audio/SFX/walking on hard floor/walkhard5");
		_clips[(int)Clips.WalkHard6] = Resources.Load<AudioClip>("Audio/SFX/walking on hard floor/walkhard6");
		_clips[(int)Clips.WalkWood1] = Resources.Load<AudioClip>("Audio/SFX/walk on wooden box/walkwood1");
		_clips[(int)Clips.WalkWood2] = Resources.Load<AudioClip>("Audio/SFX/walk on wooden box/walkwood2");
		_clips[(int)Clips.WalkWood3] = Resources.Load<AudioClip>("Audio/SFX/walk on wooden box/walkwood3");
		_clips[(int)Clips.WalkWood4] = Resources.Load<AudioClip>("Audio/SFX/walk on wooden box/walkwood4");
		_clips[(int)Clips.WalkWood5] = Resources.Load<AudioClip>("Audio/SFX/walk on wooden box/walkwood5");
		_clips[(int)Clips.WalkWood6] = Resources.Load<AudioClip>("Audio/SFX/walk on wooden box/walkwood6");
		_clips[(int)Clips.WalkMetal1] = Resources.Load<AudioClip>("Audio/SFX/walking on metal/walkmetal1");
		_clips[(int)Clips.WalkMetal2] = Resources.Load<AudioClip>("Audio/SFX/walking on metal/walkmetal2");
		_clips[(int)Clips.WalkMetal3] = Resources.Load<AudioClip>("Audio/SFX/walking on metal/walkmetal3");
		_clips[(int)Clips.WalkMetal4] = Resources.Load<AudioClip>("Audio/SFX/walking on metal/walkmetal4");
		_clips[(int)Clips.WalkMetal5] = Resources.Load<AudioClip>("Audio/SFX/walking on metal/walkmetal5");
		_clips[(int)Clips.WalkMetal6] = Resources.Load<AudioClip>("Audio/SFX/walking on metal/walkmetal6");
		_clips[(int)Clips.WalkPatrol1] = Resources.Load<AudioClip>("Audio/SFX/patrolwalking/patrolwalking1");
		_clips[(int)Clips.WalkPatrol2] = Resources.Load<AudioClip>("Audio/SFX/patrolwalking/patrolwalking2");
		_clips[(int)Clips.WalkPatrol3] = Resources.Load<AudioClip>("Audio/SFX/patrolwalking/patrolwalking3");
		_clips[(int)Clips.WalkPatrol4] = Resources.Load<AudioClip>("Audio/SFX/patrolwalking/patrolwalking4");
		_clips[(int)Clips.WalkPatrol5] = Resources.Load<AudioClip>("Audio/SFX/patrolwalking/patrolwalking5");
		_clips[(int)Clips.WalkPatrol6] = Resources.Load<AudioClip>("Audio/SFX/patrolwalking/patrolwalking6");
        _clips[(int)Clips.Jump] = Resources.Load<AudioClip>("Audio/SFX/jump/jump");
        _clips[(int)Clips.Fall] = Resources.Load<AudioClip>("Audio/SFX/jump/fall");

        GameObject obj = GameObject.Find("Audio Manager");
        if (obj == null)
        {
            obj = new GameObject("Audio Manager");
            Object.DontDestroyOnLoad(obj);
        }
		_audioSrc = obj.AddComponent(typeof(AudioSource)) as AudioSource;

        lastRand = -1;
    }

    private static int NextRand(int start, int end)
    {
        int result;
        if (lastRand >= start && lastRand <= end)
        {
            result = random.Next(start, end);
            if (result == lastRand) result = end;
        }
        else
        {
            result = random.Next(start, end + 1);
        }
        return result;
    }

    public static AudioClip GetClip(Clips clip)
    {
        Initialize();
        return _clips[(int)clip];
    }

    public static AudioClip GetRandomClip(Clips start, Clips end)
    {
        Initialize();
        return _clips[NextRand((int)start, (int)end + 1)];
    }

    public static void PlayOnce(Clips clip, float volume)
    {
        Initialize();
        _audioSrc.PlayOneShot(GetClip(clip), volume);
    }

    public static void PlayRandomOnce(Clips start, Clips end, float volume)
    {
        Initialize();
        _audioSrc.PlayOneShot(GetRandomClip(start, end), volume);
    }
}
