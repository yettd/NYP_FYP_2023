using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNagivatorScript
{
    private static TutorialNagivatorScript script = null;
    public static TutorialNagivatorScript getScript { get { return script; } }

    private InstructionManual manual;
    public InstructionManual get_manual { get { return manual; } }

    private const string InstructionStepScene = "Tutorial_LandingScene";
    private const string TutorialGameScene = "Tutorial_GameScene";

    public static TutorialNagivatorScript Instance()
    {
        if (script == null) script = new TutorialNagivatorScript();
        return script;
    }

    #region MAIN
    public string GetManualLoaded(string index)
    {
        Debug.Log(index);
        manual = Resources.Load<InstructionManual>("TutorialLevel/" + index);
        manual.Reset();
        return InstructionStepScene;
    }
    #endregion

    #region MISC
    public string GetGameScene()
    {
        return TutorialGameScene;
    }

    public string GetTitleScene()
    {
        return InstructionStepScene;
    }
    #endregion
}
