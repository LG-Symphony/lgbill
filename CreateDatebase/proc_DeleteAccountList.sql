USE [BILL]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_DeleteAccountList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_DeleteAccountList]
GO

/****** Object:  StoredProcedure [dbo].[proc_DeleteAccountList]  ***********/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[proc_DeleteAccountList] 
	--@TableName				VARCHAR(15)
AS 

BEGIN
	SET NOCOUNT ON;
	DECLARE @SQL VARCHAR(max)
	DECLARE @Code VARCHAR(15)
	CREATE TABLE #Temp 
	(   
		Code		varchar(15)
	)  
	SET @SQL = '
	INSERT INTO #Temp 
	SELECT Code FROM AccountListLog 
	WHERE datediff(mi,CreateDate,getdate())>21600 
	AND [Type] = 2'
	EXEC (@SQL)
	DECLARE @_row int 
	SELECT @_row = count(*) from #Temp
	WHILE (@_row > 0)
	BEGIN
		SELECT TOP 1 @Code = Code FROM #Temp
		SET @SQL = 'DROP TABLE '+@Code;
		DELETE FROM #Temp WHERE Code = @Code;
		DELETE FROM AccountListLog WHERE Code = @Code;
		EXEC (@SQL)
		SET @_row = @_row-1
	END
END