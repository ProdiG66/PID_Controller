using UnityEngine;

public abstract class Controller : MonoBehaviour {
    public abstract float power { get; set; }
    public abstract PIDController GetController();
    public abstract void SetTarget(int index);
}