using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectGameMenu : MonoBehaviour
{
    void Start()
    {
        iconpos = 0;
    }
    private void OnEnable()
    {
        StartCoroutine(DelayEnable());
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
    #region attributes 
    private int iconpos;
    public int Iconpos
    {
        get
        {
            return iconpos;
        }
        set
        {
            if(value < 0) value = 0;
            if(value > listGame.Length-1) value = listGame.Length-1;
            iconpos = value;
            selectIcon.transform.parent = listGame[value];
            selectIcon.GetComponent<RectTransform>().anchoredPosition=Vector2.zero;
            CenterItem(listGame[value].GetComponent<RectTransform>());
            SelectEffect(listGame[value]);
            SoundManager.Instance.playSoundone(9);
        }
    }
   
    #endregion
    #region components 
    public Transform selectIcon;
    public GridLayoutGroup listGameGrid;
    public Transform[] listGame;
    public ScrollRect scrollView;
    public RectTransform contentPanel;
    #endregion
    #region functions 
    public IEnumerator DelayEnable()
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
            Iconpos += CalculateGridDimensions(listGameGrid).y;
        };
        GameManager.GM.Act_UpDown[0] += () =>
        {
            Iconpos -= CalculateGridDimensions(listGameGrid).y;
        };
        GameManager.GM.Act_LeftDown[0] += () =>
        {
            Iconpos -= 1;
        };
        GameManager.GM.Act_RightDown[0] += () =>
        {
            Iconpos += 1;
        }; 
        GameManager.GM.Act_Fire1Down[0] += () =>
        {
            SceneManager.LoadScene(Iconpos+1);
        };
        GameManager.GM.Act_Fire2Down[0] += () =>
        {
            SceneManager.LoadScene(Iconpos + 1);
        };
        GameManager.GM.Act_Fire3Down[0] += () =>
        {
            SceneManager.LoadScene(Iconpos + 1);
        };
        GameManager.GM.Act_Fire4Down[0] += () =>
        {
            SceneManager.LoadScene(Iconpos + 1);
        };
        Debug.Log(this.name + " OnEnable");
    }
    private Vector2Int CalculateGridDimensions(GridLayoutGroup gridLayout)
    {
        int totalChildren = listGame.Length;
        int rowCount = 0;
        int columnCount = 0;
        if (totalChildren == 0)
        {
            rowCount = 0;
            columnCount = 0;
            return new Vector2Int(0,0);
        }

        // The calculation depends on the Constraint type set in the GridLayoutGroup component
        if (gridLayout.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
        {
            columnCount = gridLayout.constraintCount;
            rowCount = Mathf.CeilToInt((float)totalChildren / columnCount);
        }
        else if (gridLayout.constraint == GridLayoutGroup.Constraint.FixedRowCount)
        {
            rowCount = gridLayout.constraintCount;
            columnCount = Mathf.CeilToInt((float)totalChildren / rowCount);
        }
        return new Vector2Int(rowCount, columnCount);
    }
    public void CenterItem(RectTransform targetItem)
    {
        StopAllCoroutines();
        StartCoroutine(CenterItemCoroutine(targetItem));
    }
    private IEnumerator CenterItemCoroutine(RectTransform targetItem)
    {
        // Calculate the target normalized position
        Vector2 targetNormalizedPosition = CalculateTargetNormalizedPosition(targetItem);

        // Smoothly move the scroll view to the target position
        while (Vector2.Distance(contentPanel.anchoredPosition, CalculateTargetNormalizedPosition(targetItem)) > 0.01f)
        {
            contentPanel.anchoredPosition = Vector2.Lerp(contentPanel.anchoredPosition, CalculateTargetNormalizedPosition(targetItem), 5f * Time.deltaTime);
            yield return null;
        }

        //scrollView.normalizedPosition = targetNormalizedPosition;
    }
    private Vector2 CalculateTargetNormalizedPosition(RectTransform targetItem)
    {
        // Get the local position of the target item within the content panel
        Vector3 targetLocalPosition = contentPanel.InverseTransformPoint(targetItem.position);
        //Debug.Log(targetLocalPosition);
        // Get the size of the item
        Vector2 itemSize = targetItem.rect.size;
        //Debug.Log(itemSize);
        // Get the size of the scrollable viewport
        Vector2 viewportSize = ((RectTransform)scrollView.transform).rect.size;
        //Debug.Log(viewportSize);
        // Get the total size of the content panel
        Vector2 contentSize = contentPanel.rect.size;
        //Debug.Log(contentSize);

        // Calculate the required scroll position to center the item
        // The pivot of UI elements is typically (0.5, 0.5) for the center
        float xScroll = (targetLocalPosition.x/ contentSize.x)*(viewportSize.x - contentSize.x);
        //Debug.Log(xScroll);
        float yScroll = Mathf.Clamp01((targetLocalPosition.y + contentSize.y / 2 - viewportSize.y / 2) / (contentSize.y - viewportSize.y));

        // Unity's normalized position goes from (0,0) at the bottom-left to (1,1) at the top-right.
        // If the grid scrolls vertically, we primarily care about the Y axis (0 is bottom, 1 is top).
        // If the grid scrolls horizontally, we primarily care about the X axis.

        // Adjust for vertical scrolling (common in grid views)
        // Since content moves down as we scroll down, normalized position for items at top is 1
        float finalY = 1 - yScroll; // Invert the Y calculation if your grid layout starts from the top

        return new Vector2(xScroll, Mathf.Clamp01(finalY));
    }
    private void SelectEffect(Transform item)
    {
        foreach(var lg in listGame)
        {
            lg.transform.localScale = Vector3.one;
        }
        item.transform.localScale = new Vector3(1.2f, 1.2f, 1);
        selectIcon.transform.localScale = Vector3.one;
    }
    #endregion
}
