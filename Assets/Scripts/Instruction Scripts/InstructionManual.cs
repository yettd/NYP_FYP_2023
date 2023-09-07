using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InstructionTemplate
{
    public Texture Icon;
    public string data;
    public ItemTag[] tools;
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
    public string description;

    [Header("Tutorial Step info include mini game scene")]
    public InstructionTemplate[] step;

    [Header("SaveLog")]
    public int saveId;

    [Header("Extra")]
    public InstructionLog cleared;
}
