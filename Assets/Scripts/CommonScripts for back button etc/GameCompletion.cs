 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompletion
{
    public bool gameScaling=true;

    public bool gameFilling = true;

    public bool gameExtraion = true;

    public int scalingRating = 0;
    public int FillingRating = 0;
    public int ExtrationRating = 0;

    public float scalingInSecond = -1;
    public float FillingInSecond = -1;
    public float ExtrationInSecond = -1;

    public string scalingTime = "not done yet";
    public string FillingTime = "not done yet";
    public string ExtrationTime = "not done yet";

    public void setGC(int a)
    {
        switch(a)
        {
            case 0:
                gameScaling = true;
                break;

            case 1:
                Debug.Log("sdadsad");
                gameFilling = true;
                break;

            case 2:
                gameExtraion = true;
                break;
        }
    }

    public bool GetGC(int a)
    {
        switch (a)
        {
            case 0:
                return gameScaling;

            case 1:
                return gameFilling;

            case 2:
                return gameExtraion;
        }
        return false;
    }
}
