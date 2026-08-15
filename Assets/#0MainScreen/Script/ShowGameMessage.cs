using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShowGameMessage : MonoBehaviour
{
    public static ShowGameMessage Instance;
    public GameObject Logobj;
    public TextMeshProUGUI txtlog;
    private float showtime;
    public Image[] Character_portrait;
    public Sprite[] Character_sprlist;
    private void Awake()
    {
        Instance = this;
        Logobj.SetActive(false);
    }
    void Start()
    {
        //Showlog("hello hello", 2, 0);
    }
    private void FixedUpdate()
    {
        if (showtime == -100f) return;
        if (showtime > 0) showtime -= Time.deltaTime;
        if(showtime <= 0)
        {
            showtime = -100f;
            Logobj.SetActive(false);
            for(int i = 0; i < Character_portrait.Length; i++)
            {
                Character_portrait[i].enabled = false;
            }
        }
          
    }
    public void Showlog(string message)
    {
        showtime = 3f;
        Logobj.SetActive(true);
        Debug.Log(message);
        txtlog.text = message + "\n";
        //StartCoroutine(showlogco(message));
    }
    public void Showlog(string message, int Character_portrait_number, int Character_spr)
    {
        Image image = Character_portrait[Character_portrait_number];
        image.sprite = Character_sprlist[Character_spr];
        image.enabled = true;
        showtime = 3f;
        Logobj.SetActive(true);
        Debug.Log(message);
        txtlog.text = message + "\n";
        //StartCoroutine(showlogco(message));
    }
    public void ShowlogInfinite(string message)
    {
        showtime = 99f;
        Logobj.SetActive(true);
        //Debug.Log(message);
        txtlog.text = message + "\n";
    }
    public void Hidelog()
    {
        Logobj.SetActive(false);
    }
    private IEnumerator showlogco(string message)
    {
        Logobj.SetActive(true);
        Debug.Log(message);
        txtlog.text = message + "\n";
        yield return new WaitForSeconds(3f);
        Logobj.SetActive(false);
    }
}
