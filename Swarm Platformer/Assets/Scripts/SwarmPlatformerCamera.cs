using UnityEngine;

public class SwarmPlatformerCamera : MonoBehaviour
{
    public GameObject current_player;

    internal static int index;
    internal float camera_changing_constant = 1.0f;
    internal bool camera_transitioning;

    // Total distance between the markers.
    internal float journey_length;
    internal float journey_step;
    internal float distance_to_target;
    void Start()
    {
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (camera_transitioning)
        {
            float transition_position = transform.position.x + journey_step;
            transform.position = new Vector3(transition_position, transform.position.y, transform.position.z);
            if (Mathf.Abs(transform.position.x - current_player.transform.position.x) > Mathf.Abs(distance_to_target))
            {
                camera_transitioning = false;
                print("gone too far");
            }
            else
            {
                distance_to_target = Mathf.Abs(transform.position.x - current_player.transform.position.x);
                //print("currenly transitioning");
            }
        }
        else if (current_player != null)
        {
            transform.position = new Vector3(current_player.transform.position.x, transform.position.y, transform.position.z);
        }
    }


}
