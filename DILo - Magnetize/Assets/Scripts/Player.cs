using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Rigidbody2D rb;
	[SerializeField] private float speed = 5f;
	[SerializeField] private GameManager gameManager;
	private AudioSource crash;

	void Awake ()
	{
		rb = GetComponent<Rigidbody2D> ();
		crash = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(!gameManager.isPlay)
		{
			return;
		}

		rb.velocity = -transform.up * speed;
		
	}

	public void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.CompareTag ("Wall"))
		{
			gameManager.GameOver ();
			crash.Play ();
		}

		if (other.gameObject.CompareTag ("Tower"))
		{
			gameManager.GameOver ();
			crash.Play ();
		}
	}

	public void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Goal"))
		{
			gameManager.Goal ();
		}
	}
}
