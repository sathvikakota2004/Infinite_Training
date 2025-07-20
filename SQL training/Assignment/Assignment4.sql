--1.	Write a T-SQL Program to find the factorial of a given number.
create or alter proc sp_factorial @number int, @factorial bigint output 
as 
begin
declare @fact_num int = 1
set @factorial = 1
if @number < 0
 begin
  raiserror ('factorial cannot be calculated for negative number',16,1)
  return
 end
while @fact_num <= @number
begin
 set @factorial = @factorial * @fact_num
 set @fact_num = @fact_num + 1
end
end

declare @result bigint
exec sp_factorial @number = 8,  @factorial = @result output
select @result as calculatedvalue

--2.	Create a stored procedure to generate multiplication table that accepts a number and generates up to a given number. 
create or alter proc sp_multiplication_table
    @number INT,
    @upto INT
as
begin

    IF @number <= 0 OR @upto <= 0
    begin
        raiserror('Both input numbers must be positive integers.', 16, 1);
        return;
    end

    declare @counter INT = 1;

    
    create table #MultiplicationTable (
        Multiplier INT,
        Number INT,
        Product INT
    );

    while @counter <= @upto
    begin
        INSERT INTO #MultiplicationTable (Multiplier, Number, Product)
        VALUES (@counter, @number, @number * @counter);

        set @counter = @counter + 1;
    end

    
    select Multiplier, Number, Product
    from #MultiplicationTable
    order by Multiplier;

    drop table  #MultiplicationTable;
END

exec sp_multiplication_table @number  = 5,@upto  = 12

--3. Create a function to calculate the status of the student. If student score >=50 then pass, else fail. Display the data neatly
create table  Student (
    Sid int primary key,
    Sname varchar(10))

insert into Student VALUES 
(1, 'Jack'),
(2, 'Rithvik'),
(3, 'Jaspreeth'),
(4, 'Praveen'),
(5, 'Bisa'),
(6, 'Suraj')

create table  Marks (
    Mid int primary key,
    Sid int  references Student(Sid),
    Score int,)
     


insert into  Marks VALUES 
(1, 1, 23),
(2, 6, 95),
(3, 4, 98),
(4, 2, 17),
(5, 3, 53),
(6, 5, 13)

create or alter function  fn_stustatus  (@score int)
returns varchar(10)
as
begin 
 return case 
 when @Score >= 50 then 'Pass'
 else 'Fail'
END
END
select s.Sname,m.Score,dbo.fn_stustatus(m.Score)
from Student s
JOIN Marks m on  s.Sid = m.Sid
order by  s.Sname;



