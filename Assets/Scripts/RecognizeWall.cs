using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor.Animations;
using Unity.VisualScripting;
using UnityEngine.Playables;

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
            playerState.player.gameObject.transform.localScale = new Vector3(1, 1, 1f);
            playerState.player.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

            playerState.anim.SetTrigger("Idle");
            playerState.anim.SetBool("IsFlying", false);

            playerState.jumpCount = 0;
            playerState.rb.drag = 0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerState.isWall == true)
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
                else
                {
                    playerState.paperPlayer.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);                    
                    playerState.paperPlayer.SetActive(true);                    
                }
                playerState.player.SetActive(false);
                playerState.hasSpawned = true;
                twoDcam.Follow = playerState.paperPlayer.transform;
                twoDcam.LookAt = playerState.paperPlayer.transform;
            }
        }
    }
    public void SetNewPaperPlayer(GameObject newPaperPlayer)
    {
        playerState.paperPlayer = newPaperPlayer;
    }

}
