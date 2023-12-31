using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using DG.Tweening;
using Unity.Mathematics;
using static Unity.Burst.Intrinsics.X86.Avx;

public class toothFilling : MonoBehaviour
{

    //if want to add more tool for the procedure just add to the array it will go in order 
    toolsForFilling[] tfs = { toolsForFilling.rubberDamForceb, toolsForFilling.slowSpeed,toolsForFilling.Spoonexcavator, 
        toolsForFilling.tripleSyringe, toolsForFilling.etchant,
        toolsForFilling.tripleSyringe, toolsForFilling.Microbrush,toolsForFilling.tripleSyringe,toolsForFilling.Microbrush,
        toolsForFilling.tripleSyringe,toolsForFilling.lightCure,toolsForFilling.plasticinstrument,
        toolsForFilling.plasticinstrument,toolsForFilling.lightCure,toolsForFilling.ballBurnisher };
    int currecntTool = 0;
    [SerializeField] Mesh teethWithHold;
    [SerializeField] Material mat;

    public float threshold =0.7f;

    GameObject decay;
    Renderer decayRender;
    GameObject acid;
    Renderer acidRender;
    float drillTimer = 2;
    Mesh OriginalMesh;

    [SerializeField] Material Dirttooth;
    bool done;
    GameObject dam;
    bool flip = false;

    Color water= Vector4.zero;

    tripleSyringe TS;
    Light lightFakePrimer;

    GameObject FC;
    Renderer FR;
    // Start is called before the first frame update
    public void setUpProblem()
    {
        if (!minigameTaskListController.Instance.TBgums)
        {
            flip = true;
        }
        //set up problem for filling 
        GameObject b=Instantiate(Resources.Load<GameObject>("mat/filling/FYP_GunkNEW"), gameObject.transform);
        b.transform.localPosition = new Vector3(0, 0.344f, 0);
        OriginalMesh = GetComponent<MeshFilter>().mesh;
        GameObject cap = Resources.Load<GameObject>("Mat/filling/cap");
        GameObject getStart = Resources.Load<GameObject>("Mat/filling/d");
        Debug.Log(getStart.GetComponent<getMesh>().getM());
        teethWithHold = getStart.GetComponent<getMesh>().getM();
        mat = getStart.GetComponent<getMesh>().getR();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            nextTools(true);
        }
    }

    bool CorrectSide(RaycastHit hit)
    {
        //use to detect which side is correct
        Vector3 hitPointLocal = transform.InverseTransformPoint(hit.point);

        float yAbs = Mathf.Abs(hitPointLocal.y);

        char positionRelativeToTeeth='d';
        if (yAbs > threshold)
        {
            if (hitPointLocal.y > 0)
            {
                positionRelativeToTeeth = 't'; // Hit on top
                if(flip)
                {
                    //if the colider is flip the positionRelativeToTeeth will also be change upsidedown
                    return true;
                }
            }
            else
            {

                positionRelativeToTeeth = 'b'; // Hit on bottom
                return true;
            }
        }
        Debug.Log(positionRelativeToTeeth);
        return false;



    }

    public void NextStepForce()
    {
        if(minigameTaskListController.Instance.getCurrentStep() == Steps.CONTOUR)
        nextTools(true);
        GetComponent<MeshCollider>().enabled = true;   
    }

    public void GoToStep(RaycastHit hit)
    {
      

        if (done)
        {
            return;
        }

        if (!CorrectSide(hit))
        {
            Debug.Log(tfs[currecntTool]);
            return;
        }
        if (correctTool())
        {
            
            //Once the side is correct and tool are correct it will go to the corrosponding method and run it
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
                    washPond();
                    break;
                case Steps.ETCH:
                    ETCH();
                    break;
                case Steps.PRIMER:
                case Steps.BLOW:
                    PRIMER();
                    break;
                case Steps.ADHESIVE:
                    ADHESIVE();
                    break;

                case Steps.FILLING:
                    FILLING();
                    break;
                //case Steps.CONTOUR:
                //    CONTOUR(hit);
                //    break;

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
        if(acid == null)
        {
            acid = Instantiate(Resources.Load<GameObject>("mat/filling/acid"), gameObject.transform);
            acid.transform.localPosition = new Vector3(0, 0.11f, 0);
            acidRender = acid.GetComponent<Renderer>();
        }

        acid.transform.localScale += Vector3.one * Time.deltaTime * 0.1f;
        if (acid.transform.localScale.x > 0.5)
        {
            nextTools(true);
            return;
        }
      
    }
    private void ADHESIVE()
    {
        if (tfs[currecntTool]==toolsForFilling.Microbrush)
        {
            decay.transform.localScale += Vector3.one * Time.deltaTime * 0.1f;
            if(decay.transform.localScale.x >1.1)
            {
                nextTools(false);
            }
            return;
        }
        if (tfs[currecntTool] == toolsForFilling.tripleSyringe)
        {
            if (TS == null)
            {
                TS = FindObjectOfType<tripleSyringe>();
            }
            if(!TS.WaterBlow)
            {
                if(decayRender.material.color.a>=0.98)
                {
                    nextTools(false);
                }

                decayRender.material.color = new Color(decayRender.material.color.r, decayRender.material.color.g, decayRender.material.color.b, decayRender.material.color.a +Time.deltaTime);
            }
            return;
        }
        if (tfs[currecntTool] == toolsForFilling.lightCure)
        {
            if (decayRender.material.color.r >= 0.98)
            {
                nextTools(true);
            }
            decayRender.material.color = new Color(decayRender.material.color.r+Time.deltaTime, decayRender.material.color.g + Time.deltaTime, decayRender.material.color.b + Time.deltaTime, decayRender.material.color.a);
            
            return;
        }
    }

    void PRIMER()
    {
        if(lightFakePrimer==null)
        {
            GameObject l = Instantiate(new GameObject(),transform) as GameObject;
            l.transform.localPosition = l.transform.localPosition + new Vector3 (0, 0.61f, 0);
            l.AddComponent<Light>();
            lightFakePrimer = l.GetComponent<Light>();
            lightFakePrimer.intensity = 0;

        }


        if (lightFakePrimer.intensity>0.005 && tfs[currecntTool]==toolsForFilling.Microbrush)
        {
            nextTools(true);
            return;
        }
        else if (tfs[currecntTool] == toolsForFilling.tripleSyringe )
        {
            if (TS == null)
            {
                TS = FindObjectOfType<tripleSyringe>();
            }
            if (!TS.WaterBlow)
            {
                if (lightFakePrimer.intensity > 0.01)
                {
                    nextTools(true);
                    return;
                }
            }
        }
        lightFakePrimer.intensity += 0.001f * Time.deltaTime;

    }
    void BLOW()
    {
        if (TS == null)
        {
            TS = FindObjectOfType<tripleSyringe>();
        }
        if (!TS.WaterBlow)
        {
            if (lightFakePrimer.intensity > 0.02)
            {
                nextTools(true);
            }
        }
    }
    void FILLING()
    {
        decay.transform.localScale += Vector3.one * Time.deltaTime*0.5f;
        if(decay.transform.localScale.x > 1.3)
        {
            decay.transform.localScale = Vector3.zero;
            FC= Instantiate(Resources.Load<GameObject>("mat/filling/cap"),transform) as GameObject;

            FC.GetComponentInChildren<fillingCure>().SetTF(this);
            FR=FC.transform.GetChild(0).gameObject.GetComponent<Renderer>();
            FC.transform.localPosition = Vector3.zero;
            GetComponent<MeshCollider>().enabled = false;
            nextTools(true);
        }
    }
    void CURE()
    {

        if(FR.material.color.r>=1)
        {
            nextTools(true);
        }
        FR.material.color=new Color(FR.material.color.r+Time.deltaTime, FR.material.color.r + Time.deltaTime, FR.material.color.r + Time.deltaTime, 1.0f);
      
    }
    void POLISH()
    {

        if(FC.transform.localPosition.y > -0.5)
        {

            GetComponent<MeshFilter>().mesh = OriginalMesh;
            Destroy(FC);

            dam = Instantiate(Resources.Load<GameObject>("mat/filling/rubberDamForceb"));
            nextTools(true);
        }
        FC.transform.localPosition -= Vector3.up * Time.deltaTime * 0.5f;
    }
    void PutDam()
    {

        AudioManager.Instance.PlaySFX(8);
        dam = Instantiate(Resources.Load<GameObject>("mat/filling/rubberDamForceb"));
        dam.transform.parent=gameObject.transform;
        dam.transform.localScale = Vector3.one/2;
        dam.transform.localPosition = Vector3.zero + new Vector3(0, 0.75f, 0); 
        dam.transform.rotation = quaternion.Euler(0,- 90, 0);
     

        nextTools(true);
    }

    void washPond()//washAndBlow
    {
        if(TS==null)
        {
            TS=FindObjectOfType<tripleSyringe>();
        }

        //if(TS.WaterBlow)
        //{
        //    AudioManager.Instance.PlaySFX(11);
        //}
        //else
        //{
        //    AudioManager.Instance.PlaySFX(10);
        //}

        water = decayRender.material.color;
        if (water.a <= 0)
        {
            AudioManager.Instance.PlaySFX(10);
            water.a = 0.3f;
            decayRender.material.color = water;
            decay.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
            nextTools(true);
            if(acid)
            {
                decay.transform.GetChild(0).GetComponent<Renderer>().material = Resources.Load<Material>("mat/glue");
            }
            Destroy(acid);
            return;
        }

        if (decay.transform.localScale.x > 1.1)
        {
            if (!TS.WaterBlow)
            {
                AudioManager.Instance.PlaySFX(11);
                if (acid && acid.transform.localScale.x>0.1)
                {
                    acid.transform.localScale -= Vector3.one * Time.deltaTime*0.01f;
                }
                water = new Color(water.r, water.b, water.g, water.a - (Time.deltaTime * 0.1f));
                decayRender.material.color = water;
            }
            return;
        }

        if(TS.WaterBlow)
        {
            if(acid)
            {
                //acidRender.material.color =new Color(acidRender.material.color.r, acidRender.material.color.g, acidRender.material.color.b, acidRender.material.color.a - Time.deltaTime);
            }
            AudioManager.Instance.PlaySFX(11);
            decay.transform.localScale += Vector3.one * Time.deltaTime * 0.1f;
        }
    
    }

    void Drill()
    {
        drillTimer -= Time.deltaTime;
        AudioManager.Instance.PlaySFX(9);
        if(drillTimer<0)
        {
            Destroy(transform.GetChild(0).gameObject);

            dam.transform.parent = null;
            if (minigameTaskListController.Instance.TBgums)
            {
                Debug.Log("FLIP");
                flip=true;
                transform.localRotation = Quaternion.Euler(0, 103.362f, 180);
                gameObject.GetComponent<MeshCollider>().sharedMesh = teethWithHold;
            }
            gameObject.GetComponent<MeshCollider>().convex = true;
            decay = Instantiate(Resources.Load<GameObject>("mat/filling/FYP_GunkNEW"),gameObject.transform);

            decayRender=decay.transform.GetChild(0).gameObject.GetComponent<Renderer>();
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
        //AudioManager.Instance.PlaySFX(6);
        decay.transform.localScale -= Vector3.one * Time.deltaTime*0.1f;
        if(decay.transform.localScale.x <0.2)
        {
            decay.transform.GetChild(0).GetComponent<Renderer>().material = Resources.Load<Material>("mat/water");
            nextTools(true);
        }
    }
    
    bool correctTool()
    {
        //check if the selected tool is arccording to what is needed currently
        if (minigameTaskListController.Instance.GetSelectedtool() == tfs[currecntTool].ToString())
        {

            Debug.Log("asdasdasd :" + minigameTaskListController.Instance.GetSelectedtool());
            return true;
        }
        if (minigameTaskListController.Instance.GetSelectedtool() == tfs[currecntTool - 1].ToString())
        {
            return false;
        }
        minigameTaskListController.Instance.wrongTool();

        minigameTaskListController.Instance.wrongTool();
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
        applicator,
        etchant,
        Microbrush,
        lightCure,
        ballBurnisher,
        plasticinstrument
    }
}
