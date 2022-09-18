using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellType
{
    Blank, Yellow, RedX
}

public class Cell : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private List<Sprite> cellSprites = new List<Sprite>();

    private int _number;

    private CellType _myType;

    public CellType myType
    {
        get
        { 
            return _myType;
        }
        set
        { 
            _myType = value;
            GetComponent<SpriteRenderer>().sprite = cellSprites[(int)_myType];
        }
    }

    public int Number
    {
        get => _number;
        set
        {
            _number = value;
            GetComponentInChildren<TextMesh>().text = (_number + 1).ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Sprite[] cellImages = Resources.LoadAll<Sprite> ("Cell");

        cellSprites.AddRange(cellImages);
        myType = CellType.Blank;
    }
}
