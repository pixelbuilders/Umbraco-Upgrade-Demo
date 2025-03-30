using System;
using System.Collections.Generic;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Enums;
using Umbraco.Forms.Core.Persistence.Dtos;
using Microsoft.Extensions.Logging;

namespace MyUmbracoSite.Core.Forms
{
    /// <summary>
    /// Summary description for ExampleWorkflow
    /// </summary>
    public class ExampleWorkflow : WorkflowType
    {
        private readonly ILogger<ExampleWorkflow> _logger;
        public ExampleWorkflow(ILogger<ExampleWorkflow> logger)
        {
            Id = new Guid("ccbeb0d5-adaa-4729-8b4c-4bb439dc0202");
            Name = "ExampleWorkflow";
            Description = "This workflow is just for testing";
            Icon = "icon-chat-active";
            Group = "Services";

            _logger = logger;
        }
        public override Task<WorkflowExecutionStatus> ExecuteAsync(WorkflowExecutionContext context)
        {
            // first we log it
            _logger.LogDebug("The IP {IP} has submitted a record", context.Record.IP);

            // we can then iterate through the fields
            foreach (RecordField rf in context.Record.RecordFields.Values)
            {
                // and we can then do something with the collection of values on each field
                List<object> vals = rf.Values;

                // or get it as a string
                rf.ValuesAsString();
            }

            //Change the state
            context.Record.State = FormState.Approved;

            _logger.LogDebug("The record with unique id {RecordId} that was submitted via the Form {FormName} with id {FormId} has been changed to {RecordState} state",
               context.Record.UniqueId, context.Form.Name, context.Form.Id, "approved");

            return Task.FromResult(WorkflowExecutionStatus.Completed);
        }


        public override List<Exception> ValidateSettings()
        {
            return new List<Exception>();
        }
    }
}