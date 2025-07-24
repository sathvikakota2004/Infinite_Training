--1. Write a T-Sql based procedure to generate complete payslip of a given employee with respect to the following condition

  -- a) HRA as 10% of Salary
   --b) DA as 20% of Salary
   --c) PF as 8% of Salary
   --d) IT as 5% of Salary
   --e) Deductions as sum of PF and IT
   --f) Gross Salary as sum of Salary, HRA, DA
  -- g) Net Salary as Gross Salary - Deductions

--Print the payslip neatly with all details

create or alter proc sp_payslip(@empno int, @salary bigint)
as
begin
declare @HRA int,@DA int,@PF int,@IT int,@Deduction int,@grosssalary int,@netsalary int
set @HRA = @salary * 10 / 100;
set @DA = @salary * 20 / 100;
set @PF = @salary * 8 / 100;
set @IT = @salary * 5 / 100;
set @deduction = @PF + @IT
set @grosssalary = @salary + @HRA + @DA
set @netsalary = @grosssalary - @deduction
print 'The Employee with Emp no ' + cast(@empno as varchar(10)) +' Has a Salary of ' + cast(@salary as varchar(20)) + ',HRA as '+ cast(@HRA as varchar(20))
+ ',DA as ' + cast(@DA as varchar(10)) + ',PF as ' + cast(@PF as varchar(20)) + ',IT as ' + cast(@IT as varchar(10)) + ',Deductions as ' + cast(@deduction as 
varchar(10)) + ',Gross Salary as ' + cast(@grosssalary as varchar(10)) + ',Net salary as ' +cast(@netsalary as varchar(10))
end;
exec dbo.sp_payslip 10,20000
--2.  Create a trigger to restrict data manipulation on EMP table during General holidays. Display the error message like “Due to Independence day you cannot manipulate data” or "Due To Diwali", you cannot manipulate" etc

--Note: Create holiday table with (holiday_date,Holiday_name). Store at least 4 holiday details. try to match and stop manipulation 
create table Holiday(Holiday_date date,Holiday_name varchar(20))
INSERT INTO Holiday VALUES 
('2025-01-26', 'Republic Day'),
('2025-08-15', 'Independence Day'),
('2025-10-29', 'Diwali'),
('2025-12-25', 'Christmas');
insert into holiday values('2025-07-24','Thanksgiving day')
select * from emp
create or alter trigger tgonholiday on emp
after insert,delete,update
as
begin
declare @todaydate date = cast(getdate() as date)
declare @holidays_name varchar(20)
select @holidays_name = holiday_name from holiday where holiday_date = @todaydate
if @holidays_name is not null
begin
 raiserror ('you cannot manipulate data as today is %s'  ,16,1, @holidays_name)
 rollback transaction
 end
 end
select * from emp
update  emp  set mgr_id = 7839 where empno = 7369




