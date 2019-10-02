using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	[SerializeField] private float pullForce = 100f;
	[SerializeField] private float rotateSpeed = 360f;
	[SerializeField] private GameObject player;
	private Transform playerTransform;
	private Rigidbody2D playerRb;
	private SpriteRenderer sprite;

	void Awake ()
	{
		playerTransform = player.transform;
		playerRb = player.GetComponent <Rigidbody2D> ();
		sprite = GetComponent <SpriteRenderer> ();
	}	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseOver ()
	{
		sprite.color = Color.green;

		if (Input.GetMouseButton (0))
		{
			sprite.color = Color.blue;

			float distance = Vector2.Distance (transform.position, playerTransform.position);

			Vector3 pullDir = (transform.position - playerTransform.position).normalized;
			float newPullForce = Mathf.Clamp (pullForce / distance, 20f, 50f);
			playerRb.AddForce (pullDir * newPullForce);

			playerRb.angularVelocity = -rotateSpeed / distance;

		}

		if (Input.GetMouseButton (1))
		{
			sprite.color = Color.blue;

			float distance = Vector2.Distance (transform.position, playerTransform.position);

			Vector3 pullDir = (transform.position - playerTransform.position).normalized;
			float newPullForce = Mathf.Clamp (pullForce / distance, 20f, 50f);
			playerRb.AddForce (pullDir * newPullForce);

			playerRb.angularVelocity = rotateSpeed / distance;

		}
	}

	void OnMouseExit ()
	{
		sprite.color = Color.white;
	}
}
