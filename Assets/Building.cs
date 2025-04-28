using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public Resources ResourcesScript;
    public int Cost, resourceReq;
    public Button ThisButton;

    void Update()
    {
        if (ResourcesScript.resource[resourceReq] >= Cost)
            ThisButton.interactable = true;
        else ThisButton.interactable = false;
    }

    public void Bouhgt()
    {
        ResourcesScript.SpendResources(resourceReq, Cost);
        Destroy(gameObject);
    }
}
