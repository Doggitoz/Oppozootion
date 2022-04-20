/**** 
 * Created by: Bobby Ouyang
 * Date Created: April 19, 2022
 * 
 * Last Edited by: Bobby Ouyang 
 * Last Edited: April 19, 2022
 * 
 * Description: Using the scriptable objects
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObj : MonoBehaviour
{
   // [SerializeField]
   // Cards card;

    [SerializeField]
    SOList list;



    // Start is called before the first frame update
    void Start()
    {
     //   Debug.Log(card.animalName + " is the card name");
        ReadList(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReadList(int num)
    {
        Debug.Log(list.cardList[num].animalName + " is the animal name");
    }

}
