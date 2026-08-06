IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260806211356_InitialContentSchema'
)
BEGIN
    CREATE TABLE [Pages] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(200) NOT NULL,
        [Slug] nvarchar(200) NOT NULL,
        [IsPublished] bit NOT NULL,
        [PublishedAt] datetime2 NULL,
        [CreatedAt] datetime2 NOT NULL,
        [CreatedBy] nvarchar(100) NULL,
        [LastModified] datetime2 NOT NULL,
        [ModifiedBy] nvarchar(100) NULL,
        [IsActive] bit NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_Pages] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260806211356_InitialContentSchema'
)
BEGIN
    CREATE TABLE [PageBlocks] (
        [Id] int NOT NULL IDENTITY,
        [PageId] int NOT NULL,
        [SortOrder] int NOT NULL,
        [BlockType] nvarchar(32) NOT NULL,
        [HeadingText] nvarchar(300) NULL,
        [HeadingLevel] int NULL,
        [ParagraphText] nvarchar(max) NULL,
        [LinkText] nvarchar(300) NULL,
        [LinkUrl] nvarchar(2048) NULL,
        [OpenInNewWindow] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [CreatedBy] nvarchar(100) NULL,
        [LastModified] datetime2 NOT NULL,
        [ModifiedBy] nvarchar(100) NULL,
        [IsActive] bit NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_PageBlocks] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PageBlocks_Pages_PageId] FOREIGN KEY ([PageId]) REFERENCES [Pages] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260806211356_InitialContentSchema'
)
BEGIN
    CREATE INDEX [IX_PageBlocks_PageId_SortOrder] ON [PageBlocks] ([PageId], [SortOrder]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260806211356_InitialContentSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Pages_Slug] ON [Pages] ([Slug]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260806211356_InitialContentSchema'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260806211356_InitialContentSchema', N'10.0.10');
END;

COMMIT;
GO
