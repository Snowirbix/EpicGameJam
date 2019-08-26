using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    public static MessageBox instance;

    public GameObject box;
    public TextMeshProUGUI title;
    public TextMeshProUGUI body;
    public Slider slider;

    protected float startTime;
    protected Message message;

    public class Message
    {
        public string title;
        public Queue<string> sentences;
        public float timer = 5f;

        public Message (string title, string[] sentences, float timer)
        {
            this.title = title;
            this.sentences = new Queue<string>(sentences);
            this.timer = timer;
        }
    }

    private void Awake ()
    {
        instance = this;
    }

    private void Start ()
    {
        if (message == null)
        {
            box.SetActive(false);
        }
    }

    private void Update ()
    {
        if (message != null)
        {
            float time = (Time.time - startTime) / message.timer;
            slider.value = time;
            
            if (time >= 1)
            {
                if (message.sentences.Count > 0)
                {
                    DisplaySentence();
                }
                else
                {
                    Hide();
                }
            }
        }
    }
    
    public void Display (Message message)
    {
        this.message = message;
        title.text = message.title;
        DisplaySentence();
        // SHOW
        box.SetActive(true);
    }

    protected void DisplaySentence ()
    {
        startTime = Time.time;
        slider.value = 0;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(message.sentences.Dequeue()));
    }

    public void Hide ()
    {
        this.message = null;
        // HIDE
        box.SetActive(false);
    }

    IEnumerator TypeSentence (string sentence)
    {
        body.text = "";
        Queue<char> letters = new Queue<char>(sentence.ToCharArray());

        while (letters.Count > 0)
        {
            // add more letters if framerate is lower
            for (int i = 0; i < (int)(60 / Application.targetFrameRate); i++)
            {
                if (letters.Count > 0)
                    body.text += letters.Dequeue();
            }
            yield return null;
        }
    }
}
