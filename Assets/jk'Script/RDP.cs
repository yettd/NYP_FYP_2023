using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class RDP : drawing
{
    public float initialEpsilon = 0.5f; // Initial epsilon value for RDP simplification.
    public float minEpsilon = 0.01f;     // Minimum allowed epsilon value.
    public float maxEpsilon = 1.0f;
    protected override void Stuff()
    {
        if (Input.GetMouseButtonUp(0))
            if (currentDrawing.Count > 0)
            {
                //List<Vector3> test= new List<Vector3>();
                //test = ProcessGestureWithAdaptiveEpsilon(currentDrawing);

                
                //RecognizeGesture(test);
            }
    }

    private List<Vector3> SimplifyWithRDP(List<Vector3> gesture, float epsilon)
    {
        // If the gesture is too short or epsilon is too small, return the original gesture.
        if (gesture.Count <= 2 || epsilon <= 0.0f)
        {
            return new List<Vector3>(gesture);
        }

        // Find the point with the maximum distance from the line formed by the first and last point.
        float maxDistance = 0.0f;
        int indexFarthest = 0;

        for (int i = 1; i < gesture.Count - 1; i++)
        {
            float distance = PointToLineDistance(gesture[i], gesture[0], gesture[gesture.Count - 1]);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                indexFarthest = i;
            }
        }

        // If the maximum distance is greater than epsilon, recursively simplify the sub-gestures.
        if (maxDistance > epsilon)
        {
            List<Vector3> subGesture1 = SimplifyWithRDP(gesture.GetRange(0, indexFarthest + 1), epsilon);
            List<Vector3> subGesture2 = SimplifyWithRDP(gesture.GetRange(indexFarthest, gesture.Count - indexFarthest), epsilon);

            // Combine the simplified sub-gestures.
            List<Vector3> simplifiedGesture = new List<Vector3>();
            simplifiedGesture.AddRange(subGesture1);
            // Skip the duplicate point shared between subGesture1 and subGesture2.
            simplifiedGesture.AddRange(subGesture2.GetRange(1, subGesture2.Count - 1));
            return simplifiedGesture;
        }
        else
        {
            // Return the line segment between the first and last point.
            return new List<Vector3> { gesture[0], gesture[gesture.Count - 1] };
        }
    }

    // Calculate the accuracy rating based on a comparison between a template and a recognized gesture.
    private float CalculateAccuracy( List<Vector3> recognizedGesture)
    {

        float totalDistance = 0.0f;
        float maxDistance = 0.0f;
        foreach (Gesture gesture in ges.gestures)
        {
            int shorterLength = Mathf.Min(gesture.points.Count, recognizedGesture.Count);

            for (int i = 0; i < shorterLength; i++)
            {
                float distance = Vector3.Distance(gesture.points[i], recognizedGesture[i]);
                totalDistance += distance;
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                }
            }
        }

        // Calculate an accuracy rating based on the total distance and maximum distance.
        // This is a simple example; you can define your own accuracy rating metric.
        float accuracyRating = 1.0f - (maxDistance / totalDistance);

        return accuracyRating;
    }
    private float AdaptEpsilon(float currentEpsilon, float overallAccuracy)
    {
        // Define the bounds for adjusting epsilon.
        float minChangeRate = 0.1f; // Minimum rate of change.
        float maxChangeRate = 0.5f; // Maximum rate of change.
        float minEpsilonChange = 0.01f; // Minimum absolute change in epsilon.

        // Calculate the change rate based on overall accuracy.
        float changeRate = Mathf.Lerp(minChangeRate, maxChangeRate, 1.0f - overallAccuracy);

        // Calculate the change in epsilon based on the rate.
        float epsilonChange = currentEpsilon * changeRate;

        // Apply a minimum change threshold to prevent epsilon from becoming too small.
        epsilonChange = Mathf.Max(epsilonChange, minEpsilonChange);

        // Update epsilon by adding or subtracting the change.
        float newEpsilon = currentEpsilon + epsilonChange;

        // Ensure that the new epsilon value stays within the specified minEpsilon and maxEpsilon range.
        return Mathf.Clamp(newEpsilon, minEpsilon, maxEpsilon);
    }
    private float PointToLineDistance(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
    {
        Vector3 line = lineEnd - lineStart;
        float lineLength = line.magnitude;
        if (lineLength == 0.0f)
        {
            return Vector3.Distance(point, lineStart);
        }

        // Use the cross product to calculate the area of the triangle formed by the point and the line segment.
        float area = Vector3.Cross(line, point - lineStart).magnitude;

        // Divide the area by the length of the line segment to get the distance.
        return area / lineLength;
    }

    public List<Vector3> ProcessGestureWithAdaptiveEpsilon(List<Vector3> userGesture)
    {
        // Calculate the initial epsilon value based on the initialEpsilon or other factors.
        float currentEpsilon = initialEpsilon;

        // Apply the RDP simplification to the user gesture with the current epsilon.
        List<Vector3> simplifiedGesture = SimplifyWithRDP(userGesture, currentEpsilon);

        // Center the simplified gesture to a reference point.
        List<Vector3> centeredGesture = CenterGesture(simplifiedGesture);

        // Calculate the accuracy rating based on the comparison between the user gesture and the template.
        float accuracyRating = CalculateAccuracy(centeredGesture);

        // Adjust the epsilon value based on the accuracy rating.
        currentEpsilon = AdaptEpsilon(currentEpsilon, accuracyRating);

        // Reapply RDP simplification with the adapted epsilon.
        simplifiedGesture = SimplifyWithRDP(userGesture, currentEpsilon);

        // Recenter the simplified gesture.
        centeredGesture = CenterGesture(simplifiedGesture);

        return centeredGesture;
    }
    public string RecognizeGesture(List<Vector3> userGesture)
    {
        string recognizedGesture = "Unknown";
        float bestMatchDistance = float.MaxValue;

        foreach (Gesture template in ges.gestures)
        {
            float distance = CalculateEuclideanDistance3D(userGesture, template.points);


            if (distance < bestMatchDistance)
            {
                bestMatchDistance = distance;
                recognizedGesture = template.GestureName;
            }
        }
            Debug.Log("Recognized Gesture: " + recognizedGesture);
        

        return recognizedGesture;
    }
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
    private List<Vector3> InterpolateSequence(List<Vector3> sequence, int targetLength)
    {
        List<Vector3> interpolatedSequence = new List<Vector3>(targetLength);

        for (int i = 0; i < targetLength; i++)
        {
            float t = (float)i / (targetLength - 1);
            int index = Mathf.FloorToInt(t * (sequence.Count - 1));
            Vector3 pointA = sequence[index];
            Vector3 pointB = sequence[Mathf.Min(index + 1, sequence.Count - 1)];

            // Linear interpolation between adjacent points.
            Vector3 interpolatedPoint = Vector3.Lerp(pointA, pointB, t);
            interpolatedSequence.Add(interpolatedPoint);
        }

        return interpolatedSequence;
    }

    private List<Vector3> CenterGesture(List<Vector3> gesturePoints)
    {
        Vector3 centerOfMass = Vector3.zero;

        foreach (Vector3 point in gesturePoints)
        {
            centerOfMass += point;
        }

        centerOfMass /= gesturePoints.Count;

        List<Vector3> centeredGesture = new List<Vector3>();
        foreach (Vector3 point in gesturePoints)
        {
            centeredGesture.Add(point - centerOfMass);
        }

        return centeredGesture;
    }

}
