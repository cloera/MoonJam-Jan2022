using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    [SerializeField] float shakeDurationSeconds = 1f;
    [SerializeField] float shakePower = 1f;

    private float shakeDurationRemaining = 0f;
    private bool startShaking = false;
    private Vector3 defaultPosition = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (startShaking)
        {
            shakeDurationRemaining = shakeDurationSeconds;

            startShaking = false;
        }
    }

    void LateUpdate()
    {
        if (CanShake())
        {
            ShakeScreen();
        }
    }

    public void StartShaking()
    {
        startShaking = true;
    }

    public void StartShaking(float durationSeconds, float power)
    {
        startShaking = true;
        shakeDurationSeconds = durationSeconds;
        shakePower = power;
    }

    private bool CanShake()
    {
        return 0 < shakeDurationRemaining;
    }

    private void ShakeScreen()
    {
        shakeDurationRemaining -= Time.deltaTime;

        float powerFade = shakePower * (shakeDurationRemaining / shakeDurationSeconds);

        float xShake = UnityEngine.Random.Range(-1f, 1f) * shakePower;
        float yShake = UnityEngine.Random.Range(-.5f, .5f) * shakePower;

        Vector3 newPosition = transform.position + new Vector3(xShake, yShake, 0f);

        transform.position = CanShake() ? newPosition : defaultPosition;
    }
}
