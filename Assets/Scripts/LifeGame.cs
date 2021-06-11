using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGame : MonoBehaviour
{
    [SerializeField] GridLayoutGroup _layOutGroup = null;
    /// <summary>縦に並べる数</summary>
    [SerializeField] public int _vertical;
    /// <summary>横に並べる数</summary>
    [SerializeField] public int _horizontal;
    /// <summary>ゲーム中かのフラグ</summary>
    [SerializeField] bool _gameState = false;
    /// <summary>生成するパターンのドロップダウン</summary>
    [SerializeField] Dropdown _cellPatternDropdown;
    /// <summary>列挙型の生成パターン</summary>
    [SerializeField] public SetCellPattern _cellPattern = SetCellPattern.None;
    /// <summary>パターン生成モードか可能かのフラグ</summary>
    [SerializeField] public bool _setmode = false;

    [SerializeField] Text _liveText;

    public Cell[,] _cells;

    [SerializeField] Cell _cell = null;


    /// <summary>生成する振動子パターン</summary>
    public enum SetCellPattern
    {
        /// <summary>設定なし</summary>
        None,
        /// <summary>ブリンカー</summary>
        Blinker,
        /// <summary>左上方向に進むグライダー</summary>
        LeftUpGlider,
        /// <summary>左下方向に進むグライダー</summary>
        LeftDownGlider,
        /// <summary>右上方向に進むグライダー</summary>
        RightUpGlider,
        /// <summary>右下方向に進むグライダー</summary>
        RightDownGlider,
        /// <summary>2x2のブロック</summary>
        Block
    }

    private void OnValidate()
    {
        _layOutGroup.constraintCount = _horizontal;
    }

    private void Start()
    {
        _cells = new Cell[_horizontal, _vertical];

        for (int vert = 0; vert < _vertical; vert++)
        {
            for (int hori = 0; hori < _horizontal; hori++)
            {
                var cell = Instantiate(_cell, _layOutGroup.transform);
                cell._horizontalIndex = hori;
                cell._verticalIndex = vert;
                _cells[hori, vert] = cell;
            }
        }
    }

    private void Update()
    {
        if (_cellPattern != 0)
        {
            _setmode = false;
        }
        else
        {
            _setmode = true;
        }

        if (_gameState)
        {
           
            foreach (var item in _cells)
            {
                item._count = 0;
            }
            AriveJudg();            // 生きているセルの周りにCountを追加する
            NextGeneration();       // 次の世代で生きているかの判定
            StateChenge();          // セルのステートを変える
        }
    }

    /// <summary>
    /// ライブテキストの見た目をかえる(色とテキスト)
    /// </summary>
    public void ColorChenge()
    {
        if (_gameState)
        {
            _liveText.text = "ON LIVE";
            _liveText.color = Color.red;
        }
        else
        {
            _liveText.text = "OFF LIVE";
            _liveText.color = Color.blue;
        }
    }

    /// <summary>
    /// 全てのセルの次の世代の生死判定をする
    /// </summary>
    void AriveJudg()
    {
        for (int h = 0; h < _horizontal; h++)
        {
            for (int v = 0; v < _vertical; v++)
            {
                if (_cells[h, v]._aliveJudg)
                {
                    if (h > 0)
                    {
                        if (v < _vertical - 1)
                        {
                            _cells[h - 1, v + 1]._count++;
                        }
                        if (v < _vertical)
                        {
                            _cells[h - 1, v]._count++;
                        }
                        if (v > 0)
                        {
                            _cells[h - 1, v - 1]._count++;
                        }
                    }
                    if (h < _horizontal - 1)
                    {
                        if (v < _vertical - 1)
                        {
                            _cells[h + 1, v + 1]._count++;
                        }
                        if (v < _vertical)
                        {
                            _cells[h + 1, v]._count++;
                        }
                        if (v > 0)
                        {
                            _cells[h + 1, v - 1]._count++;
                        }
                    }
                    if (v < _vertical - 1)
                    {
                        _cells[h, v + 1]._count++;
                    }
                    if (v > 0)
                    {
                        _cells[h, v - 1]._count++;
                    }
                }
            }
        }
    }
    /// <summary>
    /// セルの次の世代を設定する
    /// </summary>
    void NextGeneration()
    {
        foreach (var item in _cells)
        {
            if (!item._aliveJudg)
            {
                if (item._count == 3)
                {
                    item._nextAliveJudg = true;
                }
                else
                {
                    item._nextAliveJudg = false;
                }
            }
            else
            {
                if (item._count == 2 || item._count == 3)
                {
                    item._nextAliveJudg = true;
                }
                else
                {
                    item._nextAliveJudg = false;
                }
            }
        }
    }
    /// <summary>
    /// 世代を更新する(Cellの生死の切り替え)
    /// </summary>
    void StateChenge()
    {
        foreach (var item in _cells)
        {
            item._aliveJudg = item._nextAliveJudg;
        }
    }
    /// <summary>
    /// 世代を更新し続ける(Buttonにアサインをして呼び出す。)
    /// </summary>
    public void GameStart()
    {
        if (!_gameState)
        {
            _gameState = true;
        }
        else
        {
            _gameState = false;
        }
    }
    /// <summary>
    /// 世代を1つ進める(Buttonのプロパティにアサインして使う。)
    /// </summary>
    public void OneStep()
    {
        foreach (var item in _cells)
        {
            item._count = 0;
        }
        AriveJudg();            // 生きているセルの周りにCountを追加する
        NextGeneration();       // 次の世代で生きているかの判定
        StateChenge();
    }
    /// <summary>
    /// 全てのCellを"死"に切り替える。(Buttonのプロパティにアサインして使う。)
    /// </summary>
    public void OLLClear()
    {
        foreach (var item in _cells)
        {
            item._aliveJudg = false;
        }
    }
    /// <summary>
    /// ランダムに生きているCellをセットする(Buttonのプロパティにアサインして使う。)
    /// </summary>
    public void RandamSetting()
    {
        foreach (var item in _cells)
        {
            int i = Random.Range(0, 3);
            if (i == 0)
            {
                item._aliveJudg = true;
            }
            else
            {
                item._aliveJudg = false;
            }
        }
    }
    /// <summary>
    /// 生成するパターンを変更する(DropDownのプロパティにアサインして使う。)
    /// </summary>
    public void SelectedCreatePattarn()
    {
        switch (_cellPattern)
        {
            case SetCellPattern.None:
                SetPattarn();
                break;
            case SetCellPattern.Blinker:
                SetPattarn();
                break;
            case SetCellPattern.LeftUpGlider:
                SetPattarn();
                break;
            case SetCellPattern.LeftDownGlider:
                SetPattarn();
                break;
            case SetCellPattern.RightUpGlider:
                SetPattarn();
                break;
            case SetCellPattern.RightDownGlider:
                SetPattarn();
                break;
            case SetCellPattern.Block:
                SetPattarn();
                break;
            default:
                break;
        }
    }
   
    private void SetPattarn()
    {
        _cellPattern = (SetCellPattern)_cellPatternDropdown.value;
    }
}
