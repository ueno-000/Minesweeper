using UnityEngine;
using UnityEngine.UI;



public enum CellState
{
    None = 0, // ‹óƒZƒ‹

    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,

    Mine = -1, // ’n—‹
}

public class Cell : MonoBehaviour
{
    [SerializeField]
    private Text _view = null;

    [SerializeField]
    private Image _cover = null;

    [SerializeField]
    private Text _checker = null;

    public bool isMine = false;

    public bool isOpen = false;

    public bool isCheck = false;

    [SerializeField]
    public CellState _cellState = CellState.None;
    public CellState CellState
    {
        get => _cellState;
        set
        {
            _cellState = value;
            OnCellStateChanged();
        }
    }


    private void OnValidate()
    {
        OnCellStateChanged();

        if (CellState == CellState.Mine)
        {
            isMine = true;
        }
    }

    private void OnCellStateChanged()
    {
        if (_view == null) { return; }

        if (_cellState == CellState.None)
        {
            _view.text = "";
        }
        else if (_cellState == CellState.Mine)
        {
            _view.text = "X";
            _view.color = Color.red;
        }
        else
        {
            _view.text = ((int)_cellState).ToString();
            _view.color = Color.blue;
        }
    }

    public void Open()
    {
        isOpen = true;
        if (_cover == null) { return; }
        _cover.gameObject.SetActive(false);
    }

    public void Check()
    {
        if (!isCheck)
        {
            _checker.gameObject.SetActive(true);
            isCheck = true;
        }
        else
        {
            _checker.gameObject.SetActive(false);
            isCheck = false; 
        }

    }

}