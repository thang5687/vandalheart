using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_ver2022 : MonoBehaviour
{
    public static UI_ver2022 instance;
    public GameObject[] Database;
    public GameObject controllerMenu, manual;
    private int Player=0;
    private KeyCode newKey;
    private Text buttonText;
    private Transform MenuPanel;
    private bool WaitingForKey;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        MenuPanel = transform.GetChild(0);
        KeyChange(Player);
        StartCoroutine(CheckForControllers());
    }
    void Update()
    {
        ConfigKey();
    }
    public void Next(int change)
    {
        Player += change;
        if (Player > 3) { Player = 0; }
        if (Player < 0) { Player = 3; }
        Database[14].transform.GetChild(0).GetComponent<Text>().text = "Control " + (Player + 1);
        KeyChange(Player);
        switch (Player)
        {
            case 0:
                Database[17].GetComponent<Image>().color = Color.white;
                break;
            case 1:
                Database[17].GetComponent<Image>().color = new Color32(202,180,180,255) ;
                break;
            case 2:
                Database[17].GetComponent<Image>().color = new Color32(59, 212, 211, 255);
                break;
            case 3:
                Database[17].GetComponent<Image>().color = new Color32(218, 229, 49, 255);
                break;
        }
    }
    public void KeyChange(int Player)
    {
        Database[0].transform.GetChild(0).GetComponent<Text>().text = GameManager.GM.Up[Player].ToString();
        Database[1].transform.GetChild(0).GetComponent<Text>().text = GameManager.GM.Down[Player].ToString();
        Database[2].transform.GetChild(0).GetComponent<Text>().text = GameManager.GM.Left[Player].ToString();
        Database[3].transform.GetChild(0).GetComponent<Text>().text = GameManager.GM.Right[Player].ToString();
        Database[4].transform.GetChild(0).GetComponent<Text>().text = GameManager.GM.Select[Player].ToString(); 
        Database[5].transform.GetChild(0).GetComponent<Text>().text = GameManager.GM.Start[Player].ToString();
        Database[6].transform.GetChild(0).GetComponent<Text>().text = GameManager.GM.Fire1[Player].ToString();
        Database[7].transform.GetChild(0).GetComponent<Text>().text = GameManager.GM.Fire2[Player].ToString();
        Database[8].transform.GetChild(0).GetComponent<Text>().text = GameManager.GM.Fire3[Player].ToString();
        Database[9].transform.GetChild(0).GetComponent<Text>().text = GameManager.GM.Fire4[Player].ToString();
        Database[10].transform.GetChild(0).GetComponent<Text>().text = GameManager.GM.R1[Player].ToString();
        Database[11].transform.GetChild(0).GetComponent<Text>().text = GameManager.GM.R2[Player].ToString();
        Database[12].transform.GetChild(0).GetComponent<Text>().text = GameManager.GM.L1[Player].ToString();
        Database[13].transform.GetChild(0).GetComponent<Text>().text = GameManager.GM.L2[Player].ToString();
    }
    #region KeyConfig
    public void startAssigment(string keyName)
    {
        if (!WaitingForKey)
        {
            StartCoroutine(AssignKey(keyName));
        }
    }
    public IEnumerator AssignKey(string keyName)
    {
        buttonText = Database[int.Parse(keyName)].transform.GetChild(0).GetComponent<Text>();
        Database[int.Parse(keyName)].GetComponent<Animator>().SetBool("blink", true);
        WaitingForKey = true;
        yield return waitForKeyPress(keyName);
        if (keyName == "0")
        {
            GameManager.GM.Up[Player] = newKey;
            buttonText.text = GameManager.GM.Up[Player].ToString();
            PlayerPrefs.SetString("upkeyp" + (Player + 1), GameManager.GM.Up[Player].ToString());
            Debug.Log("upkeyp" + (Player + 1) +" = " + newKey);
        }
        else if (keyName == "1")
        {
            GameManager.GM.Down[Player] = newKey;
            buttonText.text = GameManager.GM.Down[Player].ToString();
            PlayerPrefs.SetString("downkeyp" + (Player + 1), GameManager.GM.Down[Player].ToString());
            Debug.Log("downkeyp" + (Player + 1) + " = " + newKey);
        }
        else if (keyName == "2")
        {
            GameManager.GM.Left[Player] = newKey;
            buttonText.text = GameManager.GM.Left[Player].ToString();
            PlayerPrefs.SetString("leftkeyp" + (Player + 1), GameManager.GM.Left[Player].ToString());
            Debug.Log("leftkeyp" + (Player + 1) + " = " + newKey);
        }
        else if (keyName == "3")
        {
            GameManager.GM.Right[Player] = newKey;
            buttonText.text = GameManager.GM.Right[Player].ToString();
            PlayerPrefs.SetString("rightkeyp" + (Player + 1), GameManager.GM.Right[Player].ToString());
            Debug.Log("rightkeyp" + (Player + 1) + " = " + newKey);
        }
        else if (keyName == "4")
        {
            GameManager.GM.Select[Player] = newKey;
            buttonText.text = GameManager.GM.Select[Player].ToString();
            PlayerPrefs.SetString("Selectkeyp" + (Player + 1), GameManager.GM.Select[Player].ToString());
            Debug.Log("Selectkeyp" + (Player + 1) + " = " + newKey);
        }
        else if (keyName == "5")
        {
            GameManager.GM.Start[Player] = newKey;
            buttonText.text = GameManager.GM.Start[Player].ToString();
            PlayerPrefs.SetString("Startkeyp" + (Player + 1), GameManager.GM.Start[Player].ToString());
            Debug.Log("Startkeyp" + (Player + 1) + " = " + newKey);
        }
        else if (keyName == "6")
        {
            GameManager.GM.Fire1[Player] = newKey;
            buttonText.text = GameManager.GM.Fire1[Player].ToString();
            PlayerPrefs.SetString("fire1keyp" + (Player + 1), GameManager.GM.Fire1[Player].ToString());
            Debug.Log("fire1keyp" + (Player + 1) + " = " + newKey);
        }
        else if (keyName == "7")
        {
            GameManager.GM.Fire2[Player] = newKey;
            buttonText.text = GameManager.GM.Fire2[Player].ToString();
            PlayerPrefs.SetString("fire2keyp" + (Player + 1), GameManager.GM.Fire2[Player].ToString());
            Debug.Log("fire2keyp" + (Player + 1) + " = " + newKey);
        }
        else if (keyName == "8")
        {
            GameManager.GM.Fire3[Player] = newKey;
            buttonText.text = GameManager.GM.Fire3[Player].ToString();
            PlayerPrefs.SetString("fire3keyp" + (Player + 1), GameManager.GM.Fire3[Player].ToString());
            Debug.Log("fire3keyp" + (Player + 1) + " = " + newKey);
        }
        else if (keyName == "9")
        {
            GameManager.GM.Fire4[Player] = newKey;
            buttonText.text = GameManager.GM.Fire4[Player].ToString();
            PlayerPrefs.SetString("fire4keyp" + (Player + 1), GameManager.GM.Fire4[Player].ToString());
            Debug.Log("fire4keyp" + (Player + 1) + " = " + newKey);
        }
        else if (keyName == "10")
        {
            GameManager.GM.R1[Player] = newKey;
            buttonText.text = GameManager.GM.R1[Player].ToString();
            PlayerPrefs.SetString("R1keyp" + (Player + 1), GameManager.GM.R1[Player].ToString());
            Debug.Log("R1keyp" + (Player + 1) + " = " + newKey);
        }
        else if (keyName == "11")
        {
            GameManager.GM.R2[Player] = newKey;
            buttonText.text = GameManager.GM.R2[Player].ToString();
            PlayerPrefs.SetString("R2keyp" + (Player + 1), GameManager.GM.R2[Player].ToString());
            Debug.Log("R2keyp" + (Player + 1) + " = " + newKey);
        }
        else if (keyName == "12")
        {
            GameManager.GM.L1[Player] = newKey;
            buttonText.text = GameManager.GM.L1[Player].ToString();
            PlayerPrefs.SetString("L1keyp" + (Player + 1), GameManager.GM.L1[Player].ToString());
            Debug.Log("L1keyp" + (Player + 1) + " = " + newKey);
        }
        else if (keyName == "13")
        {
            GameManager.GM.L2[Player] = newKey;
            buttonText.text = GameManager.GM.L2[Player].ToString();
            PlayerPrefs.SetString("L2keyp" + (Player + 1), GameManager.GM.L2[Player].ToString());
            Debug.Log("L2keyp" + (Player + 1) + " = " + newKey);
        }
        yield return null;
    }
    private IEnumerator waitForKeyPress(string keyName)
    {
        bool done = false;
        while (!done) // essentially a "while true", but with a bool to break out naturally
        {
            foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode) && WaitingForKey)
                {
                    newKey = kcode;
                    done = true;
                    WaitingForKey = false;
                    Database[int.Parse(keyName)].GetComponent<Animator>().SetBool("blink", false);
                   
                }
            }
            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }
        // now this function returns
    }
    private void ConfigKey(int option = 0 )
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    settingtoggle();
                }
                if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    manualtoggle();
                }           
                if (Input.GetKeyUp(KeyCode.Tab)) //
                {
                    controllerMenutoggle();
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.F1))
        {
            //GameManager.GM.ResetGame();
            SceneManager.LoadScene(0);
        }
    }
    public void PressF1Onclick()
    {
        //GameManager.GM.ResetGame();
        SceneManager.LoadScene(0);
        ShowGameMessage.Instance.Showlog("Press F1 (keyboard) to Back to MainMenu");
    }
    public void controllerMenutoggle()
    {
        if (controllerMenu.activeSelf)
        {
            controllerMenu.SetActive(false);
        }
        else
        {
            controllerMenu.SetActive(true);
        }
        MenuPanel.gameObject.SetActive(false);
        manual.gameObject.SetActive(false);
        ShowGameMessage.Instance.Showlog("Press Shift+Ctrl+Tab (keyboard) to show/hide Control-Menu");
    }
    public void settingtoggle()
    {
        if (!MenuPanel.gameObject.activeSelf)
        {
            manual.gameObject.SetActive(false);
            MenuPanel.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            MenuPanel.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
        ShowGameMessage.Instance.Showlog("Press Shift+Ctrl+1 (keyboard) to show/hide Keyboard-Setting");
    }
    public void manualtoggle()
    {
        if (!manual.gameObject.activeSelf)
        {
            MenuPanel.gameObject.SetActive(false);
            manual.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            manual.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
        ShowGameMessage.Instance.Showlog("Press Shift+Ctrl+2 (keyboard) to show/hide Game-Manual");
    }
    #endregion
    #region detectGamepad
    private bool connected = false;
    public int controlerLength;
    public Image[] controller;
    IEnumerator CheckForControllers()
    {
        while (true)
        {
            var controllers = Input.GetJoystickNames();
            if(controlerLength== controllers.Length)
            {
                //do nothing
            }
            else
            {
                controlerLength = controllers.Length;
                if (!connected && controllers.Length > 0)
                {
                    connected = true;
                    ShowGameMessage.Instance.Showlog(controlerLength + " Gamecontroller Connected");
                    //Debug.Log("Connected");
                }
                else if (connected && controllers.Length == 0)
                {
                    connected = false;
                    ShowGameMessage.Instance.Showlog("All controller Disconnected");
                    for(int i = 0; i < controller.Length; i++)
                    {
                        controller[i].color = new Color(1, 1, 1, 0.2f);
                    }
                    //Debug.Log("Disconnected");
                }
                for (int i = 0; i < controllers.Length; i++)
                {
                    ShowGameMessage.Instance.Showlog("Controller" + (i + 1) + " " + Input.GetJoystickNames()[i] + " is connected");
                    controller[i].color = Color.white;
                    //Debug.Log(Input.GetJoystickNames()[i] + " is connected");
                }
            } 
            yield return new WaitForSeconds(5f);
        }
    }
    public void checkControllerConnection(int pos)
    {
        var controllers = Input.GetJoystickNames();
        if(controllers.Length > pos) 
        {
            ShowGameMessage.Instance.Showlog("Controller " + (pos + 1)  + " is connected");
        }
        else
        {
            ShowGameMessage.Instance.Showlog("Controller " + (pos + 1) + " is disconnected");
        }
    }
    #endregion
    #region debuglog
    public GameObject debugobj;
    public Text dbtext;
    public void showerrorlog(string msg)
    {
        debugobj.SetActive(true);
        dbtext.text = msg;
    }
    #endregion
}
