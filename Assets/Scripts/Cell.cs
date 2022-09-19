using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enumerator for each Cell sprite
public enum CellType
{
    Blank, Yellow, RedX
}

public class Cell : MonoBehaviour
{
    private List<Sprite> cellSprites = new List<Sprite>();
    private int _number;
    private CellType _myCell;
    
    public CellType myCell
    {
        get
        { 
            return _myCell;
        }
        set
        { 
            _myCell = value;
            GetComponent<SpriteRenderer>().sprite = cellSprites[(int)_myCell];
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
        myCell = CellType.Blank;
    }
}
