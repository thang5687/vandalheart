using System;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public bool[] L2Down, R2Down, downUp, downDown, upUp,upDown ,leftUp, leftDown, rightUp, rightDown, L2Up,R2Up;
    public KeyCode[] Fire1 { get; set; }   public KeyCode[] Fire2 { get; set; } public KeyCode[] Fire3{ get; set; }  public KeyCode[] Fire4 { get; set; }
    public KeyCode[] L1 { get; set; }   public KeyCode[] L2 { get; set; }  public KeyCode[] R1 { get; set; }   public KeyCode[] R2 { get; set; }
    public KeyCode[] Start { get; set; }   public KeyCode[] Select { get; set; }  public KeyCode[] Up { get; set; }   public KeyCode[] Down { get; set; }  public KeyCode[] Left { get; set; }   public KeyCode[] Right { get; set; }
    private void Awake()
    {
        Fire1 = new KeyCode[9];Fire2 = new KeyCode[9];Fire3 = new KeyCode[9]; Fire4 = new KeyCode[9]; L1 = new KeyCode[9];L2 = new KeyCode[9]; R1 = new KeyCode[9];R2 = new KeyCode[9];
        Select = new KeyCode[9]; Start = new KeyCode[9];Up = new KeyCode[9];  Down = new KeyCode[9];  Left = new KeyCode[9];  Right = new KeyCode[9]; 
        L2Down = new bool[4]; R2Down = new bool[4]; downUp = new bool[4]; downDown = new bool[4]; upUp = new bool[4]; upDown = new bool[4];
        leftUp = new bool[4]; leftDown = new bool[4]; rightUp = new bool[4]; rightDown = new bool[4]; L2Up = new bool[4]; R2Up = new bool[4];

        Act_DownDown = new Action[9];
        Act_UpDown = new Action[9];
        Act_LeftDown = new Action[9];
        Act_RightDown = new Action[9];
        Act_Fire1Down = new Action[9];
        Act_Fire2Down = new Action[9];
        Act_Fire3Down = new Action[9];
        Act_Fire4Down = new Action[9];
        Act_StartDown = new Action[9];
        Act_SelectDown = new Action[9];
        Act_L1Down = new Action[9];
        Act_L2Down = new Action[9];
        Act_R1Down = new Action[9];
        Act_R2Down = new Action[9];
        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else if (GM != this)
        {
            Destroy(gameObject);
        }
        if(PlayerPrefs.GetInt("firststartupgame", 0)==0)
        {
            PlayerPrefs.SetInt("firststartupgame", 1);
            setdefaultkey();
        }

        for (int i = 0; i <= 3; i++)
        {
            Fire1[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("fire1keyp" + (i + 1), "J"));
            Fire2[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("fire2keyp" + (i + 1), "K"));
            Fire3[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("fire3keyp" + (i + 1), "L"));
            Fire4[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("fire4keyp" + (i + 1), "I"));
            L1[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("L1keyp" + (i + 1), "Q"));
            L2[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("L2keyp" + (i + 1), "E"));
            R1[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("R1keyp" + (i + 1), "U"));
            R2[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("R2keyp" + (i + 1), "O"));
            Select[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Selectkeyp" + (i + 1), "B"));
            Start[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Startkeyp" + (i + 1), "N"));
            Up[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("upkeyp" + (i + 1), "W"));
            Down[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("downkeyp" + (i + 1), "S"));
            Left[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftkeyp" + (i + 1), "A"));
            Right[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightkeyp" + (i + 1), "D"));
        }
    }
    public void setdefaultkey()
    {
        PlayerPrefs.SetString("upkeyp1" ,"W");
        PlayerPrefs.SetString("downkeyp1", "S");
        PlayerPrefs.SetString("leftkeyp1", "A");
        PlayerPrefs.SetString("rightkeyp1", "D");
        PlayerPrefs.SetString("Selectkeyp1", "B");
        PlayerPrefs.SetString("Startkeyp1", "N");
        PlayerPrefs.SetString("fire1keyp1", "I");
        PlayerPrefs.SetString("fire2keyp1", "J");
        PlayerPrefs.SetString("fire3keyp1", "K");
        PlayerPrefs.SetString("fire4keyp1", "L");
        PlayerPrefs.SetString("L1keyp1", "Q");
        PlayerPrefs.SetString("L2keyp1", "E");
        PlayerPrefs.SetString("R1keyp1", "U");
        PlayerPrefs.SetString("R2keyp1", "O");

        PlayerPrefs.SetString("upkeyp2", "UpArrow");
        PlayerPrefs.SetString("downkeyp2", "DownArrow");
        PlayerPrefs.SetString("leftkeyp2", "LeftArrow");
        PlayerPrefs.SetString("rightkeyp2", "RightArrow");
        PlayerPrefs.SetString("Selectkeyp2", "Keypad0");
        PlayerPrefs.SetString("Startkeyp2", "KeypadPeriod");
        PlayerPrefs.SetString("fire1keyp2", "Keypad5");
        PlayerPrefs.SetString("fire2keyp2", "Keypad2");
        PlayerPrefs.SetString("fire3keyp2", "Keypad1");
        PlayerPrefs.SetString("fire4keyp2", "Keypad3");
        PlayerPrefs.SetString("L1keyp2", "Keypad4");
        PlayerPrefs.SetString("L2keyp2", "Keypad7");
        PlayerPrefs.SetString("R1keyp2", "Keypad6");
        PlayerPrefs.SetString("R2keyp2", "Keypad9");

        PlayerPrefs.SetString("upkeyp3", "C");
        PlayerPrefs.SetString("downkeyp3", "C");
        PlayerPrefs.SetString("leftkeyp3", "C");
        PlayerPrefs.SetString("rightkeyp3", "C");
        PlayerPrefs.SetString("Selectkeyp3", "C");
        PlayerPrefs.SetString("Startkeyp3", "C");
        PlayerPrefs.SetString("fire1keyp3", "C");
        PlayerPrefs.SetString("fire2keyp3", "C");
        PlayerPrefs.SetString("fire3keyp3", "C");
        PlayerPrefs.SetString("fire4keyp3", "C");
        PlayerPrefs.SetString("L1keyp3", "C");
        PlayerPrefs.SetString("L2keyp3", "C");
        PlayerPrefs.SetString("R1keyp3", "C");
        PlayerPrefs.SetString("R2keyp3", "C"); 
        
        PlayerPrefs.SetString("upkeyp4", "C");
        PlayerPrefs.SetString("downkeyp4", "C");
        PlayerPrefs.SetString("leftkeyp4", "C");
        PlayerPrefs.SetString("rightkeyp4", "C");
        PlayerPrefs.SetString("Selectkeyp4", "C");
        PlayerPrefs.SetString("Startkeyp4", "C");
        PlayerPrefs.SetString("fire1keyp4", "C");
        PlayerPrefs.SetString("fire2keyp4", "C");
        PlayerPrefs.SetString("fire3keyp4", "C");
        PlayerPrefs.SetString("fire4keyp4", "C");
        PlayerPrefs.SetString("L1keyp4", "C");
        PlayerPrefs.SetString("L2keyp4", "C");
        PlayerPrefs.SetString("R1keyp4", "C");
        PlayerPrefs.SetString("R2keyp4", "C");
    }
    #region up button control
    public bool HoldButtonUp(int player)
    {
        if (Input.GetKey(Up[player]) || Input.GetAxis("LeftUD"+(player + 1)) <0 || Input.GetAxis("UD" + (player + 1)) > 0)
        {
            return true;
        }
        else return false;
    }
    public bool UpDown(int player)
    {
        if (Input.GetKeyDown(Up[player]) || AxisUpToGetKeyDown(player))
        {
            return true;
        }
        else return false;
    }
    public bool AxisUpToGetKeyDown(int player)
    {
        if (Input.GetAxis("LeftUD" + (player + 1)) < 0 || Input.GetAxis("UD" + (player + 1)) > 0)
        {
            if (upDown[player] == false)
            {
                upDown[player] = true;
                return true;
            }
            else return false;
        }
        else if (Input.GetAxis("LeftUD" + (player + 1)) == 0 || Input.GetAxis("UD" + (player + 1)) == 0)
        {
            upDown[player] = false;
            return false;
        }
        return false;
    }
    public bool UpUp(int player)
    {
        if (Input.GetKeyUp(Up[player]) || AxisUpToGetKeyUp(player))
        {
            return true;
        }
        else return false;
    }
    public bool AxisUpToGetKeyUp(int player)
    {
        if (Input.GetAxis("LeftUD" + (player + 1)) < 0 || Input.GetAxis("UD" + (player + 1)) > 0)
        {
            upUp[player] = true;
            return false;
        }
        else if (Input.GetAxis("LeftUD" + (player + 1)) == 0 || Input.GetAxis("UD" + (player + 1)) == 0)
        {
            if (upUp[player] == true)
            {
                upUp[player] = false;
                return true;
            }
            else return false;
        }
        else return false;
    }
    #endregion
    #region down button control
    public bool HoldButtonDown(int player)
    {
        if (Input.GetKey(Down[player]) || Input.GetAxis("LeftUD" + (player + 1)) > 0 || Input.GetAxis("UD" + (player + 1)) < 0)
        {
            return true;
        }
        else return false;
    }
    public bool DownDown(int player)
    {
        if (Input.GetKeyDown(Down[player]) || AxisDownToGetKeyDown(player))
        {
            return true;
        }
        else return false;
    }
    public bool AxisDownToGetKeyDown(int player)
    {
        if (Input.GetAxis("LeftUD" + (player + 1)) > 0 || Input.GetAxis("UD" + (player + 1)) < 0)
        {
            if (downDown[player] == false)
            {
                downDown[player] = true;
                return true;
            }
            else return false;
        }
        else if (Input.GetAxis("LeftUD" + (player + 1)) == 0 || Input.GetAxis("UD" + (player + 1)) == 0)
        {
            downDown[player] = false;
            return false;
        }
        return false;
    }
    public bool DownUp(int player)
    {
        if (Input.GetKeyUp(Down[player]) || AxisDownToGetKeyUp(player))
        {
            return true;
        }
        else return false;
    }
    public bool AxisDownToGetKeyUp(int player)
    {
        if (Input.GetAxis("LeftUD" + (player + 1)) > 0 || Input.GetAxis("UD" + (player + 1)) < 0)
        {  
            downUp[player] = true;  
            return false;
        }
        else if (Input.GetAxis("LeftUD" + (player + 1)) == 0 || Input.GetAxis("UD" + (player + 1)) == 0)
        {
            if (downUp[player] == true)
            {
                downUp[player] = false;
                return true;
            }
            else return false;
        }
        else return false;
    }
    #endregion
    #region left button control
    public bool HoldButtonLeft(int player)
    {
        if (Input.GetKey(Left[player]) || Input.GetAxis("LeftLR" + (player + 1)) < 0 || Input.GetAxis("LR" + (player + 1)) < 0)
        {
            return true;
        }
        else return false;
    }
    public bool LeftDown(int player)
    {
        if (Input.GetKeyDown(Left[player]) || AxisLeftToGetKeyDown(player))
        {
            return true;
        }
        else return false;
    }
    public bool AxisLeftToGetKeyDown(int player)
    {
        if (Input.GetAxis("LeftLR" + (player + 1)) < 0 || Input.GetAxis("LR" + (player + 1)) < 0)
        {
            if (leftDown[player] == false)
            {
                leftDown[player] = true;
                return true;
            }
            else return false;
        }
        else if (Input.GetAxis("LeftLR" + (player + 1)) == 0 || Input.GetAxis("LR" + (player + 1)) == 0)
        {
            leftDown[player] = false;
            return false;
        }
        return false;
    }
    public bool LeftUp(int player)
    {
        if (Input.GetKeyUp(Left[player]) || AxisLeftToGetKeyUp(player))
        {
            return true;
        }
        else return false;
    }
    public bool AxisLeftToGetKeyUp(int player)
    {
        if (Input.GetAxis("LeftLR" + (player + 1)) < 0 || Input.GetAxis("LR" + (player + 1)) < 0)
        {
            leftUp[player] = true;
            return false;
        }
        else if (Input.GetAxis("LeftLR" + (player + 1)) == 0 || Input.GetAxis("LR" + (player + 1)) == 0)
        {
            if (leftUp[player] == true)
            {
                leftUp[player] = false;
                return true;
            }
            else return false;
        }
        else return false;
    }
    #endregion
    #region right button control
    public bool HoldButtonRight(int player)
    {
        if (Input.GetKey(Right[player]) || Input.GetAxis("LeftLR" + (player + 1)) > 0 || Input.GetAxis("LR" + (player + 1)) > 0)
        {
            return true;
        }
        else return false;
    }
    public bool RightDown(int player)
    {
        if (Input.GetKeyDown(Right[player]) || AxisRightToGetKeyDown(player))
        {
            return true;
        }
        else return false;
    }
    public bool AxisRightToGetKeyDown(int player)
    {
        if (Input.GetAxis("LeftLR" + (player + 1)) > 0 || Input.GetAxis("LR" + (player + 1)) > 0)
        {
            if (rightDown[player] == false)
            {
                rightDown[player] = true;
                return true;
            }
            else return false;
        }
        else if (Input.GetAxis("LeftLR" + (player + 1)) == 0 || Input.GetAxis("LR" + (player + 1)) == 0)
        {
            rightDown[player] = false;
            return false;
        }
        return false;
    }
    public bool RightUp(int player)
    {
        if (Input.GetKeyUp(Right[player]) || AxisRightToGetKeyUp(player))
        {
            return true;
        }
        else return false;
    }
    public bool AxisRightToGetKeyUp(int player)
    {
        if (Input.GetAxis("LeftLR" + (player + 1)) > 0 || Input.GetAxis("LR" + (player + 1)) > 0)
        {
            rightUp[player] = true;
            return false;
        }
        else if (Input.GetAxis("LeftLR" + (player + 1)) == 0 || Input.GetAxis("LR" + (player + 1)) == 0)
        {
            if (rightUp[player] == true)
            {
                rightUp[player] = false;
                return true;
            }
            else return false;
        }
        else return false;
    }
    #endregion
    #region fire buttons control
    public bool HoldFire1(int player)
    {
        if (Input.GetKey(Fire1[player]) || Input.GetKey("joystick " + (player + 1) + " button 0"))
        {
            return true;
        }
        else return false;
    }
    public bool DownFire1(int player)
    {
        if (Input.GetKeyDown(Fire1[player]) || Input.GetKeyDown("joystick " + (player + 1) + " button 0"))
        {
            return true;
        }
        else return false;
    }
    public bool UpFire1(int player)
    {
        if (Input.GetKeyUp(Fire1[player]) || Input.GetKeyUp("joystick " + (player + 1) + " button 0"))
        {
            return true;
        }
        else return false;
    }
    public bool HoldFire2(int player)
    {
        if (Input.GetKey(Fire2[player]) || Input.GetKey("joystick " + (player + 1) + " button 2"))
        {
            return true;
        }
        else return false;
    }
    public bool DownFire2(int player)
    {
        if (Input.GetKeyDown(Fire2[player]) || Input.GetKeyDown("joystick " + (player + 1) + " button 2"))
        {
            return true;
        }
        else return false;
    }
    public bool UpFire2(int player)
    {
        if (Input.GetKeyUp(Fire2[player]) || Input.GetKeyUp("joystick " + (player + 1) + " button 2"))
        {
            return true;
        }
        else return false;
    }
    public bool HoldFire3(int player)
    {
        if (Input.GetKey(Fire3[player]) || Input.GetKey("joystick " + (player + 1) + " button 1"))
        {
            return true;
        }
        else return false;
    }
    public bool DownFire3(int player)
    {
        if (Input.GetKeyDown(Fire3[player]) || Input.GetKeyDown("joystick " + (player + 1) + " button 1"))
        {
            return true;
        }
        else return false;
    }
    public bool UpFire3(int player)
    {
        if (Input.GetKeyUp(Fire3[player]) || Input.GetKeyUp("joystick " + (player + 1) + " button 1"))
        {
            return true;
        }
        else return false;
    }
    public bool HoldFire4(int player)
    {
        if (Input.GetKey(Fire4[player]) || Input.GetKey("joystick " + (player + 1) + " button 3"))
        {
            return true;
        }
        else return false;
    }
    public bool DownFire4(int player)
    {
        if (Input.GetKeyDown(Fire4[player]) || Input.GetKeyDown("joystick " + (player + 1) + " button 3"))
        {
            return true;
        }
        else return false;
    }
    public bool UpFire4(int player)
    {
        if (Input.GetKeyUp(Fire4[player]) || Input.GetKeyUp("joystick " + (player + 1) + " button 3"))
        {
            return true;
        }
        else return false;
    }
    #endregion
    #region Start Select control
    public bool DownStart(int player)
    {
        if (Input.GetKeyDown(Start[player]) || Input.GetKeyDown("joystick " + (player + 1) + " button 7"))
        {
            return true;
        }
        else return false;
    }
    public bool UpStart(int player)
    {
        if (Input.GetKeyUp(Start[player]) || Input.GetKeyUp("joystick " + (player + 1) + " button 7"))
        {
            return true;
        }
        else return false;
    }
    public bool HoldStart(int player)
    {
        if (Input.GetKey(Start[player]) || Input.GetKey("joystick " + (player + 1) + " button 7"))
        {
            return true;
        }
        else return false;
    }
    public bool DownSelect(int player)
    {
        if (Input.GetKeyDown(Select[player]) || Input.GetKeyDown("joystick " + (player + 1) + " button 6"))
        {
            return true;
        }
        else return false;
    }
    public bool UpSelect(int player)
    {
        if (Input.GetKeyUp(Select[player]) || Input.GetKeyUp("joystick " + (player + 1) + " button 6"))
        {
            return true;
        }
        else return false;
    }
    public bool HoldSelect(int player)
    {
        if (Input.GetKey(Select[player]) || Input.GetKey("joystick " + (player + 1) + " button 6"))
        {
            return true;
        }
        else return false;
    }
    #endregion
    #region LR control
    public bool HoldButtonL1(int player)
    {
        if (Input.GetKey(L1[player]) ||  Input.GetKey("joystick " + (player + 1) + " button 4"))
        {
            return true;
        }
        else return false;
    }
    public bool DownL1(int player)
    {
        if (Input.GetKeyDown(L1[player]) || Input.GetKeyDown("joystick " + (player + 1) + " button 4"))
        {
            return true;
        }
        else return false;
    }
    public bool UpL1(int player)
    {
        if (Input.GetKeyUp(L1[player]) || Input.GetKeyUp("joystick " + (player + 1) + " button 4"))
        {
            return true;
        }
        else return false;
    }
    public bool HoldButtonR1(int player)
    {
        if (Input.GetKey(R1[player]) || Input.GetKey("joystick " + (player + 1) + " button 5"))
        {
            return true;
        }
        else return false;
    }
    public bool DownR1(int player)
    {
        if (Input.GetKeyDown(R1[player]) || Input.GetKeyDown("joystick " + (player + 1) + " button 5"))
        {
            return true;
        }
        else return false;
    }
    public bool UpR1(int player)
    {
        if (Input.GetKeyUp(R1[player]) || Input.GetKeyUp("joystick " + (player + 1) + " button 5"))
        {
            return true;
        }
        else return false;
    }
    public bool HoldButtonL2(int player)
    {
        if (Input.GetKey(L2[player]) || Input.GetAxis("L2R2" + (player + 1)) < 0)
        {
            return true;
        }
        else return false;
    }
    public bool DownL2(int player)
    {
        if (Input.GetKeyDown(L2[player]) || AxisL2ToGetKeyDown(player))
        {
            return true;
        }
        else return false;
    }
    public bool AxisL2ToGetKeyDown(int player)
    {
        if (Input.GetAxis("L2R2" + (player + 1)) < 0)
        {
            if (L2Down[player] == false)
            {
                L2Down[player] = true;
                return true;
            }
            else return false;
        }
        else if (Input.GetAxis("L2R2" + (player + 1)) == 0)
        {
            L2Down[player] = false;
            return false;
        }
        return false;
    }
    public bool UpL2(int player)
    {
        if (Input.GetKeyUp(L2[player]) || AxisL2ToGetKeyUp(player))
        {
            return true;
        }
        else return false;
    }
    public bool AxisL2ToGetKeyUp(int player)
    {
        if (Input.GetAxis("L2R2" + (player + 1)) < 0)
        {
            L2Up[player] = true;
            return false;
        }
        else if (Input.GetAxis("L2R2" + (player + 1)) == 0)
        {
            if (L2Up[player] == true)
            {
                L2Up[player] = false;
                return true;
            }
            else return false;
        }
        else return false;
    }
    public bool HoldButtonR2(int player)
    {
        if (Input.GetKey(R2[player]) || Input.GetAxis("L2R2" + (player + 1)) > 0)
        {
            return true;
        }
        else return false;
    }
    public bool DownR2(int player)
    {
        if (Input.GetKeyDown(R2[player]) || AxisR2ToGetKeyDown(player))
        {
            return true;
        }
        else return false;
    }
    public bool AxisR2ToGetKeyDown(int player)
    {
        if (Input.GetAxis("L2R2" + (player + 1)) > 0)
        {
            if (R2Down[player] == false)
            {
                R2Down[player] = true;
                return true;
            }
            else return false;
        }
        else if (Input.GetAxis("L2R2" + (player + 1)) == 0)
        {
            R2Down[player] = false;
            return false;
        }
        return false;
    }
    public bool UpR2(int player)
    {
        if (Input.GetKeyUp(R2[player]) || AxisR2ToGetKeyUp(player))
        {
            return true;
        }
        else return false;
    }
    public bool AxisR2ToGetKeyUp(int player)
    {
        if (Input.GetAxis("L2R2" + (player + 1)) > 0)
        {
            R2Up[player] = true;
            return false;
        }
        else if (Input.GetAxis("L2R2" + (player + 1)) == 0)
        {
            if (R2Up[player] == true)
            {
                R2Up[player] = false;
                return true;
            }
            else return false;
        }
        else return false;
    }
    #endregion

    private void Update()
    {
        PlayerPressUpdate(0);
        PlayerPressUpdate(1);
        PlayerPressUpdate(2);
        PlayerPressUpdate(3);
    }
    #region Components
    public Action[] Act_DownDown;
    public Action[] Act_UpDown;
    public Action[] Act_LeftDown;
    public Action[] Act_RightDown;
    public Action[] Act_Fire1Down;
    public Action[] Act_Fire2Down;
    public Action[] Act_Fire3Down;
    public Action[] Act_Fire4Down;
    public Action[] Act_StartDown;
    public Action[] Act_SelectDown;
    public Action[] Act_L1Down;
    public Action[] Act_L2Down;
    public Action[] Act_R1Down;
    public Action[] Act_R2Down;
    #endregion
    #region Functions
    public void PlayerPressUpdate(int player)
    {
        if (GameManager.GM.DownDown(player))
        {
            if (Act_DownDown[player] != null) { Act_DownDown[player].Invoke(); }
        }
        if (GameManager.GM.UpDown(player))
        {
            if (Act_UpDown[player] != null) { Act_UpDown[player].Invoke(); }
        }
        if (GameManager.GM.LeftDown(player))
        {
            if (Act_LeftDown[player] != null) { Act_LeftDown[player].Invoke(); }
        }
        if (GameManager.GM.RightDown(player))
        {
            if (Act_RightDown[player] != null) { Act_RightDown[player].Invoke(); }
        }
        if (GameManager.GM.DownFire1(player))
        {
            if (Act_Fire1Down[player] != null) { Act_Fire1Down[player].Invoke(); }
        }
        if (GameManager.GM.DownFire2(player))
        {
            if (Act_Fire2Down[player] != null) { Act_Fire2Down[player].Invoke(); }
        }
        if (GameManager.GM.DownFire3(player))
        {
            if (Act_Fire3Down[player] != null) { Act_Fire3Down[player].Invoke(); }
        }
        if (GameManager.GM.DownFire4(player))
        {
            if (Act_Fire4Down[player] != null) { Act_Fire4Down[player].Invoke(); }
        }
        if (GameManager.GM.DownStart(player))
        {
            if (Act_StartDown[player] != null) { Act_StartDown[player].Invoke(); }
        }
        if (GameManager.GM.DownSelect(player))
        {
            if (Act_SelectDown[player] != null) { Act_SelectDown[player].Invoke(); }
        }
        if (GameManager.GM.DownL1(player))
        {
            if (Act_L1Down[player] != null) { Act_L1Down[player].Invoke(); }
        }
        if (GameManager.GM.DownL2(player))
        {
            if (Act_L2Down[player] != null) { Act_L2Down[player].Invoke(); }
        }
        if (GameManager.GM.DownR1(player))
        {
            if (Act_R1Down[player] != null) { Act_R1Down[player].Invoke(); }
        }
        if (GameManager.GM.DownR2(player))
        {
            if (Act_R2Down[player] != null) { Act_R2Down[player].Invoke(); }
        }
    } 
    #endregion
}
