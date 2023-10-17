using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GestureDrawing : MonoBehaviour
{
    public GameObject drawingPlane; // Assign the plane in the Inspector.
    public float drawDistance = 0.02f; // Adjust this value for drawing sensitivity.

    private List<Vector3> currentDrawing = new List<Vector3>();
    private LineRenderer lineRenderer;
    private Plane drawingSurface;
    private Vector3 lastMousePosition;

    public string GesturName;

    gestureList ges = new gestureList();
    // Start is called before the first frame update
    private void Start()
    {
        string a = Saving.save.LoadFromJson("gesture");
        CreateLineRenderer();
        if (a != null)
        {
            ges=JsonUtility.FromJson<gestureList>(a);
            foreach (Gesture v in ges.gestures)
            {
                lineRenderer.positionCount += v.points.Count;
                for (int j = 0; j < v.points.Count; j++)
                {
                    lineRenderer.SetPosition(j, v.points[j]);

                }
            }

        }

        if (drawingPlane == null)
        {
            Debug.LogError("Please assign the drawing plane in the Inspector.");
        }

        drawingSurface = new Plane(drawingPlane.transform.up, drawingPlane.transform.position);
    

    
    }

    private void Update()
    {
        HandleDrawingInput();
    }

    private void CreateLineRenderer()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.useWorldSpace = false;
    }

    private void HandleDrawingInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentDrawing.Clear();
            lineRenderer.positionCount = 0;
        }
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitDistance;

            if (drawingSurface.Raycast(ray, out hitDistance))
            {
                Vector3 hitPoint = ray.GetPoint(hitDistance);
                DrawOnPlane(hitPoint);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            
            Gesture test = new Gesture();
            test.GestureName = GesturName;
            test.points = currentDrawing;
            ges.gestures.Add(test);
            Saving.save.saveToJson(ges, "gesture");
        }
    }
    

    private void DrawOnPlane(Vector3 point)
    {
        if (currentDrawing.Count == 0 || Vector3.Distance(currentDrawing[currentDrawing.Count - 1], point) > drawDistance)
        {
            currentDrawing.Add(drawingPlane.transform.InverseTransformPoint(point));
            lineRenderer.positionCount = currentDrawing.Count;
            lineRenderer.SetPosition(currentDrawing.Count - 1, currentDrawing[currentDrawing.Count - 1]);
        }
    }


    public float CalculatePathLength(List<Vector2> points)
    {
        float distance = 0;

        for (int i = 1; i < points.Count; i++)
        {
            distance += Vector2.Distance(points[i - 1], points[i]);
        }

        return distance;
    }

}
[System.Serializable]
public class gestureList
{
    public List<Gesture> gestures = new List<Gesture>();
}
[System.Serializable]
public class Gesture
{
    public string GestureName;
    public List<Vector3> points = new List<Vector3>();


}
