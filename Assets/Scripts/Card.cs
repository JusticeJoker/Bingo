using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [Tooltip("Card Max Number")]

    public int maxCardNumber = 59;

    private List<int> allNumbers = new List<int>();

    private List<Cell> cells = new List<Cell>();

    private SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        BuildCard();       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandCard()
    {
        BuildCard();
    }

    public void BuildCard()
    {
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
}
