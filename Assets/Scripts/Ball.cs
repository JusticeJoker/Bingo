using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    private int index;
    private int number;
    private SpriteRenderer spriteRenderer;
    private List<Sprite> allNumbers = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        Sprite[] ballImages = Resources.LoadAll<Sprite> ("Balls");
        allNumbers.AddRange(ballImages);

        spriteRenderer = GetComponent<SpriteRenderer>();
        index = transform.GetSiblingIndex();

        FindObjectOfType<DrawEvent>().draw.AddListener(CheckBall);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckBall(int number, int drawIndex)
    {
        if (drawIndex == index)
        {
            spriteRenderer.sprite = allNumbers[number];
            spriteRenderer.color = Color.white;
        }
    }
}
