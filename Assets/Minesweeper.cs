using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Minesweeper : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private int _rows = 1;

    [SerializeField]
    private int _columns;

    [SerializeField]
    private int _mineCount = 1;

    [SerializeField]
    private GridLayoutGroup _gridLayoutGroup = null;

    [SerializeField]
    private Cell _cellPrefab = null;

    List<Cell> cells = new List<Cell>();

    void Start()
    {
        var parent = _gridLayoutGroup.gameObject.transform;
        if (_columns < _rows)
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = _columns;
        }
        else
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            _gridLayoutGroup.constraintCount = _rows;
        }

        var _cells = new Cell[_rows, _columns];

        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                var cell = Instantiate(_cellPrefab);
                cell.transform.SetParent(parent);
                _cells[r, c] = cell;
            }
        }

        for (var i = 0; i < _mineCount; i++)
        {
            Mine(_cells);
        }

        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                Check(_cells, r, c);
            }
        }
    }



    /// <summary>地雷設置処理</summary>
    /// <param name="_cells"></param>
    private void Mine(Cell[,] cells)
    {
        var r = UnityEngine.Random.Range(0, _rows);
        var c = UnityEngine.Random.Range(0, _columns);
        var cell = cells[r, c];
        if (cell.GetComponent<Cell>().CellState != CellState.Mine)
        {
            cell.CellState = CellState.Mine;
        }
        else
        {
            Mine(cells);
        }
    }
    /// <summary>
    /// 地雷の個数判定
    /// </summary>
    /// <param name="_cells"></param>
    /// <param name="r"></param>
    /// <param name="c"></param>
    private void Check(Cell[,] _cells, int r, int c)
    {
        int _mineNum = 0;

        //自身が地雷かの判定
        if (_cells[r, c].CellState != CellState.Mine)
        {
            //左上
            if (r != 0 && c != 0 && _cells[r - 1, c - 1].CellState == CellState.Mine)
            {
                _mineNum++;
            }
            //上
            if (r != 0 && _cells[r-1, c].CellState == CellState.Mine)
            {
                _mineNum++;
            }
            //右上
            if (r != 0 && c != _columns - 1 && _cells[r - 1, c + 1].CellState == CellState.Mine)
            {
                _mineNum++;
            }
            //左
            if (c != 0 && _cells[r, c-1].CellState == CellState.Mine)
            {
                _mineNum++;
            }
            //右
            if (c !=_columns-1 && _cells[r , c+1].CellState == CellState.Mine)
            {
                _mineNum++;
            }
            //左下
            if (c != 0 && r != _rows-1 && _cells[r + 1, c - 1].CellState == CellState.Mine)
            {
                _mineNum++;
            }
            //下
            if (r != _rows - 1 && _cells[r+1, c].CellState == CellState.Mine)
            {
                _mineNum++;
            }
            //右下
            if (c != _columns-1 && r != _rows - 1 && _cells[r + 1, c + 1].CellState == CellState.Mine)
            {
                _mineNum++;
            }
        }

        if (_cells[r, c].CellState != CellState.Mine)
        {
            ChangeState(_cells, r, c, _mineNum);
        }
    }
    /// <summary>
    /// 地雷の数に応じてセルのStateを変える
    /// </summary>
    /// <param name="_cells"></param>
    /// <param name="r"></param>
    /// <param name="c"></param>
    /// <param name="_mineNum"></param>
    private static void ChangeState(Cell[,] _cells, int r, int c, int _mineNum)
    {
        if (_mineNum == 0)
        {
            _cells[r, c].CellState = CellState.None;
        }
        if (_mineNum == 1)
        {
            _cells[r, c].CellState = CellState.One;
        }
        if (_mineNum == 2)
        {
            _cells[r, c].CellState = CellState.Two;
        }
        if (_mineNum == 3)
        {
            _cells[r, c].CellState = CellState.Three;
        }
        if (_mineNum == 4)
        {
            _cells[r, c].CellState = CellState.Four;
        }
        if (_mineNum == 5)
        {
            _cells[r, c].CellState = CellState.Five;
        }
        if (_mineNum == 6)
        {
            _cells[r, c].CellState = CellState.Six;
        }
        if (_mineNum == 7)
        {
            _cells[r, c].CellState = CellState.Seven;
        }
        if (_mineNum == 8)
        {
            _cells[r, c].CellState = CellState.Eight;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var go = eventData.pointerCurrentRaycast.gameObject;

        // クリックされたオブジェクトが Cell を持つかどうか
        var cell = go.GetComponent<Cell>();

        switch (eventData.pointerId)
        {
            case -1://Left
                if (cell != null)
                {
                    cell.Open(); // セルを開く
                    if (cell.isMine)
                    {
                        // TODO: ゲームオーバー
                    }
                }
                break;
            case -2://Right
                if (cell != null)
                {
                    cell.Mark(); // セルにマークを付ける
                }
                break;

        }
    }
}