using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwarmPlatformerCamera : MonoBehaviour
{
    public GameObject current_player;
    public GlobalSceneManager scene_manager;

    public static int index;
    float camera_changing_constant = 1.0f;
    bool camera_transitioning;

    // Total distance between the markers.
    private float journey_length;
    private float journey_step;
    private float distance_to_target;
    void Start()
    {
        scene_manager.PlayerChangedEvent += Scene_manager_PlayerChangedEvent;
        //Destroy(GlobalSceneManager.Players[index], 5.0f); //testing purposes only
    }

    private void Scene_manager_PlayerChangedEvent(object sender, GameObject e)
    {
        if(e == default)
        {
            int index = Random.Range(0, scene_manager.Players.Count());
            current_player = scene_manager.Players[index];
            UpdateCamera();
        }
        else if (current_player.GetInstanceID().Equals(e.GetInstanceID()))
        {
            var tempList = scene_manager.Players.Where(p => !p.GetInstanceID().Equals(e.GetInstanceID())).ToList();
            int index = Random.Range(0, tempList.Count());
            current_player = tempList[index];
            UpdateCamera();
        }
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
        else
        {
            UpdateCamera();
            print("updating camera position");
        }
    }

    private void UpdateCamera()
    {
        camera_transitioning = true;
        journey_length = current_player.transform.position.x - transform.position.x;
        float frames_transition = camera_changing_constant * 50.0f;
        journey_step = journey_length / frames_transition;
        distance_to_target = Mathf.Abs(transform.position.x - current_player.transform.position.x);
        //Destroy(GlobalSceneManager.Players[index], 5.0f); //testing purposes only
    }

    private void OnDestroy()
    {
        scene_manager.PlayerChangedEvent -= Scene_manager_PlayerChangedEvent;
    }
}
