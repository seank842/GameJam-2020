using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmPlatformerCamera : MonoBehaviour
{
    public GameObject current_player;
    public GameObject[] players_array; // FindGameObjectWithTag returns an array not a list 
    public List<GameObject> players = new List<GameObject>();

    int index;
    bool game_over;
    float camera_changing_constant = 1.0f;
    bool camera_transitioning;

    // Total distance between the markers.
    private float journey_length;
    private float journey_step;
    private float distance_to_target;
    void Start()
    {
        game_over = false;
         players_array = GameObject.FindGameObjectsWithTag("Player");
 
         foreach (GameObject player in players_array)
         {
            players.Add(player);
         }
        index = Random.Range(0, players.Count);
        current_player = players[index];
        Destroy(players[index], 5.0f); //testing purposes only
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!game_over)
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
                    print("currenly transitioning");
                }
            }
            else if (current_player != null)
            {
                transform.position = new Vector3(current_player.transform.position.x, transform.position.y, transform.position.z);
            }
            else
            {
                UpdateCamera();
            }
        }
    }

    private void UpdateCamera()
    {
        players.RemoveAt(index);
        if (players.Count == 0)
        {
            game_over = true;
            print("No players left");
        }
        else
        {
            index = Random.Range(0, players.Count);
            current_player = players[index];
            camera_transitioning = true;
            journey_length = current_player.transform.position.x - transform.position.x;
            float frames_transition = camera_changing_constant * 50.0f;
            journey_step = journey_length / frames_transition;
            distance_to_target = Mathf.Abs(transform.position.x - current_player.transform.position.x);
            Destroy(players[index], 5.0f); //testing purposes only
        }
    }
}
