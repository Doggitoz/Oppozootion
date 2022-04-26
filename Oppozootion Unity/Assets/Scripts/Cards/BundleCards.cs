/**** 
 * Created by: Coleton Wheeler
 * Date Created: April 20, 2022
 * 
 * Last Edited by:
 * Last Edited:
 * 
 * Description: Bundles scriptable object
****/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BundleCards : MonoBehaviour
{

    public string bundleInfo;
    public Text bundleUI;
    public Text pointsUI;

    [Header("Set in Inspector")]
    public List<Cards> cardList;
    

    [HideInInspector]
    public int playerNumber;
    private List<Cards> bundleGoals = new List<Cards>();
    GameObject[] cardInventoryClone;
    private int pointReward = 0;
    private GameManager gm;

    private void Start()
    {
        bundleInfo = "";
        gm = GameManager.GM;
        if (cardList == null)
        {
            Debug.LogWarning("Scriptable Objects List not found");
            return;
        }
        int randomIndex = Random.Range(2, 4);
        for (int i = 0; i < randomIndex; i++)
        {
            pointReward += 1;
            int randomAnimalIndex = Random.Range(0, 8);
            Debug.Log("Added animal " + cardList[randomAnimalIndex].name);
            bundleGoals.Add(cardList[randomAnimalIndex]);
            if (Random.Range(0, 101) > 75)
            {
                bundleGoals.Add(cardList[randomAnimalIndex]);
                Debug.Log("Added animal " + cardList[randomAnimalIndex].name);
                pointReward += 2;
            }
        }
        foreach (Cards item in bundleGoals)
        {
            bundleInfo += item.animalName + "\n";
        }
        bundleUI.text = bundleInfo;
        pointsUI.text = "Points: " + pointReward;
    }

    public void removeanimal(string animalname)
    {
        //foreach (scriptableobject animal in bundlegoals)
        //{
        //    if (animal.name == animalname)
        //    {
        //        debug.log("removing " + animal.name + " from bundle");
        //        bundlegoals.remove(animal);
        //        break;
        //    }
        //}

        //if (bundlegoals.count == 0)
        //{
        //    debug.log("player " + playernumber + " completed a bundle");
        //    gm.updateplayerscore(playernumber, pointreward);

        //    //handle destroying gameobject cleanly here
        //    //need to access player inventory to remove this bundle
        //    destroy(this.transform.parent);
        //}

    }

    public void ButtonPress()
    {
        if (gm.playerTurn == 1)
        {
            CompleteBundle();
        }
    }


    //Potentially make a function that completed all animals at once instead of one at a time. This will need access to player inventory to function. Add later
    public void CompleteBundle()
    {
        GameObject[] cardInventory = this.transform.parent.GetComponent<Inventory>().cardInventory;
        cardInventoryClone = cardInventory;
        List<Cards> bundleClone = bundleGoals;

        //Iterates through each card in the bundle to complete
        for (int j = 0; j < bundleClone.Count; j++)
        {
            //If it cannot find a valid card for this, return the function, skipping completion rewards
            if (CheckForCard(j) == false)
            {
                return;
            }
        }

        //Remove each card used in bundle completion
        foreach (GameObject card in cardInventory)
        {
            this.transform.parent.GetComponent<Inventory>().RemoveCard(card);
        }

        //Update Player Score
        gm.UpdatePlayerScore(gm.playerTurn, pointReward);
    }

    bool CheckForCard(int j)
    {
        //Iterate through each card in the owners inventory
        for (int i = 0; i < cardInventoryClone.Length; i++)
        {

            //If slot is null for some reason, just skip it
            if (cardInventoryClone[i] == null)
            {
                continue;
            }

            //If the card names are equal, return true
            if (cardInventoryClone[i].name == bundleGoals[j].animalName + " card")
            {
                Debug.Log("BUNDLE spot COMPLETED");

                //This just invalidates my weird workaround. Im checking gameobject name instead of animal name since it wouldnt let me for some reason
                cardInventoryClone[i].name = "xsdasd";
                return true;
            }
        }

        //If no valid card is found, return false
        Debug.Log("Invalid Bundle Completion");
        return false;
    }
}
