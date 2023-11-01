using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fillingCure : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject drawingPlane; // Assign the plane in the Inspector.
    public float drawDistance = 0.02f; // Adjust this value for drawing sensitivity.

    private List<Vector3> currentDrawing = new List<Vector3>();
    private LineRenderer lineRenderer;
    private Plane drawingSurface;
    private Vector3 lastMousePosition;

    toothFilling tf;


    bool done;

    public void SetTF(toothFilling tf)
    { this.tf = tf; }           

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

    public void Stuff(RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitDistance;

            Vector3 hitPoint = hit.point;


            DrawOnPlane(hit.point);


        
    }

    protected virtual void CreateLineRenderer()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = 0.02739716f;
        lineRenderer.positionCount = 0;
        lineRenderer.endWidth = 0.02739716f;
        lineRenderer.useWorldSpace = false;
    }

    public virtual bool HandleDrawingInput()
    {
        // DrawOnPlane(hit.point);

        //if(done)
        //{
        //    return true;
        //}
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

        return false;
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
                Debug.Log("sad");
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
        done=true;
        tf.NextStepForce();
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
