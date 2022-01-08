using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Endereco>()
               .HasOne(endereco => endereco.Cinema)
               .WithOne(cinema => cinema.Endereco)
               .HasForeignKey<Cinema>(cinema => cinema.EnderecoId);

            mb.Entity<Cinema>()
                .HasOne(cinema => cinema.Gerente)
                .WithMany(gerente => gerente.Cinemas)
                .HasForeignKey(cinema => cinema.GerenteId)
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Sessao>()
                .HasOne(sessao => sessao.Cinema)
                .WithMany(cinema => cinema.Sessoes)
                .HasForeignKey(sessao => sessao.CinemaId);

            mb.Entity<Sessao>()
                .HasOne(sessao => sessao.Filme) // the Sessao contains a Filme
                .WithMany(filme => filme.Sessoes) // this Filme is watch on many Sessoes
                .HasForeignKey(sessao => sessao.FilmeId); // relations with FilmeId
               
        }

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Gerente> Gerentes { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }

    }
}
