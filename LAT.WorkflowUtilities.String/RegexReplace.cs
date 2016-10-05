using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Text.RegularExpressions;

namespace LAT.WorkflowUtilities.String
{
    public sealed class RegexReplace : CodeActivity
    {
        [RequiredArgument]
        [Input("String To Search")]
        public InArgument<string> StringToSearch { get; set; }

        [Input("Replacement Value")]
        public InArgument<string> ReplacementValue { get; set; }

        [RequiredArgument]
        [Input("Pattern")]
        public InArgument<string> Pattern { get; set; }

        [OutputAttribute("Replaced String")]
        public OutArgument<string> ReplacedString { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();

            try
            {
                string stringToSearch = StringToSearch.Get(executionContext);
                string replacementValue = ReplacementValue.Get(executionContext);
                string pattern = Pattern.Get(executionContext);

                if (string.IsNullOrEmpty(replacementValue))
                    replacementValue = "";

                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

                string replacedString = regex.Replace(stringToSearch, replacementValue);

                ReplacedString.Set(executionContext, replacedString);
            }
            catch (Exception ex)
            {
                tracer.Trace("Exception: {0}", ex.ToString());
            }
        }
    }
}
