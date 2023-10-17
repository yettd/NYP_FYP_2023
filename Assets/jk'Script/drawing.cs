using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawing : MonoBehaviour
{
    public GameObject drawingPlane; // Assign the plane in the Inspector.
    public float drawDistance = 5.02f; // Adjust this value for drawing sensitivity.

    protected List<Vector3> currentDrawing = new List<Vector3>();
    private LineRenderer lineRenderer;
    private Plane drawingSurface;
    private Vector3 lastMousePosition;

    public string GesturName;

    public gestureList ges = new gestureList();
    // Start is called before the first frame update
    protected virtual void Start()
    {
        string a = Saving.save.LoadFromJson("gesture");

        if (a != null)
        {
            ges = JsonUtility.FromJson<gestureList>(a);

            //foreach(Gesture v in ges.gestures)
            //{

            //        Debug.Log(v.points.Count);

            //}

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
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.useWorldSpace = false;
    }

    protected virtual void HandleDrawingInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentDrawing.Clear();
            lineRenderer.positionCount = 0;
        }
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitDistance;

            if (Physics.Raycast(ray, out hitDistance))
            {
                Vector3 hitPoint = hitDistance.point;
                DrawOnPlane(hitPoint);
            }
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            Stuff();
        }
    }

    protected virtual void DrawOnPlane(Vector3 point)
    {
        if (currentDrawing.Count == 0 || Vector2.Distance(currentDrawing[currentDrawing.Count - 1], point) > drawDistance)
        {
            if(currentDrawing.Count > 0)
                Debug.Log(Vector3.Distance(currentDrawing[currentDrawing.Count - 1], point));
            currentDrawing.Add(drawingPlane.transform.InverseTransformPoint(point));
            lineRenderer.positionCount = currentDrawing.Count;
            lineRenderer.SetPosition(currentDrawing.Count - 1, currentDrawing[currentDrawing.Count - 1]);
        }
    }
}
