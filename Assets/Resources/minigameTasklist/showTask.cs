using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class TaskBreakDown
{
    public Steps s;
    public Sprite i;
}

[CreateAssetMenu(fileName = "ShowTask", menuName = "showTask")]
public class showTask : ScriptableObject
{
    public TaskBreakDown[] TBD;
}
public enum Steps
{
    //scaling
    SCRAPINGS,

    //filling
    DRILL,
    REMOVE,
    WASHBLOW,
    ETCH,
    WASHBLOW2,
    PRIMER,
    BLOW,
    FILLING,
    CONTOUR,
    CURE,
    POLISH,

    END_TASKF


}