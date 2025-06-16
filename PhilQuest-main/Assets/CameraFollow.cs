using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    public Transform target;

    void Start()
    {
        // Try to auto-find the target on load
        TryFindTarget();
    }

    void LateUpdate()
    {
        // If target was destroyed or not yet set, keep trying
        if (target == null)
        {
            TryFindTarget();
        }

        // Follow the target if it's assigned
        if (target != null)
        {
            Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
            transform.position = Vector3.Lerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        }
    }

    void TryFindTarget()
    {
        GameObject found = GameObject.FindWithTag("Player");

        // If testing this scene directly, create a dummy player
        if (found == null && Application.isEditor)
        {
            found = GameObject.Find("student"); // fallback by name
        }

        if (found != null)
        {
            target = found.transform;
        }
    }
}
