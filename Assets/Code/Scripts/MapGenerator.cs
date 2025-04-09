
using System.Collections;
using UnityEngine;


public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int mapWidth, mapHeight;
    [SerializeField] private GameObject isometricTile;
    [SerializeField] private Sprite upDownRoad, leftRightRoad, upLeftRoad, upRightRoad, leftDownRoad, rightDownRoad, emptyTile;
    private enum currentDirection
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
    private currentDirection curDirection = currentDirection.DOWN;
    private currentDirection lastDirection;
    private int curX = 0;
    private int curY = 0;
    public struct TileData {
        public Transform transform;
        public int ID;
        public SpriteRenderer spriteToUse;

    }
    private TileData[,] pathMatrix;
    private int sameDirection = 0;
    [SerializeField] private int minSameDirection = 3;
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
                Debug.Log(i +"  " + j);
                float iOffset = (i + j) / 2f;
                float jOffset = (i - j) / 4f;

                GameObject tile = Instantiate(isometricTile, new Vector3(iOffset - xOffset, jOffset + yOffset, 0), Quaternion.identity);
                pathMatrix[i, j].ID = 0;
                pathMatrix[i, j].spriteToUse = tile.GetComponent<SpriteRenderer>();
                pathMatrix[i, j].transform = tile.transform;
                pathMatrix[i, j].spriteToUse.sprite = emptyTile;
            }
        }
        StartCoroutine(GeneratePath());
    }

    IEnumerator GeneratePath()
    {
        
        curX = Random.Range(0, mapWidth);
        pathMatrix[curX, 0].ID = ++pathCounter;
        pathMatrix[curX, curY].spriteToUse.sprite = upDownRoad;

        while (curY < mapHeight - 1)
        {
            if (sameDirection < minSameDirection)
            {
                if (curDirection == currentDirection.LEFT && (curX == 0 || pathMatrix[curX - 1, curY].ID != 0) ||
                    curDirection == currentDirection.UP && (curY == 0 || pathMatrix[curX, curY - 1].ID != 0) ||
                    curDirection == currentDirection.RIGHT && (curX == mapWidth - 1 || pathMatrix[curX + 1, curY].ID != 0))
                {
                    changeDirection();
                }
            }
            else if (Random.Range(0, 3) == 0)
            {
                changeDirection();
            }
            yield return new WaitForSeconds(0.05f);
        }
        
    }

    private void changeDirection()
    {
        if(curDirection == currentDirection.UP){
            
        }
        currentDirection tempDirection = (currentDirection)Random.Range(0, 4);
        if (tempDirection == curDirection ||
            (tempDirection == currentDirection.LEFT && curX < minSameDirection) ||
            (tempDirection == currentDirection.UP && curY < minSameDirection) ||
            (tempDirection == currentDirection.RIGHT && curX > mapWidth - 1 - minSameDirection))
        {
            changeDirection();
            return;
        }
        lastDirection = curDirection;
        curDirection = tempDirection;
        sameDirection = 0;
    }

    void updateMatrix(){
        sameDirection++;
        switch(curDirection){
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
        pathMatrix[curX, curY].spriteToUse.sprite = upDownRoad;
    }

    void buildPath(){

    }
}