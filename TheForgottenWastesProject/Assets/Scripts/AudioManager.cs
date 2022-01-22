using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume;
        [Range(.1f, 3f)]
        public float pitch;
        [HideInInspector]
        public AudioSource source;
        public bool loop;
    }

    public Sound[] sounds;
    public static AudioManager inst;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound x in sounds)
        {
            x.source = gameObject.AddComponent<AudioSource>();
            x.source.clip = x.clip;
            x.source.volume = x.volume;
            x.source.pitch = x.pitch;
            x.source.loop = x.loop;
        }
    }

    void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound x = Array.Find(sounds, sound => sound.name == name);
        if (x == null)
        {
            Debug.LogWarning(name + " was not found in AudioManager");
            return;
        }
        x.source.Play();
    }

    public void Stop(string name)
    {
        Sound x = Array.Find(sounds, sound => sound.name == name);
        if (x == null)
        {
            Debug.LogWarning(name + " was not found in AudioManager");
            return;
        }
        x.source.Stop();
    }
}