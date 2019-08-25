using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Message", menuName = "Message")]
public class MessageObject : ScriptableObject
{
    public string messageTitle;
    public string[] messagecontent;
    public float timer;
}
