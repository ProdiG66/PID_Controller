using System.Collections.Generic;
using UnityEngine;

public class Vertical : Controller {
    [SerializeField]
    private PIDController controller;

    [SerializeField]
    private float _power;

    [SerializeField]
    private Transform[] targets;

    [SerializeField]
    private GameObject flame;

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

        SetTarget(0);
    }

    private void FixedUpdate() {
        float throttle = controller.Update(Time.fixedDeltaTime, _rigidbody.position.y, _targetPosition.y);
        _rigidbody.AddForce(new Vector3(0, throttle * power, 0));

        SetScale(flame, throttle);
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