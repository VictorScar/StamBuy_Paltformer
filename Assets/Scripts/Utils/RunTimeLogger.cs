using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunTimeLogger
{
    private static TMP_Text loggerText;

    public static void AttachUIText(TMP_Text uiText)
    {
        loggerText = uiText;
    }

    public static void Log(string text)
    {
        if (loggerText != null)
        {
            string logText = "LogText" + "\n" + text;
            loggerText.text = logText;
        }
    }
}
