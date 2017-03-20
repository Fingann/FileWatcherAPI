using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileWatcherAPITests
{
    using System.IO;

    using FileWatcherAPICLI;

    using NUnit.Framework;

    using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

    [TestFixture]
    public class FileWatcherTests
    {
        [Test]
        public void Test_CTOR()
        {

            var callJson =
                "{\"accessNo\":\"55127777\",\"accessToken\":\"wsid!28056!6ddc7b73-3f69-4d1e-96bf-5848424be29f\",\"adhocAktiv\":\"no\",\"AuthenticateCode\":\"0\",\"AuthenticatePwd\":\"1234\",\"AuthenticateToken\":\"wsid!28056!6ddc7b73-3f69-4d1e-96bf-5848424be29f\",\"AuthenticateUser\":\"BKKCatalogSearch\",\"bNumDesc\":\"\",\"call_tag\":\"\\u0026lt;call destination=\\u0026quot;55128476\\u0026quot; originating=\\u0026quot;$ciq_originating\\u0026quot; iq_id=\\u0026quot;$ciq_iqid\\u0026quot; iq_type=\\u0026quot;request\\u0026quot; /\\u0026gt;\",\"callerDescription\":\"Stein-Roger Bakke Svendsen\",\"CatalogPhoneSearchDone\":\"yes\",\"catalogPhoneSearchResult\":\"MatchFound\",\"catalogToSearch\":\"BKK\",\"ciq_active\":\"no\",\"ciq_destination\":\"55128476\",\"consult.originating\":\"55127565\",\"consult.ringing_timeout\":\"120s\",\"ContactFirstName_1\":\"Stein-Roger Bakke\",\"ContactLastName_1\":\"Svendsen\",\"crmdb_contact_name\":\"\",\"crmdb_lastcall_agent_names\":\",Sondre Fingann\",\"crmdb_lastcall_log_id\":\"114866417\",\"crmdb_lastcall_start\":\"1/23/2017 10:52:16 AM\",\"crmdb_more_calls\":\"true\",\"crmKml\":\"\",\"current_state\":\"Caller Joined 2 Callee\",\"dtmf.tone_detection_mode\":\"5\",\"eh_conferance_activated\":\"true\",\"eh_continue_on_caller_hangup\":\"false\",\"eh_wav_caller_hangup\":\"/SYSTEM/wav/NO/CCCT/female/caller_hangup.wav\",\"eh_wav_ciq_call_caller\":\"/28056/connect/soundfiles/ControlAudio/Deaktivert.wav\",\"eh_wav_ciq_unavailable\":\"/SYSTEM/wav/NO/CIQ/female/ciq_unavailable.wav\",\"eh_wav_hold\":\"/SYSTEM/wav/NO/CCCT/female/hold.wav\",\"eh_wav_screened\":\"/SYSTEM/wav/NO/CCCT/female/transferee_screened.wav\",\"eh_wav_unavailable\":\"/SYSTEM/wav/NO/CCCT/female/transferee_unavailable.wav\",\"eventhandler.override\":\"/Common/eventhandler/28056/bkk_eventhandler.xml\",\"GroupId\":\"20399\",\"language\":\"no\",\"lindorf_serviceid\":\"30504\",\"LogCallExit\":\"yes\",\"min\":\"0\",\"NumOfContacts\":\"1\",\"overflow_from_nett\":\"no\",\"popup_name\":\"55128476 - Stein-Roger Bakke Svendsen\",\"propertyValue\":\"Deaktivert\",\"qSize\":\"normal\",\"queue.average_speak_time\":\"2m\",\"recordingActive\":\"no\",\"result\":\"OK\",\"service\":\"bkk_it\",\"serviceAdHoc\":\"BKK IT\",\"setBackgroundColor\":\"no\",\"system_absolute_uri\":\"http://cs.prod.consorte.com/28056/connect/main_solution_BKK_AS.xml\",\"system_accessno\":\"21499021\",\"system_base_uri\":\"http://cs.prod.consorte.com\",\"system_call_progress\":\"HANGUP\",\"system_CalledPartyNumber\":\"21499021\",\"system_caller_ano\":\"55128476\",\"system_caller_progress\":\"CS_IDLE\",\"system_CallingPartyNumber\":\"55128476\",\"system_CallingPartyNumberRestriction\":\"False\",\"system_country\":\"NO\",\"system_custkey\":\"28056\",\"system_DefaultCallingPartyNumber\":\"21499000\",\"system_deny_recording\":\"no\",\"system_encoding\":\"ISO-8859-1\",\"system_guid\":\"883d4f27-5bfe-4b26-b456-ae348f733954\",\"system_hostip\":\"10.47.29.48\",\"system_hostname\":\"P1NOTM08\",\"system_iq_cc_uri\":\"http://ascluster.prod.consorte.com/iqservices2/contactcenter.asmx\",\"system_iqid\":\"744511973\",\"system_is_out_of_the_blue\":\"false\",\"system_kml\":\"main_solution_BKK_AS\",\"system_last_called\":\"55127565\",\"system_last_event_args\":\"\",\"system_last_user_id\":\"188667\",\"system_last_user_num\":\"9398\",\"system_name\":\"Konge10\",\"system_numbers\":\"file://d:\\\\wav\\\\num\",\"system_queue_agents\":\"3\",\"system_queue_available_agents\":\"2\",\"system_queue_estimate\":\"40000\",\"system_queue_id\":\"q_generic\",\"system_queue_key\":\"q_bkk_it\",\"system_queue_place\":\"1\",\"system_queue_time\":\"11040\",\"system_queue_wait_by_last_calls\":\"293\",\"system_queue_wait_by_last_minutes\":\"0\",\"system_RedirectingNumber\":\"55127777\",\"system_RemoveSecretAnoIfAdditional\":\"true\",\"system_secret_caller\":\"no\",\"system_secret_number\":\"55128476\",\"system_session_id\":\"883d4f27-5bfe-4b26-b456-ae348f733954\",\"system_speaktime\":\"45661\",\"system_version\":\"10.2.9.0\",\"textColor\":\"black\",\"timeBeforeOverflow\":\"60s\",\"transfer.ringing_timeout\":\"120s\",\"userGetNumOfResult\":\"1\",\"xhcKmlData\":\"\",\"xml_in_data\":\"\\u003cvariables\\u003e\\u003ccustkey\\u003e28056\\u003c/custkey\\u003e\\u003cuserId\\u003e188667\\u003c/userId\\u003e\\u003c/variables\\u003e\"}\r\n";

            var path = @"C:\Users\" + Environment.UserName
                       + @"\AppData\Roaming\Intelecom Group AS\Intelecom Connect\Logs\all.txt";
            FileWatcherAPI fileWatcher  = new FileWatcherAPI(path);

            Assert.AreEqual(fileWatcher.FilePath, path);
            Assert.IsFalse(fileWatcher.FileWatcher.EnableRaisingEvents);
            Assert.AreEqual(fileWatcher.FileWatcher.Filter, "all.txt");
            fileWatcher.Events.Hangup +=
                (source, call) => Assert.AreEqual(call.ToString(), "HANGUP : 55128476 - Stein-Roger Bakke Svendsen");
            fileWatcher.Start();
            Assert.IsTrue(fileWatcher.FileWatcher.EnableRaisingEvents);
            var filters = fileWatcher.FileWatcher.NotifyFilter;
            fileWatcher.Events.CheckLine(callJson);
            fileWatcher.Stop();
            Assert.IsFalse(fileWatcher.FileWatcher.EnableRaisingEvents);

        }
    }
}
