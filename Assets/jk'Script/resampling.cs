using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class resampling : drawing
{
    public int targetPointCount = 64;
    public List<Vector2> vector2s = new List<Vector2>();
    public TextMeshProUGUI t;
    public float recognitionThreshold = 0.1f;
    protected override void Update()
    {
        base.Update();

   
    }
    protected override void Stuff()
    {
        if (Input.GetMouseButtonUp(0))
            if (currentDrawing.Count > 0)
            {
               List<Vector3> test = Resample3D(currentDrawing);
                test=CenterGesture(test);
                RecognizeGesture(test);
            }
    }


    public List<Vector3> Resample3D(List<Vector3> inputPoints)
    {
        List<Vector3> resampledPoints = new List<Vector3>();

        // Calculate the path length of the input gesture.
        float totalPathLength = CalculatePathLength(inputPoints);

        // Calculate the resampling interval.
        float resamplingInterval = totalPathLength / (targetPointCount - 1);

        float currentDistance = 0;
        resampledPoints.Add(inputPoints[0]);

        // Iterate through the input points and add new points at the resampling intervals.
        for (int i = 1; i < inputPoints.Count; i++)
        {
            Vector3 previousPoint = inputPoints[i - 1];
            Vector3 currentPoint = inputPoints[i];
            float segmentLength = Vector3.Distance(previousPoint, currentPoint);

            if (currentDistance + segmentLength >= resamplingInterval)
            {
                float t = (resamplingInterval - currentDistance) / segmentLength;
                Vector3 interpolatedPoint = Vector3.Lerp(previousPoint, currentPoint, t);
                resampledPoints.Add(interpolatedPoint);
                inputPoints.Insert(i, interpolatedPoint);
                currentDistance = 0;
            }
            else
            {
                currentDistance += segmentLength;
            }
        }

        // Ensure that the last point is included.
        resampledPoints.Add(inputPoints[inputPoints.Count - 1]);

        return resampledPoints;
    }

    // Calculate the path length of a sequence of 3D points.
    private float CalculatePathLength(List<Vector3> points)
    {
        float distance = 0;
        for (int i = 1; i < points.Count; i++)
        {
            distance += Vector3.Distance(points[i - 1], points[i]);
        }
        return distance;
    }

    public string RecognizeGesture(List<Vector3> userGesture)
    {
        string recognizedGesture = "Unknown";
        float minDistance = float.PositiveInfinity;

        foreach (Gesture template in ges.gestures)
        {
            float distance = CalculateEuclideanDistance3D(userGesture, template.points);

    
            if (distance < minDistance)
            {
                minDistance = distance;
                recognizedGesture = template.GestureName;
            }
        }
        Debug.Log($"{minDistance}");
        t.text = "YOU STUPID";
        if (minDistance < recognitionThreshold)
        {
            Debug.Log("Recognized Gesture: " + recognizedGesture);
            t.text = $"YOU have DRAWN A {recognizedGesture}";
        }

        return recognizedGesture;
    }

    // Calculate Euclidean distance for 3D gestures.
    private float CalculateEuclideanDistance3D(List<Vector3> a, List<Vector3> b)
    {
        if (a.Count == 0 || b.Count == 0)
        {
            return float.MaxValue; // Cannot calculate distance with empty sequences.
        }

        // Interpolate one sequence to match the length of the other.
        List<Vector3> interpolatedA;
        List<Vector3> interpolatedB;

        if (a.Count < b.Count)
        {
            interpolatedA = InterpolateSequence(a, b.Count);
            interpolatedB = new List<Vector3>(b);
        }
        else if (a.Count > b.Count)
        {
            interpolatedA = new List<Vector3>(a);
            interpolatedB = InterpolateSequence(b, a.Count);
        }
        else
        {
            interpolatedA = new List<Vector3>(a);
            interpolatedB = new List<Vector3>(b);
        }

        // Calculate the Euclidean distance between the interpolated sequences.
        float sumOfSquares = 0.0f;
        for (int i = 0; i < interpolatedA.Count; i++)
        {
            Vector3 diff = interpolatedA[i] - interpolatedB[i];
            sumOfSquares += Vector3.Dot(diff, diff);
        }

        return Mathf.Sqrt(sumOfSquares);
    }

    // Interpolate a sequence to match a target length.
    private List<Vector3> InterpolateSequence(List<Vector3> sequence, int targetLength)
    {
        List<Vector3> interpolatedSequence = new List<Vector3>(targetLength);

        for (int i = 0; i < targetLength; i++)
        {
            float t = (float)i / (targetLength - 1);
            int index = Mathf.FloorToInt(t * (sequence.Count - 1));
            Vector3 pointA = sequence[index];
            Vector3 pointB = sequence[Math.Min(index + 1, sequence.Count - 1)];

            // Linear interpolation between adjacent points.
            Vector3 interpolatedPoint = Vector3.Lerp(pointA, pointB, t);
            interpolatedSequence.Add(interpolatedPoint);
        }

        return interpolatedSequence;
    }
    public List<Vector3> CenterGesture(List<Vector3> gesturePoints)
    {
        // Calculate the center of mass (centroid) of the gesture.
        Vector3 centerOfMass = CalculateCenterOfMass(gesturePoints);

        // Calculate the translation vector to move the center of mass to the reference point.
        Vector3 translation = Vector3.zero - centerOfMass;

        // Translate all points in the gesture by the translation vector.
        List<Vector3> centeredGesture = new List<Vector3>();
        foreach (Vector3 point in gesturePoints)
        {
            centeredGesture.Add(point + translation);
        }

        return centeredGesture;
    }

    // Calculate the center of mass (centroid) of a gesture.
    private Vector3 CalculateCenterOfMass(List<Vector3> gesturePoints)
    {
        Vector3 centerOfMass = Vector3.zero;

        foreach (Vector3 point in gesturePoints)
        {
            centerOfMass += point;
        }

        centerOfMass /= gesturePoints.Count;

        return centerOfMass;
    }



}
