using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace JobSeekerSystem.Models;

#nullable disable warnings

public class JobSeeker_DB : DbContext // this class is used for create a database table 
{
    public JobSeeker_DB(DbContextOptions options) : base(options) { }

    // ================================JobSeeker=================================
    public DbSet<User> Users { get; set; }
    public DbSet<JobSeeker> JobSeekers { get; set; }
    public DbSet<Classification> Classifications { get; set; }
    public DbSet<SubClassification> SubClassifications { get; set; }
    public DbSet<JobSeekerExperience> JobSeekerExperiences { get; set; }
    public DbSet<Resume> Resumes { get; set; }
    public DbSet<Resume_Skill> Resumes_Skills { get; set; }
    public DbSet<Skill> Skills { get; set; }
    //===========================================================================



}

// ============================ JobSeeker =================================

public class User // [Required] enforced to stored but I don't know should I use it or not
{
    // Column
    [Key, MaxLength(6)]
    [Required] // PK
    public string Id { get; set; }

    [MaxLength(100)]
    [Required]
    public string Username { get; set; }

    [MaxLength(12)]
    [Required]
    public string Password_hash { get; set; }

    [MaxLength(100)]
    [Required]
    public string Email { get; set; }

    [MaxLength(100)]
    [Required]
    public string Role { get; set; }

    [Required]
    public DateTime Create_at { get; set; } // follow the real world time 

    [Required]
    public DateTime Update_at { get; set; }
    // Navigation
    public JobSeeker JobSeeker { get; set; } // one to one relationship with JobSeeker
}

public class JobSeeker
{   
    // Column
    [Key, MaxLength(6)]
    [Required]
    //PK
    public string Id { get; set; }

    [MaxLength(100)]
    [Required]
    public string First_name { get; set; }

    [MaxLength(100)]
    [Required]
    public string Last_name { get; set; }

    [MaxLength(100)]
    [Required]
    public string Location { get; set; }

    [MaxLength(100)]
    [Required]
    public string Has_experience { get; set; }
    // FK
    [Required]
    public string UserId { get; set; }

    public User User { get; set; } // one to one relationship with User
    public List<JobSeekerExperience> JobSeekerExperiences { get; set; } = []; // one to many relationship with JobSeekerExperience
    public List<Resume> Resumes { get; set; } = []; // one to many relationship with Resume
    public List<Classification> Classifications { get; set; } = []; // one to many relationship with Classification
    public List<SubClassification> SubClassifications { get; set; } = []; // one to many relationship with SubClassification
   
}

public class JobSeekerExperience
{
    [Key, MaxLength(6)] // PK
    [Required]
    public string Id { get; set; }

    [MaxLength(100)]
    [Required]
    public string Job_title { get; set; }

    [MaxLength(100)]
    [Required]
    public string Company_name { get; set; }

    
    [Required]
    public int Start_year { get; set; }


    [Required]
    public int Start_month { get; set; }

    [MaxLength(1)]
    public int End_year { get; set; } // can be null

    [MaxLength(1)]
    public int End_month { get; set; } // can be null

    //FK
    [Required]
    public int JobSeekerId { get; set; }
    public JobSeeker JobSeeker { get; set; } // one to many relationship with JobSeeker
}

public class Classification
{
    [Key, MaxLength(6)] // PK
    [Required]
    public string Id { get; set; }

    [MaxLength(100)] 
    [Required]
    public string Name { get; set; }

    //FK
    [Required]
    public string JobSeekerId { get; set; }
    [Required]
    public string SubClassificationId { get; set; } // FK to SubClassification

    public JobSeeker Jobseeker { get; set; }
    public SubClassification SubClassification { get; set; } // one to many relationship with SubClassification
}

public class SubClassification
{
    [Key, MaxLength(6)] // PK
    [Required]
    public string Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; }

    // FK
    public string JobSeekerId { get; set; }
    public JobSeeker JobSeeker { get; set; } // one to many relationship with JobSeeker
    public List<Classification> Classifications { get; set; } = []; // one to many relationship with Classification
}

public class Resume
{
    [Key, MaxLength(6)] // PK
    [Required]
    public string Id { get; set; }

    [MaxLength(255)] // limited the image length
    [Required]
    public string Image_url { get; set; }

    [Required] // FK
    public string JobSeekerId { get; set; }
    public JobSeeker JobSeeker { get; set; } // one to many relationship with JobSeeker
    public List<Resume_Skill> Resume_Skills { get; set; } = []; // one to many relationship with Resume_Skill
}

public class Resume_Skill
{
    [Required] // FK
    public string ResumeId { get; set; }
    [Required] // FK
    public string SkillId { get; set; }
    public Resume Resume { get; set; } // one to many relationship with Resume
    public Skill Skill { get; set; } // one to many relationship with Skill
}
public class Skill
{
    [Key,MaxLength(6)]
    [Required]
    public string Id { get; set; }

    [MaxLength(100)]
    [Required]
    public string Name { get; set; }
    public List<Resume_Skill> Resume_Skills { get; set; } = []; // one to many relationship with Resume_Skill
    }
//===============================================================




