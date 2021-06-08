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
    /// <summary>自身の配列の場所(vartical)</summary>
    [SerializeField] public int _verticalIndex;
    /// <summary>自身の配列の場所(horizontal)</summary>
    [SerializeField] public int _horizontalIndex;
    /// <summary>周囲に生きているセルが何個あるか</summary>
    public int _count = 0;

    LifeGame lifeGame;
    Image _color;

    private void Start()
    {
        _color = GetComponent<Image>();
        lifeGame = GameObject.Find("LifeGame").GetComponent<LifeGame>();
    }
    private void Update()
    {
        if (_aliveJudg)
        {
            _color.color = new Color(0, 0.75f, 0.75f);
        }
        else
        {
            _color.color = Color.black;
        }
    }

    public void StateChenge()
    {
        if (lifeGame._setmode)
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

    public void SetPattern()
    {
        switch (lifeGame._cellPattern)
        {
            case LifeGame.SetCellPattern.None:
                Debug.Log("何も設定されていません");
                break;
            case LifeGame.SetCellPattern.Blinker:
                for (int i = -1; i < 2; i++)
                {
                    lifeGame._cells[_horizontalIndex + i, _verticalIndex]._aliveJudg = true;
                }
                break;
            case LifeGame.SetCellPattern.LeftUpGlider:
                if (_horizontalIndex > 0 && _horizontalIndex < lifeGame._horizontal && _verticalIndex > 0 && _verticalIndex < lifeGame._vertical)
                {
                    lifeGame._cells[_horizontalIndex - 1, _verticalIndex + 1]._aliveJudg = true;
                    lifeGame._cells[_horizontalIndex - 1, _verticalIndex]._aliveJudg = true;
                    lifeGame._cells[_horizontalIndex - 1, _verticalIndex - 1]._aliveJudg = true;
                    lifeGame._cells[_horizontalIndex, _verticalIndex - 1]._aliveJudg = true;
                    lifeGame._cells[_horizontalIndex + 1, _verticalIndex]._aliveJudg = true;
                }
                break;
            case LifeGame.SetCellPattern.LeftDownGlider:
                if (_horizontalIndex > 0 && _horizontalIndex < lifeGame._horizontal && _verticalIndex > 0 && _verticalIndex < lifeGame._vertical)
                {
                    lifeGame._cells[_horizontalIndex - 1, _verticalIndex - 1]._aliveJudg = true;
                    lifeGame._cells[_horizontalIndex - 1, _verticalIndex]._aliveJudg = true;
                    lifeGame._cells[_horizontalIndex - 1, _verticalIndex + 1]._aliveJudg = true;
                    lifeGame._cells[_horizontalIndex, _verticalIndex + 1]._aliveJudg = true;
                    lifeGame._cells[_horizontalIndex + 1, _verticalIndex]._aliveJudg = true;
                }
                break;
            case LifeGame.SetCellPattern.RightUpGlider:
                if (_horizontalIndex > 0 && _horizontalIndex < lifeGame._horizontal && _verticalIndex > 0 && _verticalIndex < lifeGame._vertical)
                {
                    lifeGame._cells[_horizontalIndex, _verticalIndex - 1]._aliveJudg = true;
                    lifeGame._cells[_horizontalIndex + 1, _verticalIndex - 1]._aliveJudg = true;
                    lifeGame._cells[_horizontalIndex - 1, _verticalIndex]._aliveJudg = true;
                    lifeGame._cells[_horizontalIndex + 1, _verticalIndex]._aliveJudg = true;
                    lifeGame._cells[_horizontalIndex + 1, _verticalIndex + 1]._aliveJudg = true;
                }
                break;
            case LifeGame.SetCellPattern.RightDownGlider:
                if (_horizontalIndex > 0 && _horizontalIndex < lifeGame._horizontal && _verticalIndex > 0 && _verticalIndex < lifeGame._vertical)
                {
                    lifeGame._cells[_horizontalIndex, _verticalIndex + 1]._aliveJudg = true;
                    lifeGame._cells[_horizontalIndex + 1, _verticalIndex + 1]._aliveJudg = true;
                    lifeGame._cells[_horizontalIndex - 1, _verticalIndex]._aliveJudg = true;
                    lifeGame._cells[_horizontalIndex + 1, _verticalIndex]._aliveJudg = true;
                    lifeGame._cells[_horizontalIndex + 1, _verticalIndex - 1]._aliveJudg = true;
                }
                break;
            default:
                break;
        }
    }
}
