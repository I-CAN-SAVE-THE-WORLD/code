using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flood : MonoBehaviour
{
    public GameObject objectToMove; // Object to be moved
    public List<Transform> targetPositions; // List of target positions (game objects)

    public int currentIndex = 0; // Current index of the target position
    public bool shouldMove = false; // Flag to indicate if the object should move

    public int maxPositions;
    public int stopAt;

    private void Start()
    {
        if (objectToMove == null || targetPositions.Count == 0)
        {
            Debug.LogError("Object to move or target positions are not assigned!");
        }

        maxPositions = targetPositions.Count;
        stopAt = currentIndex;
    }

    private void Update()
    {
        if (shouldMove)
        {

            // Move the object towards the current target position
            MoveObject(objectToMove, targetPositions[currentIndex].position);
        }
    }

    private void MoveObject(GameObject obj, Vector3 targetPosition)
    {
        // Move the object towards the target position
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, targetPosition, Time.deltaTime * 5f);

        // Check if the object has reached the target position
        if (obj.transform.position == targetPosition)
        {
            shouldMove = false;
        }
    }

    public void StartMoving()
    {
        shouldMove = true;
    }

    public void ChangeWaterPosition()
    {
        currentIndex++;
        shouldMove = true;
        stopAt = currentIndex + 1;
    }
}