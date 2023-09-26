 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompletion
{
    public bool gameScaling;

    public bool gameFilling;

    public bool gameExtraion;

    public void setGC(int a)
    {
        switch(a)
        {
            case 0:
                gameScaling = true;
                break;

            case 1:
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
