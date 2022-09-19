using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGame : MonoBehaviour
{
    //When pressed the 'CLOSE BUTTON' the game window will close
    public void OnMouseButton()
    {
        Application.Quit();
    }
}
