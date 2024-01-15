using System.Collections.Generic;
using UnityEngine;

public class Horizontal : Controller {
    [SerializeField]
    private PIDController controller;

    [SerializeField]
    private float _power;

    [SerializeField]
    private Transform[] targets;

    [SerializeField]
    private GameObject flameRight;

    [SerializeField]
    private GameObject flameLeft;

    [SerializeField]
    private float flameSize;

    private Rigidbody _rigidbody;
    private Vector3 _targetPosition;
    private List<Vector3> _targetPositions;

    public override float power {
        get => _power;
        set => _power = value;
    }

    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();

        _targetPositions = new List<Vector3>();
        foreach (Transform target in targets) _targetPositions.Add(target.position);
    }

    private void FixedUpdate() {
        float throttle = controller.Update(Time.fixedDeltaTime, _rigidbody.position.x, _targetPosition.x);
        _rigidbody.AddForce(new Vector3(throttle * power, 0, 0));

        SetScale(flameRight, -throttle);
        SetScale(flameLeft, throttle);
    }

    public override PIDController GetController() {
        return controller;
    }

    public override void SetTarget(int index) {
        _targetPosition = _targetPositions[index];
    }

    private void SetScale(GameObject go, float scale) {
        scale = Mathf.Clamp(scale, 0, 1);

        if (scale < 0.1f) {
            go.SetActive(false);
        }
        else {
            go.SetActive(true);
            go.GetComponent<Transform>().localScale = new Vector3(scale, scale, scale) * flameSize;
        }
    }
}