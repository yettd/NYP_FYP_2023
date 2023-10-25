using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class toothFilling : MonoBehaviour
{
    toolsForFilling[] tfs = { toolsForFilling.rubberDamForceb, toolsForFilling.slowSpeed,toolsForFilling.Spoonexcavator, 
        toolsForFilling.tripleSyringe, toolsForFilling.etchant };
    int currecntTool = 0;
    [SerializeField] Mesh teethWithHold;
    [SerializeField] Material mat;
    public float threshold =0.7f;
    GameObject decay;
    float drillTimer = 2;
    [SerializeField] Material Dirttooth;
    bool done;
    GameObject dam;

    Color water;

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
        Debug.Log(tfs[currecntTool]);
        if (correctTool())
        {
         
            switch (minigameTaskListController.Instance.getCurrentStep())
            {
                case Steps.DAM:
                    PutDam();
                    break;

                case Steps.DRILL:
                    Drill();
                    break;
                case Steps.REMOVE:
                    Clean();
                    break;
                case Steps.WASHBLOW:
                case Steps.WASHBLOW2:
                case Steps.BLOW:
                    washPond();
                    break;
                case Steps.ETCH:
                    ETCH();
                    break;
                case Steps.PRIMER:
                    PRIMER();
                    break;
                case Steps.FILLING:
                    FILLING();
                    break;
                case Steps.CONTOUR:
                    CONTOUR();
                    break;

                case Steps.CURE:
                    CURE(); 
                    break;

                case Steps.POLISH:
                    POLISH();   
                    break;
            }
        }
    }

    void ETCH()
    {

    }
    void PRIMER()
    {

    }
    void BLOW()
    {

    }
    void FILLING()
    {
        decay.transform.localScale += Vector3.one * Time.deltaTime * 0.5f;
        if (decay.transform.localScale.x > 0)
        {
            nextTools();
        }
    }
    void CONTOUR()
    {

    }
    void CURE()
    {

    }
    void POLISH()
    {

    }
    void PutDam()
    {
        dam =Instantiate(Resources.Load<GameObject>("mat/filling/rubberDamForceb"), gameObject.transform);
        dam.transform.localPosition = new Vector3(0.0399999991f, -0.699999988f, -3.6099999f);
        dam.transform.localRotation = Quaternion.Euler(90, 0, 0);
        dam.transform.localScale = Vector3.one/2;
        nextTools();
    }

    void washPond()//washAndBlow
    {

        if (decay.transform.localScale.x > 0.5)
        {
            water= decay.GetComponent<Renderer>().material.color;
            water= new Color(water.r,water.b,water.g,water.a-(Time.deltaTime*0.1f));
           decay.GetComponent<Renderer>().material.color = water;
            if (water.a <= 0)
            {
                nextTools();
            }
            return;
        }

        decay.transform.localScale += Vector3.one * Time.deltaTime * 0.5f;
    
    }

    void Drill()
    {
        drillTimer -= Time.deltaTime;
        if(drillTimer<0)
        {
            dam.transform.parent = null;
            if (minigameTaskListController.Instance.TBgums)
            {
                transform.localRotation = Quaternion.Euler(0, 103.362f, 180);
                gameObject.GetComponent<MeshCollider>().sharedMesh = teethWithHold;
            }
            gameObject.GetComponent<MeshCollider>().convex = true;
            decay = Instantiate(Resources.Load<GameObject>("mat/filling/decay"),gameObject.transform);

            decay.transform.localPosition = new Vector3(0,0.11f,0);
            dam.transform.parent = gameObject.transform;
            GetComponent<MeshFilter>().mesh = teethWithHold;
            GetComponent<Renderer>().material = mat;
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
        if(decay.transform.localScale.x <0)
        {
            decay.GetComponent<Renderer>().material = Resources.Load<Material>("mat/water");
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
        minigameTaskListController.Instance.gonext();
        if (currecntTool >= tfs.Length)
        {
            currecntTool = 0;
        }
    }
   

    public enum toolsForFilling
    {
        rubberDamForceb,
        slowSpeed,
        Spoonexcavator,
        tripleSyringe,
        etchant,
        Microbrush,
        plasticinstrument
    }
}
