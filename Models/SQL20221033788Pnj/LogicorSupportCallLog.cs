using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogicorSupportCalls.Models.SQL2022_1033788_pnj
{
    [Table("Logicor_SupportCallLog", Schema = "dbo")]
    public partial class LogicorSupportCallLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public string SupportAgent { get; set; }

        public int? CallDuration { get; set; }

        public DateTime? CallDate { get; set; }

        public TimeSpan? CallTime { get; set; }

        public bool? ZendeskTicket { get; set; }

        public string TicketLink { get; set; }

        public string Issue { get; set; }

        public string Description { get; set; }

    }
}