using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoardEdit : MonoBehaviour
{
    private void Awake()
    {
        instance = this;
        SwichtMatUpdate();
    }
    void Start()
    {
        
    }
    #region attribute
    public bool MatAllv;
    public int currentMat;
    public int CurrentMat
    {
        get { return currentMat; }
        set 
        {
            currentMat = value;    
        }
    }
    #endregion
    #region components
    public static BoardEdit instance;
    public TMP_InputField id, X, Y;
    public Toggle[] Block;
    public Toggle MatAll;
    public Material[] mats;
    public Sprite[] sprites;
    public Transform mat_UI;
    public Button[] matItem_btn;
    public Button MainMat;
    #endregion
    #region functions
    public void OnInputChange(string name)
    {
        switch(name)
        {
            case "id":
                if(DatabaseManager.instance.CheckIfExit("Board", "id", id.text))
                {
                    string[] data= DatabaseManager.instance.GetData("Board", "id", id.text).Split("#");
                    X.text= data[1];
                    Y.text= data[2];
                    Board.instance.BoardX = int.Parse(data[1]);
                    Board.instance.BoardY = int.Parse(data[2]);
                    Board.instance.Zmap = data[3];
                    Board.instance.MapTexture = data[4];
                    //Debug.Log(data[4]);
                    StartCoroutine(Board.instance.CreateBoard(true));
                }
                else
                {
                    string[] data = new string[4];
                    data[0] = id.text;
                    data[1] = X.text;
                    data[2] = Y.text;
                    data[3] = "";
                    DatabaseManager.instance.AddData("Board", data);
                }   
                break;
            case "X":
                if (DatabaseManager.instance.CheckIfExit("Board", "id", id.text))
                {
                    DatabaseManager.instance.UpdateData("Board", "id", id.text,"X", X.text);
                    Board.instance.BoardX = int.Parse(X.text);
                    StartCoroutine(Board.instance.CreateBoard());
                }
                break;
            case "Y":
                if (DatabaseManager.instance.CheckIfExit("Board", "id", id.text))
                {
                    DatabaseManager.instance.UpdateData("Board", "id", id.text, "Y", Y.text);
                    Board.instance.BoardY = int.Parse(Y.text);
                    StartCoroutine(Board.instance.CreateBoard());
                }
                break;
        }
        SwichtMatUpdate();
    }
    public void ToggleSwicht() //onclick UI_mapEditor/GridGroup/Tgroup/Blockup..
    {
        string tokey = "";
        for (int i = 0; i < Block.Length; i++)
        {
            if(Block[i].isOn) tokey += "1";
            else tokey += "0";
        }
        switch(tokey)
        {
            case "10":
                BlockEditHeight(0);
                break;
            case "01":
                BlockEditHeight(1);
                break;               
            default:
                BlockEditHeight(-1);
                break;
        }
    }
    public void BlockEditHeight(int type)
    {
        switch (type)
        {
            case -1://remove action
                foreach(BlockpreFrag bl in Board.instance.BlockList)
                {
                    bl.OnMouseDownAction = null;
                }
                break;
            case 0://up
                foreach (BlockpreFrag bl in Board.instance.BlockList)
                {
                    bl.OnMouseDownAction = null;
                    bl.OnMouseDownAction += () => 
                    {
                        bl.transform.localScale += new Vector3(0, 0, 0.5f);
                        Board.instance.UpdateBlockAttribute(); 
                    };
                }
                break;  
            case 1://down
                foreach (BlockpreFrag bl in Board.instance.BlockList)
                {
                    bl.OnMouseDownAction = null;
                    bl.OnMouseDownAction += () =>
                    {
                        bl.transform.localScale += new Vector3(0, 0, -0.5f);
                        Board.instance.UpdateBlockAttribute(); 
                    };
                }
                break;
        }
    }
    public void Ini_Matlist(bool all=true)
    {
        matItem_btn[0].onClick.RemoveAllListeners();
        matItem_btn[0].onClick.AddListener(() =>
        {
            mat_UI.gameObject.SetActive(false);
            MainMat.GetComponent<Image>().sprite = matItem_btn[0].GetComponent<Image>().sprite;
            CurrentMat = 0;
        });
        for (int i = 1; i< matItem_btn.Length; i++)
        {
            matItem_btn[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < sprites.Length; i++)
        {
            int tempcount = i;
            matItem_btn[i + 1].gameObject.SetActive(true);
            matItem_btn[i + 1].GetComponent<Image>().sprite = sprites[i];
            matItem_btn[i + 1].onClick.RemoveAllListeners();
            matItem_btn[i + 1].onClick.AddListener(() =>
            {
                MainMat.GetComponent<Image>().sprite = sprites[tempcount];
                mat_UI.gameObject.SetActive(false);
                CurrentMat = tempcount+1;
            });
            if (MatAllv)
            {
                matItem_btn[i + 1].onClick.AddListener(() =>
                {
                  
                    if (CurrentMat > 0)
                    {
                        foreach (var bl in Board.instance.BlockList)
                        {
                            bl.GetComponent<MeshRenderer>().material = mats[CurrentMat - 1];
                        }
                        Board.instance.UpdateBlockAttribute();
                    }
                }); 
            }
            else
            {
                if (CurrentMat > 0)
                {
                    foreach (var bl in Board.instance.BlockList)
                    {
                        bl.OnMouseDownAction = null;
                        bl.OnMouseDownAction += () =>
                        {
                            bl.GetComponent<MeshRenderer>().material = mats[CurrentMat - 1];
                            Board.instance.UpdateBlockAttribute();
                        };
                    }
                }   
            }
        }
    }
    public void SwichtMatUpdate()
    {
        MatAllv = MatAll.isOn;
        Ini_Matlist(MatAllv);
    }
    #endregion
}
