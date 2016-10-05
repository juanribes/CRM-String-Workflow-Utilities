using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace LAT.WorkflowUtilities.String
{
    public sealed class Substring : CodeActivity
    {
        [RequiredArgument]
        [Input("String To Parse")]
        public InArgument<string> StringToParse { get; set; }

        [RequiredArgument]
        [Input("Start Position")]
        public InArgument<int> StartPosition { get; set; }

        [Input("Length")]
        public InArgument<int> Length { get; set; }

        [OutputAttribute("Partial String")]
        public OutArgument<string> PartialString { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();

            try
            {
                string stringToParse = StringToParse.Get(executionContext);
                int startPosition = StartPosition.Get(executionContext);
                int length = Length.Get(executionContext);

                if (startPosition < 0)
                    startPosition = 0;

                if (startPosition > stringToParse.Length)
                {
                    tracer.Trace("Specified start position [" + startPosition + "] is after end is string [" + stringToParse + "]");
                    PartialString.Set(executionContext, null);
                }

                if (length > stringToParse.Length)
                    length = stringToParse.Length - startPosition;

                string partialString = (length == 0)
                    ? stringToParse.Substring(startPosition)
                    : stringToParse.Substring(startPosition, length);

                PartialString.Set(executionContext, partialString);
            }
            catch (Exception ex)
            {
                tracer.Trace("Exception: {0}", ex.ToString());
            }
        }
    }
}
