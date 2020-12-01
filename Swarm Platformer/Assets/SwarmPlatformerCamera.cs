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
    float camera_changing_constant;
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
        //Destroy(players[index], 10.0f); //testing purposes only
    }

    // Update is called once per frame
    void Update()
    {
        if (!game_over)
        {
            if (current_player != null)
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
        }
        else
        {
            index = Random.Range(0, players.Count);
            current_player = players[index];
            //Destroy(players[index], 10.0f); //testing purposes only
        }
    }
}
