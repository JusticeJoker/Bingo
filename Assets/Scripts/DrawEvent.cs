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
    public Ball ball;
    public float drawSpeed = 0.1f;
    public int maxNumbBall = 59;
    private int maxBallsDrawn = 30;
    private List<int> allNumbers = new List<int>();
    public List<Sprite> spriteNumb = new List<Sprite>();
    public List<Sprite> emptyBall = new List<Sprite>();
    private bool canDraw = false;
    private float lastDraw;
    private int drawIndex = 0;
    public int credit = 100;
    private int creditBet = 0;
    private int bet = 0;
    private int limitBet = 0;
    private int round = 0;
    public Text roundNumber, creditBalance, creditAmount;
    public Button startButton, randCardButton, increaseButton, decreaseButton;

    // Start is called before the first frame update
    void Start()
    {
        creditBalance.GetComponent<Text>().text = "Credit Balance: " + credit;
        roundNumber.GetComponent<Text>().text = "Round Number: " + round;
    }

    // Update is called once per frame
    void Update()
    {
        if (canDraw == true)
        {
            if(Time.time - lastDraw > drawSpeed)
            {
                DrawBall();
                lastDraw = Time.time;
            }
        }
        
        if (drawIndex == maxBallsDrawn)
        {
            drawIndex = 0;
            limitBet = credit;

            startButton.interactable = true;
            randCardButton.interactable = true;
            increaseButton.interactable = true;
            decreaseButton.interactable = true;
        }
        
    }

    public void StartGame()
    {
        if (bet >= 1)
        {
            round++;
            credit--;

            creditBalance.GetComponent<Text>().text = "Credit Balance: " + credit;
            roundNumber.GetComponent<Text>().text = "Round Number: " + round;
            
            startButton.interactable = false;
            randCardButton.interactable = false;
            increaseButton.interactable = false;
            decreaseButton.interactable = false;

            PlayRound();
        }    
    }

    private void DrawBall()
    {
        if (drawIndex >= maxBallsDrawn) 
            return;
        draw.Invoke(allNumbers[drawIndex], drawIndex);
        drawIndex++;
    }

    void PlayRound()
    {
        canDraw = true;
        Sprite[] ballImages = Resources.LoadAll<Sprite>("balls");
        spriteNumb.AddRange(ballImages);

        var numbers = Enumerable.Range(0, maxNumbBall).ToList();
        allNumbers = numbers.OrderBy(i => Guid.NewGuid()).ToList().GetRange(0, maxBallsDrawn);
    }

    //Corrigir bug dos creditos
    public void IncreaseCredit()
    {
        creditBet = bet + 1;
        limitBet = credit - 1;
        credit = credit - 1;
        if (creditBet <= limitBet)
        {
            bet = creditBet;
        }
        
        creditAmount.GetComponent<Text>().text = "Inserting: " + creditBet + " Credits";  
    }

    public void DecreaseCredit()
    {
        credit = credit + 1;

        if (bet < 1)
            bet = 0;
        else
        {
            bet--;
        }

        creditAmount.GetComponent<Text>().text = "Inserting: " + bet + " Credits"; 
    }
}
