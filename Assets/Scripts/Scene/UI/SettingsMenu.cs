using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public void SetMainVolume(int volume) {
        Content.Instance.AudioMixerMain.SetFloat("Master", volume);
        print(volume);
    }

    public void SetFXVolume(int value)
    {
        print(value);
        Content.Instance.AudioMixerFX.SetFloat("Master", value);
    }
}
