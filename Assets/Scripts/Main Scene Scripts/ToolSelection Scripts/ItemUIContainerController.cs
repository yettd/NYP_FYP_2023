using UnityEngine;
using UnityEngine.UI;


public class ItemUIContainerController : MonoBehaviour
{
    public Text ItemName, ItemDescription;
    public Image ItemPortrait;

    //Assign the data for Tool Info Panel
    public void Init(string name, string description, Sprite image)
    {
        ItemName.text = name;
        ItemDescription.text = description;
        ItemPortrait.sprite = image;
    }

}
