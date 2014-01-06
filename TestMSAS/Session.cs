using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using ehMSASLib;
using Microsoft.Win32;

namespace TestMSAS
{
    class Session : MediaStatusSession
    {
        void IMediaStatusSession.MediaStatusChange(Array tags,
                                                   Array properties)
        {
            Sink.TraceInformation("Session.MediaStatusChange called");

            for (int i = 0; i < tags.Length; i++)
            {
                MEDIASTATUSPROPERTYTAG tag =
                  (MEDIASTATUSPROPERTYTAG)tags.GetValue(i);
                object value = properties.GetValue(i);
                Sink.TraceInformation("Tag {0}={1}",
                                       tag,
                                       value);
                string tagStr = tag.ToString();
                if (tagStr.Equals("MSPROPTAG_MediaName"))
                {
                    Sink.TraceInformation("Witing to key");
                    WriteToRegistry("Name", value.ToString());
                   
                }
                else if(tagStr.Equals("MSPROPTAG_TrackNumber"))
                {
                    WriteToRegistry("Channel", value.ToString());
                }
            }
        }

        public void WriteToRegistry(String keyStr, String value)
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey("Software", true))
            {

                key.CreateSubKey("TestMSAS");
                using (RegistryKey key2 = key.OpenSubKey("TestMSAS", true))
                {


                    key2.CreateSubKey("ProgramInfo");
                    using(RegistryKey key3 = key2.OpenSubKey("ProgramInfo", true))
                    {

                        key3.SetValue(keyStr, value);
                        Sink.TraceInformation("Wrote to Key{0} Value{1}", keyStr, value);
                    }
                }
            }
        }

        public void Close()
        {
            Sink.TraceInformation(("Session.Close called"));
        }
    }
}