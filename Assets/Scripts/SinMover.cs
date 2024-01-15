using UnityEngine;

public class SinMover : MonoBehaviour {
    [SerializeField]
    private float amplitude;

    [SerializeField]
    private float frequency;

    private Vector3 _startPosition;

    private void Start() {
        _startPosition = transform.position;
    }

    private void Update() {
        transform.position = _startPosition + new Vector3(Mathf.Sin(Time.time * frequency) * amplitude, 0, 0);
    }
}