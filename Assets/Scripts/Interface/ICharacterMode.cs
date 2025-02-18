using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterMode
{
    void ChangeMode(CharacterMode newMode);
    void ChangeInput();
}
