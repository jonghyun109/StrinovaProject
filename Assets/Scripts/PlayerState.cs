using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CharacterMode
{
    ThreeD,
    TwoD,
    StringMode,
}
public class PlayerState : MonoBehaviour
{
    public CharacterMode state = CharacterMode.ThreeD;
    public GameObject player;
    public GameObject paperPrefab;
    public GameObject unityChan2D;
    private GameObject paperMode;
    public bool isWall = false;

    IPlayerMovement currentState;

    Vector3 spawnPosition;
    public Animator anim;
    float h;
    float v;

    private void Start()
    {
        anim = player.GetComponent<Animator>();
        SetState(CharacterMode.ThreeD);
    }

    private void Update()
    {
        GetInput();
        if (state == CharacterMode.TwoD && paperMode != null)
        {
            currentState.Move(paperMode.transform, h, v); // 2D 상태일 때 paperPrefab을 조작
            currentState.PlayAnimation(h, v);
        }
        else
        {
            currentState.Move(player.transform, h, v);
            currentState.PlayAnimation(h, v);
        }
    }

    private void GetInput()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (state == CharacterMode.ThreeD)
            {
                SetState(CharacterMode.StringMode);
            }
            else if (state == CharacterMode.StringMode)
            {
                SetState(CharacterMode.ThreeD);
            }
        }
        if (state == CharacterMode.TwoD && Input.GetKeyDown(KeyCode.Space))
        {
            SetState(CharacterMode.ThreeD);
        }
        if (Input.GetKeyDown(KeyCode.E) && isWall)
        {
            Debug.Log("들어옴");
            if (state == CharacterMode.ThreeD)
            {
                SetState(CharacterMode.TwoD);
                SwitchTwoD();
            }
            else if (state == CharacterMode.TwoD)
            {
                SetState(CharacterMode.ThreeD);
                SwitchThreeD();
            }
        }               
    }

    private void SetState(CharacterMode newState)
    {
        state = newState;

        switch (state)
        {
            case CharacterMode.ThreeD:
                currentState = new ThreeDState(player.transform, anim);
                break;
            case CharacterMode.TwoD:
                currentState = new TwoDState(player.transform, anim);
                break;
            case CharacterMode.StringMode:
                currentState = new StringState(player.transform, anim);
                break;
        }
    }

    void SwitchTwoD()
    {        
        spawnPosition = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);
        paperPrefab = Instantiate(paperPrefab, spawnPosition, Quaternion.identity);

        player.SetActive(false); // 기존 플레이어 비활성화
    }
    void SwitchThreeD()
    {
        spawnPosition = new Vector3(paperMode.transform.position.x, paperMode.transform.position.y, paperMode.transform.position.z);
        player.SetActive(true);
        player.transform.position = spawnPosition;

        paperPrefab.SetActive(false);
    }
}
