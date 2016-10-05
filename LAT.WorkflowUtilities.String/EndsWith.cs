using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace LAT.WorkflowUtilities.String
{
    public sealed class EndsWith : CodeActivity
    {
        [RequiredArgument]
        [Input("String To Search")]
        public InArgument<string> StringToSearch { get; set; }

        [RequiredArgument]
        [Input("Search For")]
        public InArgument<string> SearchFor { get; set; }

        [RequiredArgument]
        [Input("Case Sensitive")]
        [Default("false")]
        public InArgument<bool> CaseSensitive { get; set; }

        [OutputAttribute("Ends With String")]
        public OutArgument<bool> EndsWithString { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();

            try
            {
                string stringToSearch = StringToSearch.Get(executionContext);
                string searchFor = SearchFor.Get(executionContext);
                bool caseSensitive = CaseSensitive.Get(executionContext);

                bool endsWithString = stringToSearch.EndsWith(searchFor,
                    (caseSensitive) ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase);

                EndsWithString.Set(executionContext, endsWithString);
            }
            catch (Exception ex)
            {
                tracer.Trace("Exception: {0}", ex.ToString());
            }
        }
    }
}
