using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int mapWidth, mapHeight;
    [SerializeField] private GameObject isometricTile;


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
        int startingTile = Random.Range(0, mapWidth);
        
    }
}
