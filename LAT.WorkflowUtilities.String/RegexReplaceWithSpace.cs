using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Text.RegularExpressions;

namespace LAT.WorkflowUtilities.String
{
	public sealed class RegexReplaceWithSpace : CodeActivity
	{
		[RequiredArgument]
		[Input("String To Search")]
		public InArgument<string> StringToSearch { get; set; }

		[RequiredArgument]
		[Input("Number Of Spaces")]
		public InArgument<int> NumberOfSpaces { get; set; }

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
				int numberOfSpaces = NumberOfSpaces.Get(executionContext);
				string pattern = Pattern.Get(executionContext);

				Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

				string spaces = "";
				spaces = spaces.PadRight(numberOfSpaces, ' ');

				string replacedString = regex.Replace(stringToSearch, spaces);

				ReplacedString.Set(executionContext, replacedString);
			}
			catch (Exception ex)
			{
				tracer.Trace("Exception: {0}", ex.ToString());
			}
		}
	}
}
