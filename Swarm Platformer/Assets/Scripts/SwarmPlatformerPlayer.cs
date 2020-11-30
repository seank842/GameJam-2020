using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmPlatformerPlayer : MonoBehaviour
{
    Rigidbody player_rb;
    MeshRenderer player_mr;
    public Material sub_max_velocity_colour;
    public Material max_velocity_colour;
    bool grounded;
    bool no_input;

    public bool randomise_characteristics;

    public float velocity;
    float min_velocity = 2.0f;
    float max_velocity = 10.0f;
    float current_velocity;
    public float jump_height;
    float min_jump_height = 3.0f;
    float max_jump_height = 10.0f;
    public float size;
    float min_size = 0.25f;
    float max_size = 2.0f;
    public float fall_multiplier;
    float min_fall_multiplier = 1.5f;
    float max_fall_multiplier = 2.5f; 
    public float acceleration;
    float min_acceleration = 2.0f;
    float max_acceleration = 4.0f;

    // yet to implement reaction time

    public float reaction_time;
    float min_reaction_time = 0.0f;
    float max_reaction_time = 0.25f;

    void Start()
    {
        player_rb = GetComponent<Rigidbody>();
        player_mr = GetComponent<MeshRenderer>();
        grounded = true;
        if (randomise_characteristics)
        {
            DetermineRandomCharacteristics();
        }
    }

    void Update()
    {
        no_input = true;
        HandleMoving();
        HandleJumping();
        if (no_input)
        {
            player_rb.velocity = new Vector3(0.0f, player_rb.velocity.y, 0.0f);
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
            }
            if (player_rb.velocity.y < 0.0f)
            {
                player_rb.velocity += Vector3.up * Physics2D.gravity.y * (fall_multiplier - 1.0f) * Time.deltaTime;
            }
            no_input = false;
        }
    }

    private void HandleMoving()
    {
        current_velocity += acceleration * Time.deltaTime;
        if (current_velocity > velocity)
        {
            current_velocity = velocity;
            player_mr.material = max_velocity_colour;
        }
        else
        {
            player_mr.material = sub_max_velocity_colour;
        }
        if (Input.GetKey(KeyCode.D))
        {
            player_rb.velocity = new Vector3(current_velocity, player_rb.velocity.y, 0.0f);
            no_input = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            player_rb.velocity = new Vector3(-current_velocity, player_rb.velocity.y, 0.0f);
            no_input = false;
        }
    }

    void DetermineRandomCharacteristics()
    {
        velocity = Random.Range(min_velocity, max_velocity);
        jump_height = Random.Range(min_jump_height, max_jump_height);
        size = Random.Range(min_size, max_size);
        fall_multiplier = Random.Range(min_fall_multiplier, max_fall_multiplier);
        acceleration = Random.Range(min_acceleration, max_acceleration);
        reaction_time = Random.Range(min_reaction_time, max_reaction_time);
        transform.localScale = new Vector3(size, size, size);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor"  && collision.gameObject.transform.position.y < transform.position.y)
        {
            grounded = true;
        }
    }
}
