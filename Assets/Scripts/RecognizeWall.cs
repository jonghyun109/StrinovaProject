using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RecognizeWall : MonoBehaviour
{
    PlayerState playerState;
    public CinemachineVirtualCamera twoDcam;
    bool isPaper = false;

    private void Start()
    {
        playerState = FindObjectOfType<PlayerState>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TwoDWall"))
        {
            playerState.isWall = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TwoDWall"))
        {
            playerState.isWall = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            playerState.jumpCount = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("TwoDWall") && playerState.isWall == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && !playerState.hasSpawned)
            {
                if(!isPaper)
                {
                    Vector3 spawnPosition = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
                    var twoD = Instantiate(playerState.paperPlayer, spawnPosition, Quaternion.identity);                    
                    
                    playerState.paperPlayer = twoD;
                    isPaper = true;
                }
                playerState.paperPlayer.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
                playerState.hasSpawned = true;
                playerState.paperPlayer.SetActive(true);
                playerState.player.SetActive(false);

                twoDcam.Follow = playerState.paperPlayer.transform;
                twoDcam.LookAt = playerState.paperPlayer.transform;
            }
        }
    }

}
