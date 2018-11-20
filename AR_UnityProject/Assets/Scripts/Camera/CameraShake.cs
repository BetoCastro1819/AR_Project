using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraShake : MonoBehaviour
{
    #region Singleton
    private static CameraShake instance;
    public static CameraShake GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public ShakeTransformEventData shakeData;

    private ShakeTransform shakeTranform;

    private void Start()
    {
        shakeTranform = GetComponent<ShakeTransform>();
    }

    public void Shake()
    {
        shakeTranform.AddShakeEvent(shakeData);
    }
}
