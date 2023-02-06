using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScorlling : MonoBehaviour
{
    [SerializeField] Transform playerTransform; //플레이어 위치
    Vector2Int currentTilePosition = new Vector2Int(0,0); //현재 타일 위치
    [SerializeField] Vector2Int playerTilePosition;
    Vector2Int onTileGridPlayerPosition;
    [SerializeField] float tileSize = 40f;  //타일 크기
    GameObject[,] terrainTiles;

    [SerializeField] int terrainTileHorizontalCount;    //타일 x축
    [SerializeField] int terrianTileVerticalCount;  //타일 y축

    [SerializeField] int fieldOfVisionHeight = 3;
    [SerializeField] int fieldOfVisionWidth = 3;

    private void Awake()
    {
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrianTileVerticalCount];
    }

    private void Start()
    {
        UpdateTilesOnScreen();
    }

    private void Update()
    {
        playerTilePosition.x = (int)(playerTransform.position.x / tileSize);
        playerTilePosition.y = (int)(playerTransform.position.y / tileSize);

        playerTilePosition.x -= playerTransform.position.x < 0 ? 1 : 0;
        playerTilePosition.y -= playerTransform.position.y < 0 ? 1 : 0;

        if (currentTilePosition != playerTilePosition)
        {
            currentTilePosition = playerTilePosition;

            onTileGridPlayerPosition.x = CalculatePositionOnAxis(onTileGridPlayerPosition.x, true);
            onTileGridPlayerPosition.y = CalculatePositionOnAxis(onTileGridPlayerPosition.y, false);
            UpdateTilesOnScreen();
        }
    }

    private void UpdateTilesOnScreen()
    {
        for (int pov_x = -(fieldOfVisionWidth/2); pov_x < (fieldOfVisionWidth / 2); pov_x++)
        {
            for (int pov_y = -(fieldOfVisionHeight/2); pov_y < fieldOfVisionHeight/2; pov_y++)
            {
                int tileToUpdate_x = CalculatePositionOnAxis(playerTilePosition.x + pov_x, true);
                int tileToUpdate_y = CalculatePositionOnAxis(playerTilePosition.y + pov_y, false);

                Debug.Log("tileToUpdate_x" + tileToUpdate_x + "tileToUpdate_y" + tileToUpdate_y);

                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_y];
                tile.transform.position = CalculateTilePosition(playerTilePosition.x + pov_x, playerTilePosition.y + pov_y);
            }
        }
    }

    private Vector3 CalculateTilePosition(int x, int y)
    {
        return new Vector3(x * tileSize, y * tileSize, 0f);
    }

    private int CalculatePositionOnAxis(int currentValue, bool horizontal)
    {
        if (horizontal)
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileHorizontalCount;
            }
            else {
                currentValue += 1;
                currentValue = terrainTileHorizontalCount - 1 + currentValue % terrainTileHorizontalCount;
            }
        }
        else {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrianTileVerticalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = terrianTileVerticalCount - 1 + currentValue % terrianTileVerticalCount;
            }
        }
        return (int)currentValue;
    }

    public void Add(GameObject TileGameObject, Vector2Int tilePosition)
    {
        terrainTiles[tilePosition.x, tilePosition.y] = TileGameObject;
    }
}
