using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
	public interface IUnitOfWork : IDisposable, IUnitOfWorkSoundexInMemory
	{
		DbSet<Building> Buildings { get; set; }
		DbSet<Company> Companies { get; set; }
		DbSet<Unit> Units { get; set; }
		DbSet<Settings> Settings { get; set; }

		int SaveChanges();
	}
}
