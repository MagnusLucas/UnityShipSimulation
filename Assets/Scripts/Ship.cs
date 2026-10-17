using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.Rendering;

public class Ship : MonoBehaviour {

    const float MAST_MOVE_SPEED = 0.4f;
    const float SAIL_OPEN_CLOSE_SPEED = 0.6f;

    public static Ship Instance {  get; private set; }

    [SerializeField] private float maxBoatSpeed;

    [SerializeField] private GameObject mast;
    [SerializeField] private float mastMinDegrees;
    [SerializeField] private float mastMaxDegrees;

    [SerializeField] private GameObject sail;
    [SerializeField] private float sailMinScale;
    [SerializeField] private float sailMaxScale;

    private float degreeRange;
    private float sailRange;

    private float mastPercentRotation;
    private float sailPercentScale;

    private void Awake() {
        Instance = this;

        degreeRange = mastMaxDegrees - mastMinDegrees;
        sailRange = sailMaxScale - sailMinScale;

        mastPercentRotation = (mast.transform.rotation.y - mastMinDegrees) / degreeRange;
        sailPercentScale = (sail.transform.localScale.z - sailMinScale) / sailRange;
    }

    private void Update() {
        HandleInput();

        Quaternion globalSailRotation = mast.transform.rotation;
        Vector3 perpendicularToSail = Quaternion.Euler(globalSailRotation.eulerAngles) * Vector3.left; // Left is the prefab sail orientation

        Vector3 usableWind = Vector3.Project(Wind.Direction, perpendicularToSail);
        Debug.Log("s: " + perpendicularToSail + " w: " + Wind.Direction + " r: " + usableWind + " (" + usableWind.magnitude * 100.0f + "%)");

        //sould move relative to boat! or not, depends on the boat ig :p
        transform.position += usableWind * sailPercentScale * maxBoatSpeed * Time.deltaTime;
    }

    private void HandleInput() {
        mastPercentRotation += GameInput.Instance.GetRotationValue() * MAST_MOVE_SPEED * Time.deltaTime;
        mastPercentRotation = Mathf.Clamp(mastPercentRotation, 0f, 1f);
        Quaternion rotationChange = Quaternion.Euler(0, mastPercentRotation * degreeRange + mastMinDegrees, 0);
        mast.transform.rotation = rotationChange;


        sailPercentScale += GameInput.Instance.GetSailChangeValue() * SAIL_OPEN_CLOSE_SPEED * Time.deltaTime;
        sailPercentScale = Mathf.Clamp(sailPercentScale, 0f, 1f);
        float newZScale = sailPercentScale * sailRange + sailMinScale;
        Vector3 newScale = new Vector3(sail.transform.localScale.x, sail.transform.localScale.y, newZScale);
        sail.transform.localScale = newScale;
    }

    public float GetMastRotation() {
        float rotationDegrees = mast.transform.localRotation.eulerAngles.y;
        if (rotationDegrees > 180) {
            rotationDegrees -= 360;
        }
        return rotationDegrees;
    }

    public float GetSailOpennessValue() {
        return sailPercentScale * 100.0f;
    }
}
