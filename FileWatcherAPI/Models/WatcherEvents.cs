using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcherAPI.Models
{
    using FileWatcherAPICLI;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class WatcherEvents
    {
        /////////////////////////////////////////////////////////
        // Event Handlers 
        // System_queue_key == "__CALLOUT_" da er det Outgoing call. 
        /////////////////////////////////////////////////////////
        public event SetupEventHandler Setup;

        public event AlertingEventHandler Alerting;

        public event ConnectedEventHandler Connected;

        public event HangupEventHandler Hangup;

        public event  NoAnswerEventHandler NoAnswer;


        /// <summary>
        ///     Function to 
        /// eck if a string is a JSON Object
        /// </summary>
        private static readonly Func<string, bool> IsJsonObject = x =>
        {
            try
            {
                JToken.Parse(x);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
        };

        /// <summary>
        ///     The process lines.
        /// </summary>
        /// <param name="lastLinesInLog">
        ///     The last lines in log.
        /// </param>
        public void CheckLine(string lastLinesInLog)
        {
            if (string.IsNullOrEmpty(lastLinesInLog))
            {
                return;
            }

            if (!IsJsonObject(lastLinesInLog))
            {
                return;
            }

            this.GetCaller(lastLinesInLog);
        }

        private void GetCaller(string lastLinesInLog)
        {
            Call currentCaller;

            try
            {
                currentCaller = JsonConvert.DeserializeObject<Call>(lastLinesInLog);
            }
            catch (JsonReaderException ex)
            {
                // this.Logger.Warn("CheckLine could not Convert logline to Json: " + lastLinesInLog, ex);
                return;
            }
            catch (ArgumentNullException ex)
            {
                // this.Logger.Warn("CheckLine could not convert logline to Json becasue its null ", ex);
                return;
            }

            // sorting the caller 
            this.SortCaller(currentCaller);
        }

        /// <summary>
        ///     The sort caller.
        /// </summary>
        /// <param name="currentCaller">
        ///     The current caller.
        /// </param>
        private void SortCaller(Call currentCaller)
        {
            
            switch (currentCaller.system_call_progress)
            {
                case "SETUP":
                    this.Setup?.Invoke(this, currentCaller);

                    // this.Logger.Info("CALL_Progress: SETUP -> Sennding new caller via Notification:" + currentCaller);
                    break;

                case "ALERTING":

                    this.Alerting?.Invoke(this, currentCaller);

                    // this.Logger.Trace("CALL_Progress: ALERTING -> " + currentCaller);
                    break;
                case "CONNECTED":

                    this.Connected?.Invoke(this, currentCaller);

                    // this.Logger.Trace("CALL_Progress: CONNECTED -> " + currentCaller);
                    break;
                case "HANGUP":

                    this.Hangup?.Invoke(this, currentCaller);

                    // this.Logger.Trace("CALL_Progress: HANGUP -> " + currentCaller);
                    break;
                case "NOANSWER":

                    this.NoAnswer?.Invoke(this, currentCaller);

                    // this.Logger.Trace("CALL_Progress: NOANSWER -> " + currentCaller);
                    break;
                //case "__CALLOUT_":
                //    break;
                // case "AgentPauseOff":
                // Console.WriteLine(5);
                // break;
                // case "AgentLogoff":
                // Console.WriteLine(5);
                // break;
                default:

                    // this.Logger.Trace("DEFAULT -> Could not find a CALL_Progress" + currentCaller);
                    break;
            }
        }
    }
}
