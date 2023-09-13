using UnityEngine;

public class TeethDirtClean : MonoBehaviour
{
    public Camera mainCamera;
    public Renderer teethRenderer;
    public Texture2D maskTexture;
    public Texture2D brushTexture;

    private void Start()
    {
        Color[] pixels = maskTexture.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.white;
        }
        maskTexture.SetPixels(pixels);
        maskTexture.Apply();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector2 uv = hit.textureCoord;
                PaintAtUV(uv);
            }
        }
    }

    private void PaintAtUV(Vector2 uv)
    {
        int x = (int)(uv.x * maskTexture.width);
        int y = (int)(uv.y * maskTexture.height);

        int brushSize = 10; // Adjust brush size as needed

        for (int i = -brushSize; i <= brushSize; i++)
        {
            for (int j = -brushSize; j <= brushSize; j++)
            {
                int pixelX = Mathf.Clamp(x + i, 0, maskTexture.width - 1);
                int pixelY = Mathf.Clamp(y + j, 0, maskTexture.height - 1);

                Color brushColor = brushTexture.GetPixelBilinear((i + brushSize) / (float)(2 * brushSize), (j + brushSize) / (float)(2 * brushSize));
                Color maskColor = maskTexture.GetPixel(pixelX, pixelY);

                maskColor.r = Mathf.Max(maskColor.r - brushColor.r * 0.1f, 0);
                maskTexture.SetPixel(pixelX, pixelY, maskColor);
            }
        }

        maskTexture.Apply();
        teethRenderer.material.SetTexture("_MaskTex", maskTexture);
    }
}