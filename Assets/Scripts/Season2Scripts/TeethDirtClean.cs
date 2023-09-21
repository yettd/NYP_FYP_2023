using UnityEngine;
using System.Collections;
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
    public toolsToClean ttc;
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
        asd = GameObject.Find("asd").GetComponent<TextMeshProUGUI>();
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
    

            Vector2 textureCoord = hit.textureCoord;

            int pixelX = (int)(textureCoord.x * _templateDirtMask.width);
            int pixelY = (int)(textureCoord.y * _templateDirtMask.height);
            asd.text = $"{textureCoord.x}";
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
        Debug.Log("Percentage that look clean = "+ (remaindingDirt / toatlDirtOnTeeth));
            if ((remaindingDirt / toatlDirtOnTeeth) < percentage-0.01)
            {
                Destroy(gameObject);
                GetComponent<Renderer>().material = TooThDone;
                clean = true;
            }

            _templateDirtMask.Apply();
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