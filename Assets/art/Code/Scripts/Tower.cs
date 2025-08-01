using UnityEngine;

[System.Serializable]
public class Tower {

    public string name;
    public int cost;
    public GameObject prefab;

    public Tower(string _name, int _cost, GameObject _prefab)
    {
        name = _name;
        cost = _cost;
        prefab = _prefab;
    }

    public int getCost()
    {
        return cost;
    }

}
