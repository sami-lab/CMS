IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE TABLE [UserAddresses] (
        [id] int NOT NULL IDENTITY,
        [Country] nvarchar(max) NULL,
        [State] nvarchar(max) NULL,
        [City] nvarchar(max) NULL,
        [StreetAddress] nvarchar(max) NULL,
        CONSTRAINT [PK_UserAddresses] PRIMARY KEY ([id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE TABLE [UsersLocations] (
        [id] int NOT NULL IDENTITY,
        [IP] nvarchar(max) NULL,
        [Location] nvarchar(max) NULL,
        [Time] datetime2 NOT NULL,
        [Url] nvarchar(max) NULL,
        [Browser] nvarchar(max) NULL,
        CONSTRAINT [PK_UsersLocations] PRIMARY KEY ([id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Photopath] nvarchar(max) NULL,
        [AddressId] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUsers_UserAddresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [UserAddresses] ([id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE TABLE [Companies] (
        [id] int NOT NULL IDENTITY,
        [Date] Date NOT NULL,
        [CompanyName] nvarchar(450) NOT NULL,
        [CompanyTitle] nvarchar(max) NOT NULL,
        [CompanyDesc] nvarchar(max) NOT NULL,
        [CompanyLogo] nvarchar(max) NOT NULL,
        [CompanyBackgorund] nvarchar(max) NOT NULL,
        [whyCooseUsImage] nvarchar(max) NOT NULL,
        [whyCooseUsText] nvarchar(max) NOT NULL,
        [CompanyAdd] nvarchar(max) NOT NULL,
        [CompanyPhone] nvarchar(max) NOT NULL,
        [CompanyEmail] nvarchar(max) NOT NULL,
        [CompanyOwner] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_Companies] PRIMARY KEY ([id]),
        CONSTRAINT [FK_Companies_AspNetUsers_CompanyOwner] FOREIGN KEY ([CompanyOwner]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE TABLE [UserStats] (
        [id] int NOT NULL IDENTITY,
        [Count] int NOT NULL,
        [When] datetime2 NOT NULL,
        [UserId] nvarchar(450) NULL,
        CONSTRAINT [PK_UserStats] PRIMARY KEY ([id]),
        CONSTRAINT [FK_UserStats_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE TABLE [Categories] (
        [id] int NOT NULL IDENTITY,
        [launch] Date NOT NULL,
        [CategoryName] nvarchar(max) NOT NULL,
        [CompanyId] int NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([id]),
        CONSTRAINT [FK_Categories_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Companies] ([id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE TABLE [Contact] (
        [id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Phone] nvarchar(max) NOT NULL,
        [Subject] nvarchar(max) NULL,
        [Message] nvarchar(max) NULL,
        [CompanyId] int NULL,
        CONSTRAINT [PK_Contact] PRIMARY KEY ([id]),
        CONSTRAINT [FK_Contact_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Companies] ([id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE TABLE [Products] (
        [id] int NOT NULL IDENTITY,
        [launch] Date NOT NULL,
        [title] nvarchar(max) NOT NULL,
        [details] nvarchar(max) NOT NULL,
        [overviews] nvarchar(max) NOT NULL,
        [CategoryId] int NOT NULL,
        [isSpecial] bit NOT NULL,
        [CompanyId] int NOT NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([id]),
        CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Products_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Companies] ([id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE TABLE [Images] (
        [id] int NOT NULL IDENTITY,
        [Image_Path] nvarchar(max) NULL,
        [productId] int NOT NULL,
        CONSTRAINT [PK_Images] PRIMARY KEY ([id]),
        CONSTRAINT [FK_Images_Products_productId] FOREIGN KEY ([productId]) REFERENCES [Products] ([id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE UNIQUE INDEX [IX_AspNetUsers_AddressId] ON [AspNetUsers] ([AddressId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE INDEX [IX_Categories_CompanyId] ON [Categories] ([CompanyId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE UNIQUE INDEX [IX_Companies_CompanyName] ON [Companies] ([CompanyName]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE UNIQUE INDEX [IX_Companies_CompanyOwner] ON [Companies] ([CompanyOwner]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE INDEX [IX_Contact_CompanyId] ON [Contact] ([CompanyId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE INDEX [IX_Images_productId] ON [Images] ([productId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE INDEX [IX_Products_CompanyId] ON [Products] ([CompanyId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    CREATE INDEX [IX_UserStats_UserId] ON [UserStats] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201108115030_InitialDb')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201108115030_InitialDb', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201112115843_Added social Links companies')
BEGIN
    ALTER TABLE [Companies] ADD [FbProfile] nvarchar(max) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201112115843_Added social Links companies')
BEGIN
    ALTER TABLE [Companies] ADD [linkedinProfile] nvarchar(max) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201112115843_Added social Links companies')
BEGIN
    ALTER TABLE [Companies] ADD [twitterProfile] nvarchar(max) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201112115843_Added social Links companies')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201112115843_Added social Links companies', N'2.2.6-servicing-10079');
END;

GO

