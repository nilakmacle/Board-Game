using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Netmanager : NetworkBehaviour 
{
    public GameObject serverPrefab;
    public GameObject servantPrefab;
	// Use this for initialization
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
	void Start () 
	{
        DontDestroyOnLoad(this.gameObject);
		
		if (hasAuthority) 
		{
			GameObject.Find ("LobbyManager").GetComponent<LobbyManager> ().ownManager = this;
		}	
	}

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex > 2)
        {
            if (isServer && hasAuthority)
            {
                GameObject obj = (GameObject) Instantiate(serverPrefab);
                NetworkServer.Spawn(obj);
            }
            GetComponent<GameNetworkServent>().SetColor();
            DiscoverNetworks.Instance.StopBroadcast();
        }

        if (scene.buildIndex == 0)
        {
            if (hasAuthority)
            {
                CmdCleanup();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
	
	// Update is called once per frame
	void Update () 
	{
		
	}

    void OnDestroy()
    {
        Debug.Log(Time.time + "Destroy Called");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    [Command]
    public void CmdCleanup()
    {
        Debug.Log(Time.time + "Command Called");
        NetworkServer.Destroy(this.gameObject);
    }

	[Command]
	public void CmdSetData_1(string Name)
	{
		GameObject.Find ("LobbyManager").GetComponent<LobbyManager> ().playerOneField = Name;
	}
	[Command]
	public void CmdSetData_2(string Name)
	{
		GameObject.Find ("LobbyManager").GetComponent<LobbyManager> ().playerTwoField = Name;
	}
	[Command]
	public void CmdSetData_3(string Name)
	{
		GameObject.Find ("LobbyManager").GetComponent<LobbyManager> ().playerThreeField = Name;
	}
	[Command]
	public void CmdSetData_4(string Name)
	{
		GameObject.Find ("LobbyManager").GetComponent<LobbyManager> ().playerFourField = Name;
	}

    [Command]
    public void CmdSetPlayer(int playerValue)
    {
        LobbyManager lm = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();

        switch (playerValue)
        {
            case 1:
                if (!lm.p1 && !lm.isSelected)
                {
                    lm.p1 = true;
                }
                break;
            case 2:
                if (!lm.p2 && !lm.isSelected)
                {
                    lm.p2 = true;
                }
                break;
            case 3:
                if (!lm.p3 && !lm.isSelected)
                {
                    lm.p3 = true;
                }
                break;
            case 4:
                if (!lm.p4 && !lm.isSelected)
                {
                    lm.p4 = true;
                }
                break;

            default:
                break;
        }
    }
}
