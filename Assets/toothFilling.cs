using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using DG.Tweening;
using Unity.Mathematics;

public class toothFilling : MonoBehaviour
{
    toolsForFilling[] tfs = { toolsForFilling.rubberDamForceb, toolsForFilling.slowSpeed,toolsForFilling.Spoonexcavator, 
        toolsForFilling.tripleSyringe, toolsForFilling.etchant,
        toolsForFilling.tripleSyringe, toolsForFilling.Microbrush,toolsForFilling.tripleSyringe };
    int currecntTool = 0;
    [SerializeField] Mesh teethWithHold;
    [SerializeField] Material mat;
    public float threshold =0.7f;
    GameObject decay;
    float drillTimer = 2;
    [SerializeField] Material Dirttooth;
    bool done;
    GameObject dam;
    bool flip = false;

    Color water= Vector4.zero;

    tripleSyringe TS;

    // Start is called before the first frame update
    public void setUpProblem()
    {
        if (!minigameTaskListController.Instance.TBgums)
        {
            flip = true;
        }
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
                if(flip)
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
        nextTools(true);
    }
    void PRIMER()
    {
      
    }
    void BLOW()
    {

    }
    void FILLING()
    {
        decay.transform.localScale += Vector3.one * Time.deltaTime * 0.1f;
        if (decay.transform.localScale.x > 0)
        {
            nextTools(true);
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
        dam = Instantiate(Resources.Load<GameObject>("mat/filling/rubberDamForceb"));
        dam.transform.parent=gameObject.transform;
        dam.transform.localScale = Vector3.one/2;
        dam.transform.localPosition = Vector3.zero + new Vector3(0, 0.75f, 0); 
        if (transform.localPosition.z < 0)
        {
            dam.transform.rotation = quaternion.Euler(0, 180, 0);
        }


        nextTools(true);
    }

    void washPond()//washAndBlow
    {
        if(TS==null)
        {
            TS=FindObjectOfType<tripleSyringe>();
        }
        water = decay.GetComponent<Renderer>().material.color;
        if (water.a <= 0)
        {
            water.a = 0.3f;
            decay.GetComponent<Renderer>().material.color = water;
            decay.transform.localScale = Vector3.zero;
            nextTools(true);
            return;
        }

        if (decay.transform.localScale.x > 0.5)
        {
            if (!TS.WaterBlow)
            {
        
                water = new Color(water.r, water.b, water.g, water.a - (Time.deltaTime * 0.1f));
                decay.GetComponent<Renderer>().material.color = water;
            
           
            }
            return;
        }

        if(TS.WaterBlow)
        {

        decay.transform.localScale += Vector3.one * Time.deltaTime * 0.1f;
        }
    
    }

    void Drill()
    {
        drillTimer -= Time.deltaTime;
        if(drillTimer<0)
        {
            dam.transform.parent = null;
            if (minigameTaskListController.Instance.TBgums)
            {
                flip=true;
                transform.localRotation = Quaternion.Euler(0, 103.362f, 180);
                gameObject.GetComponent<MeshCollider>().sharedMesh = teethWithHold;
            }
            gameObject.GetComponent<MeshCollider>().convex = true;
            decay = Instantiate(Resources.Load<GameObject>("mat/filling/decay"),gameObject.transform);

            decay.transform.localPosition = new Vector3(0,0.11f,0);
            dam.transform.parent = gameObject.transform;
            GetComponent<MeshFilter>().mesh = teethWithHold;
            GetComponent<Renderer>().material = mat;
            nextTools(true);
        }
    }

    void DoneWIthFilliing()
    {
        done = true;
    }

    void Clean()
    {
        decay.transform.localScale -= Vector3.one * Time.deltaTime*0.1f;
        if(decay.transform.localScale.x <0)
        {
            decay.GetComponent<Renderer>().material = Resources.Load<Material>("mat/water");
            nextTools(true);
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
    void nextTools(bool taskDone)
    {
        currecntTool++;
        if(taskDone )
        {
        minigameTaskListController.Instance.gonext();

        }
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
