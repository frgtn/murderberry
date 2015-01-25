using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private PhotonView photonView;
	private GameObject[] spawnPoints;
	
	public int[] score;
	public int players = 2;
	public Transform level;
	public Transform player;
	private GameState.GameStage currentState = GameState.GameStage.LOBBY;
	public Transform readyButton;

	// Use this for initialization
	void Start () {
		photonView = GetComponent<PhotonView>();
		spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
	}

	public void PlayerReady() {
		photonView.RPC("RPCPlayerReady", PhotonTargets.MasterClient, PhotonNetwork.player);
	}
	
	public void Kill(GameObject other) {
		// maybe count score?
		PlayerControl pc = other.GetComponent<PlayerControl>();
		pc.die();
	}

	[RPC]
	void ClientStartMatch (int spawnPointNum) {
		Debug.Log("Starting in position " + spawnPointNum);
		Debug.Log (spawnPoints);
		var player = PhotonNetwork.Instantiate("PlayerFab", spawnPoints[spawnPointNum].transform.position, Quaternion.identity, 0);
		player.GetComponent<PlayerControl>().spriteNum = spawnPointNum;
		readyButton.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
