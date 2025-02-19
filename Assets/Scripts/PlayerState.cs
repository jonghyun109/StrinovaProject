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

    IPlayerMovement currentState;
    ThreeDState threeDState;
    TwoDState twoDState;
    StringState stringState;

    float h;
    float v;

    private void Start()
    {
        threeDState = GetComponent<ThreeDState>();
        twoDState = GetComponent<TwoDState>();
        stringState = GetComponent<StringState>();
        

        SetState(CharacterMode.ThreeD);
    }

    private void Update()
    {
        GetInput();

        if (currentState != null)
        {
            currentState.Move(player.transform, h, v);
            currentState.PlayAnimation(h, v);
        }
    }

    private void GetInput()
    {
        Debug.Log(currentState);
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (state == CharacterMode.ThreeD) SetState(CharacterMode.TwoD);
            else if (state == CharacterMode.TwoD) SetState(CharacterMode.ThreeD);
        }

        
    }

    private void SetState(CharacterMode newState)
    {
        state = newState;

        switch (state)
        {
            case CharacterMode.ThreeD:
                currentState = threeDState;
                break;
            case CharacterMode.TwoD:
                currentState = twoDState;
                break;
            case CharacterMode.StringMode:
                currentState = stringState;
                break;
        }
    }
}
