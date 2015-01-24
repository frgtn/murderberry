using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private PhotonView photonView;
	private GameObject[] spawnPoints;
	private HashSet<PhotonPlayer> readyPlayers;
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
		photonView = GetComponent<PhotonView>();
		GameObject[] SpawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
		PrepareNewGame();
	}

	void PrepareNewGame() {
		readyPlayers = new HashSet<PhotonPlayer>();
	}

	void StartMatch() {
		int i = 0;
		foreach(PhotonPlayer player in PhotonNetwork.playerList) {
			photonView.RPC("ClientStartMatch", player, i);
			i += 1;
		}
	}

	public void PlayerReady() {
		photonView.RPC("RPCPlayerReady", PhotonTargets.MasterClient, PhotonNetwork.player);
	}

	[RPC]
	void RPCPlayerReady (PhotonPlayer player) {
		if(!PhotonNetwork.isMasterClient) {
			Debug.Log("We're not master but received ready");
			return;
		}

		readyPlayers.Add(player);

		if (readyPlayers.Count == PhotonNetwork.playerList.Length) {
			StartMatch();
		}
	}

	[RPC]
	void ClientStartMatch (int spawnPointNum) {
		Debug.Log("Starting in position " + spawnPointNum);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
