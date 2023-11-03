using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GICcapsule : Tolls
{
    // Start is called before the first frame update

    [SerializeField] RectTransform canvas;
    [SerializeField] Texture2D _brush;
    [SerializeField] bool shake = false;
    int taps = 0;
    bool down = false;
    bool preRequisite = false;

    public GameObject model;
    // Start is called before the first frame update
    protected override void Start()
    {
       
        //letgoToUse = false;
    }

    private void OnMouseDown()
    {
        //if(!down && taps <3)
        //{
        //    taps++;
        //    down = true;
        //}
        //if(taps < 3)
        //{
        //    return;
        //}


        //if(transform.localScale.x > 80)
        //{
        //    transform.localScale -= new Vector3(1, 0, 0) * Time.timeScale;
        //    return;
        //}
        //preRequisite=true;
        base.Start();

    }
    private void OnMouseUp()
    {
        down= false;
    }

    // Update is called once per frame
    protected override void usetool(RaycastHit hit)
    {
        
      
        if(!shake)
        {
            amalgator amalgator;

            hit.collider.TryGetComponent<amalgator>(out amalgator);
            if (amalgator)
            {
                gameObject.SetActive(false);
                amalgator.shake(this);
            }
            return;
        }

        Applicator app;
        hit.collider.TryGetComponent<Applicator>(out app);
        if (app)
        {
            minigameTaskListController.Instance.gic = true;
            GameObject Almagotrrr = GameObject.Find("Almagotrrr");
            minigameTaskListController.Instance.ToolsSelected("Applicator", model);
            Almagotrrr.SetActive(false) ;
        }
    }

    public void SetShake()
    {

        shake = true;
    }
}
