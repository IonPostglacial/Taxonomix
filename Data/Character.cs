using System.Collections.Generic;

namespace Taxonomix.Data
{
    public class Character : Item
    {
        List<State> States { get; set; }
        List<State> RequiredStates { get; set; }
    }
}