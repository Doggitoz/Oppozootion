/**** 
 * Created by: Bobby Ouyang
 * Date Created: April 19, 2022
 * 
 * Last Edited by: Bobby Ouyang 
 * Last Edited: April 19, 2022
 * 
 * Description: List for all the scriptable objects ( 8 animals)
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_listName", menuName = "SOList")]
public class SOList : ScriptableObject
{
    public List<Cards> cardList;
    
}
