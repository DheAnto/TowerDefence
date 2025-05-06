using Unity.VisualScripting;
using UnityEngine;

public class cameraPositionScript : MonoBehaviour
{
    [SerializeField]private MapGenerator mapGenerator;
    void Start()
    {
        this.transform.position = new Vector3(mapGenerator.cameraPosition().x, mapGenerator.cameraPosition().y, -10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
