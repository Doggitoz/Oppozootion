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

public class BundleCards : MonoBehaviour
{
    [Header("Set in Inspector")]
    public List<ScriptableObject> cardList;

    [HideInInspector]
    public int playerNumber;
    private List<ScriptableObject> bundleGoals = new List<ScriptableObject>();
    private int pointReward = 0;
    private GameManager gm;

    private void Start()
    {
        gm = GameManager.GM;
        if (cardList == null)
        {
            Debug.LogWarning("Scriptable Objects List not found");
            return;
        }
        for (int i = 0; i < Random.Range(2, 4); i++)
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
        Debug.Log("Point reward = " + pointReward);
    }

    public void RemoveAnimal(string animalName)
    {
        foreach (ScriptableObject animal in bundleGoals)
        {
            if (animal.name == animalName)
            {
                Debug.Log("Removing " + animal.name + " from bundle");
                bundleGoals.Remove(animal);
                break;
            }
        }

        if (bundleGoals.Count == 0)
        {
            Debug.Log("Player " + playerNumber + " completed a bundle");
            gm.UpdatePlayerScore(playerNumber, pointReward);

            //HANDLE DESTROYING GAMEOBJECT CLEANLY HERE
            //Need to access player inventory to remove this bundle
            Destroy(this.transform.parent);
        }

    }

    //Potentially make a function that completed all animals at once instead of one at a time. This will need access to player inventory to function. Add later
    private void CompleteBundle()
    {

    }
}
