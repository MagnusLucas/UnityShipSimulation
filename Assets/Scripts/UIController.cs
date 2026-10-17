using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour {

    private UnityEngine.UIElements.Label rotationLabel;

    private UnityEngine.UIElements.Label sailValueLabel;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        var root = GetComponent<UIDocument>().rootVisualElement;

        rotationLabel = root.Q<UnityEngine.UIElements.Label>("RotationLabel");
        sailValueLabel = root.Q<UnityEngine.UIElements.Label>("ScaleLabel");
    }

    // Update is called once per frame
    void Update() {
        rotationLabel.text = "Rotation: " + Ship.Instance.GetMastRotation();
        sailValueLabel.text = "Sail: " + Ship.Instance.GetSailOpennessValue() + "%";
    }
}
