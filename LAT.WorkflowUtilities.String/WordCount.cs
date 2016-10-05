using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace LAT.WorkflowUtilities.String
{
    public sealed class WordCount : CodeActivity
    {
        [RequiredArgument]
        [Input("String To Count")]
        public InArgument<string> StringToCount { get; set; }

        [OutputAttribute("Word Count")]
        public OutArgument<int> Count { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();

            try
            {
                string stringToCount = StringToCount.Get(executionContext);

                string[] words = stringToCount.Trim().Split(' ');

                Count.Set(executionContext, words.Length);
            }
            catch (Exception ex)
            {
                tracer.Trace("Exception: {0}", ex.ToString());
            }
        }
    }
}