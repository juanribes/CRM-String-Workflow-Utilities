using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace LAT.WorkflowUtilities.String
{
	public sealed class ReplaceWithSpace : CodeActivity
	{
		[RequiredArgument]
		[Input("String To Search")]
		public InArgument<string> StringToSearch { get; set; }

		[RequiredArgument]
		[Input("Value To Replace")]
		public InArgument<string> ValueToReplace { get; set; }

		[RequiredArgument]
		[Input("Number Of Spaces")]
		public InArgument<int> NumberOfSpaces { get; set; }

		[OutputAttribute("Replaced String")]
		public OutArgument<string> ReplacedString { get; set; }

		protected override void Execute(CodeActivityContext executionContext)
		{
			ITracingService tracer = executionContext.GetExtension<ITracingService>();

			try
			{
				string stringToSearch = StringToSearch.Get(executionContext);
				string valueToReplace = ValueToReplace.Get(executionContext);
				int numberOfSpaces = NumberOfSpaces.Get(executionContext);
				
				string spaces = "";
				spaces = spaces.PadRight(numberOfSpaces, ' ');

				string replacedString = stringToSearch.Replace(valueToReplace, spaces);

				ReplacedString.Set(executionContext, replacedString);
			}
			catch (Exception ex)
			{
				tracer.Trace("Exception: {0}", ex.ToString());
			}
		}
	}
}