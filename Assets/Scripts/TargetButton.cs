using UnityEngine;

public class TargetButton : MonoBehaviour {
    [SerializeField]
    private Controller controller;

    [SerializeField]
    private int value;

    public void SetValue() {
        controller.SetTarget(value);
    }
}