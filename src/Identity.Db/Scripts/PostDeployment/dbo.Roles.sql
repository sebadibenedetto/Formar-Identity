SET NOCOUNT ON

MERGE INTO [Roles] AS [Target]
USING (VALUES
  (N'AccessApplication',N'Access Application',N'AccessApplication',NULL,N'Acceso a Aplicación')
 ,(N'IdentityVerify',N'Identity Verify',N'IdentityVerify',NULL,N'Identidad Verificada')
 ,(N'Admin',N'Admin',N'Admin',NULL,N'Administrador')
 ) AS [Source] ([RolesId],[Name],[NormalizedName],[ConcurrencyStamp],[Description])
ON ([Target].[RolesId] = [Source].[RolesId])
WHEN MATCHED AND (
	NULLIF([Source].[Name], [Target].[Name]) IS NOT NULL OR NULLIF([Target].[Name], [Source].[Name]) IS NOT NULL OR 
	NULLIF([Source].[NormalizedName], [Target].[NormalizedName]) IS NOT NULL OR NULLIF([Target].[NormalizedName], [Source].[NormalizedName]) IS NOT NULL OR 
	NULLIF([Source].[ConcurrencyStamp], [Target].[ConcurrencyStamp]) IS NOT NULL OR NULLIF([Target].[ConcurrencyStamp], [Source].[ConcurrencyStamp]) IS NOT NULL OR 
	NULLIF([Source].[Description], [Target].[Description]) IS NOT NULL OR NULLIF([Target].[Description], [Source].[Description]) IS NOT NULL) THEN
 UPDATE SET
  [Name] = [Source].[Name], 
  [NormalizedName] = [Source].[NormalizedName], 
  [ConcurrencyStamp] = [Source].[ConcurrencyStamp], 
  [Description] = [Source].[Description]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([RolesId],[Name],[NormalizedName],[ConcurrencyStamp],[Description])
 VALUES([Source].[RolesId],[Source].[Name],[Source].[NormalizedName],[Source].[ConcurrencyStamp],[Source].[Description])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [Roles]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[Roles] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET NOCOUNT OFF
GO