using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    private AudioSource _source = null;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!_source.loop && !_source.isPlaying)
            Destroy(gameObject);
    }

    public void Play(SoundDatatable.SoundData inData)
    {
        _source.clip = inData.myClip;
        _source.loop = inData.myLoop;
        _source.volume = inData.myVolume;

        name = $"{_source.clip.ToString()}";

        _source.Play();
    }

    public void Stop()
    {
        Destroy(gameObject);
    }


    public bool HasClip(AudioClip inClip) => _source.clip == inClip;




}
