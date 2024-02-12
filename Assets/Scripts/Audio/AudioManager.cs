using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // ----- FIELDS ----- //
    public static AudioManager instance;

    [SerializeField] AudioSource _maxAudioSource;
    [SerializeField] AudioSource _chloeAudioSource;

    [SerializeField] List<AudioClip> _audioClips = new List<AudioClip>();

    private Dictionary<string, AudioClip> _dictAudioClips = new Dictionary<string, AudioClip>();
    // ----- FIELDS ----- //
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (_audioClips != null)
        {
            Debug.Log("List count: " + _audioClips.Count);
        }
        else
        {
            Debug.LogError("ExampleList is null!");
        }

        // Create audio clip dictionary
        _dictAudioClips = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in _audioClips)
        {
            if (clip != null)
            {
                _dictAudioClips.Add(clip.name, clip);
                //Debug.Log("add " + clip.name);
            }
            else
            {
                Debug.LogWarning("Found null AudioClip in _audioClips list.");
            }
        }
    }

    public AudioSource GetAudioSource(string name)
    {
        switch (name)
        {
            case "Max":
                return _maxAudioSource;
            case "Chloe":
                return _chloeAudioSource;
            default:
                Debug.Log("wrong name in get audio source : " + name);
                return null;
        }
    }

    public AudioClip GetAudioClip(string name)
    {
        if (_dictAudioClips.ContainsKey(name))
        {
            return _dictAudioClips[name];
        }
        else
        {
            Debug.LogError("Audio clip with name " + name + " not found");
            return null;
        }
    }
}
