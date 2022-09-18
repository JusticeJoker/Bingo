using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class Draw : UnityEvent<int, int> //Ball number, index
{

}
public class DrawEvent : MonoBehaviour
{
    public Draw draw;
    public float drawSpeed = 0.1f;
    public int maxNumbBall = 59;
    private int maxBallsDrawn = 30;
    private List<int> allNumbers = new List<int>();
    private int index = 0;
    //private SpriteRenderer spriteRenderer;
    private List<Sprite> spriteNumb = new List<Sprite>();
    private bool canDraw = true;
    private float lastDraw;
    private int drawIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
       //PlayRound();
    }

    // Update is called once per frame
    void Update()
    {
        if (canDraw)
        {
            if(Time.time - lastDraw > drawSpeed)
            {
                DrawBall();
                lastDraw = Time.time;
            }
        }
    }

    private void DrawBall()
    {
        if (drawIndex >= maxBallsDrawn) return;
        //spriteRenderer.color = Color.white;
        //spriteRenderer.sprite = spriteNumb[allNumbers[drawIndex]];
        draw.Invoke(allNumbers[drawIndex], drawIndex);
        drawIndex++;
    }

    void PlayRound()
    {
        Sprite[] ballImages = Resources.LoadAll<Sprite>("balls");
        spriteNumb.AddRange(ballImages);

        var numbers = Enumerable.Range(0, maxNumbBall).ToList();
        allNumbers = numbers.OrderBy(i => Guid.NewGuid()).ToList().GetRange(0, maxBallsDrawn);
    }

    public void StartGame()
    {
        PlayRound();
    }
}
