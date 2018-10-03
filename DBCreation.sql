SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DimAgent](
	[AgentKey] [int] IDENTITY(1,1) NOT NULL,
	[Agent] [nvarchar](1000) NULL,
 CONSTRAINT [PK_DimAgent] PRIMARY KEY CLUSTERED 
(
	[AgentKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DimClientIp](
	[ClientIpKey] [int] IDENTITY(1,1) NOT NULL,
	[ClientIpAddress] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_DimClientIp] PRIMARY KEY CLUSTERED 
(
	[ClientIpKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DimHttpMethod](
	[HttpMethodKey] [int] IDENTITY(1,1) NOT NULL,
	[HttpMethod] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_DimHttpMethod] PRIMARY KEY CLUSTERED 
(
	[HttpMethodKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DimPort](
	[PortKey] [int] IDENTITY(1,1) NOT NULL,
	[Port] [int] NULL,
 CONSTRAINT [PK_DimPort] PRIMARY KEY CLUSTERED 
(
	[PortKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DimServer](
	[ServerKey] [int] IDENTITY(1,1) NOT NULL,
	[ServerName] [nvarchar](150) NULL,
 CONSTRAINT [PK_DimServer] PRIMARY KEY CLUSTERED 
(
	[ServerKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DimServerIp](
	[ServerIpKey] [int] IDENTITY(1,1) NOT NULL,
	[ServerIpAddress] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_DimServerIp] PRIMARY KEY CLUSTERED 
(
	[ServerIpKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DimStatus](
	[StatusKey] [int] IDENTITY(1,1) NOT NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_DimStatus] PRIMARY KEY CLUSTERED 
(
	[StatusKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DimSubStatus](
	[SubStatusKey] [int] IDENTITY(1,1) NOT NULL,
	[SubStatus] [int] NULL,
 CONSTRAINT [PK_SubDimStatus] PRIMARY KEY CLUSTERED 
(
	[SubStatusKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DimUsername](
	[UsernameKey] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](150) NULL,
 CONSTRAINT [PK_DimUserNAme] PRIMARY KEY CLUSTERED 
(
	[UsernameKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DimWin32Status](
	[Win32StatusKey] [int] IDENTITY(1,1) NOT NULL,
	[Win32Status] [int] NULL,
 CONSTRAINT [PK_Win32DimStatus] PRIMARY KEY CLUSTERED 
(
	[Win32StatusKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FactEvent](
	[EventId] [bigint] IDENTITY(1,1) NOT NULL,
	[ReferenceId] [bigint] NULL,
	[AgentKey] [int] NULL,
	[ClientIpKey] [int] NULL,
	[HttpMethodKey] [int] NULL,
	[PortKey] [int] NULL,
	[ServerKey] [int] NULL,
	[ServerIpKey] [int] NULL,
	[StatusKey] [int] NULL,
	[SubStatusKey] [int] NULL,
	[Win32StatusKey] [int] NULL,
	[UserNameKey] [int] NULL,
	[UriStem] [nvarchar](3500) NULL,
	[UriQuery] [nvarchar](max) NULL,
	[TimeTaken] [int] NULL,
	[BytesSent] [int] NULL,
	[BytesReceived] [int] NULL,
 CONSTRAINT [PK_FactEvent] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogFiles](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ServerId] [uniqueidentifier] NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_LogFiles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RawLogs](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FileId] [bigint] NOT NULL,
	[DateTime] [datetime] NULL,
	[ServerIpAddress] [nvarchar](32) NULL,
	[Method] [nvarchar](100) NULL,
	[UriStem] [nvarchar](3500) NULL,
	[UriQuery] [nvarchar](max) NULL,
	[Port] [int] NULL,
	[Username] [nvarchar](250) NULL,
	[ClientIpAddress] [nvarchar](32) NULL,
	[Agent] [nvarchar](2500) NULL,
	[Referer] [nvarchar](3500) NULL,
	[Status] [int] NULL,
	[SubStatus] [int] NULL,
	[Win32Status] [bigint] NULL,
	[TimeTaken] [int] NULL,
	[BytesSent] [int] NULL,
	[BytesReceived] [int] NULL,
	[Host] [nvarchar](150) NULL,
	[Processed] [bit] NOT NULL,
 CONSTRAINT [PK_RawLogs_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Servers](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Servers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RawLogs] ADD  CONSTRAINT [DF_RawLogs_Processed]  DEFAULT ((0)) FOR [Processed]
GO
ALTER TABLE [dbo].[FactEvent]  WITH CHECK ADD  CONSTRAINT [FK_FactEvent_DimAgent] FOREIGN KEY([AgentKey])
REFERENCES [dbo].[DimAgent] ([AgentKey])
GO
ALTER TABLE [dbo].[FactEvent] CHECK CONSTRAINT [FK_FactEvent_DimAgent]
GO
ALTER TABLE [dbo].[FactEvent]  WITH CHECK ADD  CONSTRAINT [FK_FactEvent_DimClientIp] FOREIGN KEY([ClientIpKey])
REFERENCES [dbo].[DimClientIp] ([ClientIpKey])
GO
ALTER TABLE [dbo].[FactEvent] CHECK CONSTRAINT [FK_FactEvent_DimClientIp]
GO
ALTER TABLE [dbo].[FactEvent]  WITH CHECK ADD  CONSTRAINT [FK_FactEvent_DimHttpMethod] FOREIGN KEY([HttpMethodKey])
REFERENCES [dbo].[DimHttpMethod] ([HttpMethodKey])
GO
ALTER TABLE [dbo].[FactEvent] CHECK CONSTRAINT [FK_FactEvent_DimHttpMethod]
GO
ALTER TABLE [dbo].[FactEvent]  WITH CHECK ADD  CONSTRAINT [FK_FactEvent_DimPort] FOREIGN KEY([PortKey])
REFERENCES [dbo].[DimPort] ([PortKey])
GO
ALTER TABLE [dbo].[FactEvent] CHECK CONSTRAINT [FK_FactEvent_DimPort]
GO
ALTER TABLE [dbo].[FactEvent]  WITH CHECK ADD  CONSTRAINT [FK_FactEvent_DimServer] FOREIGN KEY([ServerKey])
REFERENCES [dbo].[DimServer] ([ServerKey])
GO
ALTER TABLE [dbo].[FactEvent] CHECK CONSTRAINT [FK_FactEvent_DimServer]
GO
ALTER TABLE [dbo].[FactEvent]  WITH CHECK ADD  CONSTRAINT [FK_FactEvent_DimServerIp] FOREIGN KEY([ServerIpKey])
REFERENCES [dbo].[DimServerIp] ([ServerIpKey])
GO
ALTER TABLE [dbo].[FactEvent] CHECK CONSTRAINT [FK_FactEvent_DimServerIp]
GO
ALTER TABLE [dbo].[FactEvent]  WITH CHECK ADD  CONSTRAINT [FK_FactEvent_DimStatus] FOREIGN KEY([StatusKey])
REFERENCES [dbo].[DimStatus] ([StatusKey])
GO
ALTER TABLE [dbo].[FactEvent] CHECK CONSTRAINT [FK_FactEvent_DimStatus]
GO
ALTER TABLE [dbo].[FactEvent]  WITH CHECK ADD  CONSTRAINT [FK_FactEvent_DimSubStatus] FOREIGN KEY([SubStatusKey])
REFERENCES [dbo].[DimSubStatus] ([SubStatusKey])
GO
ALTER TABLE [dbo].[FactEvent] CHECK CONSTRAINT [FK_FactEvent_DimSubStatus]
GO
ALTER TABLE [dbo].[FactEvent]  WITH CHECK ADD  CONSTRAINT [FK_FactEvent_DimUsername] FOREIGN KEY([UserNameKey])
REFERENCES [dbo].[DimUsername] ([UsernameKey])
GO
ALTER TABLE [dbo].[FactEvent] CHECK CONSTRAINT [FK_FactEvent_DimUsername]
GO
ALTER TABLE [dbo].[FactEvent]  WITH CHECK ADD  CONSTRAINT [FK_FactEvent_DimWin32Status] FOREIGN KEY([Win32StatusKey])
REFERENCES [dbo].[DimWin32Status] ([Win32StatusKey])
GO
ALTER TABLE [dbo].[FactEvent] CHECK CONSTRAINT [FK_FactEvent_DimWin32Status]
GO
ALTER TABLE [dbo].[RawLogs]  WITH CHECK ADD  CONSTRAINT [FK_RawLogs_LogFiles] FOREIGN KEY([FileId])
REFERENCES [dbo].[LogFiles] ([Id])
GO
ALTER TABLE [dbo].[RawLogs] CHECK CONSTRAINT [FK_RawLogs_LogFiles]
GO
/****** Object:  StoredProcedure [dbo].[UpdateWarehouse]    Script Date: 10/3/2018 7:51:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateWarehouse]

AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @Counter as bigint = 0;

	WHILE EXISTS (SELECT top 1 1 FROM RawLogs WHERE Processed = 0)
	BEGIN
		
		BEGIN TRANSACTION t1 

		--RAW Data Variables
		DECLARE @DateTime as datetime
		DECLARE @ServerIpAddress as nvarchar(32)
		DECLARE @Server as nvarchar(32)
		DECLARE @Method as nvarchar(100)
		DECLARE @UriStem as nvarchar(3500)
		DECLARE @UriQuery as nvarchar(max)
		DECLARE @Port as int
		DECLARE @UserName as nvarchar(250)
		DECLARE @ClientIpAddress as nvarchar(32)
		DECLARE @Agent as nvarchar(2500)
		DECLARE @Referer as nvarchar(3500)
		DECLARE @Status as int 
		DECLARE @SubStatus as int
		DECLARE @Win32Status as int
		DECLARE @TimeTaken as int
		DECLARE @BytesSent as int
		DECLARE @BytesReceived as int
		DECLARE @Host as nvarchar(150)
		DECLARE @RawLogId as bigint

			SELECT top 1
			@RawLogId = rl.Id,
			@DateTime = rl.[DateTime],
			@ServerIpAddress = ServerIpAddress,
			@Method = Method,
			@UriStem = UriStem,
			@UriQuery = UriQuery,
			@Port = [Port],
			@UserName = UserName,
			@ClientIpAddress = ClientIpAddress,
			@Agent = Agent,
			@Referer = Referer,
			@Status = [Status],
			@SubStatus = SubStatus,
			@Win32Status = Win32Status,
			@BytesSent = BytesSent,
			@BytesReceived = BytesReceived,
			@TimeTaken = TimeTaken,
			@Host = Host,
			@Server = s.Name
		FROM RawLogs rl 
		inner join LogFiles lf on lf.Id = rl.FileId
		inner join [Servers] s on s.Id = lf.ServerId
		where rl.Processed = 0

		--Dimension Keys 
		DECLARE @DimStatusKey as int 
		DECLARE @DimAgentKey as int 
		DECLARE @DimClientIpKey as int 
		DECLARE @DimHttpMethodKey as int 
		DECLARE @DimPortKey as int 
		DECLARE @DimServerKey as int 
		DECLARE @DimServerIpKey as int 
		DECLARE @DimSubStatusKey as int 
		DECLARE @DimUserNameKey as int 
		DECLARE @DimWin32StatusKey as int 

	


		--Get/Insert DimAgent
		set @DimAgentKey = (select AgentKey from DimAgent where Agent = @Agent)
		if (@DimAgentKey is null)
		begin
			insert into DimAgent(Agent) values (@Agent)
			set @DimAgentKey = SCOPE_IDENTITY()
		end

		--Get/Insert DimClientIp
		set @DimClientIpKey = (select ClientIpKey from DimClientIp where ClientIpAddress = @ClientIpAddress)
		if (@DimClientIpKey is null)
		begin
			insert into DimClientIp(ClientIpAddress) values (@ClientIpAddress)
			set @DimClientIpKey = SCOPE_IDENTITY()
		end

		--Get/Insert DimHttpMethod
		set @DimHttpMethodKey = (select HttpMethodKey from DimHttpMethod where HttpMethod = @Method)
		if (@DimHttpMethodKey is null)
		begin
			insert into DimHttpMethod(HttpMethod) values (@Method)
			set @DimHttpMethodKey = SCOPE_IDENTITY()
		end

		--Get/Insert DimPort
		set @DimPortKey = (select PortKey from DimPort where [Port] = @Port)
		if (@DimPortKey is null)
		begin
			insert into DimPort([Port]) values (@Port)
			set @DimPortKey = SCOPE_IDENTITY()
		end

		--Get/Insert DimServer
		set @DimServerKey = (select ServerKey from DimServer where [ServerName] = @Server)
		if (@DimServerKey is null)
		begin
			insert into DimServer(ServerName) values (@Server)
			set @DimServerKey = SCOPE_IDENTITY()
		end

		--Get/Insert DimServerIp
		set @DimServerIpKey = (select ServerIpKey from DimServerIp where ServerIpAddress = @ServerIpAddress)
		if (@DimServerIpKey is null)
		begin
			insert into DimServerIp(ServerIpAddress) values (@ServerIpAddress)
			set @DimServerIpKey = SCOPE_IDENTITY()
		end

		--Get/Insert DimStatus
		set @DimStatusKey = (select StatusKey from DimStatus where [Status] = @Status)
		if (@DimStatusKey is null)
		begin
			insert into DimStatus(Status) values (@Status)
			set @DimStatusKey = SCOPE_IDENTITY()
		end

		--Get/Insert DimSubStatus
		set @DimSubStatusKey = (select SubStatusKey from DimSubStatus where [SubStatus] = @SubStatus)
		if (@DimSubStatusKey is null)
		begin
			insert into DimSubStatus(SubStatus) values (@SubStatus)
			set @DimSubStatusKey = SCOPE_IDENTITY()
		end

		--Get/Insert DimWin32Status
		set @DimWin32StatusKey = (select Win32StatusKey from DimWin32Status where [Win32Status] = @Win32Status)
		if (@DimWin32StatusKey is null)
		begin
			insert into DimWin32Status(Win32Status) values (@Win32Status)
			set @DimWin32StatusKey = SCOPE_IDENTITY()
		end

		--Get/Insert DimUsername
		set @DimUserNameKey = (select UserNameKey from DimUsername where UserName = @UserName)
		if (@DimUserNameKey is null)
		begin
			insert into DimUsername(UserName) values (@UserName)
			set @DimUserNameKey = SCOPE_IDENTITY()
		end


		INSERT INTO [dbo].[FactEvent]
           ([ReferenceId]
           ,[AgentKey]
           ,[ClientIpKey]
           ,[HttpMethodKey]
           ,[PortKey]
           ,[ServerKey]
           ,[ServerIpKey]
           ,[StatusKey]
           ,[SubStatusKey]
           ,[Win32StatusKey]
           ,[UserNameKey]
		   ,[UriStem]
           ,[UriQuery]
           ,[TimeTaken]
           ,[BytesSent]
           ,[BytesReceived])
     VALUES
           (@RawLogId
           ,@DimAgentKey
           ,@DimClientIpKey
		   ,@DimHttpMethodKey
		   ,@DimPortKey
           ,@DimServerKey
           ,@DimServerIpKey
		   ,@DimStatusKey
           ,@DimSubStatusKey
		   ,@DimWin32StatusKey
           ,@DimUserNameKey
		   ,@UriStem
           ,@UriQuery
           ,@TimeTaken
		   ,@BytesSent
		   ,@BytesReceived
           )

		update RawLogs set Processed = 1 where Id = @RawLogId

		COMMIT TRANSACTION t1

		set @Counter = @Counter + 1

		print cast(@Counter as nvarchar(10)) + 'Completed: ' + cast(@RawLogId as nvarchar(10))
	END
END
GO
