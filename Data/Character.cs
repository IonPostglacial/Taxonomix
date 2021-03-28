using System.Collections.Generic;

namespace Taxonomix.Data
{
    public class Character : Item
    {
        public List<State> States { get; set; } = new();
        public List<State> RequiredStates { get; set; } = new();
    }
}