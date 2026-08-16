using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterContr : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CharId+=0;
    }
    private void OnMouseDown()
    {
        if (AllCharaterControl.State == 0)
        {
            AllCharaterControl.State = 1;
            abilityCanvas.SetActive(true);
            MouseClick.instance.onrightMouseClick = null;
            MouseClick.instance.onrightMouseClick += () =>
            {
                AllCharaterControl.State = 0;
                abilityCanvas.SetActive(false);
                MouseClick.instance.onrightMouseClick = null;
            };
        }
           
    }
    #region Attributes
    public int state;
    public int State
    {
        get { return state; }
        set 
        {
            state = value;
            switch(state)
            {
                case 0:
                    break;
                case 1:
                    abilityCanvas.SetActive(false);
                    break;
                case 2:
                    break;
            }
        }
    }
    public Vector3 currentPos;
    private float posX, posY,posZ;
    public float PosX
    {
        get { return posX; }
        set
        {
            posX = value;
            currentPos.x = posX;
            CharPrefrag.transform.position = currentPos;
            transform.position = currentPos;
        }
    }
    public float PosY
    {
        get { return posY; }
        set 
        { 
            posY = value;
            currentPos.y = posY;
            CharPrefrag.transform.position = currentPos;
            transform.position = currentPos;
        }
    }
    public float PosZ
    {
        get { return posZ; }
        set
        {
            posZ = value;
            currentPos.z = posZ;
            CharPrefrag.transform.position = currentPos;
            transform.position = currentPos;
        }
    }
    public int charId;
    public int CharId
    {
        get { return charId; }
        set 
        { 
            charId = value;
            Mesh uniqueMesh = mesh[value];
            CharPrefrag.GetComponent<MeshFilter>().sharedMesh = uniqueMesh;
            Material mat = mats[value];
            CharPrefrag.GetComponent<MeshRenderer>().material = mat;
            if (!DatabaseManager.instance.CheckIfExit("Character","id", value.ToString()))
            {
                DatabaseManager.instance.AddAField("Character", "id", value.ToString());           
            }
            else
            {
                string[] loaddata = DatabaseManager.instance.GetData("Character", "id", value.ToString()).Split("#");
                //Debug.Log(loaddata[2]);
                CharacterStatus_load(loaddata[2]);
            }
        }
    }
    public int Armyside;

    #region BaseStatus
    public int MovePow
    {
        get { return 3+Add_Move; }
    }
    public int DescentPower
    {
        get { return 3+Add_Descent; }
    }
    public int ClimbPower
    {
        get { return 2+Add_Climb; }
    }
    public int ActionPoint
    {
        get { return 3+ Add_ActionPoint; }
    }
    #endregion
    #region SaveStatusDatabase
    string[] loaddata;
    private int add_move, add_descent, add_climb, add_actionpoint;
    private int Add_Move
    {
        get { return add_move; }
        set 
        { 
            add_move = value;
            CharacterStatus_save();
        }
    }
    private int Add_Descent
    {
        get { return add_descent; }
        set
        {
            add_descent = value; CharacterStatus_save();
        }
    }
    private int Add_Climb
    {
        get { return add_climb; }
        set
        {
            add_climb = value; CharacterStatus_save();
        }
    }
    private int Add_ActionPoint
    {
        get { return add_actionpoint; }
        set
        {
            add_actionpoint = value; CharacterStatus_save();
        }
    }
    #endregion
    #endregion
    #region Components
    public GameObject abilityCanvas;
    public GameObject CharPrefrag;
    public GameObject abilityPreFrag;
    public List<GameObject> AbilityList= new List<GameObject>();
    public Mesh[] mesh;
    public Material[] mats;
    public BlockpreFrag currentstandingbl
    {
        get { return Board.instance.GetBlockat(Mathf.RoundToInt(posX), Mathf.RoundToInt(PosY)); }
    }
    #endregion
    #region Functions
    public void UnregisterButtons(int player)
    {
        GameManager.GM.Act_DownDown[player] = null;
        GameManager.GM.Act_UpDown[player] = null;
        GameManager.GM.Act_LeftDown[player] = null;
        GameManager.GM.Act_RightDown[player] = null;
        GameManager.GM.Act_Fire1Down[player] = null;
        GameManager.GM.Act_Fire2Down[player] = null;
        GameManager.GM.Act_Fire3Down[player] = null;
        GameManager.GM.Act_Fire4Down[player] = null;
    }
    public void RegisterButtons(int player)
    {
        GameManager.GM.Act_DownDown[player] = null;
        GameManager.GM.Act_UpDown[player] = null;
        GameManager.GM.Act_LeftDown[player] = null;
        GameManager.GM.Act_RightDown[player] = null;
        GameManager.GM.Act_Fire1Down[player] = null;
        GameManager.GM.Act_Fire2Down[player] = null;
        GameManager.GM.Act_Fire3Down[player] = null;
        GameManager.GM.Act_Fire4Down[player] = null;
        GameManager.GM.Act_DownDown[player] += () =>
        {

        };
        GameManager.GM.Act_UpDown[player] += () =>
        {

        };
        GameManager.GM.Act_LeftDown[player] += () =>
        {

        };
        GameManager.GM.Act_RightDown[player] += () =>
        {

        };
        GameManager.GM.Act_Fire1Down[player] += () =>
        {
        };
        GameManager.GM.Act_Fire2Down[player] += () =>
        {
        };
        GameManager.GM.Act_Fire3Down[player] += () =>
        {
        };
        GameManager.GM.Act_Fire4Down[player] += () =>
        {
        };
        Debug.Log(this.name + " Mode0");
    }
    public void AddAbility(string AbiName)
    {
        GameObject newAbi = Instantiate(abilityPreFrag, abilityCanvas.transform);
        newAbi.GetComponentInChildren<TextMeshProUGUI>().text = AbiName;
        AbilityList.Add(newAbi);
    }
    public void RemoveAbility(string AbiName)
    {
        foreach (var abi in AbilityList)
        {
            if (abi.GetComponentInChildren<TextMeshProUGUI>().text == AbiName)
            {
                Destroy(abi);
            }
        }
    } 
    public void CharacterStatus_load(string statusString)
    {
        if (statusString.Length<=0)
        {
            CharacterStatus_save();
            return;
        }
        loaddata = statusString.Split("@@");
        add_move = int.Parse(loaddata[0]);
        add_descent = int.Parse(loaddata[1]);
        add_climb = int.Parse(loaddata[2]);
        add_actionpoint = int.Parse(loaddata[3]);
    }
    public void CharacterStatus_save()
    {
        string savedata = "";
        savedata += Add_Move+"@@";
        savedata += Add_Descent + "@@";
        savedata += Add_Climb + "@@";
        savedata += Add_ActionPoint;
        DatabaseManager.instance.UpdateData("Character", "id", CharId.ToString(),"status", savedata);
    }
    #endregion
}
