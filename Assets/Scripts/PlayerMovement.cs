using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameManager manager;
    public float moveSpeed;
    public GameObject deathParticles;

    private float maxSpeed = 5f;
    private Vector3 input;

    
    private Vector3 spawn;


	// Use this for initialization
	void Start ()
    {
        manager = manager.GetComponent<GameManager>();
        spawn = transform.position;		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (GetComponent<Rigidbody>().velocity.magnitude < maxSpeed)
        {
            GetComponent<Rigidbody>().AddRelativeForce(input * moveSpeed);
        }

        if(transform.position.y < -2)
        {
            Die();
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Enemy")
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            Die();
        }

        if (other.transform.tag == "Token")
        {
            manager.tokenCount += 1;
            Destroy(other.gameObject);
        }

        if (other.transform.tag == "Finish")
        {
            manager.CompleteLevel();  
        }
    }

    void Die()
    {
        Instantiate(deathParticles, transform.position, Quaternion.Euler(270, 0, 0));
        transform.position = spawn;
    }
}
