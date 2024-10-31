using Ava.Domain.Models.Category;

namespace Ava.Infrastructure.Db.DataProviders;

internal static class CategoriesProvider
{
    internal static IEnumerable<Category> GetCategories()
    {
        var generalTherapyCategory = new Category(Guid.NewGuid(), "General Therapy and Counseling");
        generalTherapyCategory.AddSubCategory(new Category(Guid.NewGuid(), "Individual Therapy"));
        generalTherapyCategory.AddSubCategory(new Category(Guid.NewGuid(), "Couples Therapy"));
        generalTherapyCategory.AddSubCategory(new Category(Guid.NewGuid(), "Family Therapy"));
        generalTherapyCategory.AddSubCategory(new Category(Guid.NewGuid(), "Group Therapy"));
        generalTherapyCategory.AddSubCategory(new Category(Guid.NewGuid(), "Stress Management"));
        generalTherapyCategory.AddSubCategory(new Category(Guid.NewGuid(), "Relationship Counseling"));
        yield return generalTherapyCategory;

        var mentalHealthCategory = new Category(Guid.NewGuid(), "Mental Health Disorders");
        mentalHealthCategory.AddSubCategory(new Category(Guid.NewGuid(), "Anxiety Disorders"));
        mentalHealthCategory.AddSubCategory(new Category(Guid.NewGuid(), "Depression"));
        mentalHealthCategory.AddSubCategory(new Category(Guid.NewGuid(), "Bipolar Disorder"));
        mentalHealthCategory.AddSubCategory(new Category(Guid.NewGuid(), "Obsessive Compulsive Disorder (OCD)"));
        mentalHealthCategory.AddSubCategory(new Category(Guid.NewGuid(), "Post Traumatic Stress Disorder (PTSD)"));
        mentalHealthCategory.AddSubCategory(new Category(Guid.NewGuid(), "Schizophrenia"));
        mentalHealthCategory.AddSubCategory(new Category(Guid.NewGuid(), "Panic Disorders"));
        yield return mentalHealthCategory;

        var behavioralTherapyCategory = new Category(Guid.NewGuid(), "Behavioral Therapy");
        behavioralTherapyCategory.AddSubCategory(new Category(Guid.NewGuid(), "Cognitive Behavioral Therapy (CBT)"));
        behavioralTherapyCategory.AddSubCategory(new Category(Guid.NewGuid(), "Dialectical Behavior Therapy (DBT)"));
        behavioralTherapyCategory.AddSubCategory(new Category(Guid.NewGuid(), "Acceptance and Commitment Therapy (ACT)"));
        yield return behavioralTherapyCategory;

        var psychiatricServicesCategory = new Category(Guid.NewGuid(), "Psychiatric Services");
        psychiatricServicesCategory.AddSubCategory(new Category(Guid.NewGuid(), "Medication Management"));
        psychiatricServicesCategory.AddSubCategory(new Category(Guid.NewGuid(), "Psychiatric Assessment"));
        psychiatricServicesCategory.AddSubCategory(new Category(Guid.NewGuid(), "Medication Consultation"));
        yield return psychiatricServicesCategory;

        var lifeChallengesCategory = new Category(Guid.NewGuid(), "Life Challenges and Personal Development");
        lifeChallengesCategory.AddSubCategory(new Category(Guid.NewGuid(), "Life Transitions (e.g., career changes, moving)"));
        lifeChallengesCategory.AddSubCategory(new Category(Guid.NewGuid(), "Grief and Loss Counseling"));
        lifeChallengesCategory.AddSubCategory(new Category(Guid.NewGuid(), "Building Self-Esteem"));
        lifeChallengesCategory.AddSubCategory(new Category(Guid.NewGuid(), "Personal Growth"));
        lifeChallengesCategory.AddSubCategory(new Category(Guid.NewGuid(), "Confidence Training"));
        yield return lifeChallengesCategory;

        var childrenAdultsFamilyCategory = new Category(Guid.NewGuid(), "Children, Adults and Family");
        childrenAdultsFamilyCategory.AddSubCategory(new Category(Guid.NewGuid(), "Child Counseling"));
        childrenAdultsFamilyCategory.AddSubCategory(new Category(Guid.NewGuid(), "Adolescent Therapy"));
        childrenAdultsFamilyCategory.AddSubCategory(new Category(Guid.NewGuid(), "Parental Support"));
        childrenAdultsFamilyCategory.AddSubCategory(new Category(Guid.NewGuid(), "School Related Issues"));
        childrenAdultsFamilyCategory.AddSubCategory(new Category(Guid.NewGuid(), "Behavioral Problems in Children"));
        yield return childrenAdultsFamilyCategory;

        var couplesRelationshipsCategory = new Category(Guid.NewGuid(), "Couples and Relationships");
        couplesRelationshipsCategory.AddSubCategory(new Category(Guid.NewGuid(), "Premarital Counseling"));
        couplesRelationshipsCategory.AddSubCategory(new Category(Guid.NewGuid(), "Divorce and Separation Counseling"));
        couplesRelationshipsCategory.AddSubCategory(new Category(Guid.NewGuid(), "Infidelity Counseling"));
        couplesRelationshipsCategory.AddSubCategory(new Category(Guid.NewGuid(), "Communication Issues"));
        couplesRelationshipsCategory.AddSubCategory(new Category(Guid.NewGuid(), "Conflict Resolution"));
        yield return couplesRelationshipsCategory;

        var workCareerCategory = new Category(Guid.NewGuid(), "Work and Career");
        workCareerCategory.AddSubCategory(new Category(Guid.NewGuid(), "Work Stress"));
        workCareerCategory.AddSubCategory(new Category(Guid.NewGuid(), "Career Counseling"));
        workCareerCategory.AddSubCategory(new Category(Guid.NewGuid(), "Prevention of Burns"));
        workCareerCategory.AddSubCategory(new Category(Guid.NewGuid(), "Work-Life Balance"));
        yield return workCareerCategory;

        var recoveryTraumaAbuseCategory = new Category(Guid.NewGuid(), "Recovery from Trauma and Abuse");
        recoveryTraumaAbuseCategory.AddSubCategory(new Category(Guid.NewGuid(), "Trauma Therapy"));
        recoveryTraumaAbuseCategory.AddSubCategory(new Category(Guid.NewGuid(), "Sexual Violence Recovery"));
        recoveryTraumaAbuseCategory.AddSubCategory(new Category(Guid.NewGuid(), "Counseling Against Domestic Violence"));
        yield return recoveryTraumaAbuseCategory;

        var drugAbuseAddictionCategory = new Category(Guid.NewGuid(), "Drug Abuse and Addiction");
        drugAbuseAddictionCategory.AddSubCategory(new Category(Guid.NewGuid(), "Alcohol Dependence"));
        drugAbuseAddictionCategory.AddSubCategory(new Category(Guid.NewGuid(), "Drug Addiction"));
        drugAbuseAddictionCategory.AddSubCategory(new Category(Guid.NewGuid(), "Smoking Cessation"));
        drugAbuseAddictionCategory.AddSubCategory(new Category(Guid.NewGuid(), "Addiction to Gambling"));
        drugAbuseAddictionCategory.AddSubCategory(new Category(Guid.NewGuid(), "Internet Addiction"));
        yield return drugAbuseAddictionCategory;

        var eatingDisordersBodyImageCategory = new Category(Guid.NewGuid(), "Eating Disorders and Body Image");
        eatingDisordersBodyImageCategory.AddSubCategory(new Category(Guid.NewGuid(), "Anorexia Nervosa"));
        eatingDisordersBodyImageCategory.AddSubCategory(new Category(Guid.NewGuid(), "Bulimia Nervosa"));
        eatingDisordersBodyImageCategory.AddSubCategory(new Category(Guid.NewGuid(), "Binge Eating Disorder"));
        eatingDisordersBodyImageCategory.AddSubCategory(new Category(Guid.NewGuid(), "Body Dysmorphic Disorder"));
        yield return eatingDisordersBodyImageCategory;

        var managingStressAnxietyCategory = new Category(Guid.NewGuid(), "Managing Stress, Anxiety and Depression");
        managingStressAnxietyCategory.AddSubCategory(new Category(Guid.NewGuid(), "Mindfulness-Based Stress Reduction"));
        managingStressAnxietyCategory.AddSubCategory(new Category(Guid.NewGuid(), "Relaxation Techniques"));
        managingStressAnxietyCategory.AddSubCategory(new Category(Guid.NewGuid(), "Guided Meditations"));
        managingStressAnxietyCategory.AddSubCategory(new Category(Guid.NewGuid(), "Stress Reduction Coaching"));
        yield return managingStressAnxietyCategory;

        var neurodiversityCategory = new Category(Guid.NewGuid(), "Neurodiversity");
        neurodiversityCategory.AddSubCategory(new Category(Guid.NewGuid(), "Autism Spectrum Disorder (ASD) Support"));
        neurodiversityCategory.AddSubCategory(new Category(Guid.NewGuid(), "Management of Attention Deficit Hyperactivity Disorder (ADHD)"));
        neurodiversityCategory.AddSubCategory(new Category(Guid.NewGuid(), "Dyslexia Counseling"));
        yield return neurodiversityCategory;

        var sleepDisturbanceCategory = new Category(Guid.NewGuid(), "Sleep Disturbance");
        sleepDisturbanceCategory.AddSubCategory(new Category(Guid.NewGuid(), "Treatment of Insomnia"));
        sleepDisturbanceCategory.AddSubCategory(new Category(Guid.NewGuid(), "Support for Sleep Apnea"));
        sleepDisturbanceCategory.AddSubCategory(new Category(Guid.NewGuid(), "Sleep Hygiene Consultation"));
        yield return sleepDisturbanceCategory;

        var sexualHealthGenderIdentityCategory = new Category(Guid.NewGuid(), "Sexual Health and Gender Identity");
        sexualHealthGenderIdentityCategory.AddSubCategory(new Category(Guid.NewGuid(), "Sexual Dysfunction"));
        sexualHealthGenderIdentityCategory.AddSubCategory(new Category(Guid.NewGuid(), "LGBTQ+ Affirmative Therapy"));
        sexualHealthGenderIdentityCategory.AddSubCategory(new Category(Guid.NewGuid(), "Gender Identity Support"));
        sexualHealthGenderIdentityCategory.AddSubCategory(new Category(Guid.NewGuid(), "Sexual Counseling"));
        yield return sexualHealthGenderIdentityCategory;

        var chronicIllnessPainManagementCategory = new Category(Guid.NewGuid(), "Chronic Illness and Pain Management");
        chronicIllnessPainManagementCategory.AddSubCategory(new Category(Guid.NewGuid(), "Coping with Chronic Illness"));
        chronicIllnessPainManagementCategory.AddSubCategory(new Category(Guid.NewGuid(), "Chronic Pain Management"));
        chronicIllnessPainManagementCategory.AddSubCategory(new Category(Guid.NewGuid(), "Disability Support"));
        yield return chronicIllnessPainManagementCategory;

        var mindfulnessWellBeingCategory = new Category(Guid.NewGuid(), "Mindfulness and Well-Being");
        mindfulnessWellBeingCategory.AddSubCategory(new Category(Guid.NewGuid(), "Mindfulness Coaching"));
        mindfulnessWellBeingCategory.AddSubCategory(new Category(Guid.NewGuid(), "Meditation and Relaxation Techniques"));
        mindfulnessWellBeingCategory.AddSubCategory(new Category(Guid.NewGuid(), "Positive Psychology and Well-Being"));
        yield return mindfulnessWellBeingCategory;

        var phobiaTreatmentCategory = new Category(Guid.NewGuid(), "Phobia Treatment");
        phobiaTreatmentCategory.AddSubCategory(new Category(Guid.NewGuid(), "Social Phobia"));
        phobiaTreatmentCategory.AddSubCategory(new Category(Guid.NewGuid(), "Agoraphobia"));
        phobiaTreatmentCategory.AddSubCategory(new Category(Guid.NewGuid(), "Specific Phobias (e.g., fear of flying, heights)"));
        yield return phobiaTreatmentCategory;

        var angerManagementCategory = new Category(Guid.NewGuid(), "Anger Management");
        angerManagementCategory.AddSubCategory(new Category(Guid.NewGuid(), "Anger Control Therapy"));
        angerManagementCategory.AddSubCategory(new Category(Guid.NewGuid(), "Conflict Resolution Skills"));
        yield return angerManagementCategory;

        var specializedTherapyCategory = new Category(Guid.NewGuid(), "Specialized Therapy");
        specializedTherapyCategory.AddSubCategory(new Category(Guid.NewGuid(), "Art Therapy"));
        specializedTherapyCategory.AddSubCategory(new Category(Guid.NewGuid(), "Music Therapy"));
        specializedTherapyCategory.AddSubCategory(new Category(Guid.NewGuid(), "Play Therapy for Children"));
        specializedTherapyCategory.AddSubCategory(new Category(Guid.NewGuid(), "Animal-Assisted Therapy"));
        yield return specializedTherapyCategory;
    }
}
