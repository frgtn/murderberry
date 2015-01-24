using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
	
	public float maxSpeed = 8f;				// The fastest the player can travel in the x axis.
	public float jumpVelocity = 8f;			// Amount of force added when the player jumps.
	public float distToGround = 0.0f;

	void Start() {
		distToGround = collider2D.bounds.extents.y;
	}

	bool IsGrounded() {
		return Physics2D.Raycast(transform.position, -Vector2.up, distToGround + 0.1f, 1 << 9);
	}	
	
	void Update()
	{
		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetButtonDown("Jump") && IsGrounded()) {
			jump = true;
		}

		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
	}

	void FixedUpdate ()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");

		rigidbody2D.velocity = rigidbody2D.velocity + Vector2.right * h * maxSpeed;

		// If the player should jump...
		if(jump)
		{
			// Play a random jump audio clip.
			// int i = Random.Range(0, jumpClips.Length);
			// AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
			
			// Add a vertical force to the player.
			rigidbody2D.velocity = rigidbody2D.velocity + Vector2.up * jumpVelocity;

			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}
	}
}
