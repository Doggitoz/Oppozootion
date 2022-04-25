/**** 
 * Created by: Ben Jenkins
 * Date Created: April 20,2022
 * 
 * Last Edited by: NA
 * Last Edited: NA
 * 
 * Description: Handles the player GameObject and the cards in a players hand
****/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    /*Variables*/
    [HideInInspector] public List<GameObject> AnimalCards;
    [HideInInspector] public List<GameObject> BundleCards;

    [HideInInspector] public float height = 2.5f;
    [HideInInspector] public float AnimalAreaWidth = 7.5f;
    [HideInInspector] public float BundleAreaWidth = 2.5f;


    //PlayerCardArea is 5 long and 2.5 wide for internal coords of (5,2.5) to (-5,-2.5)
    //AnimalCardArea is 7.5 long and 2.5 wide for internal coords of (5,2.5) to (-2.5,-2.5)
    //BundleCardArea is 2.5 long and 2.5 wide for internal coords of (-2.5,2.5) to (-5,-2.5)

    // Start is called before the first frame update
    void Start()
    {
        AnimalCards = new List<GameObject>();
        BundleCards = new List<GameObject>(); 
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimalCards();
        UpdateBundleCards();
    }

    public void addAnimalCard(GameObject obj)
    {
        AnimalCards.Add(obj);
    }
    public void addBundleCard(GameObject obj)
    {
        BundleCards.Add(obj);
    }

    private void UpdateAnimalCards()
    {

<<<<<<< Updated upstream
=======
        float currentspacing = 0f;
        //float AnimalCardHeightSpacing;
        

        for (int i = 0; i < AnimalCards.Count; i++)
        {
            if (i % 2 == 0)
            {
                currentspacing += cardSpacing;
            }
            if (AnimalCards[i] != null)
                AnimalCards[i].transform.localPosition = new Vector3((5f) - currentspacing, 0, ((i % 2)*2.5f-1.25f));
        }
>>>>>>> Stashed changes
    }
    private void UpdateBundleCards()
    {

    }
}
