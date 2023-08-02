


//Yo I wanted to try and recreate the logic behind the christmas prototype you showed me using modulo operator to reduce the amount of code needed.
//You can see my attempt at GenerateTileArray() commented out below, its almost right but jumps when you move horizontally. In the end ChatGPT 
//solved the problem but I had to ask many times haha. This version has a mirroring issue around the origin. DPG 31/7/23

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RepeatingTileMap : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tile;

    private GameObject player;
    private Vector2Int lastPlayerPosition;
    private const int LEVEL_WIDTH = 8;

    //super simple level data for testing
    private const string LEVEL_DATA =
        "0000##00" +
        "000#0000" +
        "00#0000#" +
        "0#000000" +
        "#0000#00" +
        "00#####0" +
        "0#000000" +
        "#0000000";

    //adding more comment

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Vector2Int currentPlayerPosition = new Vector2Int((int)player.transform.position.x, (int)player.transform.position.y);

        if (currentPlayerPosition==lastPlayerPosition){return;}

        lastPlayerPosition = currentPlayerPosition;
        Vector3Int position = new Vector3Int((int)player.transform.position.x - LEVEL_WIDTH / 2, (int)player.transform.position.y - LEVEL_WIDTH / 2, 0);
        Vector3Int size = new Vector3Int(LEVEL_WIDTH, LEVEL_WIDTH, 1);
        BoundsInt boundsInt = new BoundsInt(position, size);
        tilemap.SetTilesBlock(boundsInt, GenerateTileArray(position));
        tilemap.RefreshAllTiles();
    }


    //This is the almost working attempt that I fed to chatGPT to diagnose.
    //private TileBase[] GenerateTileArray(Vector3Int position)
    //{
    //    int xOffset = position.x % LEVEL_WIDTH;
    //    int yOffset = position.y % LEVEL_WIDTH;
    //    TileBase[] tileArray = new TileBase[LEVEL_DATA.Length];
    //    for (int i = 0; i < LEVEL_DATA.Length; i++)
    //    {
    //        int index = (((yOffset * LEVEL_WIDTH + xOffset) + i) % LEVEL_DATA.Length);
    //        if (LEVEL_DATA[index] == '#')
    //        {
    //            tileArray[i] = tile;
    //        }
    //        else
    //        {
    //            tileArray[i] = null;
    //        }
    //    }
    //    return tileArray;
    //}


  //ChatGPT revised version
    private TileBase[] GenerateTileArray(Vector3Int position)
    {
        int xOffset = position.x % LEVEL_WIDTH;
        int yOffset = position.y % LEVEL_WIDTH;
       
        TileBase[] tileArray = new TileBase[LEVEL_DATA.Length];

        for (int i = 0; i < LEVEL_DATA.Length; i++)
        {
            int xIndex = (i % LEVEL_WIDTH) + xOffset;
            int yIndex = (i / LEVEL_WIDTH) + yOffset;

            // Wrap the indices within the range of LEVEL_WIDTH using modulo
            xIndex = Mathf.Abs(xIndex) % LEVEL_WIDTH;
            yIndex = (yIndex + LEVEL_WIDTH) % LEVEL_WIDTH;

            // Calculate the new index using the wrapped xIndex and yIndex
            int index = yIndex * LEVEL_WIDTH + xIndex;

            Debug.Log("LEVEL_DATA[index]: " + LEVEL_DATA[index]);
            if (LEVEL_DATA[index] == '#')
            {
                tileArray[i] = tile;
            }
            else
            {
                tileArray[i] = null;
            }
        }

        return tileArray;
    }
}
