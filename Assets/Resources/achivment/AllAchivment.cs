using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Achivement", menuName = "achivement")]
public class AllAchivment : ScriptableObject
{
    // Start is called before the first frame update

    public string AchivmentName;
    public Sprite AchivmentImage;
    public Sprite AchivmentImageLock;


    public bool have;
    public int id;

}
