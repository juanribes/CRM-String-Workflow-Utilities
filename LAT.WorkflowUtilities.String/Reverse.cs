using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace LAT.WorkflowUtilities.String
{
    public sealed class Reverse : CodeActivity
    {
        [RequiredArgument]
        [Input("String To Reverse")]
        public InArgument<string> StringToReverse { get; set; }

        [OutputAttribute("Reversed String")]
        public OutArgument<string> ReversedString { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();

            try
            {
                string stringToReverse = StringToReverse.Get(executionContext);

                char[] letters = stringToReverse.ToCharArray();
                Array.Reverse(letters);
                string reversedString = new string(letters);

                ReversedString.Set(executionContext, reversedString);
            }
            catch (Exception ex)
            {
                tracer.Trace("Exception: {0}", ex.ToString());
            }
        }
    }
}
