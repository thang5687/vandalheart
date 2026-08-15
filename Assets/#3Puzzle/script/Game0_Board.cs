using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game0_Board : MonoBehaviour
{
    private void Awake()
    {
    }
    private void Start()
    {
        CreateBoard();
    }
    #region 
    public int[] TileValue;
    public int[] TileMapXY;
    #endregion
    #region comps
    public List<GameObject> Layer1,Layer2,Layer3;
    public Sprite[] Spr;
    #endregion
    #region func
    public void CreateBoard()
    {
        int count = 0;
        for (int col = -5; col < 5; col++)
            for (int row = -5; row < 5; row++)
            {
                Layer2[count].transform.localPosition = new Vector3(col + 0.5f, row + 0.5f, 0);
                Change_Tile(count, -1);
                Change_Tile(Layer3[count].transform, -1,3);
                count++;
            }
    }
    public void Change_Tile(int tileposition, int changeValue) //layer2
    {
        switch (changeValue)
        {
            case -1:
                TileValue[tileposition] = changeValue;
                Layer2[tileposition].GetComponent<SpriteRenderer>().sprite = null;
                break;
            case >=0:
                TileValue[tileposition] = changeValue;
                Layer2[tileposition].GetComponent<SpriteRenderer>().sprite = Spr[changeValue];
                break;
        }
    }
    public void Change_Tile(Transform tile, int changeValue, int layer=2) 
    {
        int tileposition = (int)((tile.localPosition.x + 4.5f) * 10 + tile.localPosition.y + 4.5f);
        switch (layer)
        {
            case 2:
                switch (changeValue)
                {
                    case -1:
                        TileValue[tileposition] = changeValue;
                        Layer2[tileposition].GetComponent<SpriteRenderer>().sprite = null;
                        break;
                    case >= 0:
                        TileValue[tileposition] = changeValue;
                        Layer2[tileposition].GetComponent<SpriteRenderer>().sprite = Spr[changeValue];
                        break;
                }
                break;
            case 3:
                switch (changeValue)
                {
                    case -1:
                        TileValue[tileposition] = changeValue;
                        Layer3[tileposition].GetComponent<SpriteRenderer>().sprite = null;
                        break;
                    case >= 0:
                        TileValue[tileposition] = changeValue;
                        Layer3[tileposition].GetComponent<SpriteRenderer>().sprite = Spr[changeValue];
                        break;
                }
                break;
        }  
    }
    public Transform GetTile(int tileposition, int layer = 2)
    {
        if(tileposition < 0 || tileposition >= Layer2.Count)
        {
            return null;
        }
        return Layer2[tileposition].transform;
    }
    public Transform GetTile(Vector3 position, int layer = 2)
    {
        int tileposition;
        switch (layer)
        {
            default:
                if (position.x < -4.5f || position.x > 4.5f || position.y < -4.5f || position.y > 4.5f)
                {
                    return null;
                }
                tileposition = (int)((position.x + 4.5f) * 10 + position.y + 4.5f);
                return Layer2[tileposition].transform;
            case 3:
                 if (position.x < -4.5f || position.x > 4.5f || position.y < -4.5f || position.y > 4.5f)
                {
                    return null;
                }
                tileposition = (int)((position.x + 4.5f) * 10 + position.y + 4.5f);
                return Layer3[tileposition].transform;
        }
    }
    public int Tile3Id(Transform tile)
    {
        return Layer3.IndexOf(tile.gameObject); ;
    }
    public void TileMapRefresh(Transform tile)
    {
        TileMapXY = new int[TileValue.Length];
        foreach (GameObject tileobj in Layer2)
        {
            int xvalue = Mathf.Abs(Mathf.RoundToInt(tile.localPosition.x - tileobj.transform.localPosition.x));
            int yvalue = Mathf.Abs(Mathf.RoundToInt(tile.localPosition.y - tileobj.transform.localPosition.y));
            TileMapXY[Layer2.IndexOf(tileobj)] = xvalue + yvalue;
        }
    }
    public List<GameObject> GetTilesAtRange(Transform tile, int range)
    {
        List<GameObject> tilesInRange = new List<GameObject>();
        TileMapRefresh(tile);
        for (int i = 0; i < TileMapXY.Length; i++)
        {
            if (TileMapXY[i] == range)
            {
                tilesInRange.Add(Layer2[i]);
            }
        }
        return tilesInRange;
    }
    public List<GameObject> getEmptyTilesAtUpper(Transform tile)
    {
        List<GameObject> tilesInRange = new List<GameObject>();
        TileMapRefresh(tile);
        foreach (GameObject tileobj in Layer2)
        {
            if (tileobj.GetComponent<SpriteRenderer>().sprite == null && tile.localPosition.y < tileobj.transform.localPosition.y &&  tile.localPosition.x== tileobj.transform.localPosition.x)
            {
                tilesInRange.Add(tileobj);
            }
        }
        return tilesInRange;
    }
    public List<GameObject> getEmptyTilesAtLower(Transform tile)
    {
        List<GameObject> tilesInRange = new List<GameObject>();
        TileMapRefresh(tile);
        foreach (GameObject tileobj in Layer2)
        {
            if (tileobj.GetComponent<SpriteRenderer>().sprite == null && tile.localPosition.y > tileobj.transform.localPosition.y && tile.localPosition.x == tileobj.transform.localPosition.x)
            {
                tilesInRange.Add(tileobj);
            }
        }
        return tilesInRange;
    }
    public List<GameObject> getEmptyTilesAtLeft(Transform tile)
    {
        List<GameObject> tilesInRange = new List<GameObject>();
        TileMapRefresh(tile);
        foreach (GameObject tileobj in Layer2)
        {
            if (tileobj.GetComponent<SpriteRenderer>().sprite == null && tile.localPosition.x > tileobj.transform.localPosition.x && tile.localPosition.y == tileobj.transform.localPosition.y)
            {
                tilesInRange.Add(tileobj);
            }
        }
        return tilesInRange;
    } 
    public List<GameObject> getEmptyTilesAtRight(Transform tile)
    {
        List<GameObject> tilesInRange = new List<GameObject>();
        TileMapRefresh(tile);
        foreach (GameObject tileobj in Layer2)
        {
            if (tileobj.GetComponent<SpriteRenderer>().sprite == null && tile.localPosition.x < tileobj.transform.localPosition.x && tile.localPosition.y == tileobj.transform.localPosition.y)
            {
                tilesInRange.Add(tileobj);
            }
        }
        return tilesInRange;
    }
    public List<GameObject> getAdjacentTile3From(Transform tile)
    {
        List<GameObject> AdjacentTiles = new List<GameObject>();
        if (TileisAlvailableAt(new Vector3(tile.localPosition.x - 1, tile.localPosition.y + 1)))
        {
            AdjacentTiles.Add(GetTile(new Vector3(tile.localPosition.x - 1, tile.localPosition.y + 1), 3).gameObject);
        }
        if (TileisAlvailableAt(new Vector3(tile.localPosition.x, tile.localPosition.y + 1)))
        {
            AdjacentTiles.Add(GetTile(new Vector3(tile.localPosition.x, tile.localPosition.y + 1), 3).gameObject);
        }
        if (TileisAlvailableAt(new Vector3(tile.localPosition.x + 1, tile.localPosition.y + 1)))
        {
            AdjacentTiles.Add(GetTile(new Vector3(tile.localPosition.x + 1, tile.localPosition.y + 1), 3).gameObject);
        }
        if (TileisAlvailableAt(new Vector3(tile.localPosition.x - 1, tile.localPosition.y)))
        {
            AdjacentTiles.Add(GetTile(new Vector3(tile.localPosition.x - 1, tile.localPosition.y), 3).gameObject);
        }
        if (TileisAlvailableAt(new Vector3(tile.localPosition.x, tile.localPosition.y)))
        {
            AdjacentTiles.Add(GetTile(new Vector3(tile.localPosition.x, tile.localPosition.y), 3).gameObject);
        }
        if (TileisAlvailableAt(new Vector3(tile.localPosition.x + 1, tile.localPosition.y)))
        {
            AdjacentTiles.Add(GetTile(new Vector3(tile.localPosition.x + 1, tile.localPosition.y), 3).gameObject);
        }
        if (TileisAlvailableAt(new Vector3(tile.localPosition.x -1, tile.localPosition.y -1)))
        {
            AdjacentTiles.Add(GetTile(new Vector3(tile.localPosition.x -1, tile.localPosition.y-1),3).gameObject);
        }
        if (TileisAlvailableAt(new Vector3(tile.localPosition.x, tile.localPosition.y - 1)))
        {
            AdjacentTiles.Add(GetTile(new Vector3(tile.localPosition.x, tile.localPosition.y -1),3).gameObject);
        }      
        if (TileisAlvailableAt(new Vector3(tile.localPosition.x + 1, tile.localPosition.y - 1)))
        {
            AdjacentTiles.Add(GetTile(new Vector3(tile.localPosition.x +1, tile.localPosition.y -1),3).gameObject);
        }
        //Debug.Log("AdjacentTiles count: " + AdjacentTiles.Count);
        return AdjacentTiles;
    }
    private bool TileisAlvailableAt(Vector3 pos)
    {
        if(pos.x < -4.5f || pos.x > 4.5f || pos.y < -4.5f || pos.y > 4.5f)
        {
            return false;
        }
        return true;
    }
    #endregion
}
