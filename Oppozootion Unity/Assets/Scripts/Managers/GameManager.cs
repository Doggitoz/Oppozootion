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
    public int pOneScore;
    public int pTwoScore;
    public int pThreeScore;
    public int pFourScore;
    public string finalMessage;

    public GameObject player;
    public GameObject[] AI = new GameObject[3];
    public GameObject board;

    private Dictionary<string, System.Action> startMethods = new Dictionary<string, System.Action>();

    [Header("Set in Inspector")]
    public int pointsToWinGame = 20;
    public float turnTimer = 30f;
    public float timeSpent = 0f;

    [Header("Prefabs")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject AIPrefab;
    [SerializeField] private GameObject boardPrefab;

    //Variable to store game state
    private string gameState;
    public int playerTurn;

  

    private void Start()
    {
        gameState = "menu";
        EnterMenuState();

    }

    private void Awake()
    {
        CheckGameManagerIsInScene();

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

                //If it didn't instantiate for some reason
                if (board == null)
                {
                    board = Instantiate(boardPrefab);
                    player = Instantiate(playerPrefab);
                    player.name = "Player 1";
                    for (int i = 0; i < 3; i++)
                    {
                        AI[i] = Instantiate(AIPrefab);
                        AI[i].name = "AI " + (i + 1) + " / Player " + (i + 2);
                        AI[i].GetComponent<AIScript>().playerNumber = i + 2;
                        //IMPLEMENT LOGIC FOR DIFFERENT POSITIONS PER AI (different sides of the board)
                    }
                }


                Debug.Log("Player " + playerTurn + "'s turn");

                if (playerTurn == 1)
                {
                    timeSpent += Time.deltaTime;

                    if (timeSpent > turnTimer)
                    {
                        PlayerActionCompleted();
                    }
                }
                else
                {
                    timeSpent = 0;
                }

                if (pOneScore >= pointsToWinGame)
                {
                    finalMessage = "Player " + 1 + " wins the game!";
                    gameState = "results";
                    EnterResultsState();
                }

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
        switch (playerNumber) 
        {

            case 1:
                pOneScore += addToScore;
                CheckIfWin(pOneScore, 1);
                break;
            case 2:
                pTwoScore += addToScore;
                CheckIfWin(pTwoScore, 2);
                break;
            case 3:
                pThreeScore += addToScore;
                CheckIfWin(pThreeScore, 3);
                break;
            case 4:
                pFourScore += addToScore;
                CheckIfWin(pFourScore, 4);
                break;

        }
    }

    private void CheckIfWin(int playerScore, int playerNum)
    {
        if (playerScore > pointsToWinGame)
        {
            finalMessage = "Player " + playerNum + " wins the game!";
            gameState = "results";
            EnterResultsState();
        }
    }

    public void PlayerActionCompleted()
    {
        playerTurn = playerTurn + 1;
        timeSpent = 0;
        board.GetComponent<DrawArea>().fillCards();
    }

    public void StartGame()
    {
        ChangeScene("BoardScene");
        gameState = "gameplay";
        EnterGameplayState();
    }

    public void ExitGame()
    {

    }

    public void NextTurn()
    {
        playerTurn++;
        board.GetComponent<DrawArea>().GenerateAnimalCardSlots();
        if (playerTurn > 4)
        {
            playerTurn = 1;

        }
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
    }


    /*** ENTERING STATE METHODS ***/
    public void EnterMenuState()
    {
        Debug.Log("Entering Menu State");
        ChangeScene("start_scene");
    }
    private void EnterGameplayState()
    {
        Debug.Log("Entering Gameplay State");
        playerTurn = 1;
    }
    private void EnterResultsState()
    {
        Debug.Log("Entering Results State");
        ChangeGameState("menu");
        ChangeScene("end_scene");
        pOneScore = 0;
        pTwoScore = 0;
        pThreeScore = 0;
        pFourScore = 0;
    }
    #endregion
}
