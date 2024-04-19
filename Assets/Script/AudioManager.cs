using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    AudioSource _audioSource;
    public AudioClip Clip;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = Clip;
        _audioSource.Play();
    }
}
