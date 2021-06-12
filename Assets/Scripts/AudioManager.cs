using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource _source;

    private bool _onBGM = false;

    float _timeGBM;

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
            _timeGBM = _source.time;
            _source.Stop();
            _onBGM = false;
        }
        else
        {
            _source.time = _timeGBM;
            _source.Play();
            _onBGM = true;
        }
    }
}
