﻿
namespace Models
{
    public class SessionState
    {
        public string CurrentIntent { get; set; }
        public string CurrentStep { get; set; } = "Start";
        public Dictionary<string, string> CollectedEntities { get; set; } = new();
    }
}
