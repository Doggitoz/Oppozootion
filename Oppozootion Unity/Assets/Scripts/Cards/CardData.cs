/**** 
 * Created by: Coleton Wheeler
 * Date Created: April 22, 2022
 * 
 * Last Edited by: Coleton Wheeler
 * Last Edited: April 22, 2022
 * 
 * Description: Script to hold card prefab instance data
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardData : MonoBehaviour
{

    public Cards cardData;
    public string animalName;
    public Sprite animalImage;

    [SerializeField] private Image UIImage;
    [SerializeField] private Text UIText;

    // Start is called before the first frame update
    void Start()
    {
        animalName = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        animalName = cardData.name;
        animalImage = cardData.art;
        UIImage.GetComponent<Image>().sprite = animalImage;
        UIText.GetComponent<Text>().text = animalName;
    }
}
