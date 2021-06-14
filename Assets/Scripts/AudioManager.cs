using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource _source;

    public bool _onBGM = false;

    public float _timeBGM;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _source.Stop();
    }

    /// <summary>
    /// BMGを止める
    /// </summary>
    public void BGMSwicher()
    {
        if (_onBGM)
        {
            _timeBGM = _source.time;
            _source.Stop();
            _onBGM = false;
        }
        else
        {
            _source.time = _timeBGM;
            _source.Play();
            _onBGM = true;
        }
    }
}
