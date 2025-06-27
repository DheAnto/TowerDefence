
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.U2D.Aseprite;
#endif
using UnityEngine;
using UnityEngine.Tilemaps;


public class MapGenerator : MonoBehaviour
{
    public GameObject[] treePrefabs;
    [SerializeField] private int mapWidth, mapHeight;
    [SerializeField] private GameObject isometricTile;
    [SerializeField] private Sprite upDownRoad, leftRightRoad, upLeftRoad, upRightRoad, leftDownRoad, rightDownRoad, emptyTile;
    [SerializeField] private GameObject pathPoint;
    [SerializeField] private GameObject enemyManager;
    // [SerializeField] private Color StreetColor;
    private float treeNoiseScale = .5f;
    private float treeDensity = .35f;
    
    private enum currentDirection
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
    private currentDirection curDirection = currentDirection.DOWN;
    private currentDirection lastDirection;
    private currentDirection lastTileDirection = currentDirection.DOWN;
    private int curX = 0;
    private int curY = 0;
    public struct TileData
    {
        public Transform transform;
        public int ID;
        public SpriteRenderer spriteToUse;

    }
    private TileData[,] pathMatrix;
    private int sameDirection = 0;
    [SerializeField] private int minSameDirection = 3;
    [SerializeField] private int maxSameDirection = 6;
    private int pathCounter = 0;


    void Awake()
    {
        pathMatrix = new TileData[mapWidth, mapHeight];
        GenerateMap();
    }

    void GenerateMap()
    {

        float xOffset = (mapWidth + mapHeight) / 8f;
        float yOffset = (mapWidth + mapHeight) / 8f;
        for (int i = mapWidth - 1; i >= 0; i--)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                float iOffset = (i + j) / 2f;
                float jOffset = (i - j) / 4f;

                GameObject tile = Instantiate(isometricTile, new Vector3(iOffset - xOffset, jOffset + yOffset, 0), Quaternion.identity);

                pathMatrix[i, j].ID = 0;
                pathMatrix[i, j].spriteToUse = tile.GetComponent<SpriteRenderer>();
                pathMatrix[i, j].transform = tile.transform;
                pathMatrix[i, j].spriteToUse.sprite = emptyTile;
                pathMatrix[i, j].spriteToUse.sortingOrder = -i + j;

            }
        }
        StartCoroutine(GeneratePath());
        
    }

    IEnumerator GeneratePath()
    {

        curX = Random.Range(0, mapWidth);
        pathMatrix[curX, 0].ID = ++pathCounter;
        pathMatrix[curX, curY].spriteToUse.sprite = upDownRoad;
        pathMatrix[curX, curY].transform.position = new Vector2(pathMatrix[curX, curY].transform.position.x, pathMatrix[curX, curY].transform.position.y + 0.3f);
        pathMatrix[curX, curY].spriteToUse.sortingOrder = -curX + curY;
        Vector3 pathPointPosition = new Vector3(pathMatrix[curX, curY].transform.position.x - 5, pathMatrix[curX, curY].transform.position.y + 5, 0);
        GameObject pathPointRombo = Instantiate(pathPoint, pathPointPosition, Quaternion.identity);
        LevelManager.main.addPoint(pathPointRombo);



        while (curY < mapHeight - 1)
        {
            if ((curDirection == currentDirection.LEFT && (curX == 0 || pathMatrix[curX - 1, curY].ID != 0) ||
                curDirection == currentDirection.UP && (curY == 0 || pathMatrix[curX, curY - 1].ID != 0) ||
                curDirection == currentDirection.RIGHT && (curX == mapWidth - 1 || pathMatrix[curX + 1, curY].ID != 0)) ||
                (sameDirection > minSameDirection && Random.Range(0, 2) == 0))
            {
                changeDirection();
            }
            else
            {
                lastTileDirection = curDirection;
            }
            updateMatrix();
            yield return new WaitForSeconds(0.05f);
        }
        GenerateTrees(pathMatrix);
        LevelManager.main.transferListToArray();
        enemyManager.SetActive(true);
    }

    private void changeDirection()
    {
        currentDirection tempDirection;
        if (curDirection == currentDirection.LEFT && (curX == 0 || pathMatrix[curX + 1, curY].ID != 0) ||
            curDirection == currentDirection.RIGHT && (curX == mapWidth - 1 || pathMatrix[curX + 1, curY].ID != 0))
        {
            tempDirection = currentDirection.DOWN;
        }
        else if (curDirection == currentDirection.DOWN && ((curX > 0 && pathMatrix[curX - 1, curY].ID != 0) ||
                  (curX < mapWidth - 1 && pathMatrix[curX + 1, curY].ID != 0)))
        {
            tempDirection = currentDirection.DOWN;
            return;
        }
        else if (curDirection == currentDirection.UP)
        {
            tempDirection = lastDirection;
        }
        else if ((curX - 1 < 0 || pathMatrix[curX - 1, curY].ID != 0) && (curX + 1 == mapWidth || pathMatrix[curX + 1, curY].ID != 0))
        {
            return;
        }
        else
        {
            tempDirection = (currentDirection)Random.Range(0, 4);
            if (tempDirection == curDirection ||
               (tempDirection == currentDirection.LEFT &&
               ((curX < minSameDirection || curDirection == currentDirection.RIGHT) || pathMatrix[curX - 1, curY].ID != 0)) ||
               (tempDirection == currentDirection.UP &&
               ((curY < minSameDirection || curDirection == currentDirection.DOWN) || pathMatrix[curX, curY - 1].ID != 0)) ||
               (tempDirection == currentDirection.RIGHT &&
               ((curX > mapWidth - 1 - maxSameDirection || curDirection == currentDirection.LEFT) || pathMatrix[curX + 1, curY].ID != 0)) ||
               (tempDirection == currentDirection.DOWN && curDirection == currentDirection.UP))
            {
                changeDirection();
                return;
            }
        }

        lastTileDirection = curDirection;
        lastDirection = curDirection;
        curDirection = tempDirection;
        sameDirection = 0;
    }

    void updateMatrix()
    {
        sameDirection++;
        switch (curDirection)
        {
            case (currentDirection.DOWN):
                pathMatrix[curX, ++curY].ID = ++pathCounter;
                break;
            case currentDirection.UP:
                pathMatrix[curX, --curY].ID = ++pathCounter;
                break;
            case currentDirection.LEFT:
                pathMatrix[--curX, curY].ID = ++pathCounter;
                break;
            case currentDirection.RIGHT:
                pathMatrix[++curX, curY].ID = ++pathCounter;
                break;
        }
        pathMatrix[curX, curY].spriteToUse.sprite = chooseSprite();
        updatePreviousSprite();
        pathMatrix[curX, curY].spriteToUse.sortingOrder = -curX + curY;
        pathMatrix[curX, curY].transform.position = new Vector2(pathMatrix[curX, curY].transform.position.x, pathMatrix[curX, curY].transform.position.y + 0.3f);
        disableCollider(curX, curY);
        // SpriteRenderer sr = pathMatrix[curX, curY].transform.GetComponent<SpriteRenderer>();
        // sr.color = StreetColor;
        GameObject pathPointRombo = Instantiate(pathPoint, pathMatrix[curX, curY].transform.position, Quaternion.identity);
        LevelManager.main.addPoint(pathPointRombo);
    }

    private Sprite chooseSprite()
    {
        if (curDirection == currentDirection.DOWN || curDirection == currentDirection.UP)
        {
            return upDownRoad;
        }
        else
        {
            return leftRightRoad;
        }
    }

    private void updatePreviousSprite()
    {
        if (curDirection == lastTileDirection)
        {
            return;
        }
        else
        {
            if (curDirection == currentDirection.DOWN)
            {
                if (lastTileDirection == currentDirection.LEFT)
                {
                    pathMatrix[curX, curY - 1].spriteToUse.sprite = rightDownRoad;
                }
                if (lastTileDirection == currentDirection.RIGHT)
                {
                    pathMatrix[curX, curY - 1].spriteToUse.sprite = leftDownRoad;
                }
            }
            if (curDirection == currentDirection.UP)
            {
                if (lastTileDirection == currentDirection.LEFT)
                {
                    pathMatrix[curX, curY + 1].spriteToUse.sprite = upRightRoad;
                }
                if (lastTileDirection == currentDirection.RIGHT)
                {
                    pathMatrix[curX, curY + 1].spriteToUse.sprite = upLeftRoad;
                }
            }
            if (curDirection == currentDirection.LEFT)
            {
                if (lastTileDirection == currentDirection.UP)
                {
                    pathMatrix[curX + 1, curY].spriteToUse.sprite = leftDownRoad;
                }
                if (lastTileDirection == currentDirection.DOWN)
                {
                    pathMatrix[curX + 1, curY].spriteToUse.sprite = upLeftRoad;
                }
            }
            if (curDirection == currentDirection.RIGHT)
            {
                if (lastTileDirection == currentDirection.UP)
                {
                    pathMatrix[curX - 1, curY].spriteToUse.sprite = rightDownRoad;
                }
                if (lastTileDirection == currentDirection.DOWN)
                {
                    pathMatrix[curX - 1, curY].spriteToUse.sprite = upRightRoad;
                }
            }
        }
    }

    public Vector3 cameraPosition()
    {
        return pathMatrix[mapWidth / 2, mapHeight / 2].transform.position;
    }

    private void disableCollider(int x, int y)
    {
        Debug.Log("removing collider");
        PolygonCollider2D collider = pathMatrix[x, y].spriteToUse.GetComponent<PolygonCollider2D>();
        if (collider != null)
        {
            GameObject.Destroy(collider);
            Debug.Log("collider removed");
        }
    }

    private void GenerateTrees(TileData[,] pathMatrix)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];
        (float xOffset, float yOffset) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
        
        float mapXOffset = (mapWidth + mapHeight) / 8f;
        float mapYOffset = (mapWidth + mapHeight) / 8f;

        // Generate noise map
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.PerlinNoise(x * treeNoiseScale + xOffset, y * treeNoiseScale + yOffset);
            }
        }

        // Place trees
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if (pathMatrix[x, y].ID == 0 && noiseMap[x, y] < treeDensity)
                {
                    disableCollider(x, y);
                    GameObject tree = Instantiate(treePrefabs[Random.Range(0, treePrefabs.Length)], transform);
                    
                    
                    // Same isometric positioning as tiles
                        float isoX = (x + y) / 2f - mapXOffset;
                    float isoY = (x - y) / 4f + mapYOffset;
                    tree.transform.position = new Vector3(isoX, isoY, 0.1f); // Z = 0.1f for depth
                    
                    // Set sorting order (same as tiles + small offset)
                    SpriteRenderer treeRenderer = tree.GetComponent<SpriteRenderer>();
                    if (treeRenderer != null)
                    {
                        treeRenderer.sortingOrder = -x + y + 100; // Just slightly above tile
                    }
                    
                    tree.transform.localScale = Vector3.one * Random.Range(1.2f, 1.5f);
                }
            }
        }
    }
}