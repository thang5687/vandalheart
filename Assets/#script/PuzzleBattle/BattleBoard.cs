using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using Random = UnityEngine.Random;
public class BattleBoard : MonoBehaviour
{
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        BoardX = 10;
        BoardY = 10;
    }
    #region att
    private int boardX, boardY;
    public int BoardX
    {
        get { return boardX; }
        set 
        { 
            boardX = value;
            CreateBoard();
        }
    }
    public int BoardY
    {
        get { return boardY; }
        set 
        { 
            boardY = value;
            CreateBoard();
        }
    }
    #endregion
    #region comps
    public static BattleBoard instance;
    public GameObject TilePrefrag;
    public List<GameObject> TileList;
    public int[] TileValue;
    public Sprite[] Spr;
    public Transform player1, player2;
    #endregion
    #region func
    public void CreateBoard()
    {
        foreach (var item in TileList) Destroy(item);
        TileList.Clear();
        TileValue = new int[BoardX*BoardY];
        for (int i = 0; i < BoardX; i++)          
            for (int j = 0; j < BoardY; j++)
            {
                GameObject tempTile = Instantiate(TilePrefrag, transform);
                tempTile.transform.localPosition = new Vector3 (i, j, 0);
                TileList.Add(tempTile);
            }
        for (int i = 0;i < TileList.Count; i++)
        {
            int value = Random.Range(0, Spr.Length);
            SetTile(TileList[i], value);     
        }
    }
    public void SetTile(GameObject tile, int sprValue)
    {
        try
        {
            tile.GetComponent<SpriteRenderer>().sprite = Spr[sprValue];
            int index = TileList.IndexOf(tile);
            TileValue[index] = sprValue;
        }
        catch { }
    }
    public void SwapTile(int tileIndex, int targetIndex)
    {
        int OriginValue = TileValue[tileIndex];
        SetTile(TileList[tileIndex], TileValue[targetIndex]);
        SetTile(TileList[targetIndex], OriginValue);
    }
    public GameObject getBlockAt(int x, int y)
    {
        foreach(var tile in TileList)
        {
            if(Mathf.RoundToInt(tile.transform.localPosition.x) == x && Mathf.RoundToInt(tile.transform.localPosition.y) == y)
            {
                return tile;
            }
        }
        return null;
    }
    public int GetIndexOfBlock(GameObject block)
    {
        return TileList.IndexOf(block);
    }
    public void CheckBlockat(GameObject block)
    {
        int indexValue = TileValue[GetIndexOfBlock(block)];
        //Debug.Log(indexValue);
        //checkXline
        int Xcount = 1;
        int xline = Mathf.RoundToInt(block.transform.localPosition.x);
        int yline = Mathf.RoundToInt(block.transform.localPosition.y);
        Debug.Log(TileValue[GetIndexOfBlock(block)]+ " xline = " + TileValue[GetIndexOfBlock(getBlockAtRight(block))]);
        if (TileValue[GetIndexOfBlock(getBlockAtRight(block))] == TileValue[GetIndexOfBlock(block)])
        {
            Xcount++;
        }
        Debug.Log("xline = " + Xcount);     
    }
    public GameObject getBlockAtRight(GameObject block)
    {
        int x = Mathf.RoundToInt(block.transform.localPosition.x);
        int y = Mathf.RoundToInt(block.transform.localPosition.y);
        if (x + 1 >= BoardX - 1) return null;
        else
        {
            //Debug.Log(GetIndexOfBlock(getBlockAt(x + 1, y)));
            return getBlockAt(x + 1, y);
        }
    }
    public GameObject getBlockAtLeft(GameObject block)
    {
        int x = Mathf.RoundToInt(block.transform.localPosition.x);
        int y = Mathf.RoundToInt(block.transform.localPosition.y);
        if (x - 1 < 0) return null;
        else
        {
            //Debug.Log(GetIndexOfBlock(getBlockAt(x - 1, y)));
            return getBlockAt(x - 1, y);
        }
    }
    #endregion
}
