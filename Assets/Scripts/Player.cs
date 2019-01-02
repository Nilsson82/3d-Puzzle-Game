using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager manager;
    public float moveSpeed;
    public GameObject deathParticles;

    private float maxSpeed = 5f;
    private Vector3 input;

    
    private Vector3 spawn;

    public AudioClip[] audioClip;


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
            PlaySound(3); // Spiketrap
            Die();
        }

        if (other.transform.tag == "Token")
        {
            manager.tokenCount += 1;
            PlaySound(0);
            Destroy(other.gameObject);
        }

        if (other.transform.tag == "Finish")
        {
            PlaySound(1);
            Time.timeScale = 0f;
            manager.CompleteLevel();  
        }
    }

    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().clip = audioClip[clip];
        GetComponent<AudioSource>().Play();
    }

    void Die()
    {
        Instantiate(deathParticles, transform.position, Quaternion.Euler(270, 0, 0));
        transform.position = spawn;
    }
}
