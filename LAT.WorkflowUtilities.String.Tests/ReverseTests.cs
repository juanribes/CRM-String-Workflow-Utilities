using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using Moq;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;

namespace LAT.WorkflowUtilities.String.Tests
{
    [TestClass]
    public class ReverseTests
    {
        [TestMethod]
        public void String1()
        {
            //Input parameters
            var inputs = new Dictionary<string, object> 
            {
                { "StringToReverse", "Hello"}
            };

            //Expected value
            const string expected = "olleH";

            //Invoke the workflow
            var output = InvokeWorkflow("LAT.WorkflowUtilities.String.Reverse, LAT.WorkflowUtilities.String", inputs);

            //Test
            Assert.AreEqual(expected, output["ReversedString"]);
        }

        [TestMethod]
        public void String2()
        {
            //Input parameters
            var inputs = new Dictionary<string, object> 
            {
                { "StringToReverse", "Hello World"}
            };

            //Expected value
            const string expected = "dlroW olleH";

            //Invoke the workflow
            var output = InvokeWorkflow("LAT.WorkflowUtilities.String.Reverse, LAT.WorkflowUtilities.String", inputs);

            //Test
            Assert.AreEqual(expected, output["ReversedString"]);
        }

        private static IDictionary<string, object> InvokeWorkflow(string name, Dictionary<string, object> inputs)
        {
            var testClass = Activator.CreateInstance(Type.GetType(name)) as CodeActivity;

            //Instantiate the workflowinvoker
            var invoker = new WorkflowInvoker(testClass);

            var serviceMock = new Mock<IOrganizationService>();
            var factoryMock = new Mock<IOrganizationServiceFactory>();
            var tracingServiceMock = new Mock<ITracingService>();
            var workflowContextMock = new Mock<IWorkflowContext>();

            IOrganizationService service = serviceMock.Object;

            //Mock workflow Context
            var workflowUserId = Guid.NewGuid();
            var workflowCorrelationId = Guid.NewGuid();
            var workflowInitiatingUserId = Guid.NewGuid();

            workflowContextMock.Setup(t => t.InitiatingUserId).Returns(workflowInitiatingUserId);
            workflowContextMock.Setup(t => t.CorrelationId).Returns(workflowCorrelationId);
            workflowContextMock.Setup(t => t.UserId).Returns(workflowUserId);
            var workflowContext = workflowContextMock.Object;

            //Mock servicefactory using the CRM service mock
            factoryMock.Setup(t => t.CreateOrganizationService(It.IsAny<Guid>())).Returns(service);
            var factory = factoryMock.Object;

            //Mock tracingservice - appears in test Output
            tracingServiceMock.Setup(t => t.Trace(It.IsAny<string>(), It.IsAny<object[]>())).Callback<string, object[]>(WriteTrace);
            var tracingService = tracingServiceMock.Object;

            invoker.Extensions.Add(() => tracingService);
            invoker.Extensions.Add(() => workflowContext);
            invoker.Extensions.Add(() => factory);

            return invoker.Invoke(inputs);
        }

        private static void WriteTrace(string s, object[] o)
        {
            Debug.WriteLine(s);
        }
    }
}
