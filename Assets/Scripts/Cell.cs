using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    /// <summary>現在の生存判定</summary>
    [SerializeField] public bool _aliveJudg = false;
    /// <summary>次の世代の</summary>
    [SerializeField] public bool _nextAliveJudg;
    Image _color;
    /// <summary>周囲に生きているセルが何個あるか</summary>
    public int _count = 0;

    private void Start()
    {
        _color = GetComponent<Image>();
    }
    private void Update()
    {
        if (_aliveJudg)
        {
            _color.color = Color.blue;
        }
        else
        {
            _color.color = Color.yellow;
        }
    }

    public void StateChenge()
    {
        if (_aliveJudg)
        {
            _aliveJudg = false;
        }
        else
        {
            _aliveJudg = true;
        }
    }
}
