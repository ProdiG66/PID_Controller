using UnityEngine;
using UnityEngine.UI;

public class PIDEditor : MonoBehaviour {
    [SerializeField]
    private Controller controllerSource;

    [SerializeField]
    private InputField proportionalInput;

    [SerializeField]
    private InputField integralInput;

    [SerializeField]
    private InputField derivativeInput;

    [SerializeField]
    private InputField integralSaturationInput;

    [SerializeField]
    private InputField powerInput;

    [SerializeField]
    private Text valueText;

    [SerializeField]
    private Text errorText;

    [SerializeField]
    private Text velocityText;

    [SerializeField]
    private Text saturationText;

    private PIDController _controller;

    private void Start() {
        _controller = controllerSource.GetController();

        UpdateInput(proportionalInput, _controller.proportionalGain);
        UpdateInput(integralInput, _controller.integralGain);
        UpdateInput(derivativeInput, _controller.derivativeGain);
        UpdateInput(integralSaturationInput, _controller.integralSaturation);
        UpdateInput(powerInput, controllerSource.power);
    }

    private void Update() {
        valueText.text = $"{_controller.valueLast:0.00}";
        errorText.text = $"{_controller.errorLast:0.00}";
        velocityText.text = $"{_controller.velocity:0.00}";
        saturationText.text = $"{_controller.integrationStored:0.00}";
    }

    private bool TryParse(string text, out float value) {
        if (!string.IsNullOrEmpty(text)) return float.TryParse(text, out value);
        value = 0;
        return true;
    }

    public void UpdateProportional(string text) {
        if (!TryParse(text, out float value)) return;
        proportionalInput.text = text;
        _controller.proportionalGain = value;
    }

    public void UpdateIntegral(string text) {
        if (!TryParse(text, out float value)) return;
        integralInput.text = text;
        _controller.integralGain = value;
    }

    public void UpdateDerivative(string text) {
        if (!TryParse(text, out float value)) return;
        derivativeInput.text = text;
        _controller.derivativeGain = value;
    }

    public void UpdateIntegralSaturation(string text) {
        if (!TryParse(text, out float value)) return;
        integralSaturationInput.text = text;
        _controller.integralSaturation = value;
    }

    public void UpdatePower(string text) {
        if (!TryParse(text, out float value)) return;
        powerInput.text = text;
        controllerSource.power = value;
    }

    private void UpdateInput(InputField input, float value) {
        if (input.isFocused) return;
        input.text = value.ToString();
    }
}