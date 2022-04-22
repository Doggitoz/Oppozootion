/**** 
 * Created by: Ben Jenkins
 * Date Created: April 18,2022
 * 
 * Last Edited by: Coleton Wheeler
 * Last Edited: April 22, 2022
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
    [HideInInspector]public GameObject[] AnimalcardSlots;
    
    
    //Draw area exists between the points (-5,-5) and (5,5) with (0,0) being the center

    // Start is called before the first frame update
    void Start()
    {
        
        AnimalcardSlots = new GameObject[numberOfAnimalCards];
        GenerateAnimalCardSlots();
    }

    private void GenerateAnimalCardSlots()
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
            GameObject newSlot = new GameObject();
            if (i % 2 == 0)
            {
                currentspacing += cardSpacing;
            }
            newSlot.transform.position = new Vector3((-5f) + currentspacing, 0, ((i % 2) * 3) );
            AnimalcardSlots[i] = newSlot;
        }
        Invoke("fillCards", 1f);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void fillCards()
    {
        for(int i = 0; i < numberOfAnimalCards; i++)
        {
            if(/*determine how cards are taken from a slot*/true)
            {
                DrawCard(AnimalcardSlots[i].transform.position);
            }
        }
    }

    private void DrawCard(Vector3 pos)
    {
        GameObject Animalcard = Instantiate(AnimalcardPrefab);
        Animalcard.name = "Animal Card";
        Animalcard.transform.parent = transform;
        Animalcard.transform.position = pos;
    }
}
