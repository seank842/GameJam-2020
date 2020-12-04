using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmPlatformerGibletSpawner : MonoBehaviour
{

    public GameObject giblet_arm_1;
    public GameObject giblet_arm_2;
    public GameObject giblet_leg_1;
    public GameObject giblet_leg_2;
    public GameObject giblet_head;
    public GameObject giblet_torso;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            Transform modified_transform = transform;
            modified_transform.position = new Vector3(modified_transform.position.x,
                                                      modified_transform.position.y + 0.1f,
                                                      modified_transform.position.z);
            Instantiate(giblet_arm_1, modified_transform);
            Instantiate(giblet_arm_2, modified_transform);
            Instantiate(giblet_leg_1, modified_transform);
            Instantiate(giblet_leg_2, modified_transform);
            Instantiate(giblet_head, modified_transform);
            Instantiate(giblet_torso, modified_transform);
            Destroy(gameObject, 0.1f);
        }
    }
}
