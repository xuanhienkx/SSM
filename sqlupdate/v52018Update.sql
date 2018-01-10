select * from Shipment

select * from SaleType
----Revenue
alter table Revenue    
add SaleTypeId bigint null;

--Shipment
alter table Shipment    
add SaleTypeId bigint null;


alter table SaleType   
add
	[ParentValue] [money] NULL, 
	[ParentId] [bigint] NULL;

	/****** Object:  Index [IX_SaleType]    Script Date: 01/08/2018 8:28:58 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_SaleType] ON [dbo].[SaleType]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


MERGE INTO Revenue PT
USING ( select P.Id, P.Name from SaleType P) TMP
ON (PT.SaleType = TMP.Name)
WHEN MATCHED THEN 
UPDATE SET 
    PT.SaleTypeId = TMP.Id ;

go

	MERGE INTO Shipment PT
USING ( select P.Id, P.Name from SaleType P) TMP
ON (PT.SaleType = TMP.Name)
WHEN MATCHED THEN 
UPDATE SET 
    PT.SaleTypeId = TMP.Id ; 
go

update Shipment set SaleTypeId= SaleType