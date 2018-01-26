  select distinct ServiceId,ServiceName from Shipment

--// ngay 29/12/2015



ALTER TABLE [User]
  ADD AllowInforEdit bit NULL CONSTRAINT DF_AllowInforEdit  DEFAULT 0 WITH VALUES,
   AllowInfoAll bit NULL CONSTRAINT DF_AllowInfoAll  DEFAULT 0 WITH VALUES,
   AllowRegulationEdit bit NULL CONSTRAINT DF_AllowRegulationEdit  DEFAULT 0 WITH VALUES,
   AllowRegulationApproval bit NULL CONSTRAINT DF_AllowRegulationApproval  DEFAULT 0 WITH VALUES,
   CheckingRevenue bit CONSTRAINT DF_User_CheckingRevenue DEFAULT 0 FOR CheckingRevenue,
   IsActive bit CONSTRAINT DF_User_IsActive DEFAULT 0 FOR IsActive;

  update [User] set AllowInforEdit=0,AllowInfoAll=0,AllowRegulationEdit=0,AllowRegulationApproval=0,CheckingRevenue=0 ,IsActive=1
  
  Alter Table Shipment   add  
   ServiceId int NULL,
  IsMainShipment bit CONSTRAINT DF_IsMainShipment DEFAULT 0 WITH VALUES,
  UserListInControl nvarchar(300) null,IsControl bit CONSTRAINT DF_IsControlS  DEFAULT 0 WITH VALUES,ControlStep int,
  ShipmentRef bigint null;
  
update Shipment set IsControl=0,IsMainShipment=0,ServiceId =(select id from ServicesType where SerivceName= Shipment.ServiceName) 
update Shipment set ServiceName= 'Other', ServiceId=6 where ServiceId is null


  Alter Table Revenue  add 
  IsControl bit CONSTRAINT DF_IsControlR  DEFAULT 0 WITH VALUES,
  IsRevised bit CONSTRAINT DF_Revenue_IsRevised DEFAULT 0 FOR IsRevised,
  IsRequest bit  CONSTRAINT DF_Revenue_IsRequest DEFAULT 0 FOR IsRequest 
update Revenue set IsControl=0,	IsRevised=0,IsRequest=0
 
 Alter Table ServerFile  add 
 FileMimeType nvarchar(50) null,FileSize int null;
 
 
ALTER TABLE dbo.Freight ADD ServiceId int NULL
GO 
update Freight set ServiceId =(select id from ServicesType where SerivceName= Freight.ServiceName) 
update Freight set ServiceName= 'Other', ServiceId=6 where ServiceId is null
ALTER TABLE dbo.ServicesType ADD
	IsActive bit CONSTRAINT DF_ServicesType_IsActive DEFAULT 0 FOR IsActive
GO 
update ServicesType set IsActive=1
Go
 
ALTER TABLE dbo.[Agent] ADD
	IsActive bit  CONSTRAINT DF_Agent_IsActive DEFAULT 0 FOR IsActive
GO
 
update [Agent] set IsActive =1

ALTER TABLE dbo.[CarrierAirLine] ADD
	IsActive bit CONSTRAINT DF_ACarrierAirLine_IsActive DEFAULT 0 FOR IsActive
GO 
update [CarrierAirLine] set IsActive =1 
 
ALTER TABLE dbo.[SOAInvoice] ADD
	IsPayment bit CONSTRAINT DF_SOAInvoice_IsPayment DEFAULT 0 FOR IsPayment
GO 
update [SOAInvoice] set IsPayment =0 

ALTER TABLE dbo.Department ADD
	IsActive bit CONSTRAINT DF_Department_IsActive DEFAULT 0 FOR IsActive
GO
 
update Department set IsActive=1

ALTER TABLE dbo.ScfNews ADD
	UserView nvarchar(255) NULL
GO


UPDATE Revenue set BonRequest= 5 where BonRequest= 0 or BonRequest is null
go
UPDATE Revenue set BonApprove= BonRequest where BonApprove=0 or BonApprove is null
go
MERGE INTO Revenue PT
USING ( select P.Name, P.Value from SaleType P) TMP
ON (PT.BonApprove = TMP.Value)
WHEN MATCHED THEN 
UPDATE SET 
    PT.Saletype = TMP.Name; 
go	 

	MERGE INTO Shipment PT
USING ( select P.Id, P.Saletype from Revenue P) TMP
ON (PT.Id = TMP.Id)
WHEN MATCHED THEN 
UPDATE SET 
    PT.Saletype = TMP.Saletype; 

go

ALTER TABLE Revenue  
 DROP CONSTRAINT DF__Revenue__BonAppr__3A4CA8FD
ALTER TABLE Revenue 
ALTER COLUMN BonApprove money null
ALTER TABLE Revenue  
 DROP CONSTRAINT DF__Revenue__BonRequ__3B40CD36;
ALTER TABLE Revenue 
ALTER COLUMN BonRequest money null

select SaleType,BonRequest,BonApprove from Revenue where id=13321
select SaleType,RevenueStatus from Shipment where id=13321


select * from SaleType
 select s.id, s.SaleType, r.SaleType,BonRequest,BonApprove
  from Revenue r join Shipment s on r.Id= s.Id and  CreateDate>='2018-01-01' and RevenueStatus='Submited'
  
  
	MERGE INTO Revenue PT
USING ( select P.Id, P.Saletype,P.CreateDate,P.RevenueStatus from Shipment P) TMP
ON (PT.Id = TMP.Id and  CreateDate>='2018-01-01' and RevenueStatus='Submited')
WHEN MATCHED THEN 
UPDATE SET 
    PT.Saletype = TMP.Saletype; 

go
MERGE INTO Revenue PT 
USING ( select P.Name, P.Value from SaleType P) TMP
ON (PT.Saletype = TMP.Name and Pt.id>=13235)
WHEN MATCHED THEN 
UPDATE SET 
    PT.BonRequest = TMP.Value, 
	 PT.BonApprove = TMP.Value; 
go	 
