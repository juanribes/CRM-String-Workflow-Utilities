using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace LAT.WorkflowUtilities.String
{
    public sealed class ToUpper : CodeActivity
    {
        [RequiredArgument]
        [Input("String To Upper")]
        public InArgument<string> StringToUpper { get; set; }

        [OutputAttribute("Uppered String")]
        public OutArgument<string> UpperedString { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();

            try
            {
                string stringToUpper = StringToUpper.Get(executionContext);

                string upperedString = stringToUpper.ToUpper();

                UpperedString.Set(executionContext, upperedString);
            }
            catch (Exception ex)
            {
                tracer.Trace("Exception: {0}", ex.ToString());
            }
        }
    }
}
