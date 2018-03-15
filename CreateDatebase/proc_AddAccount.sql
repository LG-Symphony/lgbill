USE [BILL]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_AddAccount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_AddAccount]
GO

/****** Object:  StoredProcedure [dbo].[proc_AddAccount]  ***********/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[proc_AddAccount] 
	@TableName				VARCHAR(15)
AS 

BEGIN
	SET NOCOUNT ON;
	DECLARE @SQL VARCHAR(max)
	
END