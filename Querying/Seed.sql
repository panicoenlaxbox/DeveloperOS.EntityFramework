EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'
GO
SET IDENTITY_INSERT [dbo].[OrderLines] ON 
GO
INSERT [dbo].[OrderLines] ([Id], [OrderId], [ProductId], [Units]) VALUES (1, 1, 1, 1)
GO
INSERT [dbo].[OrderLines] ([Id], [OrderId], [ProductId], [Units]) VALUES (2, 1, 1, 3)
GO
INSERT [dbo].[OrderLines] ([Id], [OrderId], [ProductId], [Units]) VALUES (3, 1, 1, 5)
GO
INSERT [dbo].[OrderLines] ([Id], [OrderId], [ProductId], [Units]) VALUES (4, 2, 1, 1)
GO
INSERT [dbo].[OrderLines] ([Id], [OrderId], [ProductId], [Units]) VALUES (5, 2, 1, 3)
GO
INSERT [dbo].[OrderLines] ([Id], [OrderId], [ProductId], [Units]) VALUES (6, 2, 1, 5)
GO
INSERT [dbo].[OrderLines] ([Id], [OrderId], [ProductId], [Units]) VALUES (7, 3, 1, 1)
GO
INSERT [dbo].[OrderLines] ([Id], [OrderId], [ProductId], [Units]) VALUES (8, 3, 1, 3)
GO
INSERT [dbo].[OrderLines] ([Id], [OrderId], [ProductId], [Units]) VALUES (9, 3, 1, 5)
GO
INSERT [dbo].[OrderLines] ([Id], [OrderId], [ProductId], [Units]) VALUES (10, 1, 2, 2)
GO
INSERT [dbo].[OrderLines] ([Id], [OrderId], [ProductId], [Units]) VALUES (11, 1, 2, 4)
GO
INSERT [dbo].[OrderLines] ([Id], [OrderId], [ProductId], [Units]) VALUES (12, 2, 2, 2)
GO
INSERT [dbo].[OrderLines] ([Id], [OrderId], [ProductId], [Units]) VALUES (13, 2, 2, 4)
GO
INSERT [dbo].[OrderLines] ([Id], [OrderId], [ProductId], [Units]) VALUES (14, 3, 2, 2)
GO
INSERT [dbo].[OrderLines] ([Id], [OrderId], [ProductId], [Units]) VALUES (15, 3, 2, 4)
GO
SET IDENTITY_INSERT [dbo].[OrderLines] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 
GO
INSERT [dbo].[Orders] ([Id], [CreatedDate]) VALUES (1, CAST(N'2017-05-23T15:40:27.330' AS DateTime))
GO
INSERT [dbo].[Orders] ([Id], [CreatedDate]) VALUES (2, CAST(N'2017-05-23T15:40:27.333' AS DateTime))
GO
INSERT [dbo].[Orders] ([Id], [CreatedDate]) VALUES (3, CAST(N'2017-05-23T15:40:27.333' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 
GO
INSERT [dbo].[Products] ([Id], [Name]) VALUES (1, N'Foo')
GO
INSERT [dbo].[Products] ([Id], [Name]) VALUES (2, N'Bar')
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'