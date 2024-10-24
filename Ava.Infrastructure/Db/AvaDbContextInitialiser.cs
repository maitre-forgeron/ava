using Ava.Domain.Models.Category;
using Ava.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace Ava.Infrastructure.Db;

public class AvaDbContextInitialiser
{
    private readonly ILogger<AvaDbContext> _logger;
    private readonly AvaDbContext _context;
    private readonly IServiceProvider _serviceProvider;
    private readonly UserManager<User> _userManager;

    public AvaDbContextInitialiser(ILogger<AvaDbContext> logger, AvaDbContext context, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _context = context;
        _serviceProvider = serviceProvider;
        _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
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

    private async Task TrySeedAsync()
    {
        if (_context.Database.CanConnect())
        {
            SeedUsers(_context);
            SeedRoles(_context);
            CreateCategories(_context);

            await _context.SaveChangesAsync();
        }
    }

    private void CreateCategories(AvaDbContext context)
    {
        if (!context.Categories.Any())
        {
            var categories = new List<Category>();

            var generalTherapyCategory = new Category { Id = Guid.NewGuid(), Name = "General Therapy and Counseling" };
            generalTherapyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Individual Therapy" });
            generalTherapyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Couples Therapy" });
            generalTherapyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Family Therapy" });
            generalTherapyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Group Therapy" });
            generalTherapyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Stress Management" });
            generalTherapyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Relationship Counseling" });
            categories.Add(generalTherapyCategory);

            var mentalHealthCategory = new Category { Id = Guid.NewGuid(), Name = "Mental Health Disorders" };
            mentalHealthCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Anxiety Disorders" });
            mentalHealthCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Depression" });
            mentalHealthCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Bipolar Disorder" });
            mentalHealthCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Obsessive Compulsive Disorder (OCD)" });
            mentalHealthCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Post Traumatic Stress Disorder (PTSD)" });
            mentalHealthCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Schizophrenia" });
            mentalHealthCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Panic Disorders" });
            categories.Add(mentalHealthCategory);

            var behavioralTherapyCategory = new Category { Id = Guid.NewGuid(), Name = "Behavioral Therapy" };
            behavioralTherapyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Cognitive Behavioral Therapy (CBT)" });
            behavioralTherapyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Dialectical Behavior Therapy (DBT)" });
            behavioralTherapyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Acceptance and Commitment Therapy (ACT)" });
            categories.Add(behavioralTherapyCategory);

            var psychiatricServicesCategory = new Category { Id = Guid.NewGuid(), Name = "Psychiatric Services" };
            psychiatricServicesCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Medication Management" });
            psychiatricServicesCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Psychiatric Assessment" });
            psychiatricServicesCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Medication Consultation" });
            categories.Add(psychiatricServicesCategory);

            var lifeChallengesCategory = new Category { Id = Guid.NewGuid(), Name = "Life Challenges and Personal Development" };
            lifeChallengesCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Life Transitions (e.g., career changes, moving)" });
            lifeChallengesCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Grief and Loss Counseling" });
            lifeChallengesCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Building Self-Esteem" });
            lifeChallengesCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Personal Growth" });
            lifeChallengesCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Confidence Training" });
            categories.Add(lifeChallengesCategory);

            var childrenAdultsFamilyCategory = new Category { Id = Guid.NewGuid(), Name = "Children, Adults and Family" };
            childrenAdultsFamilyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Child Counseling" });
            childrenAdultsFamilyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Adolescent Therapy" });
            childrenAdultsFamilyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Parental Support" });
            childrenAdultsFamilyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "School Related Issues" });
            childrenAdultsFamilyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Behavioral Problems in Children" });
            categories.Add(childrenAdultsFamilyCategory);

            var couplesRelationshipsCategory = new Category { Id = Guid.NewGuid(), Name = "Couples and Relationships" };
            couplesRelationshipsCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Premarital Counseling" });
            couplesRelationshipsCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Divorce and Separation Counseling" });
            couplesRelationshipsCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Infidelity Counseling" });
            couplesRelationshipsCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Communication Issues" });
            couplesRelationshipsCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Conflict Resolution" });
            categories.Add(couplesRelationshipsCategory);

            var workCareerCategory = new Category { Id = Guid.NewGuid(), Name = "Work and Career" };
            workCareerCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Work Stress" });
            workCareerCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Career Counseling" });
            workCareerCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Prevention of Burns" });
            workCareerCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Work-Life Balance" });
            categories.Add(workCareerCategory);

            var recoveryTraumaAbuseCategory = new Category { Id = Guid.NewGuid(), Name = "Recovery from Trauma and Abuse" };
            recoveryTraumaAbuseCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Trauma Therapy" });
            recoveryTraumaAbuseCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Sexual Violence Recovery" });
            recoveryTraumaAbuseCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Counseling Against Domestic Violence" });
            categories.Add(recoveryTraumaAbuseCategory);

            var drugAbuseAddictionCategory = new Category { Id = Guid.NewGuid(), Name = "Drug Abuse and Addiction" };
            drugAbuseAddictionCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Alcohol Dependence" });
            drugAbuseAddictionCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Drug Addiction" });
            drugAbuseAddictionCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Smoking Cessation" });
            drugAbuseAddictionCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Addiction to Gambling" });
            drugAbuseAddictionCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Internet Addiction" });
            categories.Add(drugAbuseAddictionCategory);

            var eatingDisordersBodyImageCategory = new Category { Id = Guid.NewGuid(), Name = "Eating Disorders and Body Image" };
            eatingDisordersBodyImageCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Anorexia Nervosa" });
            eatingDisordersBodyImageCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Bulimia Nervosa" });
            eatingDisordersBodyImageCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Binge Eating Disorder" });
            eatingDisordersBodyImageCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Body Dysmorphic Disorder" });
            categories.Add(eatingDisordersBodyImageCategory);

            var managingStressAnxietyCategory = new Category { Id = Guid.NewGuid(), Name = "Managing Stress, Anxiety and Depression" };
            managingStressAnxietyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Mindfulness-Based Stress Reduction" });
            managingStressAnxietyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Relaxation Techniques" });
            managingStressAnxietyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Guided Meditations" });
            managingStressAnxietyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Stress Reduction Coaching" });
            categories.Add(managingStressAnxietyCategory);

            var neurodiversityCategory = new Category { Id = Guid.NewGuid(), Name = "Neurodiversity" };
            neurodiversityCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Autism Spectrum Disorder (ASD) Support" });
            neurodiversityCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Management of Attention Deficit Hyperactivity Disorder (ADHD)" });
            neurodiversityCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Dyslexia Counseling" });
            categories.Add(neurodiversityCategory);

            var sleepDisturbanceCategory = new Category { Id = Guid.NewGuid(), Name = "Sleep Disturbance" };
            sleepDisturbanceCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Treatment of Insomnia" });
            sleepDisturbanceCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Support for Sleep Apnea" });
            sleepDisturbanceCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Sleep Hygiene Consultation" });
            categories.Add(sleepDisturbanceCategory);

            var sexualHealthGenderIdentityCategory = new Category { Id = Guid.NewGuid(), Name = "Sexual Health and Gender Identity" };
            sexualHealthGenderIdentityCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Sexual Dysfunction" });
            sexualHealthGenderIdentityCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "LGBTQ+ Affirmative Therapy" });
            sexualHealthGenderIdentityCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Gender Identity Support" });
            sexualHealthGenderIdentityCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Sexual Counseling" });
            categories.Add(sexualHealthGenderIdentityCategory);

            var chronicIllnessPainManagementCategory = new Category { Id = Guid.NewGuid(), Name = "Chronic Illness and Pain Management" };
            chronicIllnessPainManagementCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Coping with Chronic Illness" });
            chronicIllnessPainManagementCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Chronic Pain Management" });
            chronicIllnessPainManagementCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Disability Support" });
            categories.Add(chronicIllnessPainManagementCategory);

            var mindfulnessWellBeingCategory = new Category { Id = Guid.NewGuid(), Name = "Mindfulness and Well-Being" };
            mindfulnessWellBeingCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Mindfulness Coaching" });
            mindfulnessWellBeingCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Meditation and Relaxation Techniques" });
            mindfulnessWellBeingCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Positive Psychology and Well-Being" });
            categories.Add(mindfulnessWellBeingCategory);

            var phobiaTreatmentCategory = new Category { Id = Guid.NewGuid(), Name = "Phobia Treatment" };
            phobiaTreatmentCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Social Phobia" });
            phobiaTreatmentCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Agoraphobia" });
            phobiaTreatmentCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Specific Phobias (e.g., fear of flying, heights)" });
            categories.Add(phobiaTreatmentCategory);

            var angerManagementCategory = new Category { Id = Guid.NewGuid(), Name = "Anger Management" };
            angerManagementCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Anger Control Therapy" });
            angerManagementCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Conflict Resolution Skills" });
            categories.Add(angerManagementCategory);

            var specializedTherapyCategory = new Category { Id = Guid.NewGuid(), Name = "Specialized Therapy" };
            specializedTherapyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Art Therapy" });
            specializedTherapyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Music Therapy" });
            specializedTherapyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Play Therapy for Children" });
            specializedTherapyCategory.AddSubCategory(new Category { Id = Guid.NewGuid(), Name = "Animal-Assisted Therapy" });
            categories.Add(specializedTherapyCategory);

            context.AddRange(categories);
        }
    }
    private void SeedRoles(AvaDbContext context)
    {
        if (!context.Roles.Any())
        {
            var roles = new List<IdentityRole>
            {
                new IdentityRole("admin"),
                new IdentityRole("customer"),
                new IdentityRole("therapist")
            };

            context.Roles.AddRange(roles);
        }
    }
    private void SeedUsers(AvaDbContext context)
    {
        if (!context.Users.Any())
        {
            var admin = new User
            {
                UserName = "admin",
                Email = "admin@ava.ge",
            };

            var adminRole = context.Roles.FirstOrDefault(x => x.Name == "admin");

            if (adminRole is not null)
            {
                context.UserRoles.Add(new IdentityUserRole<string> { UserId = admin.Id, RoleId = adminRole.Id });
            }

            var result = _userManager.CreateAsync(admin, "admin1234").Result;
        }
    }

}
