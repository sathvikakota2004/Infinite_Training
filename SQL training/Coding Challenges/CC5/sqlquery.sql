use training_db
--1.	Write a query to display your birthday( day of week)
select datename(weekday,'2004-06-03') as 'day_of_the_week'




















--2.	Write a query to display your age in days
select datediff(day,'2004-06-03',getdate()) as age_in_days












drop trigger trgnochanges
select * from emp
update emp
set hire_date = case
when empno = 7654 then '2023-06-05'
when empno = 7788 then '2024-04-03'
when empno = 7900 then '2022-12-25'
when empno =7934 then'2021-11-14'
else hire_date
end
update emp
set hire_date = case
when empno = 7369 then '1980-12-17'
when empno = 7499 then '1981-02-20'
when empno =7521 then '1981-02-22'
when empno = 7698 then '1981-05-01'
when empno = 7782 then '1981-06-09'
when empno = 7839 then '1981-11-17'
when empno = 7844 then '1982-07-21'
when empno =7876 then '1975-09-05'
when empno = 7902 then '1982-11-12'
else hire_date
end
update emp
set hire_date = case
when empno = 7566 then '1981-04-02'
else hire_date
end



select * from emp









--3.Write a query to display all employees information those who joined before 5 years in the current month
select *
from emp
where cast(hire_date as date) < dateadd(year, -5, getdate())
  and month(cast(hire_date as date)) = month(getdate());











select * from emp
--4.
begin transaction
--a.insert 3 rows
insert into emp values (7886,'erick','analyst',7698,'2025-06-12',4500,null,20),
(7887,'nick','salesman',7654,'2023-08-12',3500,null,30),
(7888,'jonas','clerk',7839,'2024-01-21',4700,null,20)
--b. Update the second row sal with 15% increment
update emp
set salary = salary * 1.15
where empno = 7887

DECLARE @deleteemp table (
  empno int,
  ename varchar(30),
  job varchar(30),
  mgr_id int,
  hire_date varchar(30),
  salary int,
  comm int,
  deptno int
);

insert into @deleteemp
select * from emp where empno = 7886;

--c. Delete first row.
delete from emp where empno = 7886
--After completing above all actions, recall the deleted row without losing increment of second row.
insert into emp 
select * from @deleteemp
commit transaction
select * from emp







--5.      Create a user defined function calculate Bonus for all employees of a  given dept using 	following conditions
--	a.     For Deptno 10 employees 15% of sal as bonus.
--	b.     For Deptno 20 employees  20% of sal as bonus
create or alter function fn_calculatebonus (@deptno int, @salary int)
returns int
as
begin
 declare @bonus int;
 set @bonus = case
 when @deptno = 10 then cast(@salary * 0.15 as int)
 when @deptno = 20 then cast(@salary * 0.20 as int)
 else cast(@salary * 0.05
end;
return @bonus;
end;
SELECT dbo.fn_calculateBonus(10, 2450) AS Bonus








select * from emp
select * from dept
--Create a procedure to update the salary of employee by 500 whose dept name is Sales and current salary is below 1500 (use emp table)
create or alter proc sp_updatesal
as 
begin
 update emp
 set salary = salary + 500 where deptno in(select deptno from dept where lower(dname) = 'sales') and salary <1500
end
select * from emp
exec sp_updatesal
select * from emp

 
  