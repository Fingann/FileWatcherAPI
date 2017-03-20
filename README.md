# FileWatcherAPI
For keeping track of appended lines in a logfile for InteleCom Connect in realtime.
Creates events for Caller states.


## Setup
```cs
   FileWatcherAPI Watcher = new FileWatcherAPI("c:\Logs\Log.txt");
   
            Watcher.Events.Setup += Watcher_Setup;
            Watcher.Events.Alerting += Watcher_Alerting;
            Watcher.Events.Connected += Watcher_Connected;
            Watcher.Events.Hangup += Watcher_Hangup;
            Watcher.Events.NoAnswer += Watcher_NoAnswer;
```

Start the watcher with  
```cs 
Watcher.Start();
``` 
and stop it with  
```cs 
Watcher.Stop();
```

## Callout

To check for a outgoing call check the Call class property **System_queue_key**
IF it is equal to **"__CALLOUT_"** its an outgoing call. 
