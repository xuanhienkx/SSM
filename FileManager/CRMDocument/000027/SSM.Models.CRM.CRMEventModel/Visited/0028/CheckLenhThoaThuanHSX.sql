select * from HOGW_1F --lenh lenh mua/ban  cung cong ty
select * from HOGW_2F --lenh lenh mua khac cty
select * from HOGW_1G --lenh ben ban khac cty
select * from HOGW_2L --lenh da khop
--select * from HOGW_3B --Lenh khong chap nhan
--select * from HOGW_3C --Lenh huy 
--select * from HOGW_3D--Lenh khong chap nhan huy
--select * from [HOSE_DC]-- Lenh huy duoc HSX chap nhan

select * from [HOSE_PD]where CONFIRM_NUMBER=4  --VOLUME=100000 and PRICE=14 -- kiem tra chi tiet lenh khop
select * from HOSE_SU where SECURITY_SYMBOL='SHI'