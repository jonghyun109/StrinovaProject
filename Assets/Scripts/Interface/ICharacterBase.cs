using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterBase
{
    float HP { get; set; }
    void TakeDamage(float damage);
}

