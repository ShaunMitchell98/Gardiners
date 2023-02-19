using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardiners.Core.Models;

[Table("IncomingTransactions")]
public class IncomingTransaction
{
    public IncomingTransaction(string sourceType, string sourceEndpoint, bool done, DateTimeOffset? doneTime)
    {
        SourceType=sourceType;
        SourceEndpoint=sourceEndpoint;
        Done=done;
        DoneTime=doneTime;
    }

    [Key]
    public int TransactionId { get; set; }
    public string SourceType { get; set; }
    public string SourceEndpoint { get; set; }
    public bool Done { get; set; }
    public DateTimeOffset? DoneTime { get; set; }
}
