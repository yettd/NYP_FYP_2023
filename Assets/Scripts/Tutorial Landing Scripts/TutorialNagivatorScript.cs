using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNagivatorScript : MonoBehaviour
{
    public static TutorialNagivatorScript thisScript;

    private InstructionManual manual;
    public InstructionManual get_manual { get { return manual; } }

    private const string InstructionStepScene = "Tutorial_LandingScene";
    private const string TutorialGameScene = "Tutorial_GameScene";

    void Start()
    {
        thisScript = this;   
    }

    #region MAIN
    public string GetManualLoaded(int index)
    {
        manual = Resources.Load<InstructionManual>("TutorialLevel/Stage " + index);
        manual.Reset();
        return InstructionStepScene;
    }

    public string GetGameScene()
    {
        return TutorialGameScene;
    }
    #endregion
}
