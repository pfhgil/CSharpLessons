DELETE FROM [dbo].[Receipts]
DBCC CHECKIDENT ('dbo.Receipts', RESEED, 0)

DELETE FROM [dbo].[Staff]
DBCC CHECKIDENT ('dbo.Staff', RESEED, 0)

DELETE FROM [dbo].[Post]
DBCC CHECKIDENT ('dbo.Post', RESEED, 0)

DELETE FROM [dbo].[Machines]
DBCC CHECKIDENT ('dbo.Machines', RESEED, 0)

DELETE FROM [dbo].[Materials]
DBCC CHECKIDENT ('dbo.Materials', RESEED, 0)

DELETE FROM [dbo].[Furniture]
DBCC CHECKIDENT ('dbo.Furniture', RESEED, 0)

DELETE FROM [dbo].[FurnitureTypes]
DBCC CHECKIDENT ('dbo.FurnitureTypes', RESEED, 0)

DELETE FROM [dbo].[Factories]
DBCC CHECKIDENT ('dbo.Factories', RESEED, 0)

DELETE FROM [dbo].[MaterialsSuppliers]
DBCC CHECKIDENT ('dbo.MaterialsSuppliers', RESEED, 0)

DELETE FROM [dbo].[FurnitureCarriers]
DBCC CHECKIDENT ('dbo.FurnitureCarriers', RESEED, 0)

GO

INSERT INTO [dbo].[FurnitureCarriers]([Name]) VALUES ('������ ���������')
INSERT INTO [dbo].[FurnitureCarriers]([Name]) VALUES ('��������� ���� ����')

INSERT INTO [dbo].[MaterialsSuppliers]([Name]) VALUES ('������ ���������')
INSERT INTO [dbo].[MaterialsSuppliers]([Name]) VALUES ('��������� ���� ����')

INSERT INTO [dbo].[Factories]([Address], [MatSupplierID], [FurnitureCarrierID]) VALUES ('��. �����������, 8', 1, 1)
INSERT INTO [dbo].[Factories]([Address], [MatSupplierID], [FurnitureCarrierID]) VALUES ('��. ������, 10', 2, 2)

INSERT INTO [dbo].[FurnitureTypes]([Name]) VALUES ('�����')
INSERT INTO [dbo].[FurnitureTypes]([Name]) VALUES ('��������')

INSERT INTO [dbo].[Furniture]([Name], [Amount], [PricePerPiece], [FurnitureTypeID]) VALUES ('�����������', 250, 5000, 1)
INSERT INTO [dbo].[Furniture]([Name], [Amount], [PricePerPiece], [FurnitureTypeID]) VALUES ('����', 100, 20000, 2)

INSERT INTO [dbo].[Materials]([Name], [Amount]) VALUES ('������', 500)
INSERT INTO [dbo].[Materials]([Name], [Amount]) VALUES ('��������', 600)

INSERT INTO [dbo].[Machines]([Name], [MaterialID]) VALUES ('���-61', 1)
INSERT INTO [dbo].[Machines]([Name], [MaterialID]) VALUES ('���-62', 2)

INSERT INTO [dbo].[Post]([Name]) VALUES ('��������')
INSERT INTO [dbo].[Post]([Name]) VALUES ('�������������')
INSERT INTO [dbo].[Post]([Name]) VALUES ('������� ������')
INSERT INTO [dbo].[Post]([Name]) VALUES ('���������� ������')

INSERT INTO [dbo].[Staff]([Surname], [Name], [Patronymic], [Login], [Password], [PostID], [FactoryID], [MachineID]) VALUES ('������', '����', '������', 'fc_seller', '12345', 1, 1, 1)
INSERT INTO [dbo].[Staff]([Surname], [Name], [Patronymic], [Login], [Password], [PostID], [FactoryID], [MachineID]) VALUES ('������', '����', '��������', 'fc_admin', '12345', 2, 1, NULL)

INSERT INTO [dbo].[Receipts]([EmployeeID], [FurnitureID], [PurchaseAmount], [BuyerMoney]) VALUES (1, 1, 55000, 100000)
INSERT INTO [dbo].[Receipts]([EmployeeID], [FurnitureID], [PurchaseAmount], [BuyerMoney]) VALUES (1, 2, 15000, 20000)