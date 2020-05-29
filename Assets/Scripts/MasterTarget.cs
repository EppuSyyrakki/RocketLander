using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterTarget : MonoBehaviour
{
    private GameObject[] targetObjects;
    private Target[] targetScripts;
    private Boolean allHit = false;

    // Start is called before the first frame update
    void Start()
    {
        targetObjects = new GameObject[transform.childCount];
        targetScripts = new Target[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            targetObjects[i] = gameObject.transform.GetChild(i).gameObject;
            targetScripts[i] = targetObjects[i].GetComponent<Target>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        int targetsHit = 0;

        foreach (Target target in targetScripts)
        {
            if (target.isHit())
                targetsHit++;
        }       

        if (targetsHit == transform.childCount)
            allHit = true;
    }

    public Boolean isAllHit()
    {
        return allHit;
    }
}
