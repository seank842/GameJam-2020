using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmPlatformerPlayer : MonoBehaviour
{
    Rigidbody player_rb;
    Animator player_an;
    //MeshRenderer player_mr;
    //public Material sub_max_velocity_colour;
    //public Material max_velocity_colour;
    public GameObject giblet_arm_1;
    public GameObject giblet_arm_2;
    public GameObject giblet_leg_1;
    public GameObject giblet_leg_2;
    public GameObject giblet_head;
    public GameObject giblet_torso;
    public GameObject blood_splatter;
    public GameObject camera;
    AudioSource player_audio_source;
    public AudioClip[] player_death_noises;
    bool grounded;
    bool no_input;
    bool player_killed;

    public bool Randomise_characteristics;

    public float velocity;
    float min_velocity = 15.0f;
    float max_velocity = 25.0f;
    float current_velocity;
    public float jump_height;
    float min_jump_height = 10.0f;
    float max_jump_height = 20.0f;
    public float size;
    float min_size = 0.75f;
    float max_size = 1.25f;
    public float fall_multiplier;
    float min_fall_multiplier = 1.5f;
    float max_fall_multiplier = 4.5f; 
    public float acceleration;
    float min_acceleration = 50.0f;
    float max_acceleration = 100.0f;

    // yet to implement reaction time

    public float reaction_time;
    float min_reaction_time = 0.0f;
    float max_reaction_time = 0.25f;

    void Start()
    {
        player_audio_source = GetComponent<AudioSource>();
        player_rb = GetComponent<Rigidbody>();
        player_an = GetComponent<Animator>();
        //player_mr = GetComponent<MeshRenderer>();
        grounded = false;
        player_killed = false;
        if (Randomise_characteristics)
        {
            DetermineRandomCharacteristics();
        }
    }

    void Update()
    {
        no_input = true;
        HandleMoving();
        HandleJumping();

        bool descending = player_rb.velocity.y < 0.0f;
        if (descending)
        {
            //print("descending with velocity: " + player_rb.velocity.y);
            player_an.SetBool("falling down", true);
            player_an.SetBool("jumping up", false);
            player_rb.velocity += Vector3.up * Physics2D.gravity.y * (fall_multiplier - 1.0f) * Time.deltaTime;
        }
        if (grounded)
        {
            player_an.SetBool("falling down", false);
        }
        if (no_input)
        {
            player_rb.velocity = new Vector3(0.0f, player_rb.velocity.y, 0.0f);
            player_an.SetBool("running", false);
            current_velocity = 0.0f;
        }
    }

    private void HandleJumping()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (grounded)
            {
                grounded = false;
                player_rb.velocity = Vector3.up * jump_height;
                player_an.SetBool("jumping up", true);
            }
            // keep this here for now but can probably remove later
            //if (player_rb.velocity.y < 0.0f)
            //{
            //    player_rb.velocity += Vector3.up * Physics2D.gravity.y * (fall_multiplier - 1.0f) * Time.deltaTime;
            //}
            no_input = false;
        }
    }

    private void HandleMoving()
    {
        current_velocity += acceleration * Time.deltaTime;
        if (current_velocity > velocity)
        {
            current_velocity = velocity;
            //player_mr.material = max_velocity_colour;
        }
        else
        {
            //player_mr.material = sub_max_velocity_colour;
        }
        if (Input.GetKey(KeyCode.D))
        {
            player_rb.velocity = new Vector3(current_velocity, player_rb.velocity.y, 0.0f);
            player_rb.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            player_an.SetBool("running", true); 
            no_input = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            player_rb.velocity = new Vector3(-current_velocity, player_rb.velocity.y, 0.0f);
            player_rb.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
            player_an.SetBool("running", true);
            no_input = false;
        }
    }

    void DetermineRandomCharacteristics()
    {
        velocity = UnityEngine.Random.Range(min_velocity, max_velocity);
        jump_height = UnityEngine.Random.Range(min_jump_height, max_jump_height);
        size = UnityEngine.Random.Range(min_size, max_size);
        transform.localScale = new Vector3(transform.localScale.x * size,
                                           transform.localScale.y * size,
                                           transform.localScale.z * size);
        fall_multiplier = UnityEngine.Random.Range(min_fall_multiplier, max_fall_multiplier);
        acceleration = UnityEngine.Random.Range(min_acceleration, max_acceleration);
        reaction_time = UnityEngine.Random.Range(min_reaction_time, max_reaction_time);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor"  && collision.gameObject.transform.position.y < transform.position.y)
        {
            grounded = true;
        }
        else if (collision.gameObject.tag == "Hazard" && !player_killed)
        {
            player_killed = true;
            int clip_index = UnityEngine.Random.Range(0, player_death_noises.Length);
            AudioSource.PlayClipAtPoint(player_death_noises[clip_index], camera.transform.position);
            Instantiate(blood_splatter, transform).transform.parent = null;
            Transform modified_transform = transform;
            modified_transform.position = new Vector3(modified_transform.position.x,
                                                      modified_transform.position.y + 0.1f,
                                                      modified_transform.position.z);
            Instantiate(giblet_arm_1, modified_transform).transform.parent = null;
            Instantiate(giblet_arm_2, modified_transform).transform.parent = null;
            Instantiate(giblet_leg_1, modified_transform).transform.parent = null;
            Instantiate(giblet_leg_2, modified_transform).transform.parent = null;
            Instantiate(giblet_head, modified_transform).transform.parent = null;
            Instantiate(giblet_torso, modified_transform).transform.parent = null;
            gameObject.SetActive(false);
            Destroy(gameObject, 0.3f);
        }
        else if (collision.gameObject.tag == "Out Of Bounds Box" && !player_killed)
        {
            player_killed = true;
            int clip_index = UnityEngine.Random.Range(0, player_death_noises.Length);
            AudioSource.PlayClipAtPoint(player_death_noises[clip_index], camera.transform.position);
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" && collision.gameObject.transform.position.y < transform.position.y)
        {
            grounded = false;
        }
    }


    private void OnDestroy()
    {
        PlayerDestroyedEvent?.Invoke(this, null);
    }

    public event EventHandler PlayerDestroyedEvent;
}
