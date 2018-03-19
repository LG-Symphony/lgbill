USE [BILL]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_CreateAccountTable]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_CreateAccountTable]
GO

/****** Object:  StoredProcedure [dbo].[proc_CreateAccountTable]  ***********/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[proc_CreateAccountTable] 
	@TableName				VARCHAR(15)
AS 

BEGIN
	SET NOCOUNT ON;
	DECLARE @SQL VARCHAR(max)
	if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].['+@TableName+']') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
		BEGIN
			SET @SQL  = '
			Create Table ' + @TableName + '(
				[Id]				int identity(1,1),
				[RecorderId]		varchar(20) not null,	--记账人Id
				[UserId]			varchar(max) not null,	--使用人Id（用，分开）
				[CreateDate]		datetime not null,		--记账日期
				[Money]				real not null,			--金额
				[Category]			varchar(8) not null,	--消费类别Name		
				[Note]				varchar(300),			--描述
				Primary key([Id])
			)'
			--PRINT(@SQL)
			EXEC (@SQL)
		END
END