using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloor : MonoBehaviour
{
    private Rigidbody platform;
    //private GameObject platformObject;

    public float fallDelay;
    public Material stillMaterial;
    public Material fallingMaterial;
    private MeshRenderer meshR;

    void Start()
    {
        platform = GetComponent<Rigidbody>();
        meshR = GetComponent<MeshRenderer>();
        //platformObject = GetComponent <GameObject>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            
            StartCoroutine(Fall());
            
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        platform.isKinematic = false;
        platform.AddForce(0, -10000, 0);

        meshR.material = fallingMaterial;
        Destroy(gameObject, 2.5f);
        yield return 0;
    }

    
}
