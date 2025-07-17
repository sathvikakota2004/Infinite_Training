create database Coding_Challenge
create table book (
id int primary key ,
title varchar (30),
author varchar(20),
isbn varchar(20) unique  ,
published_date DATETIME )

insert into book values
(1, 'My First Book','Mary Parker', '9814833029127', '2012-02-22 12:08:17'),
(2,'My Second SQL Book','John Mayer','857300923713','1972-07-03 09:22:45'),
(3, 'My Third SQL book','Cary Flint','523120967812','2015-10-18 14:05:44')
select * from book as bt with(nolock)

--Write a query to fetch the details of the books written by author whose name ends with er

select * from book as bt with(nolock)  where author like '%er'











create table reviews(
id int ,
book_id int references book(id),
reviewer_name varchar(20),
content varchar(20),
rating int,
publishes_date datetime)


insert into reviews values
(1,1,'John Smith ','My first review ',4,'2017-12-10 05:50:11'),
(2,2,'John Smith ','My second review ',5,'2017-10-13 15:05:12'),
(3,2,'Alice Walker','Another review',1,'2017-10-22 23:47:10')
select * from reviews as rt with(nolock)
select * from book as bt with(nolock)
 
 
 
 
 
 
 
--Display the Title ,Author and ReviewerName for all the books from the above table
select b.title,b.author ,r.reviewer_name from book b join reviews  r on b.id = r.book_id



--Display the reviewer name who reviewed more than one book.
select reviewer_name, count(reviewer_name)as reviewers_count from reviews
group by reviewer_name
having count(reviewer_name) >1

create table customer(
ID INT,
NAME VARCHAR(20),
AGE INT,
ADDRESS VARCHAR(30),
SALARY DECIMAL(10,2))

INSERT INTO customer VALUES
(1,'Ramesh',32,'Ahmedabad',2000.00),
(2,'Khilan',25,'Delhi',1500.00),
(3,'Kaushik',23,'Kota',2000.00),
(4,'Chaitali',25,'Mumbai',6500.00),
(5,'Hardik',27,'Bhopal',8500.00),
(6,'Komal',22,'MP',4500.00),
(7,'Muffy',24,'Indore',10000.00)
select * from customer as ct with(nolock)

--Display the Name for the customer from above customer table who live in same address which has character o anywhere in address
select NAME, ADDRESS from customer where ADDRESS like '%o%'

---MAKING COLUMN AS NOT NULL
ALTER TABLE CUSTOMER 
ALTER COLUMN ID INT NOT NULL;


--add constraint to table customer
Alter table customer
add constraint p_key primary key (ID)


CREATE TABLE ORDERS(
OID INT,
date DATETIME,
Customer_Id int references customer(ID),
Amount int)

INSERT INTO ORDERS VALUES 
(102,'2009-10-08 00:00:00', 3, 3000),
(100,'2009-10-08 00:00:00',3,1500),
(101,'2009-11-20 00:00:00',2,1560),
(103,'2008-05-20 00:00:00',4,2060)

SELECT * FROM ORDERS AS BT WITH(NOLOCK)





--Write a query to display the Date,Total no of customer placed order on same Date
SELECT DATE ,count(customer_id) as no_of_customers from orders 
group by date


create table Employee(
ID INT PRIMARY KEY,
NAME VARCHAR(20),
AGE INT,
ADDRESS VARCHAR(20),
SALARY DECIMAL (10,2)) 

INSERT INTO Employee values 
(1,'Ramesh',32,'Ahmedabad',2000.00),
(2,'Khilan',25,'Delhi',1500.00),
(3,'Kaushik',23,'Kota',2000.00),
(4,'Chaitali',25,'Mumbai',6500.00),
(5,'Hardik',27,'Bhopal',8500.00),
(6,'Komal',22,'MP',null),
(7,'Muffy',24,'Indore',null)
select * from Employee as et with(nolock)








--Display the Names of the Employee in lower case, whose salary is null
select LOWER(NAME) as no_salary FROM Employee where SALARY is null 



create table Studentdetails (
RegisterNo int primary key,
Name varchar(10),
Age int,
Qualification varchar(20),
MobileNo VARCHAR(20),
Mail_id varchar(20),
Location varchar(20),
Gender varchar(1))
insert into  Studentdetails VALUES
(2, 'Sai', 22, 'B.E', '9952836777', 'Sai@gmail.com', 'Chennai', 'M'),
(3, 'Kumar', 20, 'BSC', '7890125648', 'Kumar@gmail.com', 'Madurai', 'M'),
(4, 'Selvi', 22, 'B.Tech', '8904567342', 'selvi@gmail.com', 'Selam', 'F'),
(5, 'Nisha', 25, 'M.E', '7834672310', 'Nisha@gmail.com', 'Theni', 'F'),
(6, 'SaiSaran', 21, 'B.A', '7890345678', 'saran@gmail.com', 'Madurai', 'F'),
(7, 'Tom', 23, 'BCA', '8901234675', 'Tom@gmail.com', 'Pune', 'M');






--Write a sql server query to display the Gender,Total no of male and female from the above relation


SELECT Gender, COUNT(*) as gender_count
FROM Studentdetails
GROUP BY Gender;