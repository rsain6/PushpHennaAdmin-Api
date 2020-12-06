using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Model
{
    public class Byer : CommanProperty
    {
        public int BUYER_ID { get; set; }
        public string MOBILE { get; set; }
        public string DEPENDANT_NAME { get; set; }
        public string DEPENDANT_RELATION { get; set; }
        public string DEPENDANT_NAME_TWO { get; set; }
        public string DEPENDANT_RELATION_TWO { get; set; }
        public DateTime BUYER_DOB { get; set; }
        public string PPO_NO { get; set; }
        public string BOOK_NO { get; set; }
        public DateTime VALID_UPTO { get; set; }
        public string REF_NO { get; set; }
        public string DISPENSARY { get; set; }
        public double WALLET_LIMIT { get; set; }
        public double? PREV_LIMITBALANCE { get; set; }
        public int BUYER_TYPE { get; set; }
        public string ADDRESS { get; set; }
        public string RETIRED_POST { get; set; }
        public string DEPARTMENT { get; set; }
    }
}
