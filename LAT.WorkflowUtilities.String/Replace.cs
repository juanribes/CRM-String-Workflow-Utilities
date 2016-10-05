using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace LAT.WorkflowUtilities.String
{
    public sealed class Replace : CodeActivity
    {
        [RequiredArgument]
        [Input("String To Search")]
        public InArgument<string> StringToSearch { get; set; }

        [RequiredArgument]
        [Input("Value To Replace")]
        public InArgument<string> ValueToReplace { get; set; }

        [Input("Replacement Value")]
        public InArgument<string> ReplacementValue { get; set; }

        [OutputAttribute("Replaced String")]
        public OutArgument<string> ReplacedString { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();

            try
            {
                string stringToSearch = StringToSearch.Get(executionContext);
                string valueToReplace = ValueToReplace.Get(executionContext);
                string replacementValue = ReplacementValue.Get(executionContext);

                if (string.IsNullOrEmpty(replacementValue))
                    replacementValue = "";

                string replacedString = stringToSearch.Replace(valueToReplace, replacementValue);

                ReplacedString.Set(executionContext, replacedString);
            }
            catch (Exception ex)
            {
                tracer.Trace("Exception: {0}", ex.ToString());
            }
        }
    }
}