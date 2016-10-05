using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Text.RegularExpressions;

namespace LAT.WorkflowUtilities.String
{
    public class RegexExtract : CodeActivity
    {
        [RequiredArgument]
        [Input("String To Search")]
        public InArgument<string> StringToSearch { get; set; }

        [RequiredArgument]
        [Input("Pattern")]
        public InArgument<string> Pattern { get; set; }

        [OutputAttribute("Extracted String")]
        public OutArgument<string> ExtractedString { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();

            try
            {
                string stringToSearch = StringToSearch.Get(executionContext);
                string pattern = Pattern.Get(executionContext);

                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                Match match = regex.Match(stringToSearch);

                if (match.Success)
                {
                    string extractedString = match.Value;
                    ExtractedString.Set(executionContext, extractedString);
                    return;
                }

                ExtractedString.Set(executionContext, null);
            }
            catch (Exception ex)
            {
                tracer.Trace("Exception: {0}", ex.ToString());
            }
        }
    }
}
