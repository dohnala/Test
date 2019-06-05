using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject loadingScreen;

    [SerializeField] private GameObject lobbyScreen;

    [SerializeField] private Button joinButton;

    [SerializeField] private InputField nameInput;

    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            loadingScreen.SetActive(false);
            lobbyScreen.SetActive(true);    
            joinButton.enabled = false;
        }
        else
        {
            loadingScreen.SetActive(true);
            lobbyScreen.SetActive(false);
            
            PhotonNetwork.ConnectUsingSettings();    
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");

        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to Lobby");

        loadingScreen.SetActive(false);
        lobbyScreen.SetActive(true);
        joinButton.enabled = false;
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GameScene");

        Debug.Log("Connected to " + PhotonNetwork.CurrentRoom);
    }

    public void JoinGame()
    {
        if (!IsNameValid()) return;

        PhotonNetwork.NickName = nameInput.text;

        Debug.Log("Name set to " + PhotonNetwork.NickName);

        lobbyScreen.SetActive(false);

        JoinGlobalRoom();
    }

    public void UpdateJoinButton()
    {
        joinButton.enabled = IsNameValid();
    }

    private bool IsNameValid()
    {
        return nameInput.text.Length > 0;
    }

    private static void JoinGlobalRoom()
    {
        var roomOptions = new RoomOptions {MaxPlayers = 10, IsVisible = false};

        PhotonNetwork.JoinOrCreateRoom("GLOBAL", roomOptions, TypedLobby.Default);
    }
}