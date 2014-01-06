using ehMSASLib;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TestMSAS
{
    [ComVisible(true)]
    [GuidAttribute("360d8298-97d9-4058-9052-b91efe76d3ae")]
    public class Sink : MediaStatusSink
    {
        public static bool SHOULD_TRACE = false;
        public Sink()
        {
            string logFile = string.Format("{0}MsasSink2.log",
                System.IO.Path.GetTempPath());
            Trace.Listeners.Add(new TextWriterTraceListener(logFile));
            Trace.AutoFlush = true;
            Sink.TraceInformation("Sink started");
        }

        public static void TraceInformation(string s, params object[] args)
        {
            if (ShouldTrace())
            {
                Trace.TraceInformation(s, args);
            }
        }

        public static void TraceInformation(string s)
        {
            TraceInformation(s, null);
        }

        public static bool ShouldTrace()
        {
            return SHOULD_TRACE;
        }

        public void Initialize()
        {
            Sink.TraceInformation(("Sink.Initialize called"));
        }

        public MediaStatusSession CreateSession()
        {
            Sink.TraceInformation(("Sink.CreateSession called"));
            return new Session();
        }
    }
}
