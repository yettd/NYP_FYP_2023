using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Quiz", menuName = "Quiz")]
public class quiz : ScriptableObject
{
    public List<QnA> QnA;
}
