using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game0_PlayerControl : MonoBehaviour
{
    void Start()
    {
        PlayerId+=0;
        CurrentPlayerBlockInfo = new int[3, 3]
        {
             {-1, -1, -1},  
             {-1, -1, -1},
             {-1, -1, -1}
        };
        GenerateBlockIDListControlMaster = 2;
    }
    private void OnEnable()
    {
        Xmin = -4.5f; Ymin = -4.5f; Xmax = 4.5f; Ymax = 4.5f;
        anim = StartCoroutine(iconRotateAnime());
        StartCoroutine(RegisterButtons(PlayerId));
    }
    private void OnDisable()
    {
        UnregisterButtons(PlayerId);
        StopCoroutine(anim);
    }
    #region att
    public int playerId;
    public int PlayerId
    {
        get { return playerId; }
        set 
        { 
            playerId = value;
            switch (value)
            {
                case 0:
                    PlayerBlockValueList= new List<int>() { 0, 1, 2, 3 };
                    break;
                default:
                    PlayerBlockValueList = new List<int>() { 0, 1, 2, 3 };
                    break;
            }
        }
    }
    public List<int> PlayerBlockValueList;
    public List<int> GenerateBlockIDList;
    private int generateBlockIDListControlMaster;
    private int GenerateBlockIDListControlMaster
    {
        get { return generateBlockIDListControlMaster; }
        set
        {
            generateBlockIDListControlMaster = value;
            switch (generateBlockIDListControlMaster)
            {
                case 2:
                    GenerateBlockIDList = new List<int>() { 200, 203, 206, 209, 201, 204, 207, 210 };
                    break;
            }
        }
    }
    public float Xmin, Ymin, Xmax, Ymax;
    private int lastButtonDirection;
    public int[,] CurrentPlayerBlockInfo = new int[3, 3];
    #endregion
    #region comps
    public Transform pointer;
    private Coroutine anim;
    public Transform currentTile_layer2
    {
        get
        {
            //GetComponent<Game0_Board>().TileMapRefresh(GetComponent<Game0_Board>().GetTile(pointer.transform.localPosition));
            return GetComponent<Game0_Board>().GetTile(pointer.transform.localPosition);
        }
    }
    private Transform Last_currentTile_layer3;
    #endregion
    #region func
    private IEnumerator iconRotateAnime()
    {
        Vector3 rt = new Vector3(0.01f, 0.01f, 0);
        pointer.transform.localScale = new Vector3(0.9f, 0.9f, 0);
        while (true)
        {
            for (int i = 0; i < 20; i++)
            {
                pointer.transform.localScale += rt;
                yield return new WaitForEndOfFrame();
            }
            for (int i = 0; i < 20; i++)
            {
                pointer.transform.localScale -= rt;
                yield return new WaitForEndOfFrame();
            }
        }
    }
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
    public IEnumerator RegisterButtons(int player)
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
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
            lastButtonDirection = 6;
            Action_MovePlayer(new Vector3Int(0, -1, 0));       
        };
        GameManager.GM.Act_UpDown[player] += () =>
        {
            lastButtonDirection = 0;
            Action_MovePlayer(new Vector3Int(0, 1, 0));    
        };
        GameManager.GM.Act_LeftDown[player] += () =>
        {
            lastButtonDirection = 9;
            Action_MovePlayer(new Vector3Int(-1, 0, 0)); 
        };
        GameManager.GM.Act_RightDown[player] += () =>
        {
            lastButtonDirection = 3;
            Action_MovePlayer(new Vector3Int(1, 0, 0));    
        };
        GameManager.GM.Act_Fire1Down[player] += () =>
        {
            Action_Fire1();
        };
        GameManager.GM.Act_Fire2Down[player] += () =>
        {
            GenerateRandomPlayerBlock(new List<int>(), GenerateBlockIDList[Random.Range(0, GenerateBlockIDList.Count-1)]);
        };
        GameManager.GM.Act_Fire3Down[player] += () =>
        {
            Action_Cancel();
        };
        GameManager.GM.Act_Fire4Down[player] += () =>
        {
            Action_Cancel();
        };
        Debug.Log(this.name + " Mode0");
    }
    private void Action_MovePlayer(Vector3 moveValue)
    {
        pointer.transform.localPosition += moveValue;
        if (pointer.transform.localPosition.x > Xmax)
        {
            pointer.transform.localPosition= new Vector3(Xmin, pointer.transform.localPosition.y, 0);
        }
        if (pointer.transform.localPosition.x < Xmin)
        {
            pointer.transform.localPosition = new Vector3(Xmax, pointer.transform.localPosition.y, 0);
        }
        if (pointer.transform.localPosition.y > Ymax)
        {
            pointer.transform.localPosition = new Vector3(pointer.transform.localPosition.x, Ymin, 0);
        }
        if (pointer.transform.localPosition.y < Ymin)
        {
            pointer.transform.localPosition = new Vector3(pointer.transform.localPosition.x, Ymax, 0);
        }
        //pointer.transform.localPosition = new Vector3(Mathf.Clamp(pointer.transform.localPosition.x, Xmin, Xmax), Mathf.Clamp(pointer.transform.localPosition.y, Ymin, Ymax), 0);
        if (currentTile_layer2.GetComponent<SpriteRenderer>().sprite != null)
        {
            MovePointerToNearestEmptyTileWithDirection();  
        }
        UpdateLayer3();
    }
    private void Action_Fire1()
    {
        GetComponent<Game0_Board>().Change_Tile(currentTile_layer2, 0);
        MovePointerToNearestEmptyTile();
    }
    private void Action_Cancel()
    {
        GetComponent<Game0_Board>().Change_Tile(currentTile_layer2, -1);
    }
    public void MovePointerToNearestEmptyTile()
    {
        Transform lasttile = currentTile_layer2.transform;
        Vector3 nearestEmptyTilePos = Vector3.zero;
        for (int i = 0; i < 20; i++)
        {
            foreach(GameObject tileobj in GetComponent<Game0_Board>().GetTilesAtRange(lasttile, i)) 
            {  
                if (tileobj.GetComponent<SpriteRenderer>().sprite == null)
                {
                    nearestEmptyTilePos = tileobj.transform.localPosition;
                    //Debug.Log("Nearest empty tile found at " + tileobj.name);  
                }
            }
            if (nearestEmptyTilePos != Vector3.zero) break;
        }
        pointer.transform.localPosition = nearestEmptyTilePos;
    }
    public void MovePointerToNearestEmptyTileWithDirection()
    {
        switch (lastButtonDirection)
        {
            case 0:      
                if (GetComponent<Game0_Board>().getEmptyTilesAtUpper(currentTile_layer2).Count > 0)
                {
                    int minDistance = 1000;
                    GameObject nearestTile = null;
                    foreach (GameObject tileobj in GetComponent<Game0_Board>().getEmptyTilesAtUpper(currentTile_layer2))
                    {
                        if (DistanceBetweenTwoTile_Y(currentTile_layer2, tileobj.transform) < minDistance)
                        {
                            minDistance = DistanceBetweenTwoTile_Y(currentTile_layer2, tileobj.transform);
                            nearestTile = tileobj;
                        }
                    }
                    pointer.transform.localPosition = nearestTile.transform.localPosition;
                }      
                else if(GetComponent<Game0_Board>().getEmptyTilesAtLower(currentTile_layer2).Count > 0)
                {
                    int maxDistance = 0;
                    GameObject nearestTile = null;
                    foreach (GameObject tileobj in GetComponent<Game0_Board>().getEmptyTilesAtLower(currentTile_layer2))
                    {
                        if (DistanceBetweenTwoTile_Y(currentTile_layer2, tileobj.transform) > maxDistance)
                        {
                            maxDistance = DistanceBetweenTwoTile_Y(currentTile_layer2, tileobj.transform);
                            nearestTile = tileobj;
                        }
                    }
                    pointer.transform.localPosition = nearestTile.transform.localPosition;
                }
                else
                {
                    MovePointerToNearestEmptyTile();
                }  
                break;
            case 3:
                if (GetComponent<Game0_Board>().getEmptyTilesAtRight(currentTile_layer2).Count > 0)
                {
                    int minDistance = 1000;
                    GameObject nearestTile = null;
                    foreach (GameObject tileobj in GetComponent<Game0_Board>().getEmptyTilesAtRight(currentTile_layer2))
                    {
                        if (DistanceBetweenTwoTile_X(currentTile_layer2, tileobj.transform) < minDistance)
                        {
                            minDistance = DistanceBetweenTwoTile_X(currentTile_layer2, tileobj.transform);
                            nearestTile = tileobj;
                        }
                    }
                    pointer.transform.localPosition = nearestTile.transform.localPosition;
                }
                else if (GetComponent<Game0_Board>().getEmptyTilesAtLeft(currentTile_layer2).Count > 0)
                {
                    int maxDistance = 0; 
                    GameObject nearestTile = null;
                    foreach (GameObject tileobj in GetComponent<Game0_Board>().getEmptyTilesAtLeft(currentTile_layer2))
                    {
                        if (DistanceBetweenTwoTile_X(currentTile_layer2, tileobj.transform) > maxDistance)
                        {
                            maxDistance = DistanceBetweenTwoTile_X(currentTile_layer2, tileobj.transform);
                            nearestTile = tileobj;
                        }
                    }
                    pointer.transform.localPosition = nearestTile.transform.localPosition;
                }
                else
                {
                    MovePointerToNearestEmptyTile();
                }
                break;
            case 6:
                if (GetComponent<Game0_Board>().getEmptyTilesAtLower(currentTile_layer2).Count > 0)
                {
                    int minDistance = 1000;
                    GameObject nearestTile = null;
                    foreach (GameObject tileobj in GetComponent<Game0_Board>().getEmptyTilesAtLower(currentTile_layer2))
                    {
                        if (DistanceBetweenTwoTile_Y(currentTile_layer2, tileobj.transform) < minDistance)
                        {
                            minDistance = DistanceBetweenTwoTile_Y(currentTile_layer2, tileobj.transform);
                            nearestTile = tileobj;
                        }
                    }
                    pointer.transform.localPosition = nearestTile.transform.localPosition;
                }
                else if (GetComponent<Game0_Board>().getEmptyTilesAtUpper(currentTile_layer2).Count > 0)
                {
                    int maxDistance = 0;
                    GameObject nearestTile = null;
                    foreach (GameObject tileobj in GetComponent<Game0_Board>().getEmptyTilesAtUpper(currentTile_layer2))
                    {
                        if (DistanceBetweenTwoTile_Y(currentTile_layer2, tileobj.transform) > maxDistance)
                        {
                            maxDistance = DistanceBetweenTwoTile_Y(currentTile_layer2, tileobj.transform);
                            nearestTile = tileobj;
                        }
                    }
                    pointer.transform.localPosition = nearestTile.transform.localPosition;
                }
                else
                {
                    MovePointerToNearestEmptyTile();
                }
                break;
            case 9:
                if (GetComponent<Game0_Board>().getEmptyTilesAtLeft(currentTile_layer2).Count > 0)
                {
                    int minDistance = 1000;
                    GameObject nearestTile = null;
                    foreach (GameObject tileobj in GetComponent<Game0_Board>().getEmptyTilesAtLeft(currentTile_layer2))
                    {
                        if (DistanceBetweenTwoTile_X(currentTile_layer2, tileobj.transform) < minDistance)
                        {
                            minDistance = DistanceBetweenTwoTile_X(currentTile_layer2, tileobj.transform);
                            nearestTile = tileobj;
                        }
                    }
                    pointer.transform.localPosition = nearestTile.transform.localPosition;
                }
                else if (GetComponent<Game0_Board>().getEmptyTilesAtRight(currentTile_layer2).Count > 0)
                {
                    int maxDistance = 0;
                    GameObject nearestTile = null;
                    foreach (GameObject tileobj in GetComponent<Game0_Board>().getEmptyTilesAtRight(currentTile_layer2))
                    {
                        if (DistanceBetweenTwoTile_X(currentTile_layer2, tileobj.transform) > maxDistance)
                        {
                            maxDistance = DistanceBetweenTwoTile_X(currentTile_layer2, tileobj.transform);
                            nearestTile = tileobj;
                        }
                    }
                    pointer.transform.localPosition = nearestTile.transform.localPosition;
                }
                else
                {
                    MovePointerToNearestEmptyTile();
                }
                break;
        }
    }
    public int DistanceBetweenTwoTile_X(Transform tile1, Transform tile2)
    {
        int resultX = (int)Mathf.Abs(tile2.localPosition.x - tile1.localPosition.x);
        return resultX;
    }
    public int DistanceBetweenTwoTile_Y(Transform tile1, Transform tile2)
    {
        int resultY = (int)Mathf.Abs(tile2.localPosition.y - tile1.localPosition.y);
        return resultY;
    }
    public int DistanceBetweenTwoTile_XY(Transform tile1, Transform tile2)
    {
        int resultX = (int)Mathf.Abs(tile2.localPosition.x - tile1.localPosition.x);
        int resultY = (int)Mathf.Abs(tile2.localPosition.y - tile1.localPosition.y);
        return resultX+ resultY;
    }

    public void generatePlayerBlockObject(int BlockType)
    {
        switch(BlockType)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }
    public void GenerateRandomPlayerBlock(List<int> BlockList, int blockid)
    {
        if(BlockList.Count >0)
        {
            PlayerBlockValueList = BlockList;
        }
        //generate level block from blocklist
        switch (blockid)
        {
            case 0:
                break;
            case 100:
                CurrentPlayerBlockInfo[0, 0] = -1; CurrentPlayerBlockInfo[0, 1] = -1; CurrentPlayerBlockInfo[0, 2] = -1;
                CurrentPlayerBlockInfo[1, 0] = -1; CurrentPlayerBlockInfo[1, 1] = PlayerBlockValueList[Random.Range(0, PlayerBlockValueList.Count-1)]; CurrentPlayerBlockInfo[1, 2] = -1;
                CurrentPlayerBlockInfo[2, 0] = -1; CurrentPlayerBlockInfo[2, 1] = -1; CurrentPlayerBlockInfo[2, 2] = -1;
                break;
            case 200:
                CurrentPlayerBlockInfo[0, 0] = -1; CurrentPlayerBlockInfo[0, 1] = PlayerBlockValueList[Random.Range(0, PlayerBlockValueList.Count - 1)]; CurrentPlayerBlockInfo[0, 2] = -1;
                CurrentPlayerBlockInfo[1, 0] = -1; CurrentPlayerBlockInfo[1, 1] = PlayerBlockValueList[Random.Range(0, PlayerBlockValueList.Count - 1)]; CurrentPlayerBlockInfo[1, 2] = -1;
                CurrentPlayerBlockInfo[2, 0] = -1; CurrentPlayerBlockInfo[2, 1] = -1; CurrentPlayerBlockInfo[2, 2] = -1;
                break;
            case 203:
                CurrentPlayerBlockInfo[0, 0] = -1; CurrentPlayerBlockInfo[0, 1] = -1; CurrentPlayerBlockInfo[0, 2] = -1;
                CurrentPlayerBlockInfo[1, 0] = -1; CurrentPlayerBlockInfo[1, 1] = PlayerBlockValueList[Random.Range(0, PlayerBlockValueList.Count - 1)]; CurrentPlayerBlockInfo[1, 2] = PlayerBlockValueList[Random.Range(0, PlayerBlockValueList.Count - 1)];
                CurrentPlayerBlockInfo[2, 0] = -1; CurrentPlayerBlockInfo[2, 1] = -1; CurrentPlayerBlockInfo[2, 2] = -1;
                break;
            case 206:
                CurrentPlayerBlockInfo[0, 0] = -1; CurrentPlayerBlockInfo[0, 1] = -1; CurrentPlayerBlockInfo[0, 2] = -1;
                CurrentPlayerBlockInfo[1, 0] = -1; CurrentPlayerBlockInfo[1, 1] = PlayerBlockValueList[Random.Range(0, PlayerBlockValueList.Count - 1)]; CurrentPlayerBlockInfo[1, 2] = -1;
                CurrentPlayerBlockInfo[2, 0] = -1; CurrentPlayerBlockInfo[2, 1] = PlayerBlockValueList[Random.Range(0, PlayerBlockValueList.Count - 1)]; CurrentPlayerBlockInfo[2, 2] = -1;
                break;
            case 209:
                CurrentPlayerBlockInfo[0, 0] = -1; CurrentPlayerBlockInfo[0, 1] = -1; CurrentPlayerBlockInfo[0, 2] = -1;
                CurrentPlayerBlockInfo[1, 0] = PlayerBlockValueList[Random.Range(0, PlayerBlockValueList.Count - 1)]; CurrentPlayerBlockInfo[1, 1] = PlayerBlockValueList[Random.Range(0, PlayerBlockValueList.Count - 1)]; CurrentPlayerBlockInfo[1, 2] = -1;
                CurrentPlayerBlockInfo[2, 0] = -1; CurrentPlayerBlockInfo[2, 1] = -1; CurrentPlayerBlockInfo[2, 2] = -1;
                break;
            case 201:
                CurrentPlayerBlockInfo[0, 0] = -1; CurrentPlayerBlockInfo[0, 1] = -1; CurrentPlayerBlockInfo[0, 2] = PlayerBlockValueList[Random.Range(0, PlayerBlockValueList.Count - 1)];
                CurrentPlayerBlockInfo[1, 0] = -1; CurrentPlayerBlockInfo[1, 1] = PlayerBlockValueList[Random.Range(0, PlayerBlockValueList.Count - 1)]; CurrentPlayerBlockInfo[1, 2] = -1;
                CurrentPlayerBlockInfo[2, 0] = -1; CurrentPlayerBlockInfo[2, 1] = -1; CurrentPlayerBlockInfo[2, 2] = -1;
                break;
            case 204:
                CurrentPlayerBlockInfo[0, 0] = -1; CurrentPlayerBlockInfo[0, 1] = -1; CurrentPlayerBlockInfo[0, 2] = -1;
                CurrentPlayerBlockInfo[1, 0] = -1; CurrentPlayerBlockInfo[1, 1] = PlayerBlockValueList[Random.Range(0, PlayerBlockValueList.Count - 1)]; CurrentPlayerBlockInfo[1, 2] = -1;
                CurrentPlayerBlockInfo[2, 0] = -1; CurrentPlayerBlockInfo[2, 1] = -1; CurrentPlayerBlockInfo[2, 2] = PlayerBlockValueList[Random.Range(0, PlayerBlockValueList.Count - 1)];
                break;
            case 207:
                CurrentPlayerBlockInfo[0, 0] = -1; CurrentPlayerBlockInfo[0, 1] = -1; CurrentPlayerBlockInfo[0, 2] = -1;
                CurrentPlayerBlockInfo[1, 0] = -1; CurrentPlayerBlockInfo[1, 1] = PlayerBlockValueList[Random.Range(0, PlayerBlockValueList.Count - 1)]; CurrentPlayerBlockInfo[1, 2] = -1;
                CurrentPlayerBlockInfo[2, 0] = PlayerBlockValueList[Random.Range(0, PlayerBlockValueList.Count - 1)]; CurrentPlayerBlockInfo[2, 1] = -1; CurrentPlayerBlockInfo[2, 2] = -1;
                break;
            case 210:
                CurrentPlayerBlockInfo[0, 0] = PlayerBlockValueList[Random.Range(0, PlayerBlockValueList.Count - 1)]; CurrentPlayerBlockInfo[0, 1] = -1; CurrentPlayerBlockInfo[0, 2] = -1;
                CurrentPlayerBlockInfo[1, 0] = -1; CurrentPlayerBlockInfo[1, 1] = PlayerBlockValueList[Random.Range(0, PlayerBlockValueList.Count - 1)]; CurrentPlayerBlockInfo[1, 2] = -1;
                CurrentPlayerBlockInfo[2, 0] = -1; CurrentPlayerBlockInfo[2, 1] = -1; CurrentPlayerBlockInfo[2, 2] = -1;
                break;
        }
        UpdateLayer3();
    }
    public void UpdateLayer3()
    {
        if(Last_currentTile_layer3!= null)
        {
            foreach (GameObject tileobj in GetComponent<Game0_Board>().getAdjacentTile3From(Last_currentTile_layer3))
            {
                GetComponent<Game0_Board>().Change_Tile(tileobj.transform, -1, 3);
            }
        }
        int count = 0;
        for (int x = 0; x < 3; x++)
            for (int y = 0; y < 3; y++)
            {
                GetComponent<Game0_Board>().Change_Tile(GetComponent<Game0_Board>().getAdjacentTile3From(currentTile_layer2)[count].transform, CurrentPlayerBlockInfo[x,y], 3);
                count++;
            }
        Last_currentTile_layer3= currentTile_layer2;
    }
    #endregion
}
