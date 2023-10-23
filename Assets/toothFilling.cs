using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class toothFilling : MonoBehaviour
{
    toolsForFilling tfs = toolsForFilling.slowSpeed;
    [SerializeField] Mesh teethWithHold;
    [SerializeField] Material mat;
    public float threshold =0.7f;

    float drillTimer = 5;
    [SerializeField] Material Dirttooth;


    // Start is called before the first frame update
    public void setUpProblem()
    {
        GameObject getStart = Resources.Load<GameObject>("Mat/d");


        teethWithHold = getStart.GetComponent<getMesh>().mesh.mesh;
        mat = getStart.GetComponent<getMesh>().mat.material;
    }

    bool CorrectSide(RaycastHit hit)
    {
        Vector3 hitPointLocal = transform.InverseTransformPoint(hit.point);

        float yAbs = Mathf.Abs(hitPointLocal.y);

        char positionRelativeToEnemy;
        if (yAbs > threshold)
        {
            if (hitPointLocal.y > 0)
            {
                positionRelativeToEnemy = 't'; // Hit on top
            }
            else
            {

                positionRelativeToEnemy = 'b'; // Hit on bottom
                return true;
            }
        }
        return false;



    }
    public void GoToStep(RaycastHit hit)
    {
        if(!CorrectSide(hit))
        {
            return;
        }

        if (correctTool())
        {
            switch (minigameTaskListController.Instance.getCurrentStep())
            {
                case Steps.DRILL:
                    Drill();
                    break;
            }
        }
    }

    void Drill()
    {
        drillTimer -= Time.deltaTime;

        Debug.Log("asdasd");
        if(drillTimer<0)
        {
            if(minigameTaskListController.Instance.TBgums)
            {
                transform.rotation = Quaternion.Euler(180, 0, 0);
            }
            GetComponent<MeshFilter>().mesh = teethWithHold;
            GetComponent<Renderer>().material = mat;
            minigameTaskListController.Instance.gonext();
            nextTools();
        }
    }
    
    bool correctTool()
    {
        if(minigameTaskListController.Instance.GetSelectedtool()==tfs.ToString())
        {
            return true;
        }
        return false;
    }
    void nextTools()
    {
        tfs++;
    }
   

    public enum toolsForFilling
    {
        slowSpeed,
        Spoonexcavator,
        Microbrush,
        plasticinstrument
    }
}
