using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject spaceshipPrefab;
    
    public void Awake()
    {
        var position = new Vector3(Random.Range(-4, 4), Random.Range(-2, 2));
        
        SpawnSpaceship(position, Quaternion.identity);
    }
    
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    private void SpawnSpaceship(Vector3 position, Quaternion rotation)
    {
        PhotonNetwork.Instantiate(spaceshipPrefab.name, position, rotation);
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();    
        }
    }
}
