using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapZoneCollisionDetector : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject currentShape;
    [SerializeField] private GameObject nextShape;
    [SerializeField] private GameObject leftController;
    [SerializeField] private GameObject rightController;
    [SerializeField] private bool endTask;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(HandleCollision());
    }

    private IEnumerator HandleCollision()
    {
        yield return new WaitForSeconds(2f);  // 500 milliseconds delay
        currentShape.SetActive(false);
        // yield return new WaitForSeconds(0.5f);  // 2 seconds delay
        // if (nextShape != null)
        // {
        nextShape.SetActive(true);
        // }s
        if (endTask == true)
        {
            leftController.SetActive(false);
            rightController.SetActive(false);
        }
    }

    public string GetShapeName()
    {
        return currentShape.name.ToString();
    }

    public Vector3 GetBallLocation()
    {
        return ball.transform.position;
    }
}
