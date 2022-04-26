/**** 
 * Created by: Akram Taghavi-Burrs
 * Date Created: Feb 23, 2022
 * 
 * Last Edited by: Anupam Terkonda
 * Last Edited: April 24, 2022
 * 
 * Description: Updates start canvas referecing game manager
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //libraries for UI components

public class StartCanvas : MonoBehaviour
{
    /*** VARIABLES ***/
   
    GameManager gm; //reference to game manager

    public GameObject canvas;

    [Header("Canvas SETTINGS")]
    public Text titleTextbox; //textbox for the title
    public Text creditsTextbox; //textbox for the credits
    public Text copyrightTextbox; //textbox for the copyright

    private void Start()
    {


        //Set the Canvas text from GM reference
        //Debug.Log(titleTextbox.text);
        if (gm != null)
        {
            titleTextbox.text = gm.gameTitle;
    
            creditsTextbox.text = gm.gameCredits;

            copyrightTextbox.text = gm.copyrightDate;
        }

        canvas = GameObject.Find("Canvas");
    }
    public void Awake()
    {
        gm = GameManager.GM;
    }



    public void GameStart()
    {
        gm.StartGame(); //refenece the StartGame method on the game manager

    }

   public void GameExit()
    {
        gm.ExitGame(); //refenece the ExitGame method on the game manager

    }

    public void ShowRules()
    {
        canvas.SetActive(false); //hide main menu to show rules
    }
    public void HideRules()
    {
        canvas.SetActive(true); //unhide main menu
    }
}
