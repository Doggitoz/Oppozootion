/**** 
 * Created by: Bobby Ouyang
 * Date Created: April 20, 2022
 * 
 * Last Edited by: Coleton Wheeler
 * Last Edited: April 24, 2022
 * 
 * Description: Inventory system
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{


    [SerializeField]
    public int maxCardNumber = 7;
    public int maxBundleNumber = 3;

    public GameObject[] cardInventory;
    public GameObject[] bundleInvtory;

    private GameManager gm;

    private void Awake()
    {
        gm = GameManager.GM;
    }

    // Start is called before the first frame update
    void Start()
    {
        cardInventory = new GameObject[maxCardNumber];
        bundleInvtory = new GameObject[maxBundleNumber];

    }


    public void AddCard(GameObject obj) //add a card to array
    {
        
        
        for (int i = 0; i < maxCardNumber; i++)
        {
            if (cardInventory[i] != null) //if there's a card in this index, continue the iteration
                continue;
            else
            {
                cardInventory[i] = obj; //add the card into this index of the array
                Debug.Log(obj.transform.parent);
                GameObject.Find("Player1").GetComponent<PlayerScript>().addAnimalCard(obj);
                return;
            }

        }

    }

    public void AddBundle(GameObject obj) //add a bundle card to array
    {

        for (int i = 0; i < maxBundleNumber; i++)
        {
            if (bundleInvtory[i] != null) //if there's a bundle card already in this index, continue the iteration
                continue;
            else
            {
                bundleInvtory[i] = obj; //add the bundle card into this index of the array
                return;
            }
        }

    }

    public void RemoveCard(int pos) //remove one card based on the index of the card in the array
    {
        cardInventory[pos] = null;
    }

    public void RemoveCard(string str) // remove card by interating through the array and find the card with the corresponding name
                                       // [first letter of the animal name MUST be CAPITALIZED]
    {
        for (int i = 0; i < maxCardNumber; i++)
        {
            if (cardInventory[i].GetComponent<CardData>().animalName != str) //if the current index's animal's name doesn't equal to the name of the card
                                                    //we want to remove, continue the iteration
                continue;
            else
            {
                cardInventory[i] = null;            //remove the card
                return;
            }
        }
    }

}