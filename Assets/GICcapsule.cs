using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GICcapsule : Tolls
{
    // Start is called before the first frame update

    [SerializeField] RectTransform canvas;
    [SerializeField] Texture2D _brush;
    [SerializeField] bool shake = false;
    public GameObject model;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //letgoToUse = false;
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
