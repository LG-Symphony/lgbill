--用户信息
Create table UserInfo
(
	[Id]				varchar(20),			--180314164900_abcdefg
	[Email]				varchar(50),
	[Password]			varchar(100) not null,
	[NickName]			varchar(10) not null,	--昵称
	[AccountId]			varchar(100),			--参与账单Id（用，分开，每人最多创建10个）
	[Category]			varchar(max),			--自定义类别Id（用，分开）
	[CreateDate]		datetime not null,
	[WeChat]			varchar(50),			--微信号
	[PhoneNumber]		varchar(15),			--手机号
	Primary key([Id])
)

--用户登录时效表
Create Table LoginState(
	[UserId]			varchar(20),			--用户Email
	[StartTime]			datetime,				--最后一次验证时间
	Primary key([UserId])						--主键
)
--验证码表
Create Table Verify(
	[Id]				varchar(10),			--验证码Id（随机10位字符串）
	[Code]				varchar(4),				--最后一次验证时间
	[StartTime]			datetime,				--最后一次验证时间
	Primary key([Id])							--主键不能自增
)
--找回密码邮箱验证码
Create Table FindPwdVerify(
	[Email]			varchar(50) not null,	--用户Email
	[StartTime]			datetime not null,		--最后一次验证时间
	[Code]				varchar(6) not null,	--验证码
	Primary key([Email])						--主键不能自增
)

--账单List
Create Table AccountList(
	[Code]				varchar(15),			--此为表名（A_用户ID_5位随机数）
	[CreateUserId]		int not null,			--创建人Id
	[AllUserId]			varchar(max) not null,	--使用人Id（用，分开）
	[Name]				varchar(20) not null,	--账单名（默认同[Code]字段）
	[CreateDate]		datetime not null,
	[Member]			int default(1),			--此账单包含用户数
	Primary key([Code])
)

--账单
--Create Table [AccountList->Code](
--	[Id]				int identity(1,1),
--	[RecorderId]		int not null,			--记账人Id
--	[UserId]			varchar(max),			--使用人Id（用，分开）
--	[CreateDate]		datetime not null,		--记账日期
--	[Money]				real not null,			--金额
--	[Category]			int,					--消费类别Id		
--	[Note]				varchar(300)			--描述
--	Primary key([Id])
--)

--消费种类表
Create Table AccountCategory(
	[Id]				int identity(1,1),
	[Name]				varchar(6),
	[CreateUserId]		int not null,			--创建人Id
	[CreateDate]		datetime not null,
	[UserNum]			int not null default(0),--使用人数（作为推荐类别）
	Primary key([Id])
)