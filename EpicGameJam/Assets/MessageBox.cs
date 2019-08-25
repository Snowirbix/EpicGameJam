using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    public static MessageBox instance;

    public GameObject box;
    public TextMeshProUGUI textMP;
    public Slider slider;

    protected float startTime;
    protected Message message;

    public class Message
    {
        public string text;
        public float timer = 5f;

        public Message (string text, float timer)
        {
            this.text = text;
            this.timer = timer;
        }
    }

    private void Awake ()
    {
        instance = this;
    }

    private void Start ()
    {
        Hide();
    }

    private void Update ()
    {
        if (message != null)
        {
            float time = (Time.time - startTime) / message.timer;
            slider.value = time;
            
            if (time >= 1)
            {
                Hide();
            }
        }
    }
    
    public void Display (Message message)
    {
        startTime = Time.time;
        this.message = message;
        textMP.text = message.text;
        slider.value = 0;
        // SHOW
        box.SetActive(true);
    }

    public void Hide ()
    {
        this.message = null;
        // HIDE
        box.SetActive(false);
    }

}
