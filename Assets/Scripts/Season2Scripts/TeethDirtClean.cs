using UnityEngine;
using System.Collections;
using TMPro;

public class TeethDirtClean : MonoBehaviour
{
   [SerializeField] private Camera _camera;

    [SerializeField] private Texture2D _dirtMaskBase;
    [SerializeField] private Texture2D _brush;

    [SerializeField] private Material _material;

    private Texture2D _templateDirtMask;
    [SerializeField] TextMeshProUGUI asd;
    private void Start()
    {
        CreateTexture();
        asd = GameObject.Find("asd").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {


            //RaycastHit hit;


            //// Construct a ray from the current touch coordinates
            //Ray ray = _camera.ScreenPointToRay(Input.GetTouch(0).position);

            //    if (Physics.Raycast(ray, out hit))
            //    {
            //        Vector2 textureCoord = hit.textureCoord;
            //        asd.text = $"UI:{hit.collider.gameObject.name}";

            //        int pixelX = (int)(textureCoord.x * _templateDirtMask.width);
            //        int pixelY = (int)(textureCoord.y * _templateDirtMask.height);

            //        for (int x = 0; x < _brush.width; x++)
            //        {
            //            for (int y = 0; y < _brush.height; y++)
            //            {
            //                Color pixelDirt = _brush.GetPixel(x, y);
            //                Color pixelDirtMask = _templateDirtMask.GetPixel(pixelX + x, pixelY + y);

            //                _templateDirtMask.SetPixel(pixelX + x,
            //                    pixelY + y,
            //                    new Color(0, pixelDirtMask.g * pixelDirt.g, 0));
            //            }
            //        }

            //        _templateDirtMask.Apply();
            //    }



        }
    }

    private void CreateTexture()
    {
        _templateDirtMask = new Texture2D(_dirtMaskBase.width, _dirtMaskBase.height);
        _templateDirtMask.SetPixels(_dirtMaskBase.GetPixels());
        _templateDirtMask.Apply();

        _material.SetTexture("_DirtMask", _templateDirtMask);
    }
}