# Schema2FM

This is an utility that generates a FluentMigrator migration from an existing database table.

Usage:

	schema2fm.exe connectionString schema table

For example:

	schema2fm.exe "Server=.\SQLEXPRESS;Database=LearnORM;Trusted_Connection=True;" dbo Book
