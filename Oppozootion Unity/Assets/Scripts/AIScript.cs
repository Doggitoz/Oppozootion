
/****
 * Created by: Coleton Wheeler
 * Date Created: April 24, 2022
 * 
 * Last Edited by:
 * Last Edited:
 * 
 * Description: AI Logic Handling
 ****/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    public float delayBeforePlay = 2f;
    public int playerNumber;

    private float timeSinceTurn = 0f;
    private GameManager gm;

    private int numBundleCards = 0;
    private int numAnimalCards = 0;

    void Awake()
    {
        gm = GameManager.GM;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gm.playerTurn == playerNumber)
        {
            timeSinceTurn += Time.deltaTime;
            if (timeSinceTurn > delayBeforePlay)
            {
                int random = Random.Range(0, 100);
                UpdateInventoryStats();
                if (random > 80 && numBundleCards < 3)
                {
                    Debug.Log("Player " + playerNumber + " took a bundle card " + Time.time);
                    numBundleCards++;
                    //SCRIPT TO TAKE A BUNDLE CARD ONCE IMPLEMENTED
                }
                else if (numAnimalCards < 7)
                {
                    Debug.Log("Player " + playerNumber + " took an animal card " + Time.time);

                    int randomIndex = Random.Range(0, gm.board.GetComponent<DrawArea>().CurrentBoardCards.Length);
                    GameObject currentCard = gm.board.GetComponent<DrawArea>().CurrentBoardCards[randomIndex];
                    currentCard.GetComponent<HoverScript>().TakeCard(this.gameObject);
                    numAnimalCards++;

                    if (numAnimalCards == 7)
                    {
                        gm.NextTurn();
                        return;
                    }

                    int randomIndex2 = randomIndex;

                    while ((randomIndex2 = Random.Range(0, 8)) == randomIndex) {
                        
                    }

                    currentCard = gm.board.GetComponent<DrawArea>().CurrentBoardCards[randomIndex2];
                    currentCard.GetComponent<HoverScript>().TakeCard(this.gameObject);


                }
                else
                {
                    Debug.Log("Player " + playerNumber + " discarded an animal card " + Time.time);
                    transform.GetComponent<Inventory>().RemoveCard(Random.Range(0, 7));
                }

                gm.NextTurn();
            }

        }
        else
        {
            timeSinceTurn = 0f;
        }
    }

    void UpdateInventoryStats()
    {
        numAnimalCards = 0;
        numBundleCards = 0;
        foreach (GameObject bundle in transform.GetComponent<Inventory>().bundleInvtory)
        {
            if (bundle != null)
            {
                numBundleCards++;
            }
        }
        foreach (GameObject card in transform.GetComponent<Inventory>().cardInventory)
        {
            if (card != null)
            {
                numAnimalCards++;
            }
        }
    }
}
