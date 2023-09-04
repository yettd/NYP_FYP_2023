using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InstructionTemplate
{
    public Texture Icon;
    public string data;
}

[CreateAssetMenu(fileName = "InstructionManual", menuName = "Manual")]
public class InstructionManual : ScriptableObject
{
    [Header("Manual Information Output")]
    public string Title;
    public string description;

    [Header("Tutorial Step info include mini game scene")]
    public InstructionTemplate[] step;
}
