using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorClient.Models
{
    public class TeleParameter
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public TeleComparison Comparison { get; set; }
    }
}
