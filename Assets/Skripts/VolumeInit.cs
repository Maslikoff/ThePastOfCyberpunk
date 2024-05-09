using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeInit : MonoBehaviour
{
    public string volumeParametr = "MasterVolume";
    public AudioMixer mixer;

    void Start()
    {
        var volumeVolue = PlayerPrefs.GetFloat(volumeParametr, volumeParametr == "BackgroundSound" ? 0f : -80f);
        mixer.SetFloat(volumeParametr, volumeVolue);
    }
}
