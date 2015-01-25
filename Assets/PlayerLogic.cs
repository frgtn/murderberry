using UnityEngine;
using System.Collections;

public class PlayerLogic : MonoBehaviour {
	private GameManager manager;

	// Use this for initialization
	void Start () {
		GameObject game = GameObject.Find("Game");
		manager = game.GetComponent<GameManager>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		var go = other.gameObject;
		if (go.tag == "Player") {
			if (go.rigidbody2D.velocity.y <= 0) {
				manager.Kill(gameObject);
			}
		}
	}
}
