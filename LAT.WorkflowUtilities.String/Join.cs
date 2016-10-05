using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace LAT.WorkflowUtilities.String
{
    public sealed class Join : CodeActivity
    {
        [RequiredArgument]
        [Input("String 1")]
        public InArgument<string> String1 { get; set; }

        [RequiredArgument]
        [Input("String 2")]
        public InArgument<string> String2 { get; set; }

        [Input("Joiner")]
        public InArgument<string> Joiner { get; set; }

        [OutputAttribute("Joined String")]
        public OutArgument<string> JoinedString { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();

            try
            {
                string string1 = String1.Get(executionContext);
                string string2 = String2.Get(executionContext);
                string joiner = Joiner.Get(executionContext);

                string joinedString = System.String.Join(joiner, string1, string2);

                JoinedString.Set(executionContext, joinedString);
            }
            catch (Exception ex)
            {
                tracer.Trace("Exception: {0}", ex.ToString());
            }
        }
    }
}
