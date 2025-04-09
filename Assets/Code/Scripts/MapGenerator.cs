using Unity.VisualScripting;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int mapWidth, mapHeight;
    [SerializeField] private GameObject isometricTile;
    [SerializeField] private Sprite upDownRoad, leftRightRoad, upLeftRoad, upRightRoad, leftDownRoad, rightDownRoad;
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
    private int sameDirection = 0;
    [SerializeField] private int minSameDirection = 5;
    private int pathCounter = 0;


    void Awake(){
        GenerateMap();
    }

    void GenerateMap(){
        float xOffset = (mapWidth + mapHeight) / 8f;
        float yOffset = (mapWidth + mapHeight) / 8f;
        for (int i = mapWidth; i >= 0; i--){
            for(int j = 0; j < mapHeight; j++){
                float iOffset = (i + j) / 2f;
                float jOffset = (i - j) / 4f;
                
                GameObject tile = Instantiate(isometricTile, new Vector3(iOffset - xOffset, jOffset + yOffset, 0), Quaternion.identity);
            }
        }
    }

    void GeneratePath(){
        int[,] pathMatrix = new int[mapWidth, mapHeight];
        int tileNumber = Random.Range(0, mapWidth);
        pathMatrix[tileNumber, 0] = ++pathCounter;

        while(curY < mapHeight -1)
        {
            if (sameDirection < minSameDirection)
            {
                if (curDirection == currentDirection.LEFT && (curX == 0 || pathMatrix[curX - 1, curY] != 0)|| 
                    curDirection == currentDirection.UP && (curY == 0 || pathMatrix[curX, curY - 1] != 0) || 
                    curDirection == currentDirection.RIGHT && (curX == mapWidth - 1 || pathMatrix[curX + 1, curY] != 0))
                {
                    changeDirection();
                }
            }
        }
    }

    private void changeDirection()
    {
        currentDirection tempDirection= (currentDirection)Random.Range(0, 4);
        if (tempDirection == curDirection ||
            (tempDirection == currentDirection.LEFT && curX < minSameDirection) || 
            (tempDirection == currentDirection.UP && curY < minSameDirection) ||
            (tempDirection == currentDirection.RIGHT && curX > mapWidth - 1 - minSameDirection))
        {
            changeDirection();
        }
        lastDirection = curDirection;
        curDirection = tempDirection;
        
    }
}
