﻿CREATE TABLE [dbo].[Orders] (
    [orderid]   INT         NOT NULL,
    [custid]    CHAR (11)   NOT NULL,
    [empid]     INT         NOT NULL,
    [shipperid] VARCHAR (5) NOT NULL,
    [orderdate] DATE        NOT NULL,
    [filler]    CHAR (160)  DEFAULT ('a') NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY NONCLUSTERED ([orderid] ASC),
    CONSTRAINT [FK_Orders_Customers] FOREIGN KEY ([custid]) REFERENCES [dbo].[Customers] ([custid]),
    CONSTRAINT [FK_Orders_Employees] FOREIGN KEY ([empid]) REFERENCES [dbo].[Employees] ([empid]),
    CONSTRAINT [FK_Orders_Shippers] FOREIGN KEY ([shipperid]) REFERENCES [dbo].[Shippers] ([shipperid])
);


GO
CREATE CLUSTERED INDEX [idx_cl_od]
    ON [dbo].[Orders]([orderdate] ASC);

