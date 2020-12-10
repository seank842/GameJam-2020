using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : MonoBehaviour
{
    float timer;
    // Update is called once per frame
    private void Start()
    {
        timer = 0.0f;
    }
    void Update()
    {
        if (!GlobalSceneManager.GameOver) // Or Pause or Victory
        {
            transform.Rotate(new Vector3(0f, 0f, 1f));
            timer += Time.deltaTime;
            float new_y_position = transform.position.y + 0.15f * -Mathf.Cos(1.0f * timer);
            transform.position = new Vector3(transform.position.x, new_y_position, transform.position.z);
        }
    }
}
