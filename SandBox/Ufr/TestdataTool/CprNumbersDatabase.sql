USE [TestdataTool]
GO

/****** Object:  Table [dbo].[CprNumbers]    Script Date: 05/29/2015 08:51:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CprNumbers]') AND type in (N'U'))
DROP TABLE [dbo].[CprNumbers]
GO

USE [TestdataTool]
GO

/****** Object:  Table [dbo].[CprNumbers]    Script Date: 05/29/2015 08:51:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CprNumbers](
	[CprNumId] int IDENTITY(1,1) primary key,
	[Environment] [nvarchar](10) NOT NULL,
	[CprNummer] [nvarchar](10) NOT NULL,
	[UsedBy] [nvarchar](50) NULL

	CONSTRAINT [UQ_codes] UNIQUE NONCLUSTERED
	(
		[Environment], [CprNummer]
	)
) ON [PRIMARY]

GO

