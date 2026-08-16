using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
<<<<<<< HEAD
=======
using Unity.VisualScripting.Antlr3.Runtime.Tree;
>>>>>>> 376c1458b1e99c82f151fa3c0c58e24d958e9dfa
=======
using Unity.VisualScripting.Antlr3.Runtime.Tree;
>>>>>>> main
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
<<<<<<< HEAD
<<<<<<< HEAD
        CreateBoard(10, 10);
    }
    #region att
    public int boardX, boardY;
=======
=======
>>>>>>> main
        BoardX = 10;
        BoardY = 10;
    }
    #region att
    private int boardX, boardY;
<<<<<<< HEAD
>>>>>>> 376c1458b1e99c82f151fa3c0c58e24d958e9dfa
=======
>>>>>>> main
    public int BoardX
    {
        get { return boardX; }
        set 
        { 
            boardX = value;
<<<<<<< HEAD
<<<<<<< HEAD
=======
            CreateBoard();
>>>>>>> 376c1458b1e99c82f151fa3c0c58e24d958e9dfa
=======
            CreateBoard();
>>>>>>> main
        }
    }
    public int BoardY
    {
        get { return boardY; }
        set 
        { 
            boardY = value;
<<<<<<< HEAD
<<<<<<< HEAD
=======
            CreateBoard();
>>>>>>> 376c1458b1e99c82f151fa3c0c58e24d958e9dfa
=======
            CreateBoard();
>>>>>>> main
        }
    }
    #endregion
    #region comps
    public static BattleBoard instance;
    public GameObject TilePrefrag;
    public List<GameObject> TileList;
    public int[] TileValue;
    public Sprite[] Spr;
<<<<<<< HEAD
<<<<<<< HEAD
    public List<PB_player> PlayerList;
    #endregion
    #region func
    public void CreateBoard(int boardSizex, int boardSizey)
    {
        // set board dimensions
        BoardX = boardSizex;
        BoardY = boardSizey;

        // ensure lists are initialized
        if (TileList == null) TileList = new List<GameObject>();

        // destroy existing tiles
        foreach (var item in TileList) if (item != null) Destroy(item);
        TileList.Clear();

        // prepare values array
        TileValue = new int[boardSizex * boardSizey];

        // compute offsets so the grid is centered at local (0,0)
        // For N tiles, positions range from -((N-1)/2) to +((N-1)/2)
        float offsetX = (boardSizex - 1) * 0.5f;
        float offsetY = (boardSizey - 1) * 0.5f;

        // create tiles so the first tile is top-left, then left->right, top->down
        for (int y = boardSizey - 1; y >= 0; y--)
        {
            for (int x = 0; x < boardSizex; x++)
            {
                GameObject tempTile = Instantiate(TilePrefrag, transform);
                float px = x - offsetX;
                float py = y - offsetY;
                tempTile.transform.localPosition = new Vector3(px, py, 0f);
                TileList.Add(tempTile);
            }
        }

        // assign random sprites
        for (int i = 0; i < TileList.Count; i++)
        {
            int value = Random.Range(0, Spr.Length);
            SetTile(TileList[i], 1, value);
        }
        if (PlayerList == null) PlayerList = new List<PB_player>();
        foreach (var player in PlayerList)
        {
            player.PlayerId += 0;
        }
    }
    public void SetTile(GameObject tile, int layer, int sprValue)
    {
        try
        {
            tile.transform.GetChild(layer).GetComponent<SpriteRenderer>().sprite = Spr[sprValue];
            int index = TileList.IndexOf(tile);
            TileValue[index] = sprValue;
            //Debug.Log($"Set tile at index {index} to sprite value {sprValue}");
        }
        catch { Debug.Log("Error occurred while setting tile sprite."); }
    }
    public int GetTileRow(int index)
    {
        return index / BoardX;
    }
    public int GetTileColumn(int index)
    {
        return index % BoardX;
    }
    public GameObject GetTile(int row, int column)
    {
        if (row < 0 || row >= BoardY || column < 0 || column >= BoardX)
        {
            Debug.Log("Row or column is out of bounds.");
            return null;
        }
        int index = row * BoardX + column;
        return TileList[index];
    }
    public void SwapTile(int tileId1, int tileId2)
    {
        //Debug.Log($"Swapping tiles {tileId1} and {tileId2}");
        if (tileId1 < 0 || tileId1 >= TileList.Count || tileId2 < 0 || tileId2 >= TileList.Count)
        {
            Debug.Log("Tile IDs are out of bounds.");
            return;
        }
        GameObject tile1 = TileList[tileId1];
        GameObject tile2 = TileList[tileId2];
        int tempValue = TileValue[tileId1];
        // Swap the sprites   
        SetTile(tile1, 1, TileValue[tileId2]);
        SetTile(tile2, 1, tempValue);
    }
    public void SwapTile(GameObject tile1, GameObject tile2)
    {
        int index1 = TileList.IndexOf(tile1);
        int index2 = TileList.IndexOf(tile2);
        if (index1 < 0 || index2 < 0)
        {
            Debug.Log("One or both tiles are not in the TileList.");
            return;
        }
        SwapTile(index1, index2);
=======
=======
>>>>>>> main
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
<<<<<<< HEAD
>>>>>>> 376c1458b1e99c82f151fa3c0c58e24d958e9dfa
=======
>>>>>>> main
    }
    #endregion
}
