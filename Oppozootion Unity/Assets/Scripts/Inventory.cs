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
    public int maxCardNum = 7;
    public int maxBundleNum = 3;

    public Cards[] cardInventory;
    public GameObject[] bundleInvtory;


    // Start is called before the first frame update
    void Start()
    {
        cardInventory = new Cards[maxCardNum];
        bundleInvtory = new GameObject[maxBundleNum];
    }
    

    public void AddCard(Cards card) //add a card to array
    {
        if (cardInventory.Length > 7)
        {
            Debug.Log("You already have the maximum number of animal cards!");
            return;
        }
        else
        {
            for (int i = 0; i < maxCardNum; i++)
            {
                if (cardInventory[i] != null)
                    continue;
                else
                {
                    cardInventory[i] = card;
                    return;
                }
            }
        }
    }

    void AddBundle(GameObject obj) //add a bundle to array
    {
        if (bundleInvtory.Length > 3)
        {
            Debug.Log("You already have the maximum number bundle cards!");
            return;
        }
        else
        {
            for (int i = 0; i < maxBundleNum; i++)
            {
                if (bundleInvtory[i] != null)
                    continue;
                else
                {
                    bundleInvtory[i] = obj;
                    return;
                }
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
        for (int i = 0; i < maxCardNum; i++)
        {
            if (cardInventory[i].animalName != str)
                continue;
            else
            {
                cardInventory[i] = null;
                return;
            }
        }
    }

}