using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGame : MonoBehaviour
{
    [SerializeField] Cell _cell = null;
    [SerializeField] GridLayoutGroup _layOutGroup = null;
    [SerializeField] int _vertical;
    [SerializeField] int _horizontal;
    [SerializeField] bool _gameState = false;

    Cell[,] _cells;

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
                _cells[hori, vert] = cell;
            }
        }
    }
    private void FixedUpdate()
    {
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
    void StateChenge()
    {
        foreach (var item in _cells)
        {
            item._aliveJudg = item._nextAliveJudg;
        }
    }
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
    public void OLLClear()
    {
        foreach (var item in _cells)
        {
            item._aliveJudg = false;
        }
    }
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
}
