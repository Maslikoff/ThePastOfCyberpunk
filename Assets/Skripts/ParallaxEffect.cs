using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform followTarget;

    private Vector2 startingPos;
    private float startZ;
    private Vector2 camMoveSinceStart => (Vector2) cam.transform.position - startingPos;
    private float zDistanceFromTarget => transform.position.z - followTarget.position.z;
    private float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    private float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    void Start()
    {
        startingPos = transform.position;
        startZ = transform.position.z;
    }

    void Update()
    {
        Vector2 newPosition = startingPos + camMoveSinceStart * parallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, startZ);
    }
}
