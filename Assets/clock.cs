using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clock : MonoBehaviour
{

    float totatlTime;
    float maximum = 300;
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
    }
}
