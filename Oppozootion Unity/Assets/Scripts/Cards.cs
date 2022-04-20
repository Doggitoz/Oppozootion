/**** 
 * Created by: Bobby Ouyang
 * Date Created: April 19, 2022
 * 
 * Last Edited by: Bobby Ouyang 
 * Last Edited: April 19, 2022
 * 
 * Description: Cards scriptable object
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_animal", menuName = "Card")]
public class Cards : ScriptableObject
{
    public string animalName;
    public Sprite art;

}
