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
    public GameObject bundlePrefab;
    public GameObject[] bundleList = new GameObject[3];



    // Start is called before the first frame update
    void Start()
    {
        bundleList[0] = Instantiate(bundlePrefab);
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < list.cardList.Count; i++)
        //{
        //    if (bundleList[0] != null)
        //        bundleList[0].GetComponent<BundleCards>().RemoveAnimal(list.cardList[i].name);
        //}
    }

    void ReadList(int num)
    {
        Debug.Log(list.cardList[num].animalName + " is the animal name");
    }

}
