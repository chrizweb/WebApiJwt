using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApiJwt.Models;

public partial class DbJwtContext : DbContext {
	public DbJwtContext() {
	}

	public DbJwtContext(DbContextOptions<DbJwtContext> options)
			: base(options) {
	}

	public virtual DbSet<Producto> Productos { get; set; }

	public virtual DbSet<Usuario> Usuarios { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.Entity<Producto>(entity => {
			entity.HasKey(e => e.IdProducto).HasName("PK__Producto__09889210173AA95B");

			entity.ToTable("Producto");

			entity.Property(e => e.Marca)
					.HasMaxLength(50)
					.IsUnicode(false);
			entity.Property(e => e.Nombre)
					.HasMaxLength(50)
					.IsUnicode(false);
			entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
		});

		modelBuilder.Entity<Usuario>(entity => {
			entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97B09CCB47");

			entity.ToTable("Usuario");

			entity.Property(e => e.Clave)
					.HasMaxLength(100)
					.IsUnicode(false);
			entity.Property(e => e.Correo)
					.HasMaxLength(50)
					.IsUnicode(false);
			entity.Property(e => e.Nombre)
					.HasMaxLength(50)
					.IsUnicode(false);
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
