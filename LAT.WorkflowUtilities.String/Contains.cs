using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace LAT.WorkflowUtilities.String
{
    public sealed class Contains : CodeActivity
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

        [OutputAttribute("Contains String")]
        public OutArgument<bool> ContainsString { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();

            try
            {
                string stringToSearch = StringToSearch.Get(executionContext);
                string searchFor = SearchFor.Get(executionContext);
                bool caseSensitive = CaseSensitive.Get(executionContext);

                if (!caseSensitive)
                {
                    stringToSearch = stringToSearch.ToUpper();
                    searchFor = searchFor.ToUpper();
                }

                bool containsString = stringToSearch.Contains(searchFor);

                ContainsString.Set(executionContext, containsString);
            }
            catch (Exception ex)
            {
                tracer.Trace("Exception: {0}", ex.ToString());
            }
        }
    }
}