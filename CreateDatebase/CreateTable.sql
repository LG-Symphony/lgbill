--用户信息
Drop Table UserInfo
Create table UserInfo
(
	[Id]				varchar(20),			--180314164900_abcdefg
	[Email]				varchar(50) not null,
	[Password]			varchar(100) not null,
	[Nickname]			varchar(10) not null,	--昵称
	[AccountId]			varchar(max),			--参与账单Id（用，分开，每人最多创建10个，参与数不限）
	[Category]			varchar(max),			--自定义类别Name（用，分开）
	[CreateDate]		datetime not null,
	[WeChat]			varchar(50),			--微信号
	[PhoneNumber]		varchar(15),			--手机号
	Primary key([Id])
)

--用户登录时效表
Drop Table LoginState
Create Table LoginState(
	[UserId]			varchar(20),			--用户Email
	[StartTime]			datetime,				--最后一次验证时间
	Primary key([UserId])						--主键
)
--验证码表
Drop Table Verify
Create Table Verify(
	[Id]				varchar(10),			--验证码Id（随机10位字符串）
	[Code]				varchar(4),				--最后一次验证时间
	[StartTime]			datetime,				--最后一次验证时间
	Primary key([Id])							--主键不能自增
)
--找回密码邮箱验证码
Drop Table FindPwdVerify
Create Table FindPwdVerify(
	[Email]				varchar(50) not null,	--用户Email
	[StartTime]			datetime not null,		--最后一次验证时间
	[Code]				varchar(6) not null,	--验证码
	Primary key([Email])						--主键不能自增
)

--账单List
Drop Table AccountList
Create Table AccountList(
	[Code]				varchar(15),			--此为表名（ZYYMMDD(年月日的数字)3位随机字符串+5位随机数字and字符串）例：Z180319axDze021
	[CreateUserId]		varchar(20) not null,	--创建人Id
	[AllUserId]			varchar(max) not null,	--使用人Id（用，分开，包括创建者）
	[Name]				varchar(20) not null,	--账单名（默认为“我的手账”）
	[CreateDate]		datetime not null,
	[Member]			int default(1),			--此账单包含用户数
	Primary key([Code])
)
--账单操作记录
Drop Table AccountListLog
Create Table AccountListLog(
	[Id]				int identity(1,1),
	[Code]				varchar(15) not null,
	[CreateDate]		datetime not null,
	[OldInfo]			varchar(max) not null,
	[NewInfo]			varchar(max) not null,
	[ConfirmUser]		varchar(max) not null,--已经确认知晓改动的所有人员，可以展示在账单中
	[Type]				int not null,
	[Note]				varchar(200),
	Primary key([Id])
)

--账单
--Create Table [AccountList->Code](
--	[Id]				int identity(1,1),
--	[RecorderId]		varchar(20) not null,	--记账人Id
--	[UserId]			varchar(max),			--使用人Id（用，分开）
--	[CreateDate]		datetime not null,		--记账日期
--	[Money]				real not null,			--金额
--	[Category]			varchar(8) not null,	--消费类别Name		
--	[Note]				varchar(300)			--备注
--	Primary key([Id])
--)

--账目操作记录
Drop Table AccountLog
Create Table AccountLog(
	[Id]				int identity(1,1),
	[AccountListCode]	varchar(15),
	[AccountId]			int,
	[CreateDate]		datetime not null,
	[ModifyUserId]		varchar(20) not null,
	[OldInfo]			varchar(max) not null,
	[NewInfo]			varchar(max) not null,
	[Note]				varchar(200),
	Primary key([Id])
)


--消费种类表
--(可以考虑添加热度HOT字段，每有一次使用就+1；当日HTO，当月HOT，当年HOT，数据库作业定时删除)
Drop Table AccountCategory
Create Table AccountCategory(
	--[Id]				int identity(1,1),
	[Name]				varchar(8),				--8个字
	[CreateUserId]		varchar(20) not null,	--创建人Id
	[CreateDate]		datetime not null,
	[UserNum]			int not null default(0),--使用人数（作为推荐类别）
	[IsShow]			bit,
	Primary key([Name])
)

Drop Table SystemNotice
Create Table SystemNotice(
	[Id]				int identity(1,1),
	[UserId]			varchar(20) not null,	--用户Id
	[Content]			varchar(max) not null,
	[CreateDate]		datetime default getdate() not null,
	[Confirm]			bit default 0 not null,
	Primary key([Id])
)