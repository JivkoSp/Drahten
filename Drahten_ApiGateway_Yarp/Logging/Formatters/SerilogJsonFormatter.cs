using Serilog.Sinks.Http;
using Newtonsoft.Json.Linq;

namespace Drahten_ApiGateway_Yarp.Logging.Formatters
{
    public sealed class SerilogJsonFormatter : IBatchFormatter
    {
        public void Format(IEnumerable<string> logEvents, TextWriter output)
        {
            foreach (var logEvent in logEvents)
            {
                // Parse each log event string into a JSON object.
                var logEventObject = JObject.Parse(logEvent);

                // Serialize the JSON object back to JSON with indentation for readability.
                var serializedLogEvent = logEventObject.ToString(Newtonsoft.Json.Formatting.None);

                // Write the serialized log event to the output.
                output.WriteLine(serializedLogEvent);
            }
        }
    }
}
