IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_CreateAccount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_CreateAccount]
GO

/****** Object:  StoredProcedure [dbo].[proc_CreateAccount]  ***********/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[proc_CreateAccount] 
	@TableName				VARCHAR(15)
AS 

BEGIN
	SET NOCOUNT ON;
	DECLARE @SQL VARCHAR(max)
	if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].['+@TableName+']') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
		BEGIN
			SET @SQL  = '
			Create Table ' + @TableName + '(
				[Id]				int identity(1,1),
				[RecorderId]		int not null,			--记账人Id
				[UserId]			varchar(max),			--使用人Id（用，分开）
				[CreateDate]		datetime not null,		--记账日期
				[Money]				real not null,			--金额
				[Category]			int,					--消费类别Id		
				[Note]				varchar(300),			--描述
				Primary key([Id])
			)'
			--PRINT(@SQL)
			EXEC (@SQL)
		END
END