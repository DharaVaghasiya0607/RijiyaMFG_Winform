using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class ClientTicket
    {

        public Guid ID { get; set; }
        public string TicketNoStr { get; set; }
        public int YY { get; set; }
        public int MM { get; set; }
        public int DD { get; set; }
        public int TicketNo { get; set; }
        public Int64 Customer_ID { get; set; }
        public int Project_ID { get; set; }
        public int Form_ID { get; set; }
        public string TicketGeneratedBy { get; set; }
        public string TicketDate { get; set; }
        public string ExpetedFinishDate { get; set; }
        public string TaskDetail { get; set; }
        public string Priority { get; set; }
        public string TicketStatus { get; set; }
        
        public int EntryIP { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
