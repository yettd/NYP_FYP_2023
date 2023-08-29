using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogTest : MonoBehaviour
{
    public string Input = "default test text";

    public void DebugLogSomething(string Input)
    {
        Debug.Log(Input);

    }
}
