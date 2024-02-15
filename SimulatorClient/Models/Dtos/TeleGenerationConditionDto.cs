using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorClient.Models.Dtos
{
    public class TeleGenerationConditionDto
    {
        public string Name { get; set; }
        public int? BottomRestriction { get; set; }
        public int? TopRestriction { get; set; }
    }
}
