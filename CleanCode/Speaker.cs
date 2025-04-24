namespace CleanSpeakerApp
{
    public class Speaker
    {
        private static readonly List<string> DisallowedEmailDomains = ["aol.com", "hotmail.com", "prodigy.com", "CompuServe.com"];
        private static readonly List<string> OutdatedTech = ["Cobol", "Punch Cards", "Commodore", "VBScript"];
        private static readonly List<string> PreferredEmployers = ["Microsoft", "Google", "Fog Creek Software", "37Signals"];

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public int? ExperienceYears { get; set; }
        public bool HasBlog { get; set; }
        public required string Employer { get; set; }
        public required WebBrowser Browser { get; set; }
        public List<string> Certifications { get; set; } = [];
        public List<Session> Sessions { get; set; } = [];
        public int RegistrationFee { get; private set; }

        public int Register(IRepository repository)
        {
            ValidateRequiredFields();

            if (!IsQualified())
                throw new SpeakerDoesntMeetRequirementsException("Speaker doesn't meet requirements.");

            if (Sessions.Count == 0)
                throw new ArgumentException("Can't register speaker with no sessions.");

            bool hasApproved = ApproveSessions();

            if (!hasApproved)
                throw new NoSessionsApprovedException("No sessions approved.");

            SetRegistrationFee();

            return repository.SaveSpeaker(this);
        }

        private void ValidateRequiredFields()
        {
            if (string.IsNullOrWhiteSpace(FirstName)) throw new ArgumentNullException("First name is required.");
            if (string.IsNullOrWhiteSpace(LastName)) throw new ArgumentNullException("Last name is required.");
            if (string.IsNullOrWhiteSpace(Email)) throw new ArgumentNullException("Email is required.");
        }

        private bool IsQualified()
        {
            bool qualified = ExperienceYears > 10 || HasBlog || Certifications.Count > 3 || PreferredEmployers.Contains(Employer);

            if (!qualified)
            {
                var domain = Email.Split('@').Last().ToLower();
                bool usingOldIE = Browser.Name == "InternetExplorer" && Browser.MajorVersion < 9;
                qualified = !DisallowedEmailDomains.Contains(domain) && !usingOldIE;
            }

            return qualified;
        }

        private bool ApproveSessions()
        {
            bool anyApproved = false;
            foreach (var session in Sessions)
            {
                bool outdated = OutdatedTech.Any(tech =>
                    session.Title.Contains(tech, StringComparison.OrdinalIgnoreCase) ||
                    session.Description.Contains(tech, StringComparison.OrdinalIgnoreCase));

                session.Approved = !outdated;
                if (session.Approved) anyApproved = true;
            }
            return anyApproved;
        }

        private void SetRegistrationFee()
        {
            int? exp = ExperienceYears ?? 0;
            RegistrationFee = exp switch
            {
                <= 1 => 500,
                <= 3 => 250,
                <= 5 => 100,
                <= 9 => 50,
                _ => 0
            };
        }
    }
}
