using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public Sound backgroundMusic;
	public Sound[] sounds;

	// Initializes states before Start()
	void Awake()
	{
		initializeSound(backgroundMusic);

		foreach (Sound s in sounds)
		{
			initializeSound(s);
		}
	}

	void Start()
	{
		backgroundMusic.source.Play();
	}


	public void Play(int soundIndex)
	{
		backgroundMusic.source.Stop();
		sounds[soundIndex].source.Play();
	}

	public void Stop(int soundIndex)
	{
		sounds[soundIndex].source.Stop();
	}

	private void initializeSound(Sound s)
	{
		s.source = gameObject.AddComponent<AudioSource>();
		s.source.clip = s.clip;
		s.source.volume = s.volume;
		s.source.pitch = s.pitch;
	}
}
