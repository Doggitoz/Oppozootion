/**** 
 * Created by: Bobby Ouyang
 * Date Created: April 25,2022
 * 
 * Last Edited by: NA
 * Last Edited: NA
 * 
 * Description: Swithces the camera angle when player hit "1" or "2" on the keyboard
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Camera1"))
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
        }
        if (Input.GetButtonDown("Camera2"))
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
        }


    }
}
