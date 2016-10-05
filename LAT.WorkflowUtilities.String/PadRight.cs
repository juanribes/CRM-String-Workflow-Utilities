using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace LAT.WorkflowUtilities.String
{
    public sealed class PadRight : CodeActivity
    {
        [RequiredArgument]
        [Input("String To Pad")]
        public InArgument<string> StringToPad { get; set; }

        [RequiredArgument]
        [Input("Pad Character")]
        public InArgument<string> PadCharacter { get; set; }

        [RequiredArgument]
        [Input("Length")]
        public InArgument<int> Length { get; set; }

        [OutputAttribute("Padded String")]
        public OutArgument<string> PaddedString { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();

            try
            {
                string stringToPad = StringToPad.Get(executionContext);
                string padCharacter = PadCharacter.Get(executionContext);
                int length = Length.Get(executionContext);

                string paddedString = stringToPad;

                for (int i = 0; i < length; i++)
                {
                    paddedString = paddedString + padCharacter;
                }

                PaddedString.Set(executionContext, paddedString);
            }
            catch (Exception ex)
            {
                tracer.Trace("Exception: {0}", ex.ToString());
            }
        }
    }
}
