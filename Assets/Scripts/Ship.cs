using Photon.Pun;
using UnityEngine;

public class Ship : MonoBehaviourPun
{
    public GameObject shipCamera;

    public GameObject spriteMe;

    public GameObject spriteEnemy;
    
    public void Start()
    {
        shipCamera.SetActive(photonView.IsMine);
        spriteMe.SetActive(photonView.IsMine);
        spriteEnemy.SetActive(!photonView.IsMine);
    }

    public void Update()
    {
        // do not rotate camera
        shipCamera.transform.eulerAngles = Vector3.zero;
    }
}