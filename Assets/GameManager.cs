using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	enum GameState
	{
		STARTING,
		RUNNING,
		FINISHED

	}

	public int[] score;
	public int players = 2;
	public Transform level;
	public Transform player;
	private GameState currentState = GameState.STARTING;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
