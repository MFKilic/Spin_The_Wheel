using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameConsole : MonoBehaviour
{
    public TextMeshProUGUI consoleText;
    private string logMessages = "";

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        logMessages += logString + "\n";
        consoleText.text = logMessages;
    }
}
