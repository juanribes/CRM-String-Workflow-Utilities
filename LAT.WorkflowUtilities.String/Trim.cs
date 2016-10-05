using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace LAT.WorkflowUtilities.String
{
    public class Trim : CodeActivity
    {
        [RequiredArgument]
        [Input("String To Trim")]
        public InArgument<string> StringToTrim { get; set; }

        [OutputAttribute("Trimmed String")]
        public OutArgument<string> TrimmedString { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();

            try
            {
                string stringToTrim = StringToTrim.Get(executionContext);

                string trimmedString = stringToTrim.Trim();

                TrimmedString.Set(executionContext, trimmedString);
            }
            catch (Exception ex)
            {
                tracer.Trace("Exception: {0}", ex.ToString());
            }
        }
    }
}
