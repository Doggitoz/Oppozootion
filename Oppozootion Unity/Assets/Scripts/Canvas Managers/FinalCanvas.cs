using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalCanvas : MonoBehaviour
{
    GameManager gm = GameManager.GM;
    public Text finalResults;

    public void Awake()
    {
        gm = GameManager.GM;
    }

    public void ReturnToMenu()
    {
        gm.ChangeGameState("menu");
        gm.EnterMenuState();
    }

    public void Start()
    {
        finalResults.text = gm.finalMessage;
    }

    public void GameExit()
    {
        gm.ExitGame(); //refenece the ExitGame method on the game manager

    }
}
