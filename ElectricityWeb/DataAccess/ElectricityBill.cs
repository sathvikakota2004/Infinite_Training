﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DataAccess
{
    public class ElectricityBill
    {
        private string consumerNumber;
        private string consumerName;
        private int unitsConsumed;
        private double billAmount;

        public string ConsumerNumber
        {
            get => consumerNumber;
            set
            {
                if (!Regex.IsMatch(value ?? "", @"^EB\d{5}$"))
                    throw new FormatException("Invalid Consumer Number");
                consumerNumber = value;
            }
        }

        public string ConsumerName
        {
            get => consumerName;
            set => consumerName = value;
        }

        public int UnitsConsumed
        {
            get => unitsConsumed;
            set => unitsConsumed = value;
        }

        public double BillAmount
        {
            get => billAmount;
            set => billAmount = value;
        }
    }
}
