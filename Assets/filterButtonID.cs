using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class filterButtonID : MonoBehaviour
{
    public ButtonENUM buttonID;
    public CollectionEnum ColButtonID;
    public DTHEnum DTHButtonID;

    public void AssignBackButtonID()
    {
        ButtonReferenceManager.Instance.storedButtonID = buttonID;
    }

    public void AssignDTHButtonID()
    {
        ButtonReferenceManager.Instance.storeCollectionID = ColButtonID;
        ButtonReferenceManager.Instance.storedDTHButtonID = DTHButtonID;
        Debug.Log(DTHButtonID);
        collectionManager.CM.displayShelfItem();
        //Debug.Log("stored dthButtonID with" + dthButtonID);

    }
}
