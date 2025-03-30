using System;
using System.Collections.Generic;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Enums;
using Umbraco.Forms.Core.Persistence.Dtos;
using Umbraco.Core.Logging;
using Umbraco.Core.Composing;

namespace MyUmbracoSite.Core.Forms
{
    /// <summary>
    /// Summary description for ExampleWorkflow
    /// </summary>
    public class ExampleWorkflow : WorkflowType
    {
        public ExampleWorkflow()
        {
            Id = new Guid("ccbeb0d5-adaa-4729-8b4c-4bb439dc0202");
            Name = "ExampleWorkflow";
            Description = "This workflow is just for testing";
            Icon = "icon-chat-active";
            Group = "Services";
        }
        public override WorkflowExecutionStatus Execute(Record record, RecordEventArgs e)
        {
            // first we log it
            Current.Logger.Debug<ExampleWorkflow>("the IP " + record.IP + " has submitted a record");

            // we can then iterate through the fields
            foreach (RecordField rf in record.RecordFields.Values)
            {
                // and we can then do something with the collection of values on each field
                List<object> vals = rf.Values;

                // or get it as a string
                rf.ValuesAsString();
            }

            //Change the state
            record.State = FormState.Approved;

            Current.Logger.Debug<ExampleWorkflow>("The record with unique id {RecordId} that was submitted via the Form {FormName} with id {FormId} has been changed to {RecordState} state",
               record.UniqueId, e.Form.Name, e.Form.Id, "approved");

            return WorkflowExecutionStatus.Completed;
        }

        public override List<Exception> ValidateSettings()
        {
            return new List<Exception>();
        }

    }
}