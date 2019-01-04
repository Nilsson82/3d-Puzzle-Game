using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    public GameManager manager;
    public float moveSpeed;
    public GameObject deathParticles;
    public bool usesManager = true;

    private float maxSpeed = 5f;
    private Vector3 input;

    
    private Vector3 spawn;

    public AudioClip[] audioClip;


	// Use this for initialization
	void Start ()
    {
        if (usesManager)
        {
            manager = manager.GetComponent<GameManager>();
        }
        spawn = transform.position;		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        input = new Vector3(CrossPlatformInputManager.GetAxisRaw("Horizontal"), 0, CrossPlatformInputManager.GetAxisRaw("Vertical"));
        if (GetComponent<Rigidbody>().velocity.magnitude < maxSpeed)
        {
            GetComponent<Rigidbody>().AddRelativeForce(input * moveSpeed);
        }

        if(transform.position.y < -2)
        {
            Die();
        }

        //Physics.gravity = Physics.Raycast(transform.position, Vector3.down, .6f) ? Vector3.zero : new Vector3(0, -9.5f, 0);

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
            if (usesManager)
            {
                manager.tokenCount += 1;
            }
            PlaySound(0);
            Destroy(other.gameObject);
        }

        if (other.transform.tag == "Finish")
        {
            PlaySound(1);
            Time.timeScale = 0f;
            if (usesManager)
            {
                manager.CompleteLevel();
            }
        }
    }

    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().clip = audioClip[clip];
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
        GetComponent<AudioSource>().Play();
    }

    void Die()
    {
        Instantiate(deathParticles, transform.position, Quaternion.Euler(270, 0, 0));
        transform.position = spawn;
    }
}
