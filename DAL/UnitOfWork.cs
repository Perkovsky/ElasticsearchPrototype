using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DAL
{
	public class UnitOfWork : DbContext, IUnitOfWork
	{
		private readonly IUnitOfWorkSoundexInMemory _soundex;

		public DbSet<Building> Buildings { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<Unit> Units { get; set; }
		public DbSet<Settings> Settings { get; set; }

		//public DbSet<SoundexSynonym> SoundexSynonyms { get; set; }
		//public DbSet<SoundexStopWord> SoundexStopWords { get; set; }
		public IEnumerable<SoundexSynonym> SoundexSynonyms => _soundex?.SoundexSynonyms;
		public IEnumerable<SoundexStopWord> SoundexStopWords => _soundex?.SoundexStopWords;

		public UnitOfWork(DbContextOptions<UnitOfWork> options)
			: base(options)
		{
			//Database.EnsureCreated();
			_soundex = new UnitOfWorkSoundexInMemory();
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			foreach (var entity in builder.Model.GetEntityTypes())
			{
				entity.SetTableName(entity.ClrType.Name);
			}
		}
	}
}
