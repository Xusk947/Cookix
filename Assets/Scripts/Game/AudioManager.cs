using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public List<AudioClip> MusicList = new List<AudioClip>();
    public AudioClip UnderMinuteClip;
    public AudioClip LevelFinishClip;

    private AudioSource _audioSource;
    private AudioSource _finishSource;
    private AudioClip _current;

    public void Start()
    {
        Instance = this;
        _current = MusicList[UnityEngine.Random.Range(0, MusicList.Count)];

        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.outputAudioMixerGroup = Content.Instance.AudioMixerMain.outputAudioMixerGroup;
        _audioSource.clip = _current;
        _audioSource.playOnAwake = true;
        _audioSource.loop = true;
        _audioSource.Play();

        _finishSource = gameObject.AddComponent<AudioSource>();
        _finishSource.outputAudioMixerGroup = Content.Instance.AudioMixerMain.outputAudioMixerGroup;
        _finishSource.playOnAwake = true;
        _finishSource.volume = 0;
        Events.UnderMinute += UnderMinute;
    }

    private void Update()
    {
        if (_finishSource.clip != null)
        {
            if (_audioSource.volume > 0)
            {
                _audioSource.volume -= Time.deltaTime;
                if (_audioSource.volume < 0) _audioSource.Stop();
            }
            if (_finishSource.volume < 1)
            {
                _finishSource.volume = (_finishSource.volume > 1 ? 1 : _finishSource.volume + Time.deltaTime);
            }
        }
    }

    private void UnderMinute(object sender, EventArgs _)
    {
        _finishSource.clip = UnderMinuteClip;
        _finishSource.Play();
    }

    public void PlayLevelFinishSounds()
    {
        _audioSource.loop = false;
    }

    private void OnDestroy()
    {
        Events.UnderMinute -= UnderMinute;
    }
}
