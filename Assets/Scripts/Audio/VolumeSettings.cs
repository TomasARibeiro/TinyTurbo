using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
	[SerializeField] private AudioMixer _audioMixer;

	[SerializeField] private Slider _masterSlider;
	[SerializeField] private Slider _musicSlider;
	[SerializeField] private Slider _sfxSlider;

	private void Start()
	{
		if (PlayerPrefs.HasKey("masterVolume"))
		{
			LoadMasterVolume();
		}
		else
		{
			SetMasterVolume();
		}

		if (PlayerPrefs.HasKey("musicVolume"))
		{
			LoadMusicVolume();
		}
		else
		{
			SetMusicVolume();
		}

		if (PlayerPrefs.HasKey("sfxVolume"))
		{
			LoadSFXVolume();
		}
		else
		{
			SetSFXVolume();
		}
	}

	public void SetMasterVolume()
	{
		float volume = _masterSlider.value;
		_audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
		PlayerPrefs.SetFloat("masterVolume", volume);
	}

	public void SetMusicVolume()
	{
		float volume = _musicSlider.value;
		_audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
		PlayerPrefs.SetFloat("musicVolume", volume);
	}

	public void SetSFXVolume()
	{
		float volume = _sfxSlider.value;
		_audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
		PlayerPrefs.SetFloat("sfxVolume", volume);
	}

	private void LoadMasterVolume()
	{
		_masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
		SetMasterVolume();
	}

	private void LoadMusicVolume()
	{
		_musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
		SetMusicVolume();
	}

	private void LoadSFXVolume()
	{
		_sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
		SetSFXVolume();
	}
}
