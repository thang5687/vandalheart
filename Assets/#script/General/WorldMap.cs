using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WorldMap : MonoBehaviour
{
    private void Awake()
    {
        instance=this;
        LoadAsset();
    }
    void Start()
    {
        btn= Canvasgrid.GetComponentsInChildren<Button>();
        Mapid += 0;
        CurrentBtn += 0;
    }
    private void OnEnable()
    {
        StartCoroutine(DelayEnable0());
    }
    private void OnDisable()
    {
        GameManager.GM.Act_DownDown[0] = null;
        GameManager.GM.Act_UpDown[0] = null;
        GameManager.GM.Act_LeftDown[0] = null;
        GameManager.GM.Act_RightDown[0] = null;
        GameManager.GM.Act_Fire1Down[0] = null;
        GameManager.GM.Act_Fire2Down[0] = null;
        GameManager.GM.Act_Fire3Down[0] = null;
        GameManager.GM.Act_Fire4Down[0] = null;
        Debug.Log(this.name + " OnDisable");
    }
    #region att
    private int mapid;
    public int Mapid
    {
        get { return mapid; }
        set 
        { 
            mapid = value;   
            mapid = Mathf.Clamp(mapid, 0, Maplist.Length-1);
            StartCoroutine(CreateMap());
        }
    }
    private int currentBtn;
    public int CurrentBtn
    {
        get { return currentBtn; }
        set 
        { 
            currentBtn = value; 
            currentBtn = Mathf.Clamp(currentBtn, 0, btn.Length-1);
            activebtn(currentBtn);
        }
    }
    private int mapstate;
    public int Mapstate
    {
        get { return mapstate; }
        set
        {
            mapstate = value; 
            switch(mapstate)
            {
                case 0:
                    menu.gameObject.SetActive(true);
                    this.gameObject.SetActive(false);
                    break;
                case 1:
                    StartCoroutine(DelayEnable1());
                    break;
            }
        }
    }
    #endregion
    #region com
    public static WorldMap instance;
    public Sprite[] sprMap, sprIcon;
    public Transform LocList
    {
        get { return currentMap.transform.Find("Location"); }
    }
    public Transform Currentitem
    {
        get { return WorldMapEdit.instance.CurrentItem; }
        set
        {
            WorldMapEdit.instance.CurrentItem = value;
        }
    }
    public GameObject currentMap, CharacterCurrentTarget;
    public GameObject[] Maplist;
    public List<GameObject> LocationList;
    public Material linemat;
    public Transform Canvasgrid;
    public Button[] btn;
    public MainVandalMenu menu;
    #endregion
    #region func
    public IEnumerator DelayEnable0()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

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
            CurrentBtn++;
        };
        GameManager.GM.Act_UpDown[0] += () =>
        {
            CurrentBtn--;
        };
        GameManager.GM.Act_LeftDown[0] += () =>
        {

        };
        GameManager.GM.Act_RightDown[0] += () =>
        {

        };
        GameManager.GM.Act_Fire1Down[0] += () =>
        {
            Mapstate = CurrentBtn;
        };
        GameManager.GM.Act_Fire2Down[0] += () =>
        {
            Mapstate = CurrentBtn;
        };
        GameManager.GM.Act_Fire3Down[0] += () =>
        {
            Mapstate = CurrentBtn;
        };
        GameManager.GM.Act_Fire4Down[0] += () =>
        {
            //Mapstate = CurrentBtn;
        };
        Debug.Log(this.name + " OnEnable");
    }
    public IEnumerator DelayEnable1()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
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
                Currentitem.GetComponent<LocationItem>().CurrentWay++;
        };
        GameManager.GM.Act_UpDown[0] += () =>
        {
            if (MapCharacter.instance.CharState == 1)
                Currentitem.GetComponent<LocationItem>().CurrentWay--;
        };
        GameManager.GM.Act_LeftDown[0] += () =>
        {
            if (MapCharacter.instance.CharState == 1)
                Currentitem.GetComponent<LocationItem>().CurrentWay++;
        };
        GameManager.GM.Act_RightDown[0] += () =>
        {
            if (MapCharacter.instance.CharState == 1)
                Currentitem.GetComponent<LocationItem>().CurrentWay--;
        };
        GameManager.GM.Act_Fire1Down[0] += () =>
        {
            if (WorldMap.instance.CharacterCurrentTarget != null)
            {
                StartCoroutine(MapCharacter.instance.MoveTo(CharacterCurrentTarget.transform));
            }           
        };
        GameManager.GM.Act_Fire2Down[0] += () =>
        {
            if (WorldMap.instance.CharacterCurrentTarget != null)
            {
                StartCoroutine(MapCharacter.instance.MoveTo(CharacterCurrentTarget.transform));
            }
        };
        GameManager.GM.Act_Fire3Down[0] += () =>
        {
            if (WorldMap.instance.CharacterCurrentTarget != null)
            {
                StartCoroutine(MapCharacter.instance.MoveTo(CharacterCurrentTarget.transform));
            }
        };
        GameManager.GM.Act_Fire4Down[0] += () =>
        {
            StartCoroutine(DelayEnable0());
            MapCharacter.instance.CharState = 0;
            CharacterCurrentTarget = null;
            Currentitem.GetComponent<LocationItem>().AlllineDeactive();
        };
        Debug.Log(this.name + " OnEnable");
    }
    public IEnumerator CreateMap()
    {
        if(currentMap!=null) Destroy(currentMap);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        GameObject tempmap = Instantiate(Maplist[Mapid], transform);
        currentMap= tempmap;
        LocationList.Clear();
        foreach (var ojb in currentMap.GetComponentsInChildren<LocationItem>())
        {
            LocationList.Add(ojb.gameObject);
        }
        Currentitem = LocationList[0].transform;
    }
    public void LoadAsset()
    {
        sprMap = Resources.LoadAll<Sprite>("Map");
        sprIcon = Resources.LoadAll<Sprite>("Icon");
        Maplist = Resources.LoadAll<GameObject>("MapPre");
    }
    private void activebtn(int vl)
    {
        foreach (var bt in btn)
        {
            bt.GetComponentInChildren<Text>().color = new Color32(50, 50, 50, 255);
            bt.transform.localScale = Vector3.one;
        }
        btn[vl].GetComponentInChildren<Text>().color = Color.red;
        btn[vl].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }
    #endregion
}
