CREATE TABLE [dbo].[UserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](450) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserLogins] ADD  CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLogins] CHECK CONSTRAINT [FK_UserLogins_Users_UserId]
GO
CREATE NONCLUSTERED INDEX IX_UserLogins_UserIdProviderDisplayName ON [dbo].[UserLogins] ([UserId]) INCLUDE ([ProviderDisplayName]) 
GO
CREATE NONCLUSTERED INDEX [IX_UserLogins_Users_UserId] ON [dbo].[UserLogins] ([UserId])
GO
CREATE NONCLUSTERED INDEX IX_UserLogins_ProviderDisplayName ON [dbo].[UserLogins] ([ProviderDisplayName]) 
GO