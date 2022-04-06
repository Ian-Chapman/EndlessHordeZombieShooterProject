using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardText : MonoBehaviour
{

    public Camera activeCam;
    public float sizeOfText = .2f;


    void Update()
    {
        this.transform.rotation = Camera.main.transform.rotation;

        Vector3 textScreenSpace = activeCam.WorldToScreenPoint(transform.position);
        Vector3 adjustedScreenSpace = new Vector3(textScreenSpace.x + sizeOfText, textScreenSpace.y, textScreenSpace.z);
        Vector3 adjustedWorldSpace = activeCam.ScreenToWorldPoint(adjustedScreenSpace);
        transform.localScale = Vector3.one * (transform.position - adjustedWorldSpace).magnitude;
        transform.rotation = activeCam.transform.rotation;
    }

}
