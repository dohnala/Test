using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerShip;
    public GameObject playerHUD;

    public void Awake()
    {
        SpawnPlayerShip();
        CreatePlayerHUD();
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