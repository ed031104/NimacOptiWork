using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public string InvoiceNumber { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string Createdby { get; set; } = null!;

    public string? TaskCode { get; set; }

    #region Constructors
    public Task()
    {
    }
    public Task(int taskId, string invoiceNumber, string description, DateTime createdDate, string createdby, string? taskCode)
    {
        TaskId = taskId;
        InvoiceNumber = invoiceNumber;
        Description = description;
        CreatedDate = createdDate;
        Createdby = createdby;
        TaskCode = taskCode;
    }
    #endregion

    #region Pattern Builder
    public class Builder
    {
        private readonly Task _task;
        public Builder()
        {
            _task = new Task();
        }
        public Builder WithTaskId(int taskId)
        {
            _task.TaskId = taskId;
            return this;
        }
        public Builder WithInvoiceNumber(string invoiceNumber)
        {
            _task.InvoiceNumber = invoiceNumber;
            return this;
        }
        public Builder WithDescription(string description)
        {
            _task.Description = description;
            return this;
        }
        public Builder WithCreatedDate(DateTime createdDate)
        {
            _task.CreatedDate = createdDate;
            return this;
        }
        public Builder WithCreatedby(string createdby)
        {
            _task.Createdby = createdby;
            return this;
        }
        public Builder WithTaskCode(string? taskCode)
        {
            _task.TaskCode = taskCode;
            return this;
        }
        public Task Build()
        {
            return _task;
        }
    }
    #endregion
}
