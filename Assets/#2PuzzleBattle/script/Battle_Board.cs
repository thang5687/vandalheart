using UnityEngine;
using System.Collections.Generic;

public class Battle_Board : MonoBehaviour
{
    private void Start()
    {
        GenerateBoard(BoardRow, BoardCollum, TileSquare);

    }
    #region Attributes
    public int BoardRow, BoardCollum;
    #endregion
    #region Components
    public GameObject TileSquare;
    public List<GameObject> BoardTile;
    #endregion
    #region Functions
    public void GenerateBoard(int width, int height, GameObject tilePrefab)
    {
        if (tilePrefab == null)
        {
            Debug.LogWarning("GenerateBoard: tilePrefab is null");
            return;
        }

        // set dimensions
        BoardRow = Mathf.Max(0, width);
        BoardCollum = Mathf.Max(0, height);
        TileSquare = tilePrefab;

        // clear existing children (previous board)
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            var child = transform.GetChild(i);
            if (Application.isPlaying)
                Destroy(child.gameObject);
            else
                DestroyImmediate(child.gameObject);
        }
        BoardTile.Clear();

        // determine tile size (use SpriteRenderer bounds if available, fallback to (1,1))
        Vector2 tileSize = Vector2.one;
        var sr = tilePrefab.GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            tileSize = sr.bounds.size;
            if (tileSize.x == 0) tileSize.x = 1f;
            if (tileSize.y == 0) tileSize.y = 1f;
        }

        // compute offsets so board is centered at this GameObject's local (0,0)
        float offsetX = (BoardRow - 1) * 0.5f * tileSize.x;
        float offsetY = (BoardCollum - 1) * 0.5f * tileSize.y;

        // create tiles: start from top-left, then left->right, top->bottom
        for (int y = BoardCollum - 1; y >= 0; y--)
        {
            for (int x = 0; x < BoardRow; x++)
            {
                var tile = Instantiate(TileSquare, transform);
                float px = x * tileSize.x - offsetX;
                float py = y * tileSize.y - offsetY;
                tile.transform.localPosition = new Vector3(px, py, 0f);
                BoardTile.Add(tile);
                tile.name = $"Tile_{x}_{y}";
                SetTileLevel(tile, Random.Range(0, 5)); 
            }
        }
    }
    public int GetTileRow(GameObject Tile)
    {
        int index = BoardTile.IndexOf(Tile);
        return index / BoardRow;
    }
    public int GetTileCol(GameObject Tile)
    {
        int index = BoardTile.IndexOf(Tile);
        return index % BoardRow;
    }
    public void SetTileLevel(GameObject Tile, int level)
    {
        var tileSquare = Tile.GetComponent<TileSquare>();
        if (tileSquare != null)
        {
            tileSquare.TileLevel = level;
        }
    }
    public void SetTileLevel(int row, int col, int level)
    {
        int index = row * BoardRow + col;
        if (index >= 0 && index < BoardTile.Count)
        {
            SetTileLevel(BoardTile[index], level);
        }
    }
    #endregion
}
