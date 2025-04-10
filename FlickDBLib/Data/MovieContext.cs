﻿namespace FlickDBLib.Data;

public partial class MovieContext : DbContext
{
    private IDbConnection _database;

    public MovieContext(IDbConnection database)
    {
        _database = database;
    }

    public virtual DbSet<Actor>? Actors { get; set; }

    public virtual DbSet<Crew>? Crews { get; set; }

    public virtual DbSet<Genre>? Genres { get; set; }

    public virtual DbSet<Movie>? Movies { get; set; }

    public virtual DbSet<MovieActor>? Moviesactors { get; set; }

    public virtual DbSet<MovieCrew>? Moviescrews { get; set; }

    public virtual DbSet<MovieGenre>? Moviesgenres { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(_database.GetConnectionString(), new MySqlServerVersion(new Version(8, 0, 19))).LogTo(Console.WriteLine, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.Actorid).HasName("PRIMARY");

            entity.ToTable("actors");

            entity.Property(e => e.Actorid).HasColumnName("actorid");
            entity.Property(e => e.Firstname)
                .HasMaxLength(40)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(40)
                .HasColumnName("lastname");
            entity.Property(e => e.Birth).HasColumnName("birth");
            entity.Property(e => e.Biography)
                .HasMaxLength(600)
                .HasColumnName("biography");
            entity.Property(e => e.Picture)
                .HasMaxLength(50)
                .HasColumnName("picture");
        });

        modelBuilder.Entity<Crew>(entity =>
        {
            entity.HasKey(e => e.Crewid).HasName("PRIMARY");

            entity.ToTable("crews");

            entity.Property(e => e.Crewid).HasColumnName("crewid");
            entity.Property(e => e.Firstname)
                .HasMaxLength(40)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(40)
                .HasColumnName("lastname");
            entity.Property(e => e.Birth).HasColumnName("birth");
            entity.Property(e => e.Biography)
                .HasMaxLength(600)
                .HasColumnName("biography");
            entity.Property(e => e.Position)
                .HasMaxLength(40)
                .HasColumnName("position");
            entity.Property(e => e.Picture)
                .HasMaxLength(50)
                .HasColumnName("picture");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Genreid).HasName("PRIMARY");

            entity.ToTable("genres");

            entity.Property(e => e.Genreid).HasColumnName("genreid");
            entity.Property(e => e.Genre1)
                .HasMaxLength(40)
                .HasColumnName("genre");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Movieid).HasName("PRIMARY");

            entity.ToTable("movies");

            entity.Property(e => e.Movieid)
                .ValueGeneratedNever()
                .HasColumnName("movieid");
            entity.Property(e => e.Duration)
                .HasColumnType("time")
                .HasColumnName("duration");
            entity.Property(e => e.Format)
                .HasMaxLength(20)
                .HasColumnName("format");
            entity.Property(e => e.Poster)
                .HasMaxLength(50)
                .HasColumnName("poster");
            entity.Property(e => e.Rating)
                .HasMaxLength(40)
                .HasColumnName("rating");
            entity.Property(e => e.Release).HasColumnName("release");
            entity.Property(e => e.Symbol)
                .HasMaxLength(50)
                .HasColumnName("symbol");
            entity.Property(e => e.Title)
                .HasMaxLength(80)
                .HasColumnName("title");
            entity.Property(e => e.Story)
                .HasMaxLength(600)
                .HasColumnName("story");
        });

        modelBuilder.Entity<MovieActor>(entity =>
        {
            entity.HasKey(e => new { e.Movieactorid, e.Movieid, e.Actorid })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("moviesactors");

            entity.HasIndex(e => e.Actorid, "movie_actors_idx");

            entity.HasIndex(e => e.Movieid, "movie_movies_idx");

            entity.Property(e => e.Movieactorid)
                .ValueGeneratedOnAdd()
                .HasColumnName("movieactorid");
            entity.Property(e => e.Movieid).HasColumnName("movieid");
            entity.Property(e => e.Actorid).HasColumnName("actorid");
            entity.Property(e => e.Character)
                .HasMaxLength(40)
                .HasColumnName("character");

            entity.HasOne(d => d.Actor).WithMany(p => p.Moviesactors)
                .HasForeignKey(d => d.Actorid)
                .HasConstraintName("moviesactors_actors");

            entity.HasOne(d => d.Movie).WithMany(p => p.Moviesactors)
                .HasForeignKey(d => d.Movieid)
                .HasConstraintName("moviesactors_movies");
        });

        modelBuilder.Entity<MovieCrew>(entity =>
        {
            entity.HasKey(e => new { e.Moviecrewid, e.Movieid, e.Crewid })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("moviescrews");

            entity.HasIndex(e => e.Crewid, "movie_crews_idx");

            entity.HasIndex(e => e.Movieid, "movie_movies_idx");

            entity.Property(e => e.Moviecrewid)
                .ValueGeneratedOnAdd()
                .HasColumnName("moviecrewid");
            entity.Property(e => e.Movieid).HasColumnName("movieid");
            entity.Property(e => e.Crewid).HasColumnName("crewid");

            entity.HasOne(d => d.Crew).WithMany(p => p.Moviescrews)
                .HasForeignKey(d => d.Crewid)
                .HasConstraintName("moviescrews_crews");

            entity.HasOne(d => d.Movie).WithMany(p => p.Moviescrews)
                .HasForeignKey(d => d.Movieid)
                .HasConstraintName("moviescrews_movies");
        });

        modelBuilder.Entity<MovieGenre>(entity =>
        {
            entity.HasKey(e => new { e.Moviegenreid, e.Movieid, e.Genreid })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("moviesgenres");

            entity.HasIndex(e => e.Genreid, "movie_genres_idx");

            entity.HasIndex(e => e.Movieid, "movie_movies_idx");

            entity.Property(e => e.Moviegenreid)
                .ValueGeneratedOnAdd()
                .HasColumnName("moviegenreid");
            entity.Property(e => e.Movieid).HasColumnName("movieid");
            entity.Property(e => e.Genreid).HasColumnName("genreid");

            entity.HasOne(d => d.Genre).WithMany(p => p.Moviesgenres)
                .HasForeignKey(d => d.Genreid)
                .HasConstraintName("moviesgenres_genres");

            entity.HasOne(d => d.Movie).WithMany(p => p.Moviesgenres)
                .HasForeignKey(d => d.Movieid)
                .HasConstraintName("moviesgenres_movies");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
