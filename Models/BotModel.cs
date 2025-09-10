namespace Models
{
    public class BotModel
    {
        public Animal Animal { get; set; }
        public BotState BotState { get; set; }
    }

    // 1. General Pet Care & Education
    public class GeneralPetCare
    {
        public string FeedingGuidelines { get; set; }
        public string VaccinationSchedule { get; set; }
        public string GroomingTips { get; set; }
        public string ExerciseSuggestions { get; set; }
    }

    // 2. Symptom Checker (First Aid Guidance)
    public class SymptomChecker
    {
        public string Symptom { get; set; }
        public string SeverityLevel { get; set; } // e.g. "Mild", "Moderate", "Severe"
        public string SuggestedAction { get; set; } // e.g. "Monitor", "Visit Vet"
        public string RedFlagAlert { get; set; } // warning message if urgent
    }

    // 3. Poison & Emergency Guidance
    public class EmergencyGuidance
    {
        public string ToxicSubstance { get; set; }
        public int EmergencyLevel { get; set; }
        public string EmergencyAction { get; set; }
        public string NearestClinic { get; set; }
    }
}
