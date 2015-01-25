using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {

	public Transform game;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			GameObject go = GameObject.Find("Game");
			((GameManager)go.GetComponent<GameManager>()).Kill(other.gameObject);
		}
	}
}
