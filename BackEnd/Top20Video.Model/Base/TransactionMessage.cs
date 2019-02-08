using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Top20Video.Framework;

namespace Top20Video.Models
{
    /// <summary>
    /// to manage application transaction message
    /// </summary>
    public class TransactionMessage
    {
        public string Message { get; set; }
        public MessageStatus Status { get; set; }
        public string Data { get; set; }
    }
}