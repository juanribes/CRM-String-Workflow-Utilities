using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Globalization;

namespace LAT.WorkflowUtilities.String
{
    public sealed class ToTitleCase : CodeActivity
    {
        [RequiredArgument]
        [Input("String To Title Case")]
        public InArgument<string> StringToTitleCase { get; set; }

        [OutputAttribute("Title Cased String")]
        public OutArgument<string> TitleCasedString { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();

            try
            {
                string stringToTitleCase = StringToTitleCase.Get(executionContext);

                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

                string titleCasedString = ti.ToTitleCase(stringToTitleCase);

                TitleCasedString.Set(executionContext, titleCasedString);
            }
            catch (Exception ex)
            {
                tracer.Trace("Exception: {0}", ex.ToString());
            }
        }
    }
}
