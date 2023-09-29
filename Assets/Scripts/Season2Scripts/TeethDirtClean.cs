using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class TeethDirtClean : MonoBehaviour
{
    [SerializeField] private Texture2D _dirtMaskBase;
    //[SerializeField] private Texture2D _brush;
    [SerializeField] private Material _material;
    TextMeshProUGUI asd;
    private Texture2D _templateDirtMask;
    float toatlDirtOnTeeth = 0;
    float remaindingDirt;
    public List<toolsToClean> ttc= new List<toolsToClean>();
    public BoxCollider BCs; 
    Material TooThDone;
    public float percentage;
    bool clean;
    private void Start()
    {
        TooThDone = Resources.Load<Material>("Mat/clean");
        CreateTexture();
        for (int i = 0; i < _dirtMaskBase.width; i++)
        {
            for (int j = 0; j < _dirtMaskBase.height; j++)
            {
                toatlDirtOnTeeth += _dirtMaskBase.GetPixel(i, j).g;
            }
        }
        remaindingDirt = toatlDirtOnTeeth;
        _material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    RaycastHit hit;

        //    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //       // Clean(hit);
        //        //Vector2 textureCoord = hit.textureCoord;

        //        //int pixelX = (int)(textureCoord.x * _templateDirtMask.width);
        //        //int pixelY = (int)(textureCoord.y * _templateDirtMask.height);

        //        //for (int x = 0; x < _brush.width; x++)
        //        //{
        //        //    for (int y = 0; y < _brush.height; y++)
        //        //    {
        //        //        Color pixelDirt = _brush.GetPixel(x, y);
        //        //        Color pixelDirtMask = _templateDirtMask.GetPixel(pixelX + x, pixelY + y);

        //        //        _templateDirtMask.SetPixel(pixelX + x,
        //        //            pixelY + y,
        //        //            new Color(0, pixelDirtMask.g * pixelDirt.g, 0));
        //        //    }
        //        //}

        //        //_templateDirtMask.Apply();
        //    }



        //}
    }

    private void CreateTexture()
    {
        _templateDirtMask = new Texture2D(_dirtMaskBase.width, _dirtMaskBase.height);
        _templateDirtMask.SetPixels(_dirtMaskBase.GetPixels());
        _templateDirtMask.Apply();

        _material.SetTexture("_DirtMask", _templateDirtMask);
    }

    public void Clean(RaycastHit hit, Texture2D _brush)
    {
        RaycastHit thisSeemsDumb = hit;

        Vector3 norm = hit.point - transform.forward;

        Debug.Log(norm.normalized);

        

        //for (int total = 0; total < BCs.Count; total++)
        //{
        //    if (BCs[total] == hit.collider)
        //    {
        //        if (minigameTaskListController.Instance.GetSelectedtool() == ttc[total].ToString())
        //        {
        //            cleanining(hit,_brush);
        //        }
        //        else
        //        {

        //            return;
        //        }
        //    }
        //}


    }

    void cleanining(RaycastHit hit, Texture2D _brush)
    {
        Vector2 textureCoord = hit.textureCoord;
        int pixelX = (int)(textureCoord.x * _templateDirtMask.width);
        int pixelY = (int)(textureCoord.y * _templateDirtMask.height);
        for (int x = 0; x < _brush.width; x++)
        {
            for (int y = 0; y < _brush.height; y++)
            {
                Color pixelDirt = _brush.GetPixel(x, y);
                Color pixelDirtMask = _templateDirtMask.GetPixel(pixelX + x, pixelY + y);

                _templateDirtMask.SetPixel(pixelX + x, pixelY + y, new Color(0, pixelDirtMask.g * pixelDirt.g, 0));
            }
        }
        remaindingDirt = 0;

        for (int i = 0; i < _dirtMaskBase.width; i++)
        {
            for (int j = 0; j < _dirtMaskBase.height; j++)
            {
                remaindingDirt += _templateDirtMask.GetPixel(i, j).g;
            }
        }
        Debug.Log("Percentage that look clean = " + (remaindingDirt / toatlDirtOnTeeth));
        if ((remaindingDirt / toatlDirtOnTeeth) < (percentage + 0.005) && !clean)
        {
            GetComponent<Renderer>().material = TooThDone;
            clean = true;
            minigameTaskListController.Instance.CheckGameComplete();
            Instantiate(minigameTaskListController.Instance.goodJob, cameraChanger.Instance.GetCurrentCam().transform.position + cameraChanger.Instance.GetCurrentCam().transform.forward, Quaternion.Euler(-86.65f, 0, 0));

        }

        _templateDirtMask.Apply();
    }

    public void Cheat()
    {
        GetComponent<Renderer>().material = TooThDone;
        _templateDirtMask.Apply();
        Invoke("CheatClear", 4);
    }

    public void CheatClear()
    {

        minigameTaskListController.Instance.CheckGameComplete();
    }

    public enum toolsToClean
    {
        Gracey1_2,
        Gracey5_6, 
        Gracey7_8,
        Gracey11_12,
        Gracey13_14
    }

}