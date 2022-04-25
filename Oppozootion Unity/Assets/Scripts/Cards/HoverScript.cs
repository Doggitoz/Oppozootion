/**** 
 * Created by: Ben Jenkins
 * Date Created: April 20,2022
 * 
 * Last Edited by: NA
 * Last Edited: NA
 * 
 * Description: Controls the halo when a card is hovered
****/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverScript : MonoBehaviour
{
    [HideInInspector]
    public GameObject thisCubeSlot;
    [HideInInspector]
    public GameObject HoverSpot;

    // Start is called before the first frame update
    void Start()
    {
        thisCubeSlot = this.gameObject;
        HoverSpot = thisCubeSlot.transform.Find("HaloHolder").gameObject;
        HoverSpot.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        this.HoverSpot.SetActive(true);
        //hover = true;
    }

    private void OnMouseExit()
    {
        this.HoverSpot.SetActive(false);
<<<<<<< Updated upstream
        //hover = false;
=======
        hover = false;
    }

    private void OnMouseDown()  
    {
        if (hover && GM.playerTurn == 1)
        {
            GM.player.GetComponent<PlayerScript>().cardsTakenByPlayer += 1;
            TakeCard(GM.player);
        }
    }

    public void TakeCard(GameObject source)
    {
        if (this.gameObject.GetComponent<CardData>())
        {
            source.GetComponent<Inventory>().AddCard(this.gameObject);
        }
        else
        {
            source.GetComponent<Inventory>().AddBundle(this.gameObject);
        }
        this.gameObject.SetActive(false);
>>>>>>> Stashed changes
    }
}
