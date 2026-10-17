using UnityEngine;

public class Wind : MonoBehaviour
{

    public static Vector3 Direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Direction = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * Vector3.right;
        Debug.Log("Wind: " + Direction);
    }
}
