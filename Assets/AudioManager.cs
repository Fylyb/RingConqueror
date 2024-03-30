using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip audioRingBellStart;
    public AudioClip audioRingBellEnd;
    public AudioClip audioAudience;
    public AudioClip audioBlock;
    public AudioClip audioPunchNoReaction;
    public AudioClip audioPunchNoReaction2;
    public AudioClip audioPunchWithReaction;
    public AudioClip audioPunchWithReaction2;

    private AudioSource _ringBellStartSource;
    private AudioSource _ringBellEndSource;
    private AudioSource _audienceSource;
    private AudioSource _blockSource;
    private AudioSource _punchNoReactionSource;
    private AudioSource _punchNoReaction2Source;
    private AudioSource _punchWithReactionSource;
    private AudioSource _punchWithReaction2Source;

    public AudioMixer theMixer;

    void Start()
    {
        _ringBellStartSource = gameObject.AddComponent<AudioSource>();
        _ringBellStartSource.clip = audioRingBellStart;

        _ringBellEndSource = gameObject.AddComponent<AudioSource>();
        _ringBellEndSource.clip = audioRingBellEnd;

        _audienceSource = gameObject.AddComponent<AudioSource>();
        _audienceSource.clip = audioAudience;

        _blockSource = gameObject.AddComponent<AudioSource>();
        _blockSource.clip = audioBlock;

        _punchNoReactionSource = gameObject.AddComponent<AudioSource>();
        _punchNoReactionSource.clip = audioPunchNoReaction;

        _punchNoReaction2Source = gameObject.AddComponent<AudioSource>();
        _punchNoReaction2Source.clip = audioPunchNoReaction2;

        _punchWithReactionSource = gameObject.AddComponent<AudioSource>();
        _punchWithReactionSource.clip = audioPunchWithReaction;

        _punchWithReaction2Source = gameObject.AddComponent<AudioSource>();
        _punchWithReaction2Source.clip = audioPunchWithReaction2;

        // Get the AudioMixer component
        //theMixer = GetComponent<AudioMixer>();

        // Load the master volume from PlayerPrefs and set it in the AudioMixer
        if (PlayerPrefs.HasKey("MasterVol"))
        {
            float masterVol = PlayerPrefs.GetFloat("MasterVol");
            SetMasterVolume(masterVol);
        }
    }

    public void RingBellStart()
    {
        _ringBellStartSource.Play();
    }

    public void RingBellEnd()
    {
        _ringBellEndSource.Play();
    }

    public void AudienceSound()
    {
        _audienceSource.volume = 0.5f;
        _audienceSource.Play();
    }

    public void BlockSound()
    {
        _blockSource.Play();
    }

    public void SoundWhenPunch()
    {
        int randomRange = Random.Range(0, 100);
        if (randomRange < 25)
        {
            _punchNoReaction2Source.Play();
        }
        else if (randomRange >= 25 && randomRange < 50)
        {
            _punchNoReactionSource.Play();
        }
        else if (randomRange >= 50 && randomRange < 75)
        {
            _punchWithReactionSource.Play();
        }
        else if (randomRange >= 75 && randomRange <= 100)
        {
            _punchWithReaction2Source.Play();
        }
    }

    // Method to set the master volume
    public void SetMasterVolume(float volume)
    {
        theMixer.SetFloat("MasterVol", volume);
    }
}
