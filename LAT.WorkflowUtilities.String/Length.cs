using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace LAT.WorkflowUtilities.String
{
    public class Length : CodeActivity
    {
        [RequiredArgument]
        [Input("String To Measure")]
        public InArgument<string> StringToMeasure { get; set; }

        [OutputAttribute("String Length")]
        public OutArgument<int> StringLength { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();

            try
            {
                string stringToMeasure = StringToMeasure.Get(executionContext);

                int stringLength = stringToMeasure.Length;

                StringLength.Set(executionContext, stringLength);
            }
            catch (Exception ex)
            {
                tracer.Trace("Exception: {0}", ex.ToString());
            }
        }
    }
}
