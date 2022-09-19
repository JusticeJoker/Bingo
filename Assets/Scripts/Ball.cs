using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private int index;
    private SpriteRenderer spriteRenderer;
    private List<Sprite> allNumbers = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        //Array of sprites and get the sprites filename
        Sprite[] ballImages = Resources.LoadAll<Sprite> ("Balls");
        allNumbers.AddRange(ballImages);

        spriteRenderer = GetComponent<SpriteRenderer>();
        index = transform.GetSiblingIndex();

        FindObjectOfType<DrawEvent>().draw.AddListener(CheckBall);
        //FindObjectOfType<DrawEvent>().clearBalls.AddListener(ClearBalls);
    }

    //Function to check which ball is which and match with the correct sprite
    public void CheckBall(int number, int drawIndex)
    {
        if (drawIndex == index)
        {
            spriteRenderer.sprite = allNumbers[number];
            spriteRenderer.color = Color.white;
        }
    }

    // public void ClearBalls()
    // {
    //     Color tempColor = spriteRenderer.color;
    //     tempColor.a = 0;
    //     spriteRenderer.color = tempColor;
    // }
}
