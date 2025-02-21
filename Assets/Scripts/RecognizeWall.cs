using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecognizeWall : MonoBehaviour
{
    PlayerState playerState;

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
}
