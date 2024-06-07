using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDisplayManager : MonoBehaviour
{
    [SerializeField] private GameObject currentScene;
    [SerializeField] private GameObject nextScene;
    [SerializeField] private GameObject leftController;
    [SerializeField] private GameObject rightController;
    public int taskNumber;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(HandleCollision());
    }

    private IEnumerator HandleCollision()
    {
        yield return new WaitForSeconds(1f);
        currentScene.SetActive(false);
        nextScene.SetActive(true);

        // Check if leftController is assigned before setting it active
        if (leftController != null)
        {
            if (taskNumber == 2 || taskNumber == 3)
            {
                leftController.SetActive(true);
            }
            else if (taskNumber == 4)
            {
                leftController.SetActive(false);
            }

        }

        // Check if rightController is assigned before setting it active
        if (rightController != null)
        {
            if (taskNumber == 2 || taskNumber == 3)
            {
                rightController.SetActive(true);
            }
            else if (taskNumber == 4)
            {
                rightController.SetActive(false);
            }

        }
    }
}