/****
 * Created by: Coleton Wheeler
 * Date Created: April 13, 2022
 * 
 * Last Edited by: Coleton Wheeler
 * Last Edited: April 18, 2022
 * 
 * Description: Basic game manager setup
 ****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region GameManager Singleton
    static private GameManager gm; //reference GameManager
    static public GameManager GM { get { return gm; } } //Public access read only gm

    void CheckGameManagerIsInScene()
    {
        if (gm == null)
        {
            gm = this;
            Debug.Log(gm);
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        Debug.Log(gm);
    }
    #endregion

    //Game Info Variables
    [Header("General Information")]
    public string gameTitle = "Oppozootion";
    public string gameCredits = "Created by ";
    public string copyrightDate = "2022";

    [Header("Game Settings")]
    public string tempHolder = "";

    //Gameplay Variables
    [HideInInspector]
    public static int pOneScore;
    public static int pTwoScore;
    public static int pThreeScore;
    public static int pFourScore;
    public static int[] playerScores =
    {
        pOneScore, pTwoScore, pThreeScore, pFourScore
    };
    public GameObject player;
    public GameObject[] AI = new GameObject[3];

    private Dictionary<string, System.Action> startMethods = new Dictionary<string, System.Action>();

    [Header("Set in Inspector")]
    public static int pointsToWinGame = 20;
    public static float turnTimer = 30f;
    private static float timeSpent;

    //Variable to store game state
    private string gameState;
    private int playerTurn;

    private void Start()
    {
        //Instantiate
        //Populate dictionary for start method calling
        startMethods.Add("menu", EnterMenuState);
        startMethods.Add("gameplay", EnterGameplayState);
        startMethods.Add("results", EnterResultsState);

        gameState = "menu";
        EnterMenuState();
        ChangeGameState("gameplay");
    }

    private void Update()
    {
        switch (gameState)
        {
            case "menu":
                Debug.Log("Running Menu State");

                //Run Menu State Logic



                //End Menu State Logic

                break;

            case "gameplay":
                Debug.Log("Running Gameplay State");

                //Run Gameplay State Logic

                timeSpent += Time.deltaTime;
                Debug.Log("Player " + playerTurn + "'s turn");

                //Logic for turn timer
                if (timeSpent > turnTimer)
                {
                    if (playerTurn >= 4)
                    {
                        playerTurn = 1;
                    } else
                    {
                        playerTurn = playerTurn + 1;
                    }
                    timeSpent = 0;
                    return;
                }

                //Logic for disabling/enabling player action and AI scripts
                /**NOT IMPLEMENTED YET
                 * 
                 * 
                 * 
                 */

                //End Gameplay State Logic

                break;

            case "results":
                Debug.Log("Running Results State");

                //Run Results State Logic



                //End Results State Logic

                break;

            default:
                Debug.LogWarning("Invalid Game State Selected");
                break;

        }
    }

    //Will reset all of the game variables
    public void ResetGameStats()
    {
        pOneScore = 0;
        pTwoScore = 0;
        pThreeScore = 0;
        pFourScore = 0;
    }

    public void UpdatePlayerScore(int playerNumber, int addToScore)
    {
        //If invalid player index passed through method, return and spit out a warning.
        if (playerNumber > 4 || playerNumber < 1)
        {
            Debug.LogWarning("Passing an invalid player number through function. Keep value between 1 and 4");
            return;
        }

        playerScores[playerNumber - 1] += addToScore;

        if (playerScores[playerNumber - 1] > pointsToWinGame)
        {
            Debug.Log("Player " + playerNumber + " wins the game!");
            ChangeGameState("results");
        }
    }

    public void PlayerActionCompleted()
    {
        playerTurn = playerTurn + 1;
        timeSpent = 0;
    }

    public void StartGame()
    {
        gameState = "gameplay";
    }

    public void ExitGame()
    {

    }

    #region Scene Management

    //Call to change the scene using the scene name
    public void ChangeScene(string sceneName)
    {
        Debug.Log("Attempting to enter scene " + sceneName + "...");
        if (EditorSceneManager.GetSceneByName(sceneName) != null)
        {
            if (EditorSceneManager.GetActiveScene().name != sceneName)
            {
                Debug.Log("Loading scene " + sceneName);
                EditorSceneManager.LoadScene(sceneName);
            } 
            else
            {
                Debug.Log("Scene " + sceneName + " is alraedy loaded");
            }
            return;
        }
        Debug.LogWarning("Scene name does not exist!");
    }

    //Call to change the scene using build number
    public void ChangeScene(int sceneIndex)
    {
        Debug.Log("Attempting to enter scene at index " + sceneIndex + "...");
        if (EditorSceneManager.GetSceneByBuildIndex(sceneIndex) != null)
        {
            if (EditorSceneManager.GetActiveScene().buildIndex != sceneIndex)
            {
                Debug.Log("Loading scene index " + sceneIndex);
                EditorSceneManager.LoadScene(sceneIndex);
            }
            else
            {
                Debug.Log("Scene at index " + sceneIndex + " is alraedy loaded");
            }
            return;
        }
        Debug.LogWarning("Scene index does not exist!");
    }

    #endregion


    #region Game State Handling
    //Method to call to change the game state from other classes
    public void ChangeGameState(string newState)
    {   
        gameState = newState.ToLower();
        startMethods[gameState]();
    }


    /*** ENTERING STATE METHODS ***/
    private void EnterMenuState()
    {
        Debug.Log("Entering Menu State");
    }
    private void EnterGameplayState()
    {
        Debug.Log("Entering Gameplay State");
        playerTurn = 1;
    }
    private void EnterResultsState()
    {
        Debug.Log("Entering Results State");
    }
    #endregion
}
