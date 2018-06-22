CREATE TABLE [dbo].[Movies] (
    [ID]          INT             IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (60)   NULL,
    [ReleaseDate] DATETIME        NOT NULL,
    [Genre]       NVARCHAR (30)   NOT NULL,
    [Price]       DECIMAL (18, 2) NOT NULL,
    [Rating]      NVARCHAR (5)    NULL,
    CONSTRAINT [PK_dbo.Movies] PRIMARY KEY CLUSTERED ([ID] ASC)
);

