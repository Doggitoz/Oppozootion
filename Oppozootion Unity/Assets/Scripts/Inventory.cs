/**** 
 * Created by: Bobby Ouyang
 * Date Created: April 20, 2022
 * 
 * Last Edited by: Bobby Ouyang 
 * Last Edited: April 20, 2022
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

    public Cards[] cardInventory;
    public GameObject[] bundleInvtory;


    // Start is called before the first frame update
    void Start()
    {
        cardInventory = new Cards[maxCardNumber];
        bundleInvtory = new GameObject[maxBundleNumber];

    }


    void AddCard(Cards card) //add a card to array
    {
        for (int i = 0; i < maxCardNumber; i++)
        {
            if (cardInventory[i] != null) //if there's a card in this index, continue the iteration
                continue;
            else
            {
                cardInventory[i] = card; //add the card into this index of the array
                return;
            }

        }

    }

    void AddBundle(GameObject obj) //add a bundle card to array
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

    void RemoveCard(int pos) //remove one card based on the index of the card in the array
    {
        cardInventory[pos] = null;
    }
    void RemoveCard(int pos1, int pos2) //remove two cards based on the index of the card in the array
    {
        cardInventory[pos1] = null;
        cardInventory[pos2] = null;
    }
    void RemoveCard(int pos1, int pos2, int pos3) //remove three cards based on the index of the card in the array
    {
        cardInventory[pos1] = null;
        cardInventory[pos2] = null;
        cardInventory[pos3] = null;
    }

    void RemoveCard(string str) // remove card by interating through the array and find the card with the corresponding name
                                // [first letter of the animal name MUST be CAPITALIZED]
    {
        for (int i = 0; i < maxCardNumber; i++)
        {
            if (cardInventory[i].animalName != str) //if the current index's animal's name doesn't equal to the name of the card
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