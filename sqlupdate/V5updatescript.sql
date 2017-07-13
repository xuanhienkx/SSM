-- em giu lai cac form theo format hien tai cua web anh: deliverorderHan,printdeliveriorderHan, printarriveNoticeHan
---//update Country
--//Alter table Country   set Id identity

  -- User update 
  ALTER TABLE [User]
  ADD AllowFollowDept bit NOT NULL CONSTRAINT DF_AllowFollowDept  DEFAULT 0 WITH VALUES,
   AllowEditReport bit NOT NULL CONSTRAINT DF_AllowEditReport  DEFAULT 0 WITH VALUES,
   DeptFollowIds  varchar(5000) NULL,
   EmailPassword nvarchar(200) NULL,
   EmailNameDisplay nvarchar(2500)NULL
   -- Customer Update
   ALTER TABLE [Customer]
   ADD IsSee bit NOT NULL CONSTRAINT DF_IsSee  DEFAULT 0 WITH VALUES,
   IsMove bit NOT NULL CONSTRAINT DF_IsMove  DEFAULT 0 WITH VALUES,
   IsHideUser bit NOT NULL CONSTRAINT DF_CIsHideUser  DEFAULT 0 WITH VALUES ,
   MovedBy  Bigint NULL,
   CrmCusId Bigint NULL 
   -- 
  -- update [User] set PassWord='123' 
   
    ALTER TABLE [Agent]
   ADD IsSee bit NOT NULL CONSTRAINT DF_AIgIsSee  DEFAULT 0 WITH VALUES,
   IsHideUser bit NOT NULL CONSTRAINT DF_AgIsHideUser  DEFAULT 0 WITH VALUES
   go
   ALTER TABLE [Area]
   ADD IsSee bit NOT NULL CONSTRAINT DF_AIsSee  DEFAULT 0 WITH VALUES,
   IsHideUser bit NOT NULL CONSTRAINT DF_AIsHideUser  DEFAULT 0 WITH VALUES
   go
 ALTER TABLE [CarrierAirLine]
   ADD IsSee bit NOT NULL CONSTRAINT DF_ACIsSee  DEFAULT 0 WITH VALUES,
   IsHideUser bit NOT NULL CONSTRAINT DF_ACIsHideUser  DEFAULT 0 WITH VALUES
   go
   --//CarrierAirLine
   update [Customer] set IsSee=1
   go
   update [Agent] set IsSee=1
   go
   update [Area] set IsSee=1
   go
   update [CarrierAirLine] set IsSee=1

   --//ALTER TABLE ServerFile
 ALTER TABLE ServerFile
ALTER COLUMN ObjectType nvarchar(300);
GO
ALTER TABLE ServerFile
ALTER COLUMN [Path] nvarchar(1000);
GO
ALTER TABLE ServerFile
ALTER COLUMN FileName nvarchar(400);
GO
ALTER TABLE ServerFile
ALTER COLUMN FileMimeType nvarchar(300);
--// chih
