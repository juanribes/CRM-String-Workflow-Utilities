using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Text.RegularExpressions;

namespace LAT.WorkflowUtilities.String
{
    public sealed class RegexMatch : CodeActivity
    {
        [RequiredArgument]
        [Input("String To Search")]
        public InArgument<string> StringToSearch { get; set; }

        [RequiredArgument]
        [Input("Pattern")]
        public InArgument<string> Pattern { get; set; }

        [OutputAttribute("Contains Pattern")]
        public OutArgument<bool> ContainsPattern { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();

            try
            {
                string stringToSearch = StringToSearch.Get(executionContext);
                string pattern = Pattern.Get(executionContext);

                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                Match match = regex.Match(stringToSearch);
                bool containsPattern = match.Success;

                ContainsPattern.Set(executionContext, containsPattern);
            }
            catch (Exception ex)
            {
                tracer.Trace("Exception: {0}", ex.ToString());
            }
        }
    }
}