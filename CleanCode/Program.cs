using System;
using System.Collections.Generic;

namespace CleanSpeakerApp
{
    class Program
    {
        static void Main()
        {
            var speaker = new Speaker
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@gmail.com",
                ExperienceYears = 12,
                HasBlog = true,
                Employer = "Microsoft",
                Browser = new WebBrowser { Name = "Chrome", MajorVersion = 90 },
                Certifications = new List<string> { "Cert1", "Cert2", "Cert3", "Cert4" },
                Sessions = new List<Session>
                {
                    new Session { Title = "Modern .NET", Description = "What's new in .NET 8." }
                }
            };

            try
            {
                var repo = new FakeRepository();
                int speakerId = speaker.Register(repo);
                Console.WriteLine($"Speaker registered successfully! ID: {speakerId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Registration failed: {ex.Message}");
            }
        }
    }
}
