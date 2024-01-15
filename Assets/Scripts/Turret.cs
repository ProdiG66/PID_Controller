using UnityEngine;

public class Turret : Controller {
    [SerializeField]
    private PIDController controller;

    [SerializeField]
    private float _power;

    [SerializeField]
    private Transform target;

    private Rigidbody _rigidbody;

    public override float power {
        get => _power;
        set => _power = value;
    }

    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        Vector3 targetPosition = target.position;
        Vector3 position = _rigidbody.position;
        targetPosition.y = position.y;
        Vector3 targetDir = (targetPosition - position).normalized;
        Vector3 forwardDir = _rigidbody.rotation * Vector3.forward;

        float currentAngle = Vector3.SignedAngle(Vector3.forward, forwardDir, Vector3.up);
        float targetAngle = Vector3.SignedAngle(Vector3.forward, targetDir, Vector3.up);

        float input = controller.UpdateAngle(Time.fixedDeltaTime, currentAngle, targetAngle);
        _rigidbody.AddTorque(new Vector3(0, input * power, 0));
    }

    public override PIDController GetController() {
        return controller;
    }

    public override void SetTarget(int index) { }
}