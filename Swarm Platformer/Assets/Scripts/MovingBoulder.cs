using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBoulder : MonoBehaviour
{
    /******************************/
    /*   Don't delete this code   */
    /******************************/

    /*private Vector3 posA;
    private Vector3 posB;
    private Vector3 nextPos;

    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform platform;

    [SerializeField]
    private Transform transformB;

    // Start is called before the first frame update
    void Start()
    {
        posB = platform.localPosition;
        posB = transform.localPosition;
        nextPos = posB;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        platform.localPosition = Vector3.MoveTowards(platform.localPosition, nextPos, speed * Time.deltaTime);
        if (Vector3.Distance(platform.localPosition,nextPos) <= 0.1)
        {
            ChangeDestination();
        }
    }
    private void ChangeDestination()
    {
        nextPos = nextPos != posA ? posA : posB;
    }*/




    //will finish moving platform tomorrow
}
