/**** 
 * Created by: Akram Taghavi-Burrs
 * Date Created: Feb 23, 2022
 * 
 * Last Edited by: NA
 * Last Edited: March 31, 2022
 * 
 * Description: Basic GameManager Template
****/

/*** Using Namespaces ***/
using System; //C# library for system properties
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //libraries for accessing scenes

//Setting the enum outside the class allows for direct access by the enum (classes) name directly in other classes.
public enum GameState { Title, Playing, BeatLevel, LostLevel, GameOver, Idle, Testing };
//enum of game states (work like it's own class)

public class GameManager : MonoBehaviour
{
    /*** VARIABLES ***/


    public int P1Score = 0;
    public int P2Score = 0;
    public int P3Score = 0;
    public int P4Score = 0;

    public int gameMaxScore = 1000;

    #region GameManager Singleton
    static private GameManager gm; //refence GameManager
    static public GameManager GM { get { return gm; } } //public access to read only gm 

    //Check to make sure only one gm of the GameManager is in the scene
    void CheckGameManagerIsInScene()
    {

        //Check if instnace is null
        if (gm == null)
        {
            gm = this; //set gm to this gm of the game object
            Debug.Log(gm);
        }
        else //else if gm is not null a Game Manager must already exsist
        {
            Destroy(this.gameObject); //In this case you need to delete this gm
        }
        DontDestroyOnLoad(this); //Do not delete the GameManager when scenes load
        Debug.Log(gm);
    }//end CheckGameManagerIsInScene()
    #endregion

    //Game State Varaiables
    [HideInInspector] public GameState gameState = GameState.Title; //first game state

    [Header("GENERAL SETTINGS")]
    public string gameTitle = "Untitled Game";  //name of the game
    public string gameCredits = "Made by Me"; //game creator(s)
    public string copyrightDate = "Copyright " + thisDay; //date cretaed

    [Header("GAME SETTINGS")]

    [Tooltip("Will the high score be recoreded")]
    public bool recordHighScore = false; //is the High Score recorded

    [SerializeField] //Access to private variables in editor
    private int defaultHighScore = 1000;
    static public int highScore = 1000; // the default High Score
    public int HighScore { get { return highScore; } set { highScore = value; } }//access to private variable highScore [get/set methods]

    [Space(10)]

    public int defaultsLives; //set number of lives in the inspector
    private int currentLives; //number of lives remaining in level
    [Tooltip("Does the level get reset when a life is lost")]
    public bool resetLostLevel; //reset the lost level
    public float gameRestartDelay = 2f; //the amount of delay before restart
    static public int lives; // number of lives for player 
    public int Lives { get { return lives; } set { lives = value; } }//access to static variable lives [get/set methods]

    static public int score;  //score value
    public int Score { get { return score; } set { score = value; } }//access to static variable score [get/set methods]

    [Space(10)]
    public string defaultEndMessage = "Game Over";//the end screen message, depends on winning outcome
    public string looseMessage = "You Loose"; //Message if player looses
    public string winMessage = "You Win"; //Message if player wins
    [HideInInspector] public string endMsg;//the end screen message, depends on winning outcome

    [Header("SCENE SETTINGS")]
    [Tooltip("Name of the start scene")]
    public string startScene;

    [Tooltip("Name of the game over scene")]
    public string gameOverScene;

    [Tooltip("Count and name of each Game Level (scene)")]
    public string[] gameLevels; //names of levels
    [HideInInspector]
    public int gameLevelsCount; //what level we are on
    private int loadLevel; //what level from the array to load

    public static string currentSceneName; //the current scene name;

    [Header("FOR TESTING")]
    public bool TestGameManager = false; // test game manager functionality

    [SerializeField] //Access to private variables in editor
    [Tooltip("Check to test player lost the level")]
    private bool levelLost = false;//we have lost the level (ie. player died)

    //test next level
    [SerializeField] //Access to private variables in editor
    public bool nextLevel = false; //test for next level

    //Win/Loose conditon
    [SerializeField] //Access to private variables in editor
    private bool playerWon = false;


    //reference to system time
    private static string thisDay = System.DateTime.Now.ToString("yyyy"); //today's date as string


    /*** MEHTODS ***/

    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        //runs the method to check for the GameManager
        CheckGameManagerIsInScene();

        //store the current scene
        currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        //Get the saved high score
        GetHighScore();

    }//end Awake()

    //Start is called once before the update
    void Start()
    {
        //if we run play the game from the level instead of start scene (PLAYTESTING ONLY)
        if (currentSceneName != startScene) { SetGameState(GameState.Testing); }//set the game state for testing }

    }//end Start()


    // Update is called once per frame
    private void Update()
    {
        //if ESC is pressed , exit game
        if (Input.GetKey("escape")) { ExitGame(); }

        //check for game state changes
        CheckGameState();

        //Outpot game state
        Debug.Log("Game State " + gameState);

    }//end Update


    //SET GAME STATES
    public void SetGameState(GameState state)
    {
        this.gameState = state;
    }//end SetGameState()


    //CHECK FOR GAME STATE CHANGES
    private void CheckGameState()
    {
        switch (gameState)
        {
            case GameState.Title:
                currentLives = defaultsLives; //set current lives to default (inital) value
                break;

            case GameState.Playing:
                //if testing
                if (TestGameManager) { RunTests(); }
                break;

            case GameState.BeatLevel:
                endMsg = winMessage; //set win message
                Debug.Log("beat level");
                NextLevel(); //check for the next level
                break;

            case GameState.LostLevel:
                currentLives = defaultsLives; //reset current lives to default (inital) value

                endMsg = looseMessage; //set loose message
                GameOver(); //move to game over
                break;

            case GameState.GameOver:
                //do nothing
                currentLives = defaultsLives; //set current lives to default (inital) value
                break;

            case GameState.Idle:
                //do nothing
                break;

            case GameState Testing:
                currentLives = defaultsLives; //set current lives to default (inital) value
                SetDefaultGameStats(); //Run the default game stats to playtest
                break;

        }//end switch(gameStates)
    }//end CheckGameState()


    //LOAD THE GAME FOR THE FIRST TIME OR RESTART
    public void StartGame()
    {
        //get first game level
        gameLevelsCount = 1; //set the count for the game levels
        loadLevel = gameLevelsCount - 1; //the level from the array

        //load first game level
        SceneManager.LoadScene(gameLevels[loadLevel]);

        SetDefaultGameStats(); // the game stats defaults 

    }//end StartGame()


    public void SetDefaultGameStats()
    {
        //store the current scene
        currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;


        //SET ALL GAME LEVEL VARIABLES FOR START OF GAME
        lives = currentLives; //set the number of lives
        score = 0; //set starting score

        //set High Score
        if (recordHighScore) //if we are recording highscore
        {
            //if the high score, is less than the default high score
            if (highScore <= defaultHighScore)
            {
                highScore = defaultHighScore; //set the high score to defulat
                PlayerPrefs.SetInt("HighScore", highScore); //update high score PlayerPref
            }//end if (highScore <= defaultHighScore)
        }//end  if (recordHighScore) 

        endMsg = defaultEndMessage; //set the end message default

        playerWon = false; //set player winning condition to false

        SetGameState(GameState.Playing);//set the game state to playing

    }//end SetDefaultGameStats()


    //EXIT THE GAME
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exited Game");
    }//end ExitGame()


    //GO TO THE GAME OVER SCENE
    public void GameOver()
    {
        SetGameState(GameState.GameOver);//set the game state to Game Over

        SceneManager.LoadScene(gameOverScene); //load the game over scene

    }//end GameOver()


    //GO TO THE NEXT LEVEL
    void NextLevel()
    {

        //as long as our level count is not more than the amount of levels
        if (gameLevelsCount < gameLevels.Length)
        {
            gameLevelsCount++; //add to level count for next level
            loadLevel = gameLevelsCount - 1; //find the next level in the array
            SceneManager.LoadScene(gameLevels[loadLevel]); //load next level

            SetGameState(GameState.Playing);//set the game state to playing

        }
        else
        { //if we have run out of levels go to game over
            GameOver();
        } //end if (gameLevelsCount <=  gameLevels.Length)

    }//end NextLevel()


    //PLAYER LOST A LIFE
    public void LostLife()
    {
        if (lives == 1) //if there is one life left and it is lost
        {
            SetGameState(GameState.LostLevel); //set the state to Lost Level

        }
        else
        {
            lives--; //subtract from lives reset level lost 

            //if this level resets when life is lost
            if (resetLostLevel)
            {
                currentLives = lives; //set current lives to remaining for level reload
                Invoke("StartGame", gameRestartDelay); //restart the level
            }//end if (resetLostLevel)

        } // end elseif
    }//end LostLife()


    //CHECK SCORE UPDATES
    public void UpdateScore(int point = 0)
    { //This method manages the score on update. 

        score += point;

        //if the score is more than the high score
        if (score > highScore)
        {
            highScore = score; //set the high score to the current score
            PlayerPrefs.SetInt("HighScore", highScore); //set the playerPref for the high score
        }//end if(score > highScore)

    }//end CheckScore()

    void GetHighScore()
    {//Get the saved highscore

        //if the PlayerPref alredy exists for the high score
        if (PlayerPrefs.HasKey("HighScore"))
        {
            Debug.Log("Has Key");
            highScore = PlayerPrefs.GetInt("HighScore"); //set the high score to the saved high score
        }//end if (PlayerPrefs.HasKey("HighScore"))

        PlayerPrefs.SetInt("HighScore", highScore); //set the playerPref for the high score
    }//end GetHighScore()


    private void RunTests()
    {
        //test to move to next level
        if (nextLevel) { nextLevel = false; NextLevel(); }

        //test for lossing level
        if (levelLost) { levelLost = false; LostLife(); }

        //test if player won
        if (playerWon) { playerWon = false; SetGameState(GameState.BeatLevel); }

    }//end RunTest()

}
