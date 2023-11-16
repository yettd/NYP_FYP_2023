using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clock : MonoBehaviour
{

    float totatlTime;
    public float maximum = 100;
    public Image img;
    // Start is called before the first frame update
    void Start()
    {
        totatlTime = maximum;
    }

    // Update is called once per frame
    void Update()
    {
        totatlTime -= Time.deltaTime;

        img.fillAmount = (totatlTime / maximum);
        Color a = Color.red;
        a.a -= Time.deltaTime;
    }

}
