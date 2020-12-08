using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmPlatformerBloodSplatter : MonoBehaviour
{
    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //ps.
    }

    private void OnParticleCollision(GameObject other)
    {
        
    }
}
