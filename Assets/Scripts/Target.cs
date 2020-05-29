using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Material green;
    private Boolean hit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.material = green;
            hit = true;
        }
    }

    public Boolean isHit()
    {
        return hit;
    }
}
