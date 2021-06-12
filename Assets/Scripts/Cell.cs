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
    /// <summary>グライダー生成時に鳴らすSE</summary>
    [SerializeField] public AudioClip _gliderSE;
    /// <summary>周囲に生きているセルが何個あるか</summary>
    public int _count = 0;

    AudioSource _source;
    LifeGame _lifeGame;
    Image _color;

    private void Start()
    {
        _color = GetComponent<Image>();
        _lifeGame = GameObject.Find("LifeGame").GetComponent<LifeGame>();
        _source = GetComponent<AudioSource>();
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

    public void OnGliderSE(AudioClip clip)
    {
        _source.clip = clip;
        _source.PlayOneShot(clip);
    }

    public void StateChenge()
    {
        if (_lifeGame._setmode)
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
        switch (_lifeGame._cellPattern)
        {
            case LifeGame.SetCellPattern.None:
                Debug.Log("何も設定されていません");
                break;
            case LifeGame.SetCellPattern.Blinker:
                if (_horizontalIndex > 0 && _horizontalIndex < _lifeGame._horizontal - 1)
                {
                    for (int i = -1; i < 2; i++)
                    {
                        _lifeGame._cells[_horizontalIndex + i, _verticalIndex]._aliveJudg = true;
                    }
                }
                break;
            case LifeGame.SetCellPattern.LeftUpGlider:
                if (_horizontalIndex > 0 && _horizontalIndex < _lifeGame._horizontal - 1 && _verticalIndex > 0 && _verticalIndex < _lifeGame._vertical - 1)
                {
                    CreatePattern(true, true, false, true, false, true, true, false, false);
                }
                break;
            case LifeGame.SetCellPattern.LeftDownGlider:
                if (_horizontalIndex > 0 && _horizontalIndex < _lifeGame._horizontal - 1 && _verticalIndex > 0 && _verticalIndex < _lifeGame._vertical - 1)
                {
                    CreatePattern(true, false, false, true, false, true, true, true, false);
                }
                break;
            case LifeGame.SetCellPattern.RightUpGlider:
                if (_horizontalIndex > 0 && _horizontalIndex < _lifeGame._horizontal - 1 && _verticalIndex > 0 && _verticalIndex < _lifeGame._vertical - 1)
                {
                    CreatePattern(false, true, true, true, false, true, false, false, true);
                }
                break;
            case LifeGame.SetCellPattern.RightDownGlider:
                if (_horizontalIndex > 0 && _horizontalIndex < _lifeGame._horizontal - 1 && _verticalIndex > 0 && _verticalIndex < _lifeGame._vertical - 1)
                {
                    CreatePattern(false, false, true, true, false, true, false, true, true);
                }
                break;
            case LifeGame.SetCellPattern.Block:
                if (_horizontalIndex < _lifeGame._horizontal - 1 && _verticalIndex < _lifeGame._vertical - 1)
                {
                    CreatePattern(false, false, false, false, true, true, false, true, true);
                }
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 指定したパターンのセルを生成する。誕生させる場所はTrue
    /// </summary>
    /// <param name="leftUp">左上</param>
    /// <param name="up">上</param>
    /// <param name="rightUp">右上</param>
    /// <param name="left">左</param>
    /// <param name="center">真ん中</param>
    /// <param name="right">右</param>
    /// <param name="leftDown">左下</param>
    /// <param name="down">下</param>
    /// <param name="rightDown">右下</param>
    void CreatePattern(bool leftUp, bool up, bool rightUp, bool left, bool center, bool right, bool leftDown, bool down, bool rightDown)
    {
        if (leftUp) _lifeGame._cells[_horizontalIndex - 1, _verticalIndex - 1]._aliveJudg = true;
        if (up) _lifeGame._cells[_horizontalIndex, _verticalIndex - 1]._aliveJudg = true;
        if (rightUp) _lifeGame._cells[_horizontalIndex + 1, _verticalIndex - 1]._aliveJudg = true;
        if (left) _lifeGame._cells[_horizontalIndex - 1, _verticalIndex]._aliveJudg = true;
        if (center) _lifeGame._cells[_horizontalIndex, _verticalIndex]._aliveJudg = true;
        if (right) _lifeGame._cells[_horizontalIndex + 1, _verticalIndex]._aliveJudg = true;
        if (leftDown) _lifeGame._cells[_horizontalIndex - 1, _verticalIndex + 1]._aliveJudg = true;
        if (down) _lifeGame._cells[_horizontalIndex, _verticalIndex + 1]._aliveJudg = true;
        if (rightDown) _lifeGame._cells[_horizontalIndex + 1, _verticalIndex + 1]._aliveJudg = true;
    }
}
