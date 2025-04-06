using EmploiApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace EmploiApp.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Candidat> Candidats { get; set; }
    public DbSet<CV> CVs { get; set; }
    public DbSet<Diplome> Diplomes { get; set; }
    public DbSet<DiplomeCV> DiplomeCVs { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Competence> Competences { get; set; }
    public DbSet<CompetenceCV> CompetenceCVs { get; set; }
    public DbSet<Langue> Langues { get; set; }
    public DbSet<LangueCV> LangueCVs { get; set; }
    public DbSet<Secteur> Secteurs { get; set; }
    public DbSet<OffreEmploi> OffresEmploi { get; set; }
    public DbSet<Recruteur> Recruteurs { get; set; }
    public DbSet<Candidature> Candidatures { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Renommer les tables
        builder.Entity<ApplicationUser>().ToTable("Utilisateurs"); // Renomme AspNetUsers
        builder.Entity<IdentityRole>().ToTable("Roles"); 
        builder.Entity<IdentityUserRole<string>>().ToTable("UtilisateursRoles"); 
        builder.Entity<IdentityUserClaim<string>>().ToTable("UtilisateursClaims"); 
        builder.Entity<IdentityUserLogin<string>>().ToTable("UtilisateursLogins"); 
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RolesClaims"); 
        builder.Entity<IdentityUserToken<string>>().ToTable("UtilisateursTokens");

        // Configurer la relation one-to-one entre ApplicationUser et Candidat
        builder.Entity<ApplicationUser>()
            .HasOne(u => u.Candidat)
            .WithOne(c => c.Utilisateur)
            .HasForeignKey<Candidat>(c => c.UtilisateurId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configurer la relation one-to-one entre ApplicationUser et Recruteur
        builder.Entity<ApplicationUser>()
            .HasOne(u => u.Recruteur)
            .WithOne(r => r.Utilisateur)
            .HasForeignKey<Recruteur>(r => r.UtilisateurId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relation 1:n entre Candidat et CV
        builder.Entity<CV>()
       .HasOne(cv => cv.Candidat)
       .WithMany(cand => cand.CVs)
       .HasForeignKey(cv => cv.CandidatId)
       .OnDelete(DeleteBehavior.Restrict);

        // Relation 1:N entre CV et Experience
        builder.Entity<Experience>()
            .HasOne(e => e.CV)
            .WithMany(c => c.Experiences)
            .HasForeignKey(e => e.CVId);

        /// --- Relation Diplome - CV via Diplome_CV (N:N) ---
        builder.Entity<DiplomeCV>()
               .HasKey(dc => new { dc.DiplomeId, dc.CVId });
        builder.Entity<DiplomeCV>()
               .HasOne(dc => dc.Diplome)
               .WithMany(d => d.DiplomeCVs)
               .HasForeignKey(dc => dc.DiplomeId);
        builder.Entity<DiplomeCV>()
               .HasOne(dc => dc.CV)
               .WithMany(c => c.DiplomeCVs)
               .HasForeignKey(dc => dc.CVId);

        // --- Relation Competence - CV via Competence_CV (N:N) ---
        builder.Entity<CompetenceCV>()
               .HasKey(cc => new { cc.CompetenceId, cc.CVId });
        builder.Entity<CompetenceCV>()
               .HasOne(cc => cc.Competence)
               .WithMany(c => c.CompetenceCVs)
               .HasForeignKey(cc => cc.CompetenceId);
        builder.Entity<CompetenceCV>()
               .HasOne(cc => cc.CV)
               .WithMany(c => c.CompetenceCVs)
               .HasForeignKey(cc => cc.CVId);

        // --- Relation Langue - CV via Langue_CV (N:N) ---
        builder.Entity<LangueCV>()
               .HasKey(lc => new { lc.LangueId, lc.CVId });
        builder.Entity<LangueCV>()
               .HasOne(lc => lc.Langue)
               .WithMany(l => l.LangueCVs)
               .HasForeignKey(lc => lc.LangueId);
        builder.Entity<LangueCV>()
               .HasOne(lc => lc.CV)
               .WithMany(c => c.LangueCVs)
               .HasForeignKey(lc => lc.CVId);

        // --- Relation OffreEmploi - Secteur (N:1) ---
        builder.Entity<OffreEmploi>()
               .HasOne(o => o.Secteur)
               .WithMany(s => s.Offres)
               .HasForeignKey(o => o.SecteurId)
               .OnDelete(DeleteBehavior.Restrict);
        // --- Relation OffreEmploi - Recruter (N:1) ---
        builder.Entity<OffreEmploi>()
               .HasOne(o => o.Recruteur)
               .WithMany(r => r.Offres)
               .HasForeignKey(o => o.RecruteurId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Candidature>()
           .HasKey(c => new { c.IdOffre, c.IdCV }); // Définition de la clé composite
        builder.Entity<Candidature>()
            .HasOne(c => c.OffreEmploi)
            .WithMany(o => o.Candidatures)
            .HasForeignKey(c => c.IdOffre)
            .OnDelete(DeleteBehavior.Restrict);
        // .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Candidature>()
            .HasOne(c => c.CV)
            .WithMany(cv => cv.Candidatures)
            .HasForeignKey(c => c.IdCV)
            .OnDelete(DeleteBehavior.Restrict);
        //.OnDelete(DeleteBehavior.Cascade);


    }
}
