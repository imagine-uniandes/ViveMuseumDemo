using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR;

public class XRDeviceInput : MonoBehaviour
{
    private List<InputDevice> leftHandDevices;
    private List<InputDevice> rightHandDevices;

    private Dictionary<XRInputButton, bool> leftButtonStatus;
    private Dictionary<XRInputButton, bool> rightButtonStatus;

    public void Start()
    {
        leftHandDevices = new List<InputDevice>();
        rightHandDevices = new List<InputDevice>();

        leftButtonStatus = new Dictionary<XRInputButton, bool>();
        rightButtonStatus = new Dictionary<XRInputButton, bool>();

        foreach (int i in Enum.GetValues(typeof(XRInputButton)))
        {
            leftButtonStatus.Add((XRInputButton)i, false);
            rightButtonStatus.Add((XRInputButton)i, false);
        }
    }

    public void LateUpdate()
    {
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandDevices);
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandDevices);

        foreach (int i in Enum.GetValues(typeof(XRInputButton)))
        {
            if (leftHandDevices.Count > 0)
                UpdateButtonStatus((XRInputButton)i, leftHandDevices[0], true);
            if (rightHandDevices.Count > 0)
                UpdateButtonStatus((XRInputButton)i, rightHandDevices[0], false);
        }

    }

    public bool GetDown(XRInputButton button)
    {
        InputFeatureUsage<bool> feature = GetInputFeatureUsage(button);
        bool lvalue = false;
        bool rvalue = false;

        return (leftHandDevices.Count > 0 && !leftButtonStatus[button] && leftHandDevices[0].TryGetFeatureValue(feature, out lvalue) && lvalue) ||
               (rightHandDevices.Count > 0 && !rightButtonStatus[button] && rightHandDevices[0].TryGetFeatureValue(feature, out rvalue) && rvalue);
    }

    public bool GetUp(XRInputButton button)
    {
        InputFeatureUsage<bool> feature = GetInputFeatureUsage(button);
        bool lvalue = false;
        bool rvalue = false;
        return (leftHandDevices.Count > 0 && leftButtonStatus[button] && leftHandDevices[0].TryGetFeatureValue(feature, out lvalue) && !lvalue) ||
               (rightHandDevices.Count > 0 && rightButtonStatus[button] && rightHandDevices[0].TryGetFeatureValue(feature, out rvalue) && !rvalue);
    }


    private void UpdateButtonStatus(XRInputButton button, InputDevice device, bool isLeft)
    {
        bool value = false;

        InputFeatureUsage<bool> feature = GetInputFeatureUsage(button);

        if (device.TryGetFeatureValue(feature, out value))
        {
            if (isLeft)
                leftButtonStatus[button] = value;
            else
                rightButtonStatus[button] = value;
        }
    }

    private InputFeatureUsage<bool> GetInputFeatureUsage(XRInputButton button)
    {
        switch (button)
        {
            case XRInputButton.Trigger:
                return CommonUsages.triggerButton;
            case XRInputButton.Primary:
                return CommonUsages.primaryButton;
            case XRInputButton.Secondary:
                return CommonUsages.secondaryButton;
            case XRInputButton.Grip:
                return CommonUsages.gripButton;
        }

        return new InputFeatureUsage<bool>();
    }


    public Vector2 GetAnyAxis()
    {
        Vector2 lvalue = Vector2.zero;
        Vector2 rvalue = Vector2.zero;

        if (leftHandDevices.Count > 0)
            leftHandDevices[0].TryGetFeatureValue(CommonUsages.primary2DAxis, out lvalue);

        if (rightHandDevices.Count > 0)
            rightHandDevices[0].TryGetFeatureValue(CommonUsages.primary2DAxis, out rvalue);

        return lvalue + rvalue;
    }

    public XRDeviceType GetActiveDevice(XRInputButton button)
    {
        InputFeatureUsage<bool> feature = GetInputFeatureUsage(button);
        bool lvalue = false;
        bool rvalue = false;
        if (leftHandDevices.Count > 0 && leftHandDevices[0].TryGetFeatureValue(feature, out lvalue) && lvalue != leftButtonStatus[button])
            return XRDeviceType.LeftController;
        else if (rightHandDevices.Count > 0 && rightHandDevices[0].TryGetFeatureValue(feature, out rvalue) && rvalue != rightButtonStatus[button])
            return XRDeviceType.RightController;
        else
            return XRDeviceType.None;
    }
}

public enum XRInputButton
{
    Primary,
    Secondary,
    Trigger,
    Grip
}

public enum XRDeviceType
{
    None,
    LeftController,
    RightController
}

