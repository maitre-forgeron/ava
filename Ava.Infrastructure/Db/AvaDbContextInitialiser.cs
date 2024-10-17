using Ava.Domain.Models.Category;

namespace Ava.Infrastructure.Db;

public class AvaDbContextInitialiser
{
    private readonly ILogger<AvaDbContext> _logger;
    private readonly AvaDbContext _context;

    public AvaDbContextInitialiser(ILogger<AvaDbContext> logger, AvaDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, GeneralErrorMessages.DatabaseInitializationErrorMessage);
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, GeneralErrorMessages.DatabaseSeedingErrorMessage);
            throw;
        }
    }
    public async Task TrySeedAsync()
    {
        if (!_context.Categories.Any())
        {
            var categories = new List<Category>
        {
            new() {
                Id = Guid.Parse("e64b97e6-9f23-44e0-b007-91fbdd20bbd1"),
                Name = "General Therapy And Counseling",
                SubCategories =
                [
                    new SubCategory { Id = Guid.Parse("5821c1c5-1f4b-4f63-b798-68042e6f939e"), Name = "Individual Therapy" },
                    new SubCategory { Id = Guid.Parse("701ee3e0-cd6e-4cb4-a230-5f8e422a3f77"), Name = "Couples Therapy" },
                    new SubCategory { Id = Guid.Parse("89dcb735-bcd8-4b5d-b58a-d7f0faad20e7"), Name = "Family Therapy" },
                    new SubCategory { Id = Guid.Parse("82b5035f-b142-4f3f-b567-b1fa234fc8a7"), Name = "Group Therapy" },
                    new SubCategory { Id = Guid.Parse("d47490c3-8b30-4e26-9abf-c576af2e2fd6"), Name = "Stress Management" },
                    new SubCategory { Id = Guid.Parse("3fbbf2c8-7df7-4b60-90ca-43fc097c245f"), Name = "Relationship Counseling" }
                ]
            },
            new() {
                Id = Guid.Parse("3fbbf2c8-7df7-4b60-90ca-43fc097c245f"),
                Name = "Mental Health Disorders",
                SubCategories =
                [
                    new SubCategory { Id = Guid.Parse("b4e0b22a-4099-42e4-9a48-9d48c1c6a2c8"), Name = "Anxiety Disorders" },
                    new SubCategory { Id = Guid.Parse("1eb90d70-bf62-4bff-b1b6-c99897d437de"), Name = "Depression" },
                    new SubCategory { Id = Guid.Parse("bdefb089-d72a-4373-bf54-b4c5cbb1257e"), Name = "Bipolar Disorder" },
                    new SubCategory { Id = Guid.Parse("9175c1a3-293e-460f-9215-0e7fd7b1e97e"), Name = "Obsessive Compulsive Disorder (OCD)" },
                    new SubCategory { Id = Guid.Parse("fc4fc592-7754-4b16-b0eb-b4fffb17887a"), Name = "Post Traumatic Stress Disorder (PTSD)" },
                    new SubCategory { Id = Guid.Parse("e7dc340d-2b12-4e6e-866f-08f69d7ba8fd"), Name = "Schizophrenia" },
                    new SubCategory { Id = Guid.Parse("c3b5d40c-5cda-4484-836a-2288a801d8d5"), Name = "Panic Disorders" }
                ]
            },
            new() {
                Id = Guid.Parse("5b6350cc-9602-49f4-9336-c09d81df4ba9"),
                Name = "Behavioral Therapy",
                SubCategories =
                [
                    new SubCategory { Id = Guid.Parse("e5a76d78-6b29-431b-b673-5876a08b7fd2"), Name = "Cognitive Behavioral Therapy (CBT)" },
                    new SubCategory { Id = Guid.Parse("9781b876-3124-4f8f-816e-3f16fb03dd53"), Name = "Dialectical Behavior Therapy (DBT)" },
                    new SubCategory { Id = Guid.Parse("1e50b080-d69a-4ed5-9d77-e7f399f3ebd2"), Name = "Acceptance And Commitment Therapy (ACT)" }
                ]
            },
            new() {
                Id = Guid.Parse("817978ec-795f-4c85-81e2-9c5f21e95323"),
                Name = "Psychiatric Services",
                SubCategories =
                [
                    new SubCategory { Id = Guid.Parse("ec7dd153-3706-41f4-89c1-bd1b9940320a"), Name = "Medication Management" },
                    new SubCategory { Id = Guid.Parse("17ec0ae9-514f-47e2-a5b3-7d6aaf597f98"), Name = "Psychiatric Assessment" },
                    new SubCategory { Id = Guid.Parse("f1a7a206-ffb4-4e6a-b84e-7d20f8fc7852"), Name = "Medication Consultation" }
                ]
            },
            new() {
                Id = Guid.Parse("51a50ef4-7f9a-4e94-9101-704b9d1abf64"),
                Name = "Life Challenges And Personal Development",
                SubCategories =
                [
                    new SubCategory { Id = Guid.Parse("d0ef5548-54f4-4b97-85d5-2a8da27c2992"), Name = "Life Transitions (Eg, Career Changes, Moving)" },
                    new SubCategory { Id = Guid.Parse("5c08f00e-d05a-4b7e-931f-95e81b95fded"), Name = "Grief And Loss Counseling" },
                    new SubCategory { Id = Guid.Parse("680fe451-9447-4dbd-b0ba-3026179d02a3"), Name = "Building Self-Esteem" },
                    new SubCategory { Id = Guid.Parse("ad9e6ecb-3d28-4e3c-8b4a-4d57b1a21924"), Name = "Personal Growth" },
                    new SubCategory { Id = Guid.Parse("92d1a763-38fd-48ac-8345-53c6fcb5c9c4"), Name = "Confidence Training" }
                ]
            },
            new() {
                Id = Guid.Parse("9926e3a5-9b07-4ac7-bb5b-c3fa8d290264"),
                Name = "Children, Adults And Family",
                SubCategories =
                [
                    new SubCategory { Id = Guid.Parse("7dd63a53-60e3-4ab1-bde5-b2b8fc1a7db4"), Name = "Child Counseling" },
                    new SubCategory { Id = Guid.Parse("6cf55428-c166-48d7-85da-49f9e2c2c97f"), Name = "Adolescent Therapy" },
                    new SubCategory { Id = Guid.Parse("4b77c839-b6eb-4e5e-b2e6-279ca53550b6"), Name = "Parental Support" },
                    new SubCategory { Id = Guid.Parse("52bb96aa-14c7-49e8-b715-dac55f5b5f10"), Name = "School Related Issues" },
                    new SubCategory { Id = Guid.Parse("b9f7177b-669a-421d-a1e7-e58b88dfdc8f"), Name = "Behavioral Problems In Children" }
                ]
            },
            new() {
                Id = Guid.Parse("7b58a846-3437-47e2-88bb-34b40f0bc856"),
                Name = "Couples And Relationships",
                SubCategories =
                [
                    new SubCategory { Id = Guid.Parse("9b825d2d-47f8-4b92-9497-9fcd774d2431"), Name = "Premarital Counseling" },
                    new SubCategory { Id = Guid.Parse("b3fd0380-32df-4846-9f64-08afaf9ecb90"), Name = "Divorce And Separation Counseling" },
                    new SubCategory { Id = Guid.Parse("fd7a2ed9-6d94-42d1-bb9d-72c5adf27332"), Name = "Infidelity Counseling" },
                    new SubCategory { Id = Guid.Parse("04cc9270-bb7a-4d97-a3b7-37c0b9c0db4b"), Name = "Communication Issues" },
                    new SubCategory { Id = Guid.Parse("6d945eac-cb80-4690-81ea-b5e681c378f1"), Name = "Conflict Resolution" }
                ]
            },
            new() {
                Id = Guid.Parse("407e7d83-4ec1-4021-85db-008b015c3df3"),
                Name = "Work And Career",
                SubCategories =
                [
                    new SubCategory { Id = Guid.Parse("b5364b90-b71d-46b1-8c54-c7493b9e0035"), Name = "Work Stress" },
                    new SubCategory { Id = Guid.Parse("6193a4ab-4024-40a8-bc83-36eabdb82cfc"), Name = "Career Counseling" },
                    new SubCategory { Id = Guid.Parse("5c19166d-fb46-4e05-9c93-41554bc7e507"), Name = "Prevention Of Burns" },
                    new SubCategory { Id = Guid.Parse("0a88f8d0-6918-4265-9cc0-5ae97b193a85"), Name = "Work-Life Balance" }
                ]
            },
            new() {
                Id = Guid.Parse("7b06f72e-3f7f-4528-89b9-6e4703f7e374"),
                Name = "Recovery From Trauma And Abuse",
                SubCategories =
                [
                    new SubCategory { Id = Guid.Parse("cc3d56e8-4c5b-4909-a0aa-91ca4e803d84"), Name = "Trauma Therapy" },
                    new SubCategory { Id = Guid.Parse("44088b1c-b0bb-41d7-bef0-489c370cb9f9"), Name = "Sexual Violence Recovery" },
                    new SubCategory { Id = Guid.Parse("345b6a8e-164b-4260-b29f-5c035ec5cf77"), Name = "Counseling Against Domestic Violence" }
                ]
            },
            new() {
                Id = Guid.Parse("5a807d81-1b5e-4036-b6f8-063ab3fc14b4"),
                Name = "Drug Abuse And Addiction",
                SubCategories =
                [
                    new SubCategory { Id = Guid.Parse("4799d14f-5c88-4299-8749-6cfcd86f32ab"), Name = "Alcohol Dependence" },
                    new SubCategory { Id = Guid.Parse("44cf77ba-4ecf-472f-bd6f-4f66c4824aa3"), Name = "Drug Addiction" },
                    new SubCategory { Id = Guid.Parse("c9e802e9-1a06-4fd2-8a94-23d1fcb8a8f4"), Name = "Smoking Cessation" },
                    new SubCategory { Id = Guid.Parse("be57c7b4-58b4-4b93-94d7-3d3a77a13912"), Name = "Addiction To Gambling" },
                    new SubCategory { Id = Guid.Parse("42bbf18c-d0bb-489c-87d6-0ee1e888c9ff"), Name = "Internet Addiction" }
                ]
            },
            new() {
                Id = Guid.Parse("85f3d088-f0ae-4f83-8135-5746e31e10cc"),
                Name = "Eating Disorders And Body Image",
                SubCategories =
                [
                    new SubCategory { Id = Guid.Parse("580b682f-6191-4e62-8150-d10b155e8e73"), Name = "Anorexia Nervosa" },
                    new SubCategory { Id = Guid.Parse("b2be31b1-4017-4ef0-9858-8d2d35cc6935"), Name = "Bulimia Nervosa" },
                    new SubCategory { Id = Guid.Parse("3aabbc82-6940-4c62-b554-5cbccf93b3c3"), Name = "Binge Eating Disorder" },
                    new SubCategory { Id = Guid.Parse("9eb4444c-b5d7-4d8e-99c5-d9d56a246b4e"), Name = "Body Dysmorphic Disorder" }
                ]
            },
            new() {
                Id = Guid.Parse("420b187c-79d4-4c53-bb55-1449dc3ff5d7"),
                Name = "Managing Stress, Anxiety And Depression",
                SubCategories =
                [
                    new SubCategory { Id = Guid.Parse("408b9c54-98dc-4931-8ed1-0ac2e39d1c16"), Name = "Mindfulness-Based Stress Reduction" },
                    new SubCategory { Id = Guid.Parse("07a2f3eb-0017-42ff-9279-f8f3f61e8bc7"), Name = "Relaxation Techniques" },
                    new SubCategory { Id = Guid.Parse("4b823ff4-69b0-4f11-b478-eae3035e26a4"), Name = "Guided Meditations" },
                    new SubCategory { Id = Guid.Parse("7aa78c6f-fb7b-41e0-9c43-16700404af11"), Name = "Stress Reduction Coaching" }
                ]
            },
            new() {
                Id = Guid.Parse("f088caa4-1d53-4f3a-a10f-3940f0f4b6ef"),
                Name = "Neurodiversity",
                SubCategories =
                [
                    new SubCategory { Id = Guid.Parse("b890d5bb-42b0-4f1d-9100-8c6362bcd960"), Name = "Autism Spectrum Disorder (ASD) Support" },
                    new SubCategory { Id = Guid.Parse("8d046c4b-bc30-46d5-bd80-4cf3a29e057b"), Name = "Management Of Attention Deficit Hyperactivity Disorder (ADHD)" },
                    new SubCategory { Id = Guid.Parse("7c4fdc07-0189-4c43-83c6-66011e8a8a81"), Name = "Dyslexia Counseling" }
                ]
            },
            new() {
                Id = Guid.Parse("02f65fef-ff88-47ff-a78c-748a05d3f842"),
                Name = "Sleep Disturbance",
                SubCategories =
                [
                    new SubCategory { Id = Guid.Parse("e07c9496-5aeb-4654-8b88-4dfd928b7f3c"), Name = "Treatment Of Insomnia" },
                    new SubCategory { Id = Guid.Parse("dc4fc261-9c8e-4ed5-b540-706ec40eb3a2"), Name = "Support For Sleep Apnea" },
                    new SubCategory { Id = Guid.Parse("6e222f9e-b464-40e3-8d9d-2db8e51a1679"), Name = "Sleep Hygiene Consultation" }
                ]
            },
            new() {
                Id = Guid.Parse("c5f5798a-0af2-46f1-bdd1-77f8af9f4a46"),
                Name = "Sexual Health And Gender Identity",
                SubCategories =
                [
                    new SubCategory { Id = Guid.Parse("e6f61b8d-dcb2-4b9e-92a2-b57cfdd28c62"), Name = "Sexual Dysfunction" },
                    new SubCategory { Id = Guid.Parse("53c0d3f8-b403-4e0b-9639-0b9a2c6b2e9a"), Name = "LGBTQ+ Affirmative Therapy" },
                    new SubCategory { Id = Guid.Parse("20c4c065-fdb0-42aa-b7a0-f68a1fa62c7f"), Name = "Gender Identity Support" },
                    new SubCategory { Id = Guid.Parse("14d51fa0-b5a4-4654-84b1-920a4c7c86e2"), Name = "Sexual Counseling" }
                ]
            },
            new() {
                Id = Guid.Parse("e4478961-3b4c-4f02-8b2e-8faee4e8cb48"),
                Name = "Chronic Illness And Pain Management",
                SubCategories =
                [
                    new SubCategory { Id = Guid.Parse("61838243-90ff-4db2-9084-9c1e687f16dc"), Name = "Coping With Chronic Illness" },
                    new SubCategory { Id = Guid.Parse("cf82ec67-e97f-482e-9230-4974a464f38b"), Name = "Chronic Pain Management" },
                    new SubCategory { Id = Guid.Parse("5a823ae2-b8aa-4ef2-8d65-8c4cfdbd4bc2"), Name = "Disability Support" }
                ]
            },
            new() {
                Id = Guid.Parse("3e5f60e4-5920-4875-8367-8dc68f3d49a5"),
                Name = "Mindfulness And Well-Being",
                SubCategories =
                [
                    new SubCategory { Id = Guid.Parse("9b348ef2-9b3e-4520-b96f-647591d20308"), Name = "Mindfulness Coaching" },
                    new SubCategory { Id = Guid.Parse("f69b51d4-d4ae-4d65-b76d-3f0f6349eb77"), Name = "Meditation And Relaxation Techniques" },
                    new SubCategory { Id = Guid.Parse("ef76c95b-6d0a-48c2-9e02-e9c2b96b8454"), Name = "Positive Psychology And Well-Being" }
                ]
            },
            new() {
                Id = Guid.Parse("dc2c19a6-9de5-4712-8129-ef1fc746d1a5"),
                Name = "Phobia Treatment",
                SubCategories =
                [
                    new SubCategory { Id = Guid.Parse("9e629f79-e776-4385-bf79-d75b69e99c08"), Name = "Social Phobia" },
                    new SubCategory { Id = Guid.Parse("39d34899-b93c-470b-bd81-18d68bb4de94"), Name = "Agoraphobia" },
                    new SubCategory { Id = Guid.Parse("9ef1d8cc-7d97-4c5e-9565-abcabc4f31aa"), Name = "Specific Phobias (Eg, Fear Of Flying, Heights)" }
                ]
            },
            new() {
                Id = Guid.Parse("863f7079-0d88-40c0-a8f3-f9c4fa375b84"),
                Name = "Anger Management",
                SubCategories =
                [
                    new SubCategory { Id = Guid.Parse("69eb184b-7f4c-43a2-b53d-bcf8d775fb93"), Name = "Anger Control Therapy" },
                    new SubCategory { Id = Guid.Parse("fe35c61f-f6ba-4be5-a98f-cf9b4cb78cb2"), Name = "Conflict Resolution Skills" }
                ]
            },
            new() {
                Id = Guid.Parse("f8c58512-83cd-44ea-b74a-1f60d2de7b46"),
                Name = "Specialized Therapy",
                SubCategories =
                [
                    new SubCategory { Id = Guid.Parse("4fc84c6f-dfa8-48a5-bdd7-d4f6c7ebbb78"), Name = "Art Therapy" },
                    new SubCategory { Id = Guid.Parse("158d1885-588c-4fa3-81da-54258232de72"), Name = "Music Therapy" },
                    new SubCategory { Id = Guid.Parse("a681e82f-d7f3-4176-bb0e-629bb409a5a0"), Name = "Play Therapy For Children" },
                    new SubCategory { Id = Guid.Parse("1ee3c456-09f4-4e40-b9f1-1ae086c4d701"), Name = "Animal-Assisted Therapy" }
                ]
            }
        };

            await _context.Categories.AddRangeAsync(categories);
            await _context.SaveChangesAsync();
        }
    }
}
