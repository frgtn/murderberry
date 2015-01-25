using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameServer : MonoBehaviour {

	private PhotonView photonView;
	private HashSet<PhotonPlayer> readyPlayers;
	private GameState.GameStage gameStage = GameState.GameStage.LOBBY;
	private int alivePlayers = 0;
	private GameObject readyButton;
	
	void Start () {
		NewMatch();
		photonView = GetComponent<PhotonView>();
		readyButton = GameObject.Find("Ready");
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

	[RPC]
	void UpdateReadyText(string text) {
		readyButton.SetActive(true);
		Text readyText = GameObject.Find("ReadyText").GetComponent<Text>();
		readyText.text = text;
	}

	void SendReadyText(string text) {
		photonView.RPC("UpdateReadyText", PhotonTargets.All, text);
	}
	
	[RPC]
	public void MurderPlayer(int spawnPointNum) {
		alivePlayers -= 1;
		if (alivePlayers == 0) {
			StartCoroutine("StartMatch");
			foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
				PhotonNetwork.Destroy(gameObject);
			}
		}
	}
		
	IEnumerator StartMatch() {
		gameStage = GameState.GameStage.STARTING; // change this to "STARTING" once we've implemented countdown
		SendReadyText("What");
		yield return new WaitForSeconds(1f);
		SendReadyText("Do");
		yield return new WaitForSeconds(1f);
		SendReadyText("We");
		yield return new WaitForSeconds(1f);
		SendReadyText("Do");
		yield return new WaitForSeconds(1f);
		SendReadyText("Now?");
		yield return new WaitForSeconds(1f);
		SendReadyText("Kill");
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
		alivePlayers = readyPlayers.Count;
	}
	
	void Update () {
	
	}
}
