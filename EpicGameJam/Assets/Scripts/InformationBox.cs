using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InformationBox : MonoBehaviour
{
    public static InformationBox instance;
    public GameObject box;
    public TextMeshProUGUI informationText;
    // Start is called before the first frame update

    protected Information information;
     public class Information
    {
        public string title;
        public Queue<string> sentences;
        public float timer = 5f;

        public Information (string[] sentences)
        {
            this.sentences = new Queue<string>(sentences);
        }
    }
    private void Awake ()
    {
        instance = this;
    }
    void Start()
    {
        if (information == null)
        {
            box.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (information != null)
        {
            if (information.sentences.Count > 0)
            {
                DisplaySentence();
            }
        }
    }

    public InformationBox Display (Information information)
    {
        this.information = information;
        DisplaySentence();
        // SHOW
        box.SetActive(true);
        return this;
    }
    protected void DisplaySentence ()
    {
        //StopAllCoroutines();
        StartCoroutine(TypeSentence(information.sentences.Dequeue()));
    }
    public void Hide ()
    {
        this.information = null;
        // HIDE
        box.SetActive(false);
    }

    IEnumerator TypeSentence (string sentence)
    {
        informationText.text = "";
        Queue<char> letters = new Queue<char>(sentence.ToCharArray());

        while (letters.Count > 1)
        {
            // add more letters if framerate is lower
            for (int i = 0; i < (int)(60 / Application.targetFrameRate); i++)
            {
                if (letters.Count > 0)
                    informationText.text += letters.Dequeue();
            }
            yield return null;
        }
    }
}
