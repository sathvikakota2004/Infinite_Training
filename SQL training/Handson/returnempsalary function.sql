--write a function that takes employee number and return salary
create or alter function fmempsal(@empnum int)
 returns decimal(10,2)
  as
  begin 
  declare @salary decimal (10,2)
  select @salary = sal from emp where empno = @empnum
  return @salary
  end 
select dbo.fmempsal(7782) as empsalary
select * from emp