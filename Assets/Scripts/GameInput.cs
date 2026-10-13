using UnityEngine;

public class GameInput : MonoBehaviour {

    private PlayerInputActions playerInputActions;
    public static GameInput Instance { get; private set; }

    public GameInput() {
        Instance = this;
    }

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }

    public float GetRotationValue() {
        float rotationDirection = playerInputActions.Player.SailRotationDirection.ReadValue<float>();
        Debug.Log("Rotation: " + rotationDirection);
        return rotationDirection;
    }

    public float GetSailChangeValue() {
        return 0;
    }

}
