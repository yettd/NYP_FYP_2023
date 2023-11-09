using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class TaskBreakDown
{
    public Steps s;
    public Sprite i;
    public string Des;
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
    DAM,
    DRILL,
    REMOVE,
    WASHBLOW,
    ETCH,
    WASHBLOW2,
    PRIMER,
    BLOW,
    ADHESIVE,
    FILLING,
    CONTOUR,
    CURE,
    POLISH,
    //gic
    DENTINE,
    APPLICATOR,
    SPREAD,

    END_TASKF


}