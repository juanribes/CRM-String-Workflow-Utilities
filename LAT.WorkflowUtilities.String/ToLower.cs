using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace LAT.WorkflowUtilities.String
{
    public sealed class ToLower : CodeActivity
    {
        [RequiredArgument]
        [Input("String To Lower")]
        public InArgument<string> StringToLower { get; set; }

        [OutputAttribute("Lowered String")]
        public OutArgument<string> LoweredString { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();

            try
            {
                string stringToLower = StringToLower.Get(executionContext);

                string loweredString = stringToLower.ToLower();

                LoweredString.Set(executionContext, loweredString);
            }
            catch (Exception ex)
            {
                tracer.Trace("Exception: {0}", ex.ToString());
            }
        }
    }
}
