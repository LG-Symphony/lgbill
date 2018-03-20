--�û���Ϣ
Drop Table UserInfo
Create table UserInfo
(
	[Id]				varchar(20),			--180314164900_abcdefg
	[Email]				varchar(50) not null,
	[Password]			varchar(100) not null,
	[Nickname]			varchar(10) not null,	--�ǳ�
	[AccountId]			varchar(max),			--�����˵�Id���ã��ֿ���ÿ����ഴ��10�������������ޣ�
	[Category]			varchar(max),			--�Զ������Name���ã��ֿ���
	[CreateDate]		datetime not null,
	[WeChat]			varchar(50),			--΢�ź�
	[PhoneNumber]		varchar(15),			--�ֻ���
	Primary key([Id])
)

--�û���¼ʱЧ��
Drop Table LoginState
Create Table LoginState(
	[UserId]			varchar(20),			--�û�Email
	[StartTime]			datetime,				--���һ����֤ʱ��
	Primary key([UserId])						--����
)
--��֤���
Drop Table Verify
Create Table Verify(
	[Id]				varchar(10),			--��֤��Id�����10λ�ַ�����
	[Code]				varchar(4),				--���һ����֤ʱ��
	[StartTime]			datetime,				--���һ����֤ʱ��
	Primary key([Id])							--������������
)
--�һ�����������֤��
Drop Table FindPwdVerify
Create Table FindPwdVerify(
	[Email]				varchar(50) not null,	--�û�Email
	[StartTime]			datetime not null,		--���һ����֤ʱ��
	[Code]				varchar(6) not null,	--��֤��
	Primary key([Email])						--������������
)

--�˵�List
Drop Table AccountList
Create Table AccountList(
	[Code]				varchar(15),			--��Ϊ������ZYYMMDD(�����յ�����)3λ����ַ���+5λ�������and�ַ���������Z180319axDze021
	[CreateUserId]		varchar(20) not null,	--������Id
	[AllUserId]			varchar(max) not null,	--ʹ����Id���ã��ֿ������������ߣ�
	[Name]				varchar(20) not null,	--�˵�����Ĭ��Ϊ���ҵ����ˡ���
	[CreateDate]		datetime not null,
	[Member]			int default(1),			--���˵������û���
	Primary key([Code])
)
--�˵�������¼
Drop Table AccountListLog
Create Table AccountListLog(
	[Id]				int identity(1,1),
	[Code]				varchar(15) not null,
	[CreateDate]		datetime not null,
	[OldInfo]			varchar(max) not null,
	[NewInfo]			varchar(max) not null,
	[ConfirmUser]		varchar(max) not null,--�Ѿ�ȷ��֪���Ķ���������Ա������չʾ���˵���
	[Type]				int not null,
	[Note]				varchar(200),
	Primary key([Id])
)

--�˵�
--Create Table [AccountList->Code](
--	[Id]				int identity(1,1),
--	[RecorderId]		varchar(20) not null,	--������Id
--	[UserId]			varchar(max),			--ʹ����Id���ã��ֿ���
--	[CreateDate]		datetime not null,		--��������
--	[Money]				real not null,			--���
--	[Category]			varchar(8) not null,	--�������Name		
--	[Note]				varchar(300)			--��ע
--	Primary key([Id])
--)

--��Ŀ������¼
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


--���������
--(���Կ�������ȶ�HOT�ֶΣ�ÿ��һ��ʹ�þ�+1������HTO������HOT������HOT�����ݿ���ҵ��ʱɾ��)
Drop Table AccountCategory
Create Table AccountCategory(
	--[Id]				int identity(1,1),
	[Name]				varchar(8),				--8����
	[CreateUserId]		varchar(20) not null,	--������Id
	[CreateDate]		datetime not null,
	[UserNum]			int not null default(0),--ʹ����������Ϊ�Ƽ����
	[IsShow]			bit,
	Primary key([Name])
)