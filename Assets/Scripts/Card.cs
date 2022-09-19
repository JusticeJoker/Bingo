using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Card : MonoBehaviour
{
    [Tooltip("Card value between 0 and 60")]
    public int maxCardNumber = 59;
    private List<int> allNumbers = new List<int>();
    private List<Cell> cells = new List<Cell>();

    // Start is called before the first frame update
    void Start()
    {
        BuildCard();       
    }

    //Function randomize the card
    public void RandCard()
    {
        //Call BuildCard() function to build a new card
        BuildCard();
        
        //Remove any cells incase they have a red X sprite back to default Sprite
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].myCell = CellType.Blank;
        }
    }

    //Function to build the card and order the numbers
    public void BuildCard()
    {
        FindObjectOfType<DrawEvent>().draw.AddListener(CheckNumber);
        
        cells = Transform.FindObjectsOfType<Cell>().ToList();
        cells = cells.OrderBy(c => c.transform.GetSiblingIndex()).ToList();

        var numbers = Enumerable.Range(1, maxCardNumber).ToList();
        numbers = numbers.OrderBy(i => Guid.NewGuid()).ToList().GetRange(0, 15);

        allNumbers = numbers.OrderBy(n => n).ToList();

        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].Number = allNumbers[i];
        }
    }


    //Function to check if the ball number and number is the same
    public void CheckNumber(int number, int drawNumber)
    {
        //Finds the cell and marks said cell with a Red X sprite
        if (cells.Any(c => c.Number == number))
        {
            var currentCell = cells.Find(e => e.Number == number);
            currentCell.myCell = CellType.RedX;
        }
    }

    //Function to check Bingo condition
    public bool Bingo()
    {
        //Check if one cell doesnt not contain a red X will return false
        foreach (var cell in cells)
        {
            if (cell.myCell != CellType.RedX)
                return false;
        }
        
        //Incase of Bingo substitute all cells with a yellow background
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].myCell = CellType.Yellow;
        }

        return true;
    }
}
