using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : MonoBehaviour
{
    float timer;
    public float pos;
    // Update is called once per frame
    private void Start()
    {
        timer = 0.0f;
    }
    void Update()
    {
        if (Time.timeScale!=0) // Or Pause or Victory
        {
            transform.Rotate(new Vector3(0f, 0f, 1f));
            timer += Time.deltaTime;
            float new_y_position = transform.position.y + pos * -Mathf.Cos(1.0f * timer);
            transform.position = new Vector3(transform.position.x, new_y_position, transform.position.z);
        }
    }
}
