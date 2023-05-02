CREATE TABLE [dbo].[Users]
(
	[UserId] [nvarchar](450) NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[UserName] [nvarchar](256) NULL,
	CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX  [UK_Email] ON [dbo].[Users]
(
	Email ASC
)
WHERE (Email IS NOT NULL)
GO
CREATE UNIQUE NONCLUSTERED INDEX  [UK_NormalizedEmail] ON [dbo].[Users]
(
	NormalizedEmail ASC
)
WHERE (Email IS NOT NULL)
GO
CREATE UNIQUE NONCLUSTERED INDEX  [UK_UserName] ON [dbo].[Users]
(
	[UserName] ASC
)
GO
CREATE NONCLUSTERED INDEX IX_Users_Email ON [dbo].[Users] ([Email]) 
GO
CREATE NONCLUSTERED INDEX IX_Users_NormalizedUserName ON [dbo].[Users] ([NormalizedUserName]) 
GO
CREATE NONCLUSTERED INDEX IX_Users_NormalizedEmail ON [dbo].[Users] ([NormalizedEmail]) 
GO