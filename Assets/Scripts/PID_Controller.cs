using System;
using UnityEngine;

[Serializable]
public class PIDController {
    public enum DerivativeMeasurement {
        Velocity,
        ErrorRateOfChange
    }

    public float proportionalGain;
    public float integralGain;
    public float derivativeGain;

    public float outputMin = -1;
    public float outputMax = 1;
    public float integralSaturation;
    public DerivativeMeasurement derivativeMeasurement;

    public float valueLast;
    public float errorLast;
    public float integrationStored;
    public float velocity;
    public bool derivativeInitialized;

    public void Reset() {
        derivativeInitialized = false;
    }

    public float Update(float dt, float currentValue, float targetValue) {
        if (dt <= 0) throw new ArgumentOutOfRangeException(nameof(dt));

        float error = targetValue - currentValue;

        float P = proportionalGain * error;

        integrationStored = Mathf.Clamp(integrationStored + error * dt, -integralSaturation, integralSaturation);
        float I = integralGain * integrationStored;

        float errorRateOfChange = (error - errorLast) / dt;
        errorLast = error;

        float valueRateOfChange = (currentValue - valueLast) / dt;
        valueLast = currentValue;
        velocity = valueRateOfChange;

        float deriveMeasure = 0;

        if (derivativeInitialized) {
            if (derivativeMeasurement == DerivativeMeasurement.Velocity)
                deriveMeasure = -valueRateOfChange;
            else
                deriveMeasure = errorRateOfChange;
        }
        else {
            derivativeInitialized = true;
        }

        float D = derivativeGain * deriveMeasure;

        float result = P + I + D;

        return Mathf.Clamp(result, outputMin, outputMax);
    }

    private float AngleDifference(float a, float b) {
        return (a - b + 540) % 360 - 180;
    }

    public float UpdateAngle(float dt, float currentAngle, float targetAngle) {
        if (dt <= 0) throw new ArgumentOutOfRangeException(nameof(dt));
        float error = AngleDifference(targetAngle, currentAngle);

        float P = proportionalGain * error;

        integrationStored = Mathf.Clamp(integrationStored + error * dt, -integralSaturation, integralSaturation);
        float I = integralGain * integrationStored;

        float errorRateOfChange = AngleDifference(error, errorLast) / dt;
        errorLast = error;

        float valueRateOfChange = AngleDifference(currentAngle, valueLast) / dt;
        valueLast = currentAngle;
        velocity = valueRateOfChange;

        float deriveMeasure = 0;

        if (derivativeInitialized) {
            if (derivativeMeasurement == DerivativeMeasurement.Velocity)
                deriveMeasure = -valueRateOfChange;
            else
                deriveMeasure = errorRateOfChange;
        }
        else {
            derivativeInitialized = true;
        }

        float D = derivativeGain * deriveMeasure;

        float result = P + I + D;

        return Mathf.Clamp(result, outputMin, outputMax);
    }
}