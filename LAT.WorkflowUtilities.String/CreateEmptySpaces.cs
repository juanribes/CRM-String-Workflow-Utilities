using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace LAT.WorkflowUtilities.String
{
    public class CreateEmptySpaces : CodeActivity
    {
        [RequiredArgument]
        [Input("Number Of Spaces")]
        public InArgument<int> NumberOfSpaces { get; set; }

        [OutputAttribute("Empty String")]
        public OutArgument<string> EmptyString { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();

            try
            {
                int numberOfSpaces = NumberOfSpaces.Get(executionContext);

                string emptyString = new string(' ', numberOfSpaces);

                EmptyString.Set(executionContext, emptyString);
            }
            catch (Exception ex)
            {
                tracer.Trace("Exception: {0}", ex.ToString());
            }
        }
    }
}
