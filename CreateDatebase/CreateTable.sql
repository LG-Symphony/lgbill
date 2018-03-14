--�û���Ϣ
Create table UserInfo
(
	[Id]				varchar(20),			--180314164900_abcdefg
	[Email]				varchar(50),
	[Password]			varchar(100) not null,
	[NickName]			varchar(10) not null,	--�ǳ�
	[AccountId]			varchar(100),			--�����˵�Id���ã��ֿ���ÿ����ഴ��10����
	[Category]			varchar(max),			--�Զ������Id���ã��ֿ���
	[CreateDate]		datetime not null,
	[WeChat]			varchar(50),			--΢�ź�
	[PhoneNumber]		varchar(15),			--�ֻ���
	Primary key([Id])
)

--�û���¼ʱЧ��
Create Table LoginState(
	[UserId]			varchar(20),			--�û�Email
	[StartTime]			datetime,				--���һ����֤ʱ��
	Primary key([UserId])						--����
)
--��֤���
Create Table Verify(
	[Id]				varchar(10),			--��֤��Id�����10λ�ַ�����
	[Code]				varchar(4),				--���һ����֤ʱ��
	[StartTime]			datetime,				--���һ����֤ʱ��
	Primary key([Id])							--������������
)
--�һ�����������֤��
Create Table FindPwdVerify(
	[Email]			varchar(50) not null,	--�û�Email
	[StartTime]			datetime not null,		--���һ����֤ʱ��
	[Code]				varchar(6) not null,	--��֤��
	Primary key([Email])						--������������
)

--�˵�List
Create Table AccountList(
	[Code]				varchar(15),			--��Ϊ������A_�û�ID_5λ�������
	[CreateUserId]		int not null,			--������Id
	[AllUserId]			varchar(max) not null,	--ʹ����Id���ã��ֿ���
	[Name]				varchar(20) not null,	--�˵�����Ĭ��ͬ[Code]�ֶΣ�
	[CreateDate]		datetime not null,
	[Member]			int default(1),			--���˵������û���
	Primary key([Code])
)

--�˵�
--Create Table [AccountList->Code](
--	[Id]				int identity(1,1),
--	[RecorderId]		int not null,			--������Id
--	[UserId]			varchar(max),			--ʹ����Id���ã��ֿ���
--	[CreateDate]		datetime not null,		--��������
--	[Money]				real not null,			--���
--	[Category]			int,					--�������Id		
--	[Note]				varchar(300)			--����
--	Primary key([Id])
--)

--���������
Create Table AccountCategory(
	[Id]				int identity(1,1),
	[Name]				varchar(6),
	[CreateUserId]		int not null,			--������Id
	[CreateDate]		datetime not null,
	[UserNum]			int not null default(0),--ʹ����������Ϊ�Ƽ����
	Primary key([Id])
)