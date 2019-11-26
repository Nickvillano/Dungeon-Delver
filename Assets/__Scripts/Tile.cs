using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [Header("Set Dynamically")]
    public int x;
    public int y;
    public int tileNum;

    public void SetTile(int eX, int eY, int eTileNum = -1) //a
    {
        x = eX;
        y = eY;
        transform.localPosition = new Vector3(x, y, 0);
        gameObject.name = x.ToString("D3") + "x" + y.ToString("D3"); //b

        if(eTileNum == -1)
        {
            eTileNum = TileCamera.GET_MAP(x, y);    //c
        }
        tileNum = eTileNum;
        GetComponent<SpriteRenderer>().sprite = TileCamera.SPRITES[tileNum]; //d
    }
}
