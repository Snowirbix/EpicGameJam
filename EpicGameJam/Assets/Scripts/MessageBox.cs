using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class MessageBox : MonoBehaviour
{
    public static MessageBox instance;

    public GameObject box;
    public TextMeshProUGUI title;
    public TextMeshProUGUI body;
    public Slider slider;

    protected float startTime;
    protected Message message;
    protected Sentence sentence;

    [System.Serializable]
    public class Message
    {
        public string title;
        public List<Sentence> sentences = new List<Sentence>();
        public int idx = 0;

        public Message (string title, Sentence[] sentences)
        {
            this.title = title;
            this.sentences = new List<Sentence>(sentences);
        }
        
        public Message (string title, MessageObject.Sentence[] msgSentences)
        {
            this.title = title;
            // conversion from scriptable sentence to object sentence
            for (int i = 0; i < msgSentences.Length; i++)
            {
                this.sentences.Add(new Sentence(msgSentences[i].text, msgSentences[i].timer));
            }
        }
    }

    [System.Serializable]
    public class Sentence
    {
        public string text;
        public float timer = 5f;
        //[HideInInspector]
        public bool next = false;

        public Sentence (string text, float timer = 0)
        {
            this.text = text;
            // 0 means forever
            this.timer = timer;
        }

        public void Next ()
        {
            this.next = true;
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
        if (sentence != null)
        {
            if (sentence.timer > 0)
            {
                float time = (Time.time - startTime) / sentence.timer;
                slider.value = time;
                
                if (time >= 1)
                {
                    message.idx++;
                    if (message.idx < message.sentences.Count)
                    {
                        DisplaySentence();
                    }
                    else
                    {
                        Hide();
                    }
                }
            }
            else if (sentence.next)
            {
                message.idx++;
                if (message.idx < message.sentences.Count)
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
        this.sentence = message.sentences[message.idx];
        StartCoroutine(TypeSentence(sentence));
    }

    public void Hide ()
    {
        this.message = null;
        this.sentence = null;
        // HIDE
        box.SetActive(false);
    }

    IEnumerator TypeSentence (Sentence sentence)
    {
        body.text = "";
        Queue<char> letters = new Queue<char>(sentence.text.ToCharArray());

        while (letters.Count > 0)
        {
            body.text += letters.Dequeue();
            yield return null;
        }
    }
}
