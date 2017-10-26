go
ALTER TABLE CHECKINOUT
ADD IsCalculate bit DEFAULT 0
go
create view CHECKINOUTVIEW as 
select u.Badgenumber,c.CHECKTIME,IsCalculate from userinfo as u join CHECKINOUT as c on c.USERID=u.USERID
