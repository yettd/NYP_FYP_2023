using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InstructionTemplate
{
    public Texture Icon;
    public string title;
    public string instruction;
    public string requiredTool;
    public bool completed;
}

[System.Serializable]
public class InstructionLog
{
    public bool completed;
}

[CreateAssetMenu(fileName = "InstructionManual", menuName = "Manual")]
public class InstructionManual : ScriptableObject
{
    [Header("Manual Information Output")]
    public string Title;
    public Texture Icon;
    public string description;

    [Header("Tutorial Step info include mini game scene")]
    public InstructionTemplate[] step;

    [Header("Extra")]
    public ItemTag[] tools;
    public InstructionLog cleared;
    public int toolAccessId;

    public void Reset()
    {
        foreach (InstructionTemplate s in step)
            s.completed = false;

        cleared.completed = false;
    }
}
