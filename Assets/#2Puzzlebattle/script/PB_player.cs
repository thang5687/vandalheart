using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PB_player : MonoBehaviour
{
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
        anim = StartCoroutine(iconRotateAnime());
        StartCoroutine(RegisterButtons(PlayerId));
    }
    private void OnDisable()
    {
        UnregisterButtons(PlayerId);
        StopCoroutine(anim);
    }
    #region att
    public int PlayerId;
    public int Xmin, Ymin, Xmax, Ymax;
    public GameObject CurrentSelectTile, CurrentStandingTile;
    #endregion
    #region com
    private Coroutine anim;
    #endregion
    #region func
    private IEnumerator iconRotateAnime()
    {
        Vector3 rt = new Vector3(0, 0, 0.3f);
        transform.eulerAngles = new Vector3(0,0,-3f);
        while (true)
        {
            for (int i = 0; i < 20; i++)
            {
                transform.eulerAngles += rt;
                yield return new WaitForEndOfFrame();
            }
            for (int i = 0; i < 20; i++)
            {
                transform.eulerAngles -= rt;
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
    }
    #endregion
}
