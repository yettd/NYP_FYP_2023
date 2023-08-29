using UnityEngine;

public class DebugOnOff : MonoBehaviour
{
    public GameObject targetGameObject;

    public void Toogle(bool state)
    {
        targetGameObject.SetActive(state);
    }

}
