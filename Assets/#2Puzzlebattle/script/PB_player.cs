using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PB_player : MonoBehaviour
{
<<<<<<< HEAD
<<<<<<< HEAD
    private void OnEnable()
    {
        holding = false;
=======
=======
>>>>>>> main
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void OnEnable()
    {
        switch (PlayerId)
        {
            case 0:
                transform.localPosition = Vector3.zero;
                Xmin = 0; Ymin = 0; Xmax = 4; Ymax = 9;
                break;
            case 1:
                transform.localPosition = new Vector3(9,0,0);
                Xmin = 5; Ymin = 0; Xmax = 9; Ymax = 9;
                break;
        }
<<<<<<< HEAD
>>>>>>> 376c1458b1e99c82f151fa3c0c58e24d958e9dfa
=======
>>>>>>> main
        anim = StartCoroutine(iconRotateAnime());
        StartCoroutine(RegisterButtons(PlayerId));
    }
    private void OnDisable()
    {
        UnregisterButtons(PlayerId);
        StopCoroutine(anim);
    }
    #region att
<<<<<<< HEAD
<<<<<<< HEAD
    public int playerid;
    public int PlayerId
    {
        get { return playerid; }
        set   
        { 
            playerid = value;
            switch (playerid)
            {
                case 0:
                    Pointer.GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                case 1:
                    Pointer.GetComponent<SpriteRenderer>().color = Color.blue;
                    break;
                case 2:
                    Pointer.GetComponent<SpriteRenderer>().color = Color.green;
                    break;
                case 3:
                    Pointer.GetComponent<SpriteRenderer>().color = Color.yellow;
                    break;
            }
            MovePlayerTo(Mathf.RoundToInt((BattleBoard.instance.TileList.Count - 1) * 0.25f * PlayerId));
        }
    }
    public bool holding=false;
    #endregion
        #region com
    public Transform Pointer, CurrentSelectTile, CurrentStandingTile;
    private Coroutine anim;
    private BattleBoard battleBoard
    {
        get { return BattleBoard.instance; }
    }
=======
=======
>>>>>>> main
    public int PlayerId;
    public int Xmin, Ymin, Xmax, Ymax;
    public GameObject CurrentSelectTile, CurrentStandingTile;
    #endregion
    #region com
    private Coroutine anim;
<<<<<<< HEAD
>>>>>>> 376c1458b1e99c82f151fa3c0c58e24d958e9dfa
=======
>>>>>>> main
    #endregion
    #region func
    private IEnumerator iconRotateAnime()
    {
        Vector3 rt = new Vector3(0, 0, 0.3f);
<<<<<<< HEAD
<<<<<<< HEAD
        Pointer.transform.eulerAngles = new Vector3(0,0,-3f);
=======
        transform.eulerAngles = new Vector3(0,0,-3f);
>>>>>>> 376c1458b1e99c82f151fa3c0c58e24d958e9dfa
=======
        transform.eulerAngles = new Vector3(0,0,-3f);
>>>>>>> main
        while (true)
        {
            for (int i = 0; i < 20; i++)
            {
<<<<<<< HEAD
<<<<<<< HEAD
                Pointer.transform.eulerAngles += rt;
                  yield return new WaitForEndOfFrame();
            }
            for (int i = 0; i < 20; i++)
            {
                Pointer.transform.eulerAngles -= rt;
=======
=======
>>>>>>> main
                transform.eulerAngles += rt;
                yield return new WaitForEndOfFrame();
            }
            for (int i = 0; i < 20; i++)
            {
                transform.eulerAngles -= rt;
<<<<<<< HEAD
>>>>>>> 376c1458b1e99c82f151fa3c0c58e24d958e9dfa
=======
>>>>>>> main
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
<<<<<<< HEAD
<<<<<<< HEAD
            if(!holding) MovePlayer("Down");
            else if(holding) SwapTile("Down");
        };
        GameManager.GM.Act_UpDown[player] += () =>
        {
            if(!holding) MovePlayer("Up");
            else if(holding) SwapTile("Up");
        };
        GameManager.GM.Act_LeftDown[player] += () =>
        { 
            if(!holding) MovePlayer("Left");
            else if (holding) SwapTile("Left");
        };
        GameManager.GM.Act_RightDown[player] += () =>
        {
            if(!holding) MovePlayer("Right");
            else if (holding) SwapTile ("Right");
        };
        GameManager.GM.Act_Fire1Down[player] += () =>
        {
            holding = !holding;
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
    }
    public void MovePlayer(string direction)
    {
         switch (direction)
         {
             case "Up":
                // Get tile above the current tile
                int upRow = battleBoard.GetTileRow(battleBoard.TileList.IndexOf(CurrentStandingTile.gameObject)) - 1;
                int upColumn = battleBoard.GetTileColumn(battleBoard.TileList.IndexOf(CurrentStandingTile.gameObject));
                GameObject upTile = battleBoard.GetTile(upRow, upColumn);
                if(upTile != null)
                {
                    MovePlayerTo(upTile);
                }
                break;
             case "Down":
                int downRow = battleBoard.GetTileRow(battleBoard.TileList.IndexOf(CurrentStandingTile.gameObject)) + 1;
                int downColumn = battleBoard.GetTileColumn(battleBoard.TileList.IndexOf(CurrentStandingTile.gameObject));
                GameObject downTile = battleBoard.GetTile(downRow, downColumn);
                if (downTile != null)
                {
                    MovePlayerTo(downTile);
                }
                break;
             case "Left":
                int leftRow = battleBoard.GetTileRow(battleBoard.TileList.IndexOf(CurrentStandingTile.gameObject));
                int leftColumn = battleBoard.GetTileColumn(battleBoard.TileList.IndexOf(CurrentStandingTile.gameObject)) - 1;
                GameObject leftTile = battleBoard.GetTile(leftRow, leftColumn);
                if (leftTile != null)
                {
                    MovePlayerTo(leftTile);
                }
                break;
             case "Right":
                int rightRow = battleBoard.GetTileRow(battleBoard.TileList.IndexOf(CurrentStandingTile.gameObject));
                int rightColumn = battleBoard.GetTileColumn(battleBoard.TileList.IndexOf(CurrentStandingTile.gameObject)) + 1;
                GameObject rightTile = battleBoard.GetTile(rightRow, rightColumn);
                if (rightTile != null)
                {
                    MovePlayerTo(rightTile);
                }
                break;
         }
    }
    public void MovePlayerTo(int tileId)
    {
        if (tileId < 0 || tileId >= battleBoard.TileList.Count) return;
        var target = battleBoard.TileList[tileId];
        int row = battleBoard.GetTileRow(tileId);
        int col = battleBoard.GetTileColumn(tileId);
        if (!IsTileAllowedForPlayer(row, col)) return;
        Pointer.position = target.transform.position;
        CurrentSelectTile = target.transform;
        CurrentStandingTile = target.transform;
    }
    public void MovePlayerTo(GameObject tile)
    {
        if (tile == null) return;
        int index = battleBoard.TileList.IndexOf(tile);
        if (index < 0) return;
        int row = battleBoard.GetTileRow(index);
        int col = battleBoard.GetTileColumn(index);
        if (!IsTileAllowedForPlayer(row, col)) return;
        Pointer.position = tile.transform.position;
        CurrentSelectTile = tile.transform;
        CurrentStandingTile = tile.transform;
    }
    private bool IsTileAllowedForPlayer(int row, int column)
    {
        int midRow = battleBoard.BoardY / 2; // rows 0..midRow-1 are upper
        int midCol = battleBoard.BoardX / 2; // cols 0..midCol-1 are left

        switch (PlayerId)
        {
            case 0: // UpperLeft
                return row >= 0 && row < midRow && column >= 0 && column < midCol;
            case 1: // UpperRight
                return row >= 0 && row < midRow && column >= midCol && column < battleBoard.BoardX;
            case 2: // LowerLeft
                return row >= midRow && row < battleBoard.BoardY && column >= 0 && column < midCol;
            case 3: // LowerRight
                return row >= midRow && row < battleBoard.BoardY && column >= midCol && column < battleBoard.BoardX;
            default:
                return false;
        }
    }
    public void SwapTile(string direction)
    {
        if (CurrentSelectTile == null || CurrentStandingTile == null) return;
        holding = false;
        int currentIndex = battleBoard.TileList.IndexOf(CurrentStandingTile.gameObject);
        int targetIndex = -1;
        switch (direction)
        {
            case "Up":
                targetIndex = currentIndex - battleBoard.BoardX;
                break;
            case "Down":
                targetIndex = currentIndex + battleBoard.BoardX;
                break;
            case "Left":
                targetIndex = currentIndex - 1;
                break;
            case "Right":
                targetIndex = currentIndex + 1;
                break;
        }
        if (targetIndex >= 0 && targetIndex < battleBoard.TileList.Count)
        {
            GameObject targetTile = battleBoard.TileList[targetIndex];
            if (IsTileAllowedForPlayer(battleBoard.GetTileRow(targetIndex), battleBoard.GetTileColumn(targetIndex)))
            {
                battleBoard.SwapTile(CurrentStandingTile.gameObject, targetTile);
                MovePlayerTo(targetTile);
            }
        }
=======
=======
>>>>>>> main
            MovePlayer(new Vector3Int(0, -1, 0));
        };
        GameManager.GM.Act_UpDown[player] += () =>
        {
            MovePlayer(new Vector3Int(0, 1, 0));
        };
        GameManager.GM.Act_LeftDown[player] += () =>
        {
            MovePlayer(new Vector3Int(-1, 0, 0));
        };
        GameManager.GM.Act_RightDown[player] += () =>
        {
            MovePlayer(new Vector3Int(1, 0, 0));
        };
        GameManager.GM.Act_Fire1Down[player] += () =>
        {
            SelectBlock();
        };
        GameManager.GM.Act_Fire2Down[player] += () =>
        {
            SelectBlock();
        };
        GameManager.GM.Act_Fire3Down[player] += () =>
        {
            SelectBlock();
        };
        GameManager.GM.Act_Fire4Down[player] += () =>
        {
            UnselectBlock();
        };
        Debug.Log(this.name + " Mode0");
    }
    public void MovePlayer(Vector3 moveValue)
    {
        transform.localPosition += moveValue;
        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x,Xmin, Xmax), Mathf.Clamp(transform.localPosition.y, Ymin, Ymax), 0);
        CurrentStandingTile = BattleBoard.instance.getBlockAt(Mathf.RoundToInt(transform.localPosition.x), Mathf.RoundToInt(transform.localPosition.y));
        if (CurrentSelectTile != null)
        {
            BattleBoard.instance.SwapTile(BattleBoard.instance.GetIndexOfBlock(CurrentSelectTile),BattleBoard.instance.GetIndexOfBlock(CurrentStandingTile));
            SelectBlock();
        }
        BattleBoard.instance.CheckBlockat(CurrentStandingTile);
    }
    public void SelectBlock()
    {
        UnselectBlock();
        CurrentSelectTile = BattleBoard.instance.getBlockAt(Mathf.RoundToInt(transform.localPosition.x), Mathf.RoundToInt(transform.localPosition.y));
        CurrentSelectTile.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }
    public void UnselectBlock()
    {
        try
        {
            CurrentSelectTile.transform.localScale = Vector3.one;
            CurrentSelectTile = null;
        }
        catch { }
<<<<<<< HEAD
>>>>>>> 376c1458b1e99c82f151fa3c0c58e24d958e9dfa
=======
>>>>>>> main
    }
    #endregion
}
