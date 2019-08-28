using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Options : ScriptableObject
{
   public enum Keyboard
    {
        QWERTY,
        AZERTY
    }

   public Keyboard keyboard;
}
