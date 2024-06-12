using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logger : MonoBehaviour
{
    // variables for 01_FittsPoke and 02_FittsRay
    private float buttonScale;
    private float buttonDistance;
    private int targetButton;
    private int clickedButton;
    private Vector3 centerLocation;
    private Vector3 fingerLocation;

    // variables for 03_TracingTask
    public GetBallLocation getBallLocation;
    private string shapeName;
    private Vector3 ballLocation;

    // variables for 04_TypingTask
    public static string targetSentence { get; set; }
    public static string enteredSentence { get; set; }

    // global variables
    private bool startRecord;
    private string currentEntry;
    private List<string> allEntries;
    private string logPath;
    private string iterationNumStr;
    private string sceneNumStr;
    private string sceneName;
    private bool logButtonActive;

    // Start is called before the first frame update
    void Start()
    {
        startRecord = false;

        allEntries = new List<string>();

        iterationNumStr = RandomSceneManager.currentIndex.ToString();
        sceneNumStr = SceneManager.GetActiveScene().buildIndex.ToString();
        sceneName = SceneManager.GetActiveScene().name;

        string fname = iterationNumStr + "_" + sceneNumStr + "_" + sceneName + "_" + System.DateTime.Now.ToString("dd-MMM HH-mm-ss") + ".csv";
        logPath = Path.Combine(Application.persistentDataPath, fname);

        if (sceneName == "00_Tutorial")
        {
            allEntries.Add("sceneName,buttonScale,buttonDistance,targetButton,clickedButton,centerLocationX,centerLocationY,centerLocationZ,fingerLocationX,fingerLocationY,fingerLocationZ,shapeName,ballLocationX,ballLocationY,ballLocationZ,targetSentence,enteredSentence,currentTime");
        }
        else if (sceneName == "01_FittsPoke" || sceneName == "02_FittsRay")
        {
            allEntries.Add("sceneName,sceneNum,iterationNum,buttonScale,buttonDistance,targetButton,clickedButton,centerLocationX,centerLocationY,centerLocationZ,fingerLocationX,fingerLocationY,fingerLocationZ,currentTime");
        }
        else if (sceneName == "03_TracingTask")
        {
            allEntries.Add("sceneName,sceneNum,iterationNum,shapeName,currentTime,ballLocationX,ballLocationY,ballLocationZ");
        }
        else if (sceneName == "04_TypingTask_New")
        {
            allEntries.Add("sceneName,sceneNum,iterationNum,targetSentence,enteredSentence,currentTime");
        }
    }

    void FixedUpdate()
    {
        if (startRecord)
        {
            if (sceneName == "00_Tutorial")
            {
                LogTutorial();
            }
            else if (sceneName == "01_FittsPoke")
            {
                if (logButtonActive)
                {
                    LogFittsPoke();
                    LogButtonDeactive();
                }
            }
            else if (sceneName == "02_FittsRay")
            {
                if (logButtonActive)
                {
                    LogFittsRay();
                    LogButtonDeactive();
                }
            }
            else if (sceneName == "03_TracingTask")
            {
                LogTracingTask();
            }
            else if (sceneName == "04_TypingTask_New")
            {
                if (logButtonActive)
                {
                    LogTypingTask();
                    LogButtonDeactive();
                }
            }
        }
    }

    void WriteToCSV()
    {
        File.AppendAllLines(logPath, allEntries);
        allEntries.Clear();
        Debug.Log("WriteToCSV");
    }

    string GetTimeStamp()
    {
        DateTime currentTime = DateTime.Now;
        return currentTime.ToString("HH:mm:ss.fff");
    }

    string Vector3ToString(Vector3 position)
    {
        return position.x.ToString() + "," +
            position.y.ToString() + "," +
            position.z.ToString();
    }

    public void StartRecordLog()
    {
        startRecord = true;
        Debug.Log("StartRecord");
    }

    public void StopRecordLog()
    {
        startRecord = false;
        Debug.Log("StopRecord");
        WriteToCSV();
    }

    public void LogButtonActive()
    {
        logButtonActive = true;
    }

    void LogButtonDeactive()
    {
        logButtonActive = false;
    }

    void LogFittsPoke()
    {
        buttonScale = PointTask.scales[PointTask.randomList[PointTask.currentIteration]];
        buttonDistance = PointTask.distances[PointTask.randomList[PointTask.currentIteration]];
        targetButton = PointTask.currentIndex;
        clickedButton = PointTask.buttonNumber;
        centerLocation = PointTask.centerLocation;
        fingerLocation = PointTask.fingerLocation;

        currentEntry = new string(
            sceneName + "," +
            sceneNumStr + "," +
            iterationNumStr + "," +
            buttonScale.ToString() + "," +
            buttonDistance.ToString() + "," +
            targetButton.ToString() + "," +
            clickedButton.ToString() + "," +
            Vector3ToString(centerLocation) + "," +
            Vector3ToString(fingerLocation) + "," +
            GetTimeStamp());

        allEntries.Add(currentEntry);

        currentEntry = new string("");
    }

    void LogFittsRay()
    {
        buttonScale = RayTask.scales[RayTask.randomList[RayTask.currentIteration]];
        buttonDistance = RayTask.distances[RayTask.randomList[RayTask.currentIteration]];
        targetButton = RayTask.currentIndex;
        clickedButton = RayTask.buttonNumber;
        centerLocation = RayTask.centerLocation;
        fingerLocation = RayTask.fingerLocation;

        currentEntry = new string(
            sceneName + "," +
            sceneNumStr + "," +
            iterationNumStr + "," +
            buttonScale.ToString() + "," +
            buttonDistance.ToString() + "," +
            targetButton.ToString() + "," +
            clickedButton.ToString() + "," +
            Vector3ToString(centerLocation) + "," +
            Vector3ToString(fingerLocation) + "," +
            GetTimeStamp());

        allEntries.Add(currentEntry);

        currentEntry = new string("");
    }

    void LogTracingTask()
    {
        shapeName = getBallLocation.GetShapeName();
        ballLocation = getBallLocation.GetBallPosition();

        currentEntry = new string(
            sceneName + "," +
            sceneNumStr + "," +
            iterationNumStr + "," +
            shapeName + "," +
            GetTimeStamp() + "," +
            Vector3ToString(ballLocation));

        allEntries.Add(currentEntry);

        currentEntry = new string("");
    }

    void LogTypingTask()
    {
        currentEntry = new string(
            sceneName + "," +
            sceneNumStr + "," +
            iterationNumStr + "," +
            targetSentence + "," +
            enteredSentence + "," +
            GetTimeStamp());

        allEntries.Add(currentEntry);

        currentEntry = new string("");
    }

    void LogTutorial()
    {

    }
}