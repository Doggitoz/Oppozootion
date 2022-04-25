/**** 
 * Created by: Ben Jenkins
 * Date Created: April 18,2022
 * 
 * Last Edited by: Coleton Wheeler
 * Last Edited: April 24, 2022
 * 
 * Description: Handles generating new cards to the draw area
****/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawArea : MonoBehaviour
{
    //Variables
    public int numberOfAnimalCards=0;
    public GameObject AnimalcardPrefab;
    public List<Cards> AnimalCards;
    public GameObject BundleCardPrefab;


    [HideInInspector] public GameObject[] AnimalcardSlots;
     public GameObject[] CurrentBoardCards;
    [HideInInspector] public GameObject[] BoardSlots = new GameObject[8];
    
    
    //Draw area exists between the points (-5,-5) and (5,5) with (0,0) being the center

    // Start is called before the first frame update
    void Start()
    {
        
        AnimalcardSlots = new GameObject[numberOfAnimalCards];
        CurrentBoardCards = new GameObject[numberOfAnimalCards];
        GenerateAnimalCardSlots();
        GenerateBundleStack();
    }

    public void GenerateAnimalCardSlots()
    {
        float cardSpacing = 10f / ((numberOfAnimalCards / 2) + 1);
        if (numberOfAnimalCards % 2 == 1)
        {
            cardSpacing = 10f / (((numberOfAnimalCards + 1) / 2) + 1);
        }
        else
        {
            cardSpacing = 10f / ((numberOfAnimalCards / 2) + 1);
        }


        float currentspacing = 0f;


        for (int i = 0; i < numberOfAnimalCards; i++)
        {
            if (AnimalcardSlots[i] == null)
            {
                GameObject newSlot = new GameObject();
                if (i % 2 == 0)
                {
                    currentspacing += cardSpacing;
                }
                newSlot.transform.position = new Vector3((-5f) + currentspacing, 0, ((i % 2) * 3));
                AnimalcardSlots[i] = newSlot;
            }
        }
        Invoke("fillCards", 1f);

        /*** 
         * WILL NEED SCRIPTS TO INSTANTIATE BUNDLE CARDS
         * 
         * 
         */
    }

    public void GenerateBundleStack()
    {
        GameObject BundleStack = Instantiate(BundleCardPrefab);
        BundleStack.transform.position = new Vector3(0, 0, -3f);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveCardFromBoard(GameObject card)
    {
        for (int i = 0; i < numberOfAnimalCards; i++)
        {
            if (CurrentBoardCards[i] == card)
            {
                CurrentBoardCards[i] = null;
            }
        }
    }

    public void fillCards()
    {
        for(int i = 0; i < numberOfAnimalCards; i++)
        {
            if(CurrentBoardCards[i]==null)
            {
                CurrentBoardCards[i] = DrawCard(AnimalcardSlots[i].transform.position);
            }
        }
    }

    private GameObject DrawCard(Vector3 pos)
    {
        GameObject Animalcard = Instantiate(AnimalcardPrefab);

        //Load Random Animal Card
        Cards newCard = AnimalCards[Random.Range(0, AnimalCards.Count)];
        CardData currentCardData = Animalcard.GetComponent<CardData>();
        currentCardData.cardData = newCard;
        currentCardData.Init();

        //Set World Data
        Animalcard.name = newCard.name + " card";
        Animalcard.transform.parent = transform;
        Animalcard.transform.position = pos;

        return Animalcard;
    }
}
