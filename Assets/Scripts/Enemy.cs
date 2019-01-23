using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    MeshRenderer meshRenderer;
    // Total number of frames when the enemy has seen spotted
    public int spottedFrame = -100;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        if (spottedFrame >= Time.frameCount - 10)
        {
            meshRenderer.enabled = true;
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }
}
