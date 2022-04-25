/**** 
 * Created by: Ben Jenkins
 * Date Created: April 20,2022
 * 
 * Last Edited by: NA
 * Last Edited: NA
 * 
 * Description: Controls the halo when a card is hovered
****/
using UnityEngine;

public class HoverScript : MonoBehaviour
{
    [HideInInspector] public GameObject thisCubeSlot;
    [HideInInspector] public GameObject HoverSpot;
    [HideInInspector]
    private bool hover;

    private GameManager GM;

    private void Awake()
    {
        GM = GameManager.GM;
    }

    // Start is called before the first frame update
    void Start()
    {
        thisCubeSlot = this.gameObject;
        HoverSpot = thisCubeSlot.transform.Find("HaloHolder").gameObject;
        HoverSpot.SetActive(false);
    }

    private void OnMouseEnter()
    {
        this.HoverSpot.SetActive(true);
        hover = true;
    }

    private void OnMouseExit()
    {
        this.HoverSpot.SetActive(false);
        hover = false;
    }

    private void OnMouseDown()  
    {
        if (hover && GM.playerTurn == 1)
        {
            if (this.gameObject.GetComponent<CardData>())
            {
                GM.player.GetComponent<PlayerScript>().cardsTakenByPlayer += 1;
                TakeCard(GM.player);
            }
        }
    }

    public void TakeCard(GameObject source)
    {
        Debug.LogWarning(source);
        source.GetComponent<Inventory>().AddCard(this.gameObject);
        GM.board.GetComponent<DrawArea>().RemoveCardFromBoard(this.gameObject);
    }

    public void TakeBundle(GameObject source)
    {
        source.GetComponent<Inventory>().AddBundle(this.gameObject);
    }
}
