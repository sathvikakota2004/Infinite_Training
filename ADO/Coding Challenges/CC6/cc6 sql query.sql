use Coding_Challenge
create table Employee_Details (
EmpId int identity(1,1) primary key,
Name NVARCHAR(100),
Salary decimal(10,2),
Gender varchar(10),
NetSalary as (Salary - (Salary * 0.10)) PERSISTED);
insert into Employee_Details values ('sathvika',50000,'Female')
select * from Employee_Details
--stored proc for inserting values
create or alter proc sp_empinsert( @Name nvarchar(100), @Salary Decimal (10,2),@Gender varchar(10),@Empidnew int output,@netsalarynew decimal (10,2) output)
as 
begin
insert into Employee_Details (Name,Salary,Gender) values (@Name,@Salary,@Gender)
set @Empidnew  = SCOPE_IDENTITY()
select @netsalarynew = NetSalary from Employee_Details where  EmpId = @Empidnew 
end;
DECLARE @EmpId int;
DECLARE @NetSalary decimal(10,2);

exec sp_empinsert
    @Name = 'Ramya',
    @Salary = 45000.00,
    @Gender = 'Female',
    @Empidnew = @EmpId output,
    @netsalarynew = @NetSalary output;
select  * from Employee_Details
--2. Write a procedure that takes empid as input and outputs the updated salary as current salary + 100 for the give employee.


create or alter proc sp_updateempSalary (@EmpId INT,@updatedsalary DECIMAL(10,2) output)
as

begin
    update Employee_Details
    set Salary = Salary + 100
    where EmpId = @EmpId;

    select @updatedsalary = Salary
    from Employee_Details
where EmpId = @EmpId;
end

select * from Employee_Details
 
 
    
	
