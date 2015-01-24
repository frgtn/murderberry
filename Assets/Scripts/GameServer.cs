using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameServer : MonoBehaviour {

	private PhotonView photonView;
	private HashSet<PhotonPlayer> readyPlayers;
	
	void Start () {
		NewMatch();
		photonView = GetComponent<PhotonView>();

	}

	void NewMatch() {
		readyPlayers = new HashSet<PhotonPlayer>();
	}

	[RPC]
	void RPCPlayerReady (PhotonPlayer player) {
		if(!PhotonNetwork.isMasterClient) {
			Debug.Log("We're not master but received ready");
			return;
		}
		
		readyPlayers.Add(player);
		Debug.Log (readyPlayers.Count + " out of " + PhotonNetwork.playerList.Length + " players ready.");
		
		if (readyPlayers.Count == PhotonNetwork.playerList.Length) {
			StartMatch();
		}
	}
	
	void StartMatch() {
		int i = 0;
		foreach(PhotonPlayer player in PhotonNetwork.playerList) {
			photonView.RPC("ClientStartMatch", player, i);
			i += 1;
		}
	}
	
	void Update () {
	
	}
}
