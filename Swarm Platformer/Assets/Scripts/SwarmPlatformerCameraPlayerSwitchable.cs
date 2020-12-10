using System.Linq;
using UnityEngine;

public class SwarmPlatformerCameraPlayerSwitchable : SwarmPlatformerCamera
{
    public GlobalSceneManager scene_manager;

    void Awake()
    {
        scene_manager.PlayerChangedEvent += Scene_manager_PlayerChangedEvent;
    }

    private void Scene_manager_PlayerChangedEvent(object sender, GameObject e)
    {
        if (e == default)
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