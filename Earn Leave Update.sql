select e.SL,e.Code, E.Name, t.Name, c.AvailableDay from tbl_LeaveCount c join tbl_LeaveType t on t.Sl = c.LeaveTypeId join tbl_Employee e on e.Sl = c.EmployeeId where t.Sl = 2



update c set c.AvailableDay = 20.91 from
 tbl_LeaveCount c join tbl_LeaveType t on t.Sl = c.LeaveTypeId join tbl_Employee e on e.Sl = c.EmployeeId where t.Sl = 2 and 
 e.Code = '237'


CREATE PROCEDURE updateLeaveCount   
    @code nvarchar(50),   
    @availableDay float   
AS 
    update c set c.AvailableDay = @availableDay from
 tbl_LeaveCount c join tbl_LeaveType t on t.Sl = c.LeaveTypeId join tbl_Employee e on e.Sl = c.EmployeeId where t.Sl = 2 and 
 e.Code = @code 

exec updateLeaveCount '237',20.91
exec updateLeaveCount '917',13.07	
exec updateLeaveCount '317',45.44
exec updateLeaveCount '856',11.48
exec updateLeaveCount '97',41.74
exec updateLeaveCount '93',41.75
exec updateLeaveCount '593',52.35
exec updateLeaveCount '178',41.22
exec updateLeaveCount '658',34.65
exec updateLeaveCount '94',73.73
exec updateLeaveCount '407',68.34
exec updateLeaveCount '290',38.42
exec updateLeaveCount '949',3.29
exec updateLeaveCount '61',14.71
exec updateLeaveCount '911',6
exec updateLeaveCount '841',2.69
exec updateLeaveCount '912',4.82
exec updateLeaveCount '914',8.62
exec updateLeaveCount '716',28.59
exec updateLeaveCount '895',12.57
exec updateLeaveCount '786',17.33
exec updateLeaveCount '10',53.2
exec updateLeaveCount '14',106.54
exec updateLeaveCount '403',69.96
exec updateLeaveCount '933',8.57
exec updateLeaveCount '794',30.98
exec updateLeaveCount '901',5.64
exec updateLeaveCount '790',34.66
exec updateLeaveCount '788',16.72
exec updateLeaveCount '132',20.01
exec updateLeaveCount '891',10
exec updateLeaveCount '726',37.92


