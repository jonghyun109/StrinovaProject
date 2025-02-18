using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterMode
{
    ThreeD,
    TwoD,
    StringMode,
}
public interface IPlayerMovement
{        
    void MoveW(Transform transform){}
    void MoveS(Transform transform){}
    void MoveA(Transform transform){}
    void MoveD(Transform transform){}
}
