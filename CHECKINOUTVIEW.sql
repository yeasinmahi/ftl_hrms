go
ALTER TABLE CHECKINOUT
ADD IsCalculate bit DEFAULT 0
go
create view [dbo].[CHECKINOUTVIEW] as 
select u.USERID, u.Badgenumber as EmployeeCode,c.CHECKTIME,IsCalculate from userinfo as u join CHECKINOUT as c on c.USERID=u.USERID
