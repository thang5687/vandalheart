using UnityEngine;

public class TileSquare : MonoBehaviour
{
    #region Attributes
    private int tileIndex, tileLevel;
    public int TileIndex
    {
        get { return tileIndex; }
        set { tileIndex = value; }
    }
    public int TileLevel
    {
        get { return tileLevel; }
        set 
        { 
            tileLevel = value;
            // Update the sprite based on the new tile level
            if (TileSprite != null && tileLevel >= 0 && tileLevel < TileSprite.Length)
            {
                Layer4.sprite = TileSprite[tileLevel];
            }
        }
    }
    #endregion

    #region Components
    public Sprite[] TileSprite;
    public SpriteRenderer Layer4;
    #endregion
    #region Functions

    #endregion
}
