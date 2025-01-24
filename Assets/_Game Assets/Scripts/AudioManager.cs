using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private float audioSourceDestroyDelay;
    
    [Header("Default Values")]
    [SerializeField] private AudioClipSettings defaultAudioClipSettings = AudioClipSettings.Default;
    [SerializeField] private PitchSettingsTemplate defaultPitchSettings;

    public void PlayAudioClip(AudioClip audioClip)
    {
        defaultAudioClipSettings.pitchSettings = defaultPitchSettings == PitchSettingsTemplate.DEFAULT
            ? PitchSettings.Default
            : PitchSettings.SFX;
        
        PlayAudioClip(audioClip, defaultAudioClipSettings);
    }
    public void PlayAudioClip(AudioClip audioClip, AudioClipSettings audioClipSettings)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = audioClip;
        audioSource.volume = audioClipSettings.volume;
        
        audioSource.loop = audioClipSettings.IsLooped;
        audioSource.pitch = audioClipSettings.Pitch;
        
        ProcessAudioClipCall(audioSource, audioClipSettings);
        
        Debug.Log("Now playing audioClip with settings: " + audioClipSettings);
    }

    private void ProcessAudioClipCall(AudioSource audioSource, AudioClipSettings audioClipSettings)
    {
        audioSource.Play();

        float audioCallLength = audioSource.clip.length;
        if (audioClipSettings.IsLooped)
        {
            audioCallLength *= audioClipSettings.loops + 1;
        }
        
        Destroy(audioSource, audioCallLength + audioSourceDestroyDelay);
    }
}

/// <summary>
/// A struct that represents the settings passed to the
/// <see cref="AudioManager"/> when playing an AudioClip.
/// </summary>
[Serializable]
public struct AudioClipSettings
{
    // Template values
    public static AudioClipSettings Default => new AudioClipSettings(1f, PitchSettings.Default);
    public static AudioClipSettings SFX => new AudioClipSettings(1f, PitchSettings.SFX);
    
    public float volume;
    
    [HideInInspector] public PitchSettings pitchSettings;
    public float Pitch => pitchSettings.Pitch; 
    
    public int loops;
    public bool IsLooped => loops > 0;
    
    public AudioClipSettings(float volume, int loops = 0) : this()
    {
        this.volume = volume;
        pitchSettings = PitchSettings.Default;
        this.loops = loops;
    }
    
    public AudioClipSettings(float volume, PitchSettings pitchSettings, int loops = 0) : this()
    {
        this.volume = volume;
        this.pitchSettings = pitchSettings;
        this.loops = loops;
    }
}

/// <summary>
/// A struct that represents the pitch that can be found inside an <see cref="AudioClipSettings"/> struct
/// </summary>
[Serializable]
public struct PitchSettings
{
    // Template values
    public static PitchSettings Default => new PitchSettings(1f);
    public static PitchSettings SFX => new PitchSettings(0.8f, 1.2f);
    
    public float Pitch;
    
    public PitchSettings(float pitch)
    {
        Pitch = pitch;
    }

    public PitchSettings(float randomMax, float randomMin) : this()
    {
        Pitch = Random.Range(randomMax, randomMin);
    }
}

public enum PitchSettingsTemplate
{
    DEFAULT,
    SFX
}