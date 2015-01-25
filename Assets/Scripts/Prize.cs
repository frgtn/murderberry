using UnityEngine;
using System.Collections;

public class Prize : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			GameObject go = GameObject.Find("Game");
			((GameManager)go.GetComponent<GameManager>()).prize(other.gameObject);
		}
	}

}
