namespace CleanSpeakerApp
{
    public class SpeakerDoesntMeetRequirementsException : Exception
    {
        public SpeakerDoesntMeetRequirementsException(string message) : base(message) { }
    }

    public class NoSessionsApprovedException : Exception
    {
        public NoSessionsApprovedException(string message) : base(message) { }
    }
}
