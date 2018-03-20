USE [BILL]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_GetNicknameByUserId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_GetNicknameByUserId]
GO

/****** Object:  StoredProcedure [dbo].[proc_GetNicknameByUserId]  ***********/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[proc_GetNicknameByUserId] 
	@UserId				VARCHAR(15)
AS 

BEGIN
	SET NOCOUNT ON;
	DECLARE @SQL VARCHAR(max)
	SET @UserId=replace(@UserId,',',''',''');
	SET @SQL  = '
		SELECT [Nickname] FROM UserInfo WHERE Id IN ('''+@UserId+''')
	'
	--PRINT(@SQL)
	EXEC (@SQL)
	
END