using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    public string volumeParametr = "MasterVolume";
    public AudioSource introSource, loopSource;
    public AudioMixer mixer;
    public Slider sliderSource;

    private const float _multiplier = 20f;
    private float _volumeVolue;

    private void Awake()
    {
        sliderSource.onValueChanged.AddListener(HundlerSliderValueChanged);
    }

    private void HundlerSliderValueChanged(float volue)
    {
        _volumeVolue = Mathf.Log10(volue) * _multiplier;
        mixer.SetFloat(volumeParametr, _volumeVolue);
    }

    private void Start()
    {
        _volumeVolue = PlayerPrefs.GetFloat(volumeParametr, Mathf.Log10(sliderSource.value) * _multiplier);
        sliderSource.value = Mathf.Pow(10f, _volumeVolue / _multiplier);
    }


    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParametr, _volumeVolue);
    }
}
