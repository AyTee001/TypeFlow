using Bogus;
using TypeFlow.Core.Entities;

namespace TypeFlow.Infrastructure.Seed
{
    public static class SeedingHelper
    {
        public static List<TypingChallenge> GetTypingChallenges()
        {
            var texts = GetTexts();

            int counter = -1;
            var faker = new Faker<TypingChallenge>()
                .RuleFor(x => x.CreatedAt, x => DateTime.UtcNow)
                .RuleFor(x => x.Id, x => Guid.NewGuid())
                .RuleFor(x => x.Text, x =>
                {
                    counter++;
                    return texts[counter];
                });

            var challenges = faker.Generate(texts.Length);
            return challenges;
        }

        private static string[] GetTexts()
        {
            return [
                "Technology has transformed education with online courses, digital tools, and virtual classrooms. It enhances engagement, enables collaboration, and makes remote learning accessible. The future of education will rely even more on technology.",
                "Exercise benefits physical and mental health, improving cardiovascular fitness, immunity, and mood. It boosts energy, enhances sleep, and reduces disease risks. Regular activity sharpens focus and productivity.",
                "Good nutrition supports energy, immunity, and cellular repair. A balanced diet with essential nutrients prevents disease, aids cognition, and maintains overall health.",
                "Renewable energy—solar, wind, hydro—is clean and sustainable. Advancing technology reduces costs, making it essential for reducing emissions and ensuring energy security.",
                "Social media connects people but fosters unrealistic standards and stress. It enables advocacy but requires mindful use to protect mental well-being.",
                "Reading expands knowledge, sharpens thinking, and reduces stress. Books enrich minds, improve focus, and provide intellectual and emotional growth.",
                "AI enhances healthcare with advanced diagnostics and personalized treatments. It streamlines operations, improving efficiency and patient outcomes.",
                "Travel exposes individuals to diverse cultures and ideas, fostering growth. It reduces stress, fuels creativity, and offers memorable experiences.",
                "Environmental conservation protects biodiversity and reduces pollution. Sustainable practices, like recycling, mitigate climate change and preserve ecosystems.",
                "Globalization connects economies and cultures but creates inequalities. It offers opportunities yet poses environmental and ethical challenges.",
                "Learning a second language boosts cognition, career opportunities, and cultural understanding. Multilingualism enhances confidence and global connectivity.",
                "Education fuels personal and societal progress, fostering critical thinking and equality. It’s vital for addressing global challenges.",
                "Mental health awareness is crucial for reducing stigma and encouraging support. Understanding and proactive care enhance well-being.",
                "Social media connects people but also fuels comparison and cyberbullying. Healthy boundaries are essential for balanced relationships.",
                "The internet revolutionized communication, commerce, and learning but raises concerns about privacy, security, and misinformation.",
                "Effective time management boosts productivity and balance. Techniques like to-do lists and time blocks prevent stress and burnout.",
                "Electric vehicles offer a sustainable alternative to gas-powered cars. Advances in battery tech make EVs more affordable and efficient.",
                "Meditation improves mental clarity, reduces stress, and promotes mindfulness. It enhances sleep, concentration, and emotional balance.",
                "Clean water is vital for health, yet many lack access. Protecting resources and improving infrastructure ensures global well-being.",
                "Urbanization brings opportunities but strains resources. Sustainable planning can address congestion, pollution, and housing challenges.",
            ];
        }
    }
}
