using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameServer : MonoBehaviour {

	private PhotonView photonView;
	private HashSet<PhotonPlayer> readyPlayers;
	private GameState.GameStage gameStage = GameState.GameStage.LOBBY;
	
	void Start () {
		NewMatch();
		photonView = GetComponent<PhotonView>();
	}

	void NewMatch() {
		readyPlayers = new HashSet<PhotonPlayer>();
		gameStage = GameState.GameStage.LOBBY;
	}

	[RPC]
	void RPCPlayerReady (PhotonPlayer player) {
		if(!PhotonNetwork.isMasterClient) {
			Debug.LogError("We're not master but received ready");
			return;
		}

		if(gameStage != GameState.GameStage.LOBBY) {
			Debug.LogError("Received PlayerReady when game isn't in stage STARTING");
			return;
		}
		
		readyPlayers.Add(player);
		Debug.Log (readyPlayers.Count + " out of " + PhotonNetwork.playerList.Length + " players ready.");
		
		if (readyPlayers.Count == PhotonNetwork.playerList.Length && PhotonNetwork.playerList.Length > 1 && gameStage == GameState.GameStage.LOBBY) {
			StartCoroutine("StartMatch");
		}
	}

	void UpdateReadyText(string text) {
		Text readyText = GameObject.Find("ReadyText").GetComponent<Text>();
		readyText.text = text;
	}

	IEnumerator StartMatch() {
		gameStage = GameState.GameStage.STARTING; // change this to "STARTING" once we've implemented countdown
		UpdateReadyText("What");
		yield return new WaitForSeconds(1f);
		UpdateReadyText("Do");
		yield return new WaitForSeconds(1f);
		UpdateReadyText("We");
		yield return new WaitForSeconds(1f);
		UpdateReadyText("Do");
		yield return new WaitForSeconds(1f);
		UpdateReadyText("Now?");
		yield return new WaitForSeconds(1f);
		UpdateReadyText("Kill");
		yield return new WaitForSeconds(2f);
		ReallyStartMatch();
	}

	void ReallyStartMatch() {
		int i = 0;
		gameStage = GameState.GameStage.RUNNING; // change this to "STARTING" once we've implemented countdown
		foreach(PhotonPlayer player in PhotonNetwork.playerList) {
			photonView.RPC("ClientStartMatch", player, i);
			i += 1;
		}
	}
	
	void Update () {
	
	}
}
