using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WpfTest.UI
{
    internal class ClientAppInsights
    {
        internal static TelemetryClient TelemetryClient;

        public static void Shutdown()
        {
            TelemetryClient.Flush();

            // Allow some time for flushing before shutdown.
            Thread.Sleep(1000);
        }

        static ClientAppInsights()
        {
            TelemetryClient = new TelemetryClient();
        }
    }
}
