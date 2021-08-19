using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Oscillator : MonoBehaviour
{
    private Vector3 startingPosition;

    [SerializeField]private Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;

    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cylces = Time.time / period; // zamanla artıyor
        
        const float tau = Mathf.PI * 2; //sabit 6.283
        float rawSinWave = Mathf.Sin(cylces * tau); // -1 ve 1 arası gider gelir

        movementFactor = (rawSinWave + 1f) / 2f; // 0 ve 1 arası gider gelir
        
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
