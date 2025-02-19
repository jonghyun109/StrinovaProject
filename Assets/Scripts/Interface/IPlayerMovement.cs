using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPlayerMovement
{
    void Move(Transform transform, float h, float v);
    void PlayAnimation(float h, float v);
}
