using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class TeethDirtClean : MonoBehaviour
{
    public ScreenShake screenShake;

    [SerializeField] private Texture2D _dirtMaskBase;
    //[SerializeField] private Texture2D _brush;
    [SerializeField] private Material _material;
    TextMeshProUGUI asd;
    private Texture2D _templateDirtMask;
    float toatlDirtOnTeeth = 0;
    float remaindingDirt;
    [Header("FRONT < LEFT< BACK < RIGHT")]
    public List<toolsToClean> ttcf= new List<toolsToClean>();
    public List<toolsToClean> ttcb = new List<toolsToClean>();
    public List<toolsToClean> ttcr = new List<toolsToClean>();
    public List<toolsToClean> ttcl = new List<toolsToClean>();



    public BoxCollider BCs;
    public MeshCollider MC;
    [SerializeField]Material TooThDone;
    Material Dirttooth;
    public float percentage;
    bool clean;
    AudioManager audioManager;
    private void Start()
    {

    }
    private void Awake()
    {
        audioManager = AudioManager.Instance;

        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found.");
        }
    }

    public void SetProblem()
    {
        Debug.Log("sdad");
        TooThDone = Resources.Load<Material>("Mat/clean");
        Dirttooth = Resources.Load<Material>("Mat/teethMasks");

        
        GetComponent<Renderer>().material = Dirttooth;
        _material = GetComponent<Renderer>().material;
        CreateTexture();
        for (int i = 0; i < _dirtMaskBase.width; i++)
        {
            for (int j = 0; j < _dirtMaskBase.height; j++)
            {
                toatlDirtOnTeeth += _dirtMaskBase.GetPixel(i, j).g;
            }
        }
        remaindingDirt = toatlDirtOnTeeth;

        BCs = GetComponent<BoxCollider>();
        MC = GetComponent<MeshCollider>();
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

    public void Clean(RaycastHit[] hit, Texture2D _brush, Ray ray)
    {
        if(clean || minigameTaskListController.Instance.currentStep != Steps.SCRAPINGS)
        {
            return;
        }

        
     //   Debug.Log(positionRelativeToEnemy);
        Vector3 hitPointLocal = transform.InverseTransformPoint(hit[0].point);

        float xAbs = Mathf.Abs(hitPointLocal.x);
        float zAbs = Mathf.Abs(hitPointLocal.z);

        char positionRelativeToEnemy;

        if (xAbs > zAbs)
        {
            // Hit point is either "left" or "right" relative to the enemy
            if (hitPointLocal.x > 0)
            {
                positionRelativeToEnemy = 'r';
            }
            else
            {
                positionRelativeToEnemy = 'l';
            }
        }
        else
        {
            // Hit point is either "front" or "back" relative to the enemy
            if (hitPointLocal.z > 0)
            {
                positionRelativeToEnemy = 'f';
            }
            else
            {
                positionRelativeToEnemy = 'b';
            }
        }
        bool correctTool = checkTools(positionRelativeToEnemy);
        if (correctTool)
        {
                cleanining(hit[0], _brush);
        }
        else
        {
       
        }

    }
    public void Clean(RaycastHit hit, Texture2D _brush, Ray ray)
    {
        if (clean)
        {
            return;
        }

        //   Debug.Log(positionRelativeToEnemy);
        Vector3 hitPointLocal = transform.InverseTransformPoint(hit.point);

        float xAbs = Mathf.Abs(hitPointLocal.x);
        float zAbs = Mathf.Abs(hitPointLocal.z);

        char positionRelativeToEnemy;

        if (xAbs > zAbs)
        {
            // Hit point is either "left" or "right" relative to the enemy
            if (hitPointLocal.x > 0)
            {
                positionRelativeToEnemy = 'r';
            }
            else
            {
                positionRelativeToEnemy = 'l';
            }
        }
        else
        {
            // Hit point is either "front" or "back" relative to the enemy
            if (hitPointLocal.z > 0)
            {
                positionRelativeToEnemy = 'f';
            }
            else
            {
                positionRelativeToEnemy = 'b';
            }
        }
        bool correctTool = checkTools(positionRelativeToEnemy);
        if (correctTool)
        {
            cleanining(hit, _brush);
        }

    }
    bool  checkTools(char a)
    {
        bool correct = false;
        switch(a)
        {
            case 'f':
                foreach(toolsToClean tc in ttcf)
                {
                    if (minigameTaskListController.Instance.GetSelectedtool() == tc.ToString())
                    {
                        correct = true;
                    }
                    break;
                }
             
                break;
            case 'l':
                foreach (toolsToClean tc in ttcl)
                {
                    if (minigameTaskListController.Instance.GetSelectedtool() == tc.ToString())
                    {
                        correct = true;
                    }
                    break;
                }
                break;
            case 'b':
                foreach (toolsToClean tc in ttcb)
                {
                    if (minigameTaskListController.Instance.GetSelectedtool() == tc.ToString())
                    {
                        correct = true;
                    }
                    break;
                }
                break;
            case 'r':
                foreach (toolsToClean tc in ttcr)
                {
                    if (minigameTaskListController.Instance.GetSelectedtool() == tc.ToString())
                    {
                        correct = true;
                    }
                    break;
                }
                break;
        }
        if(!correct)
        {
            minigameTaskListController.Instance.wrongTool();
        }

        return correct;

    }

    void cleanining(RaycastHit hit, Texture2D _brush)
    {

        Vector2 textureCoord = hit.textureCoord;
        int pixelX = (int)(textureCoord.x * _templateDirtMask.width);
        int pixelY = (int)(textureCoord.y * _templateDirtMask.height);

        int offSetX = pixelX - (_brush.width / 2);

        int offSetY = pixelY - (_brush.height / 2);
        for (int x = 0; x < _brush.width; x++)
        {
            for (int y = 0; y < _brush.height; y++)
            {
                Color pixelDirt = _brush.GetPixel(x, y);
                Color pixelDirtMask = _templateDirtMask.GetPixel(offSetX + x, offSetY + y);
                float removedAmount = pixelDirtMask.g - (pixelDirtMask.g * pixelDirt.g);
                remaindingDirt -= removedAmount;
                audioManager.PlaySFX(6);
                _templateDirtMask.SetPixel(offSetX + x, offSetY + y, new Color(0, pixelDirtMask.g * pixelDirt.g, 0));
            }
        }

       // Debug.Log("Percentage that look clean = " + (remaindingDirt / toatlDirtOnTeeth));
        if ((remaindingDirt / toatlDirtOnTeeth) < (0.95f) && !clean)
        {
            clear();
        }
        _templateDirtMask.Apply();
        //_material.SetTexture("_DirtMask", _templateDirtMask);

    }

    void clear()
    {
        GetComponent<Renderer>().material = TooThDone;
        clean = true;
        // BCs.enabled = false;
        minigameTaskListController.Instance.gonext();
        Instantiate(minigameTaskListController.Instance.goodJob, cameraChanger.Instance.GetCurrentCam().transform.position + cameraChanger.Instance.GetCurrentCam().transform.forward, Quaternion.Euler(-86.65f, 0, 0));

    }

    public void Cheat()
    {
        GetComponent<Renderer>().material = TooThDone;
        Invoke("CheatClear", 4);

      //  StartCoroutine(screenShake.Shaking());
    }

    public void CheatClear()
    {

        minigameTaskListController.Instance.CheckGameComplete();
    }

    public enum toolsToClean
    {
        Gracey1_2,
        Gracey3_4,
        Gracey5_6, 
        Gracey7_8,
        Gracey9_10,
        Gracey11_12,
        Gracey13_14
    }

}