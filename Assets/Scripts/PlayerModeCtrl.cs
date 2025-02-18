using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModeCtrl : MonoBehaviour, ICharacterMode
{
    public CharacterMode currentMode = CharacterMode.ThreeD;
    private bool isNearWall = false; // 벽 감지 여부
    private bool isStringMode = false; // StringMode 여부

    public GameObject player;
    public PlayerMove playerMove;

    public GameObject twoDPrefab;
    private void Update()
    {
        ChangeInput();
        
    }
    public void ChangeInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentMode == CharacterMode.ThreeD && isNearWall)
            {
                ChangeMode(CharacterMode.TwoD);
            }
            else if (currentMode == CharacterMode.TwoD)
            {
                ChangeMode(CharacterMode.ThreeD);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (currentMode == CharacterMode.ThreeD)
                ChangeMode(CharacterMode.StringMode);
            else if (currentMode == CharacterMode.StringMode)
                ChangeMode(CharacterMode.ThreeD);
        }

        if (currentMode == CharacterMode.TwoD && Input.GetKeyDown(KeyCode.Space))
        {
            ChangeMode(CharacterMode.ThreeD);
        }
    }

    public void ChangeMode(CharacterMode newMode)
    {
        if (currentMode == newMode) return; // 같은 모드로 변경 방지

        switch (newMode)
        {
            case CharacterMode.ThreeD:
                ChangeThreeDMode();
                break;

            case CharacterMode.TwoD:
                ChangeTwoDMode();
                break;

            case CharacterMode.StringMode:
                ChangeStringMode();
                break;
        }

        currentMode = newMode;
    }
    void ChangeThreeDMode()
    {
        player.transform.localScale = new Vector3(1, 1, 1);
        playerMove.hasSpawned = false;
        isStringMode = false;
    }
    void ChangeTwoDMode()
    {
        Vector3 spawnPosition = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
        Instantiate(twoDPrefab, spawnPosition, Quaternion.identity);
        playerMove.hasSpawned = true;
    }
    void ChangeStringMode()
    {
        player.transform.localScale = new Vector3(1, 1, 0.2f);
        isStringMode = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TwoDWall"))
        {
            isNearWall = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TwoDWall"))
        {
            isNearWall = false;
        }
    }
}
