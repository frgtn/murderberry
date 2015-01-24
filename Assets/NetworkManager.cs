using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	string gameName = "Murderberry GGJ2015";

	// Use this for initialization
	void Start () {
		Debug.Log ("Network manager startup called");
		if (Network.InitializeServer (3, 12345, !Network.HavePublicAddress ()) != NetworkConnectionError.NoError) {
			Debug.Log ("Failed to initialize server");
			RefreshHostList();
		}
	}

	void OnServerInitialized () {
		Debug.Log ("Hai network server up");
		MasterServer.RegisterHost(gameName, "Murderberry");
	}

	void OnMasterServerEvent (MasterServerEvent msEvent) {
		if(msEvent == MasterServerEvent.RegistrationSucceeded) {
			Debug.Log("Successfully registered on MasterServer");

		} else if (msEvent == MasterServerEvent.HostListReceived) {
			PrintHostList(MasterServer.PollHostList());
			// hax hax
			if (!Network.isServer) {
				Network.Connect(MasterServer.PollHostList()[0]);
			}
		}
	}

	void PrintHostList(HostData[] hostList) {
		foreach (HostData host in hostList) {
			Debug.Log (host.gameName);
			Debug.Log (host.gameType);
			int i = 0;
			string tmpIp = "";
			while (i < host.ip.Length) {
				tmpIp = host.ip[i] + " ";
				i++;
			}
			Debug.Log (tmpIp + ":" + host.port);
		}
	}

	void RefreshHostList() {
		MasterServer.RequestHostList (gameName);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
