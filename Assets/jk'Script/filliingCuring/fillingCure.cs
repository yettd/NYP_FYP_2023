using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fillingCure : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject drawingPlane; // Assign the plane in the Inspector.
    public float drawDistance = 5.02f; // Adjust this value for drawing sensitivity.

    private List<Vector3> currentDrawing = new List<Vector3>();
    private LineRenderer lineRenderer;
    private Plane drawingSurface;
    private Vector3 lastMousePosition;

    public gestureList ges = new gestureList();

    [SerializeField] List<HitPoint> hp = new List<HitPoint>();
    // Start is called before the first frame update
    protected virtual void Start()
    {

        foreach (Transform t in transform)
        {
            Debug.Log(t.gameObject);    
            hp.Add(new HitPoint(false, t.transform));
        }

        if (drawingPlane == null)
        {
            Debug.LogError("Please assign the drawing plane in the Inspector.");
        }

        drawingSurface = new Plane(drawingPlane.transform.up, drawingPlane.transform.position);


        CreateLineRenderer();
    }

    protected virtual void Update()
    {
        HandleDrawingInput();
    }

    protected virtual void Stuff()
    {

    }

    protected virtual void CreateLineRenderer()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = .5f;
        lineRenderer.positionCount = 0;
        lineRenderer.endWidth = .5f;
        lineRenderer.useWorldSpace = false;
    }

    protected virtual void HandleDrawingInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            STOP();
        }
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitDistance;

            if (Physics.Raycast(ray, out hitDistance))
            {
                Vector3 hitPoint = hitDistance.point;

                DrawOnPlane(hitDistance.point);
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            STOP();
        }
    }

    protected virtual void DrawOnPlane(Vector3 point)
    {
        if (currentDrawing.Count == 0 || Vector2.Distance(currentDrawing[currentDrawing.Count - 1], point) > drawDistance)
        {
           

            currentDrawing.Add(drawingPlane.transform.InverseTransformPoint(point));
            lineRenderer.positionCount = currentDrawing.Count;
            lineRenderer.SetPosition(currentDrawing.Count - 1, currentDrawing[currentDrawing.Count - 1]);
          
        }
    }

    public void STOP()
    {
        currentDrawing.Clear();
        lineRenderer.positionCount = 0;
        foreach(HitPoint aa in hp)
        {
            aa.hitted = false;
        }
    }

    public void GetHit(Transform pos)
    {
        foreach (HitPoint aa in hp)
        {
  
            if (aa.point==pos)
            {
                aa.hitted = true;
             
                CheckAllHIt();
            }
        }
    }

    public void CheckAllHIt()
    {
        foreach (HitPoint aa in hp)
        {
          
            if (aa.hitted==false)
            {
                return;
            }
        }
        Debug.Log("HIT");
        Destroy(gameObject);
    }

}

public class HitPoint
{
    public bool hitted;
    public Transform point;

    public HitPoint(bool hitted, Transform point)
    {
        this.hitted = hitted;
        this.point = point;
    }
}
