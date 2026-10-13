using UnityEngine;

public class Ship : MonoBehaviour {


    private void Update() {
        GameInput.Instance.GetRotationValue();
    }
}
