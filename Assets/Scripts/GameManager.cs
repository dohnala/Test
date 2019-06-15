using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerShip;
    public GameObject playerHUD;
    public FPSCounter fpsCounter;
    
    private static GameManager _instance;

    public void Awake()
    {
        _instance = this;

        SpawnPlayerShip();
        CreatePlayerHUD();
    }

    public static int GetPing()
    {
        return PhotonNetwork.GetPing();
    }

    public static int GetFPS()
    {
        return _instance.fpsCounter.CurrentFPS;
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    private void SpawnPlayerShip()
    {
        var position = new Vector3(Random.Range(-4, 4), Random.Range(-2, 2));

        PhotonNetwork.Instantiate(playerShip.name, position, Quaternion.identity);
    }

    private void CreatePlayerHUD()
    {
        Instantiate(playerHUD);
    }
}