using UnityEngine;
using UnityEngine.UI;

public class WorldMapEdit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }
    private void OnDisable()
    {
        DeactiveMode();
    }
    #region att
    private int mode;
    public int Mode
    {
        get { return mode; }
        set
        {
            DeactiveMode();
            mode = value;
            switch(mode)
            {
                case 0:
                    GameObject temp = Instantiate(CreatePreFrag, LocList);
                    CurrentItem = temp.transform;
                    WorldMap.instance.LocationList.Add(temp);
                    activebtn(0);
                    ActiveMode0();
                    Mode = 1;
                    break;                 
                case 1:
                    activebtn(1);
                    ActiveMode1();
                    break;
                case 2:
                    activebtn(2);
                    break;
                case 3:
                    ActiveMode3();
                    activebtn(3);
                    break;
            }
        }
    }
    private int iconid;
    public int Iconid
    {
        get { return iconid; }
        set 
        { 
            iconid = value;
            iconid = Mathf.Clamp(iconid, 0, WorldMap.instance.sprIcon.Length-1);
            CurrentItem.GetComponent<SpriteRenderer>().sprite = WorldMap.instance.sprIcon[iconid];
        }
    }
    #endregion
    #region com
    public static WorldMapEdit instance;
    public Button[] btn;
    public GameObject CreatePreFrag;
    private Transform currentitem;
    public Transform CurrentItem
    {
        get { return currentitem; }
        set 
        { 
            currentitem = value; 
            //disableAll other
            foreach(var loc in WorldMap.instance.LocationList)
            {
                loc.GetComponent<LocationItem>().AlllineDeactive();
            }
            if(Mode == 2)
            CurrentItem.GetComponent<LocationItem>().AllLineactive();
        }
    }
    public Transform LocList
    {
        get { return WorldMap.instance.LocList; }
    }
    #endregion
    #region func
    public void DeactiveMode()
    {
        GameManager.GM.Act_DownDown[0] = null;
        GameManager.GM.Act_UpDown[0] = null;
        GameManager.GM.Act_LeftDown[0] = null;
        GameManager.GM.Act_RightDown[0] = null;
        GameManager.GM.Act_Fire1Down[0] = null;
        GameManager.GM.Act_Fire2Down[0] = null;
        GameManager.GM.Act_Fire3Down[0] = null;
        GameManager.GM.Act_Fire4Down[0] = null;
    }
    public void ActiveMode0()
    {
        GameManager.GM.Act_DownDown[0] = null;
        GameManager.GM.Act_UpDown[0] = null;
        GameManager.GM.Act_LeftDown[0] = null;
        GameManager.GM.Act_RightDown[0] = null;
        GameManager.GM.Act_Fire1Down[0] = null;
        GameManager.GM.Act_Fire2Down[0] = null;
        GameManager.GM.Act_Fire3Down[0] = null;
        GameManager.GM.Act_Fire4Down[0] = null;
        GameManager.GM.Act_DownDown[0] += () =>
        {
           
        };
        GameManager.GM.Act_UpDown[0] += () =>
        {
          
        };
        GameManager.GM.Act_LeftDown[0] += () =>
        {
           
        };
        GameManager.GM.Act_RightDown[0] += () =>
        {
            
        };
        GameManager.GM.Act_Fire1Down[0] += () =>
        {
        };
        GameManager.GM.Act_Fire2Down[0] += () =>
        {
        };
        GameManager.GM.Act_Fire3Down[0] += () =>
        {
        };
        GameManager.GM.Act_Fire4Down[0] += () =>
        {
        };
        Debug.Log(this.name + " Mode0");
    }
    public void ActiveMode1()
    {
        GameManager.GM.Act_DownDown[0] = null;
        GameManager.GM.Act_UpDown[0] = null;
        GameManager.GM.Act_LeftDown[0] = null;
        GameManager.GM.Act_RightDown[0] = null;
        GameManager.GM.Act_Fire1Down[0] = null;
        GameManager.GM.Act_Fire2Down[0] = null;
        GameManager.GM.Act_Fire3Down[0] = null;
        GameManager.GM.Act_Fire4Down[0] = null;
        GameManager.GM.Act_DownDown[0] += () =>
        {
            CurrentItem.transform.position += new Vector3Int(0, -1, 0);
        };
        GameManager.GM.Act_UpDown[0] += () =>
        {
            CurrentItem.transform.position += new Vector3Int(0, 1, 0);
        };
        GameManager.GM.Act_LeftDown[0] += () =>
        {
            CurrentItem.transform.position += new Vector3Int(-1, 0, 0);
        };
        GameManager.GM.Act_RightDown[0] += () =>
        {
            CurrentItem.transform.position += new Vector3Int(1, 0, 0);
        };
        GameManager.GM.Act_Fire1Down[0] += () =>
        {
            Iconid += 10;
        };
        GameManager.GM.Act_Fire2Down[0] += () =>
        {
            Iconid -= 10;
        };
        GameManager.GM.Act_Fire3Down[0] += () =>
        {
            Iconid -= 1;
        };
        GameManager.GM.Act_Fire4Down[0] += () =>
        {
            Iconid += 1;
        };
        Debug.Log(this.name + " Mode1");
    }
    public void ActiveMode3()
    {
        MapCharacter.instance.CharState = 1;

        GameManager.GM.Act_DownDown[0] = null;
        GameManager.GM.Act_UpDown[0] = null;
        GameManager.GM.Act_LeftDown[0] = null;
        GameManager.GM.Act_RightDown[0] = null;
        GameManager.GM.Act_Fire1Down[0] = null;
        GameManager.GM.Act_Fire2Down[0] = null;
        GameManager.GM.Act_Fire3Down[0] = null;
        GameManager.GM.Act_Fire4Down[0] = null;
        GameManager.GM.Act_DownDown[0] += () =>
        {
            if (MapCharacter.instance.CharState == 1)
                CurrentItem.GetComponent<LocationItem>().CurrentWay++;
        };
        GameManager.GM.Act_UpDown[0] += () =>
        {
            if (MapCharacter.instance.CharState == 1)
                CurrentItem.GetComponent<LocationItem>().CurrentWay--;
        };
        GameManager.GM.Act_LeftDown[0] += () =>
        {
            if (MapCharacter.instance.CharState == 1)
                CurrentItem.GetComponent<LocationItem>().CurrentWay++;
        };
        GameManager.GM.Act_RightDown[0] += () =>
        {
            if (MapCharacter.instance.CharState == 1)
                CurrentItem.GetComponent<LocationItem>().CurrentWay--;
        };
        GameManager.GM.Act_Fire1Down[0] += () =>
        {
            StartCoroutine(MapCharacter.instance.MoveTo(WorldMap.instance.CharacterCurrentTarget.transform));
        };
        GameManager.GM.Act_Fire2Down[0] += () =>
        {
            StartCoroutine(MapCharacter.instance.MoveTo(WorldMap.instance.CharacterCurrentTarget.transform));
        };
        GameManager.GM.Act_Fire3Down[0] += () =>
        {
            StartCoroutine(MapCharacter.instance.MoveTo(WorldMap.instance.CharacterCurrentTarget.transform));
        };
        GameManager.GM.Act_Fire4Down[0] += () =>
        {
            StartCoroutine(MapCharacter.instance.MoveTo(WorldMap.instance.CharacterCurrentTarget.transform));
        };
        Debug.Log(this.name + " Mode3");
    }
    private void activebtn(int vl)
    {
        foreach (var bt in btn)
        {
            bt.GetComponentInChildren<Text>().color = new Color32(50, 50, 50, 255);
        }
        btn[vl].GetComponentInChildren<Text>().color = Color.red;
    }
    #endregion
}
