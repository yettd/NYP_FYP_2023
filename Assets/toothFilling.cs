using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class toothFilling : MonoBehaviour
{
    toolsForFilling[] tfs = { toolsForFilling.slowSpeed,toolsForFilling.Spoonexcavator };
    int currecntTool = 0;
    [SerializeField] Mesh teethWithHold;
    [SerializeField] Material mat;
    public float threshold =0.7f;
    GameObject decay;
    float drillTimer = 2;
    [SerializeField] Material Dirttooth;
    bool done;

    // Start is called before the first frame update
    public void setUpProblem()
    {
        GameObject getStart = Resources.Load<GameObject>("Mat/filling/d");

        Debug.Log(getStart.GetComponent<getMesh>().getM());
        teethWithHold = getStart.GetComponent<getMesh>().getM();
        mat = getStart.GetComponent<getMesh>().getR();
    }

    bool CorrectSide(RaycastHit hit)
    {
        Vector3 hitPointLocal = transform.InverseTransformPoint(hit.point);

        float yAbs = Mathf.Abs(hitPointLocal.y);

        char positionRelativeToEnemy='d';
        if (yAbs > threshold)
        {
            if (hitPointLocal.y > 0)
            {
                positionRelativeToEnemy = 't'; // Hit on top
                if(transform.rotation.eulerAngles.x !=0)
                {
                    return true;
                }
            }
            else
            {

                positionRelativeToEnemy = 'b'; // Hit on bottom
                return true;
            }
        }
        Debug.Log(positionRelativeToEnemy);
        return false;



    }
    public void GoToStep(RaycastHit hit)
    {
        if (done)
        {
            return;
        }
       
        if(!CorrectSide(hit))
        {
            
            return;
        }

        if (correctTool())
        {
            Debug.Log(tfs);
            switch (minigameTaskListController.Instance.getCurrentStep())
            {
                case Steps.DRILL:
                    Drill();
                    break;
                case Steps.REMOVE:
                    Clean();
                    break;
            }
        }
    }

    void Drill()
    {
        drillTimer -= Time.deltaTime;
        if(drillTimer<0)
        {
            if(minigameTaskListController.Instance.TBgums)
            {
                transform.rotation = Quaternion.Euler(180, 0, 0);
                gameObject.GetComponent<MeshCollider>().sharedMesh = teethWithHold;
            }
            gameObject.GetComponent<MeshCollider>().convex = true;
            decay = Instantiate(Resources.Load<GameObject>("mat/filling/decay"),gameObject.transform);

            decay.transform.transform.localScale = Vector3.one;
            decay.transform.localPosition = new Vector3(0,0.11f,0);

            GetComponent<MeshFilter>().mesh = teethWithHold;
            GetComponent<Renderer>().material = mat;
            minigameTaskListController.Instance.gonext();
            nextTools();
        }
    }

    void DoneWIthFilliing()
    {
        done = true;
    }

    void Clean()
    {
        decay.transform.localScale -= Vector3.one * Time.deltaTime*0.5f;
        Debug.Log("ddd");
        if(decay.transform.localScale.x <0)
        {
            minigameTaskListController.Instance.gonext();
            nextTools();
        }
    }
    
    bool correctTool()
    {
        if (minigameTaskListController.Instance.GetSelectedtool() == tfs[currecntTool].ToString())
        {
            return true;
        }
        return false;
    }
    void nextTools()
    {
        currecntTool++;
        if(currecntTool >= tfs.Length)
        {
            currecntTool = 0;
        }
    }
   

    public enum toolsForFilling
    {
        slowSpeed,
        Spoonexcavator,
        Microbrush,
        plasticinstrument
    }
}
