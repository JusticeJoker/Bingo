using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable] //Stacks the events 
public class Draw : UnityEvent<int, int> //Ball number and index
{

}
public class DrawEvent : MonoBehaviour
{
    public Draw draw;
    public Card card;
    public float drawSpeed = 0.1f;
    public int maxNumbBall = 60;
    private int maxBallsDrawn = 30;
    private List<int> allNumbers = new List<int>();
    public List<Sprite> spriteNumb = new List<Sprite>();
    [SerializeField] List<GameObject> ballsOBJ;
    private bool canDraw = false;
    private float lastDraw;
    private int drawIndex = 0;
    public int credit = 100;
    private int creditBet = 0;
    private int bet = 0;
    private int limitBet = 0;
    private int round = 0;
    public GameObject bingoText;
    public Text roundNumber, creditBalance, creditAmount;
    public Button startButton, randCardButton, increaseButton, decreaseButton;

    void Awake()
    {
        //Get all the ball sprites
        Sprite[] ballImages = Resources.LoadAll<Sprite>("balls");
        spriteNumb.AddRange(ballImages);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Show on the UI the Round Number and Credit Balance that is set by default in the variable
        creditBalance.GetComponent<Text>().text = "Credit Balance: " + credit;
        roundNumber.GetComponent<Text>().text = "Round Number: " + round;
    }

    // Update is called once per frame
    void Update()
    {
        if (canDraw == true)
        {
            //Draw each ball over time with a delay
            if(Time.time - lastDraw > drawSpeed)
            {
                DrawBall();
                lastDraw = Time.time;
            }
        }
        
        //After all the thirty balls are drawn out
        if (drawIndex == maxBallsDrawn)
        {
            canDraw = false;

            //Enables back the buttons to play again
            startButton.interactable = true;
            randCardButton.interactable = true;
            increaseButton.interactable = true;
            decreaseButton.interactable = true;

            //Check if there's a Bingo
            if (card.Bingo())
            {
                //Disables all teh buttons and only leaves the Close Game button enabled
                startButton.interactable = false;
                randCardButton.interactable = false;
                increaseButton.interactable = false;
                decreaseButton.interactable = false;

                //Show Bingo text on the UI
                bingoText.SetActive(true);
                
                //Rewards the player by updating the credits and updates it on the UI 
                credit += (bet * 2);
                creditBalance.GetComponent<Text>().text = "Credit Balance: " + credit;
            }

            //Reset the values of Index for the balls and bet incase the player wants to play again
            drawIndex = 0;
            bet = 0;

            //updates the bet value back to 0 for a new round
            creditAmount.GetComponent<Text>().text = "Inserting: " + bet + " Credits"; 

            //updates the new bet limit for the new round
            limitBet = credit;            
        }
        
    }

    //Function to start a round
    public void StartGame()
    {
        //Keeps the bingo text disabled
        bingoText.SetActive(false);

        if (bet >= 1)
        {   
            //Clean the ball table
            ClearBalls();

            //Increment a round bumber and reduce one credit for each round played
            round++;
            credit--;

            //Update the credit and round value
            creditBalance.GetComponent<Text>().text = "Credit Balance: " + credit;
            roundNumber.GetComponent<Text>().text = "Round Number: " + round;
            
            //Disable all buttons while the round is in play
            startButton.interactable = false;
            randCardButton.interactable = false;
            increaseButton.interactable = false;
            decreaseButton.interactable = false;

            //Call function to play the round
            PlayRound();
        }    
    }

    //Function to get the ball number
    private void DrawBall()
    {
        if (drawIndex >= maxBallsDrawn) 
            return;
        draw.Invoke(allNumbers[drawIndex], drawIndex);
        drawIndex++;
        //Debug.Log(drawIndex);
    }

    //Method to clear the balls from the screen when a new round starts
    void ClearBalls()
    {
        for (int i = 0; i < ballsOBJ.Count; i++)
        {
            //ballsOBJ[i].GetComponent<SpriteRenderer>().color = new Color = 0;

            //Clears all the balls sprite from the list by changing the alpha of the sprite to 0 or transparent
            Color tempColor = ballsOBJ[i].GetComponent<SpriteRenderer>().color;
            tempColor.a = 0;
            ballsOBJ[i].GetComponent<SpriteRenderer>().color = tempColor;
        }
    }

    //Play the round by getting the balls and order them
    void PlayRound()
    {
        canDraw = true;

        var numbers = Enumerable.Range(0, maxNumbBall).ToList();
        allNumbers = numbers.OrderBy(i => Guid.NewGuid()).ToList().GetRange(0, maxBallsDrawn);
    }

    //Function to increase the amount of credits the player wants to bet
    public void IncreaseCredit()
    {
        //Updates the maximum bet the player can do, the credit amount and the limit of credits the player has
        creditBet = bet + 1;
        limitBet = credit - 1;
        credit = credit - 1;

        //Check if the maximum bet is valid accordint to the player balance
        if (creditBet <= limitBet)
            bet = creditBet;
        
        //Update on the UI the bet amount
        creditAmount.GetComponent<Text>().text = "Inserting: " + creditBet + " Credits";  
    }

    //Function to decrease the amount of credits the player wants to bet
    public void DecreaseCredit()
    {
        //Incase player tries to click on the decrease button and doesn't allow a negative value
        if (bet < 1)
            bet = 0;
        else
        {
            bet -= 1;
            credit += 1;
        }
        
        //Update on the UI the bet amount
        creditAmount.GetComponent<Text>().text = "Inserting: " + bet + " Credits"; 
    }
}
