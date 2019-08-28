using UnityEngine;

[CreateAssetMenu(fileName = "Message", menuName = "Message")]
public class MessageObject : ScriptableObject
{
    public string messageTitle;

    [System.Serializable]
    public class Sentence
    {
        public string text;
        public float timer = 3;
    }

    public Sentence[] messageContent;
}
