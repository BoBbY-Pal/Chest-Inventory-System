using UnityEngine;
// Script that will allow to disable logs in whole project just by changing show logs bool to false. 
public static class GameLogsManager
{
    private static bool showLogs = false;

    public static void CustomLog(object message)
    {
        if (!showLogs )
        {
            return;
        }

        Debug.Log(message);
    }
}